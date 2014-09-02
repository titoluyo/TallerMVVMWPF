
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Simple.Core.BusinessEntity {
    /// <summary>
    /// BusinessEntityBase provides infrastructure for WPF to utilize the DataAnnotations ValidationAtributes without any additional developer code.
    /// Implements <see cref="IDataErrorInfo"/> and returns validation information through this interface.
    /// Implements <see cref="INotifyPropertyChanged"/> and provides <c>RaisePropertyChanged</c> for derived classes to raise their property changed notifications.
    /// </summary>
    [Serializable]
    abstract public class BusinessEntityBase : INotifyPropertyChanged, IDataErrorInfo {

        #region Declarations

        const String STRING_END_LOADING_NEVER_CALLED = "EndLoading never called after a BeginLoading call was made.  No operations are permitted until EndLoading has been called.";
        [field: NonSerialized]
        ObservableCollection<BrokenRule> _brokenRules;
        Boolean _hasBeenValidated;
        Boolean _isDirty = false;
        Boolean _isLoading = false;
        [field: NonSerialized]
        static Object _lockObject = new Object();
        enum CalledByCheckAllRules { Yes, No }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the broken rules for this entity.
        /// </summary>
        /// <value>The broken rules for this entity.</value>
        public ObservableCollection<BrokenRule> BrokenRules {
            get {

                if (_brokenRules == null)
                    _brokenRules = new ObservableCollection<BrokenRule>();
                return _brokenRules;
            }

            private set {
                BeforePropertyChanged("BrokenRules");
                _brokenRules = value;
                AfterPropertyChanged("BrokenRules");
                RaisePropertyChanged("BrokenRules");
            }
        }

        /// <summary>
        /// Gets the count of <see cref="BrokenRule"/>
        /// </summary>
        public Int32 BrokenValidationRulesCount {
            get { return BrokenRules.Count; }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public String Error {
            get {

                if (BrokenValidationRulesCount > 0) {
                    return String.Join(Environment.NewLine, BrokenRules.Select(r => r.ErrorMessage).ToArray());
                } else
                    return String.Empty;
            }
        }

        /// <summary>
        /// Gets a value indicating if this entity been validated since creation or since the last changed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has been validated; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>This value is updated when the IsValid method executed.</remarks>
        public Boolean HasBeenValidated {
            get {

                if (_isLoading)
                    throw new InvalidOperationException(STRING_END_LOADING_NEVER_CALLED);
                return _hasBeenValidated;
            }

            private set {
                BeforePropertyChanged("HasBeenValidated");
                _hasBeenValidated = value;
                AfterPropertyChanged("HasBeenValidated");
                RaisePropertyChanged("HasBeenValidated");
            }
        }

        /// <summary>
        /// Gets a value indicating if this type has no broken rules.
        /// </summary>
        /// <remarks>Typically used for binding in the UI and you want to use the BooleanToVisiblityConverter and only display a UIElement when this entity is valid.</remarks>
        public Boolean HasNoBrokenValidationRules {
            get { return BrokenRules.Count == 0; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is dirty.
        /// </summary>
        /// <value><c>true</c> if this instance is dirty; otherwise, <c>false</c>.</value>
        public Boolean IsDirty {
            get { return _isDirty; }

            private set {
                BeforePropertyChanged("IsDirty");
                _hasBeenValidated = value;
                AfterPropertyChanged("IsDirty");
                RaisePropertyChanged("IsDirty");
            }
        }

        /// <summary>
        /// Gets a value indicating if this object is currently being loaded.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>This property is toggled by the <c>BeginLoading</c> and <c>EndLoading</c> methods of this class.</remarks>
        public Boolean IsLoading {
            get { return _isLoading; }

            private set {
                BeforePropertyChanged("IsLoading");
                _hasBeenValidated = value;
                AfterPropertyChanged("IsLoading");
                RaisePropertyChanged("IsLoading");
            }
        }

        /// <summary>
        /// Gets a String representing all broken rules for this property
        /// </summary>
        /// <param name="propertyName">Name of the property to check rules for.</param>
        public String this[String propertyName] {
            get {

                //kdawg's contribution to global chaos
                if (BrokenRules.Count(r => r.PropertyName == propertyName) > 0) {
                    return String.Join(Environment.NewLine, BrokenRules.Where(r => r.PropertyName == propertyName).Select(r => r.ErrorMessage).ToArray());
                } else
                    return String.Empty;
            }
        }
        /// <summary>
        /// Shortcut to get to this entities ValidationRulesManager
        /// </summary>
        ValidationRulesManager ValidationRulesManager {
            get { return SharedValidationRules.GetManager(this.GetType()); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessEntityBase"/> class.
        /// </summary>
        public BusinessEntityBase() {
            //TODO - developers you can add more initialization code here
            AddSharedBusinessRules();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds all shared rules for this <see cref="Type"/> 
        /// </summary>
        /// <remarks>
        /// Shared rules are rules that apply to all instances of a class as opposed to one or more rules that only apply to a single instance of a class.
        /// </remarks>
        void AddSharedBusinessRules() {
            lock (_lockObject) {

                if (!SharedValidationRules.RulesExistFor(this.GetType())) {

                    foreach (PropertyInfo prop in this.GetType().GetProperties()) {

                        foreach (ValidationAttribute atr in prop.GetCustomAttributes(typeof(ValidationAttribute), false)) {
                            this.ValidationRulesManager.Add(new ValidationRule(prop.Name, atr));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks all validation rules against entity values.  
        /// </summary>
        public void CheckAllRules() {
            Boolean foundBrokenRule = false;
            Boolean hasBrokenRules = BrokenValidationRulesCount != 0;
            BrokenRules.Clear();

            foreach (String item in ValidationRulesManager.GetAllPropertyNamesWithRules()) {
                Object value = this.GetType().GetProperty(item).GetValue(this, null);

                if (CheckRulesForProperty(item, value, CalledByCheckAllRules.Yes))
                    foundBrokenRule = true;
            }

            if (foundBrokenRule || hasBrokenRules) {
                RaiseBrokenRuleFountNotification(foundBrokenRule);
            }
        }
        /// <summary>
        /// Checks rules for a property, validating the property against all shared and instance rules for the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        /// <param name="calledByCheckAllRules">The called by check all rules.</param>
        /// <returns><c>true</c> if this property is valid; otherwise, <c>false</c>.</returns>
        Boolean CheckRulesForProperty(String propertyName, Object value, CalledByCheckAllRules calledByCheckAllRules = CalledByCheckAllRules.No) {
            Boolean foundBrokenRule = false;
            Boolean removedBrokenRules = false;

            //CheckAllRules clears all BrokenRules before calling, so no reason to run this code.
            if (calledByCheckAllRules == CalledByCheckAllRules.No) {

                foreach (var item in (from r in BrokenRules where r.PropertyName == propertyName select r).ToList()) {
                    BrokenRules.Remove(item);
                    removedBrokenRules = true;
                }
            }

            foreach (ValidationRule vr in ValidationRulesManager.GetRulesForProperty(propertyName)) {

                if (!vr.IsValid(value)) {
                    BrokenRules.Add(new BrokenRule(vr.PropertyName, vr.ErrorMessage));
                    foundBrokenRule = true;
                }
            }

            //if CheckAllRules called this method, there is no reason to send out change 
            //  notification for every property since these properties are scoped to the entity.
            //  CheckAllRules will raise the notificaitons when completed.
            if (calledByCheckAllRules == CalledByCheckAllRules.No && (foundBrokenRule || removedBrokenRules)) {
                RaiseBrokenRuleFountNotification(foundBrokenRule);
            }
            return foundBrokenRule;
        }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Calling this method checks all validation rules and returns the result.  
        /// If all validation checks pass, the HasBeenValidated property will be set to <c>true</c>, otherwise <c>false</c>.
        /// Always call this method before passing an entity between tiers and always call this method when receiving an entity from another tier.
        /// </remarks>
        public Boolean IsValid() {

            if (_isLoading == true)
                throw new InvalidOperationException(STRING_END_LOADING_NEVER_CALLED);
            CheckAllRules();
            Boolean isValid = BrokenValidationRulesCount == 0;
            HasBeenValidated = isValid;
            return isValid;
        }

        void RaiseBrokenRuleFountNotification(Boolean setHasBeenValidatedToFalse) {
            RaisePropertyChanged("Error");
            RaisePropertyChanged("BrokenValidationRulesCount");
            RaisePropertyChanged("HasNoBrokenValidationRules");

            if (setHasBeenValidatedToFalse)
                HasBeenValidated = false;
        }

        /// <summary>
        /// Sets the property value and runs all validation rules for the property.
        /// </summary>
        /// <typeparam name="T">The type of the property calling this method</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="currentValue">The current value.</param>
        /// <param name="newValue">The new value.</param>
        /// <example>
        /// <code>set { base.SetPropertyValue&lt;Double&gt;("Paid", ref _paid, value); }</code>
        /// </example>
        protected void SetPropertyValue<T>(String propertyName, ref T currentValue, T newValue) {

            if (currentValue == null) {

                if (newValue == null)
                    return;
            } else if (newValue != null && currentValue.Equals(newValue)) {
                return;
            }

            if (!_isLoading) {
                HasBeenValidated = false;
                IsDirty = true;
                BeforePropertyChanged(propertyName);
                currentValue = newValue;
                CheckRulesForProperty(propertyName, newValue);
                RaisePropertyChanged(propertyName);
                AfterPropertyChanged(propertyName);
            } else {
                //if we are loading the object then just assign the value
                currentValue = newValue;
            }
        }

        #endregion

        #region Loading

        /// <summary>
        /// Sets <c>IsLoading</c> to <c>true</c>.  Call before populating this entity from a database, saves time by not processing validation code during loading.
        /// </summary>
        public void BeginLoading() {
            IsLoading = true;
        }

        /// <summary>
        /// Sets <c>IsLoading</c> to <c>false</c>. If BeginLoading was called, must call EndLoading after object loading is completed.
        /// </summary>
        public void EndLoading() {
            IsLoading = false;
            IsDirty = false;
            HasBeenValidated = false;
        }

        #endregion

        #region Property Change Methods

        /// <summary> 
        /// Derived classes can override this method to execute logic after the property is set. The base implementation does nothing. 
        /// </summary> 
        /// <param name="strPropertyName"> 
        /// The property which was changed. 
        /// </param> 
        protected virtual void AfterPropertyChanged(String propertyName) {
        }

        /// <summary> 
        /// Derived classes can override this method to execute logic before the property is set. The base implementation does nothing. 
        /// </summary> 
        /// <param name="strPropertyName"> 
        /// The property which was changed. 
        /// </param> 
        protected virtual void BeforePropertyChanged(String propertyName) {
        }

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void RaisePropertyChanged(String propertyName) {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}

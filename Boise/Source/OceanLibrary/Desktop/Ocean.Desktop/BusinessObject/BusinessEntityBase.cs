using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using Ocean.Audit;
using Ocean.Infrastructure;
using Ocean.InputStringFormatting;
using Ocean.OceanValidation;
using Ocean.Properties;

namespace Ocean.BusinessObject {
    /// <summary>
    /// Base class for business entity objects.
    /// </summary>
    [Serializable]
    public abstract class BusinessEntityBase : ObservableObject, IDataErrorInfo, IBusinessEntityAudit, IBusinessEntity {

        #region  Declarations

        const String _STRING_ERROR = "Error";
        const String _STRING_HASERRORS = "HasErrors";
        const String _STRING_HASNOERRORS = "HasNoErrors";
        const String _STRING_HASBEENVALIDATED = "HasBeenValidated";
        const String _STRING_ISDIRTY = "IsDirty";

        Boolean _hasBeenValidated;
        Boolean _isDirty;

        [field: NonSerialized]
        ValidationRulesManager _instanceValidationRulesManager;
        
        static readonly Object _LockObject = new Object();

        [field: NonSerialized]
        Dictionary<String, ValidationError> _validationErrors;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the active rule set. Use this property to have a specific rule set checked in addition to all rules that are not assigned a specific rule set.  For example, if you set this property to, Insert; when the rules are checked, all general rules will be checked and the Insert rules will also be checked.
        /// </summary>
        /// <value>The active rule set.</value>
        public String ActiveRuleSet {get; set; }

        /// <summary>
        /// Gets a multi-line error message indicating what is wrong with this Object.  Every error in the Broken Rules collection is returned.
        /// </summary>
        /// <returns>String</returns>
        public String Error {
            get {

                if (this.IsLoading) {
                    throw new InvalidOperationException(Resources.BusinessEntityBase_STRING_END_LOADING_NEVER_CALLED_EndLoading_never_called_after_a_BeginLoading_call_was_made___No_operations_are_permitted_until_EndLoading_has_been_called_);
                }

                var sb = new StringBuilder(4096);

                foreach (ValidationError obj in this.GetAllBrokenRules()) {

                    if (!String.IsNullOrWhiteSpace(obj.BrokenRule.RuleBase.OverrideMessage)) {
                        sb.AppendLine(obj.BrokenRule.RuleBase.OverrideMessage);

                    } else if(!String.IsNullOrWhiteSpace(obj.BrokenRule.RuleBase.CustomMessage)) {
                        sb.Append(obj.BrokenRule.RuleBase.CustomMessage);
                        sb.Append(" : ");
                        sb.AppendLine(obj.BrokenRule.RuleBase.BrokenRuleDescription);

                    } else {
                        sb.AppendLine(obj.BrokenRule.RuleBase.BrokenRuleDescription);
                    }

                    sb.AppendLine();
                }

                //this removes the last append line characters
                if (sb.Length > 2) {
                    sb.Length = sb.Length - 2;
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Has this Object been validated.  This value is automatically assigned by this base class.  After the IsValid property has returned a True value, this value is then set to True.
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean HasBeenValidated {
            get {

                if (this.IsLoading) {
                    throw new InvalidOperationException(Resources.BusinessEntityBase_STRING_END_LOADING_NEVER_CALLED_EndLoading_never_called_after_a_BeginLoading_call_was_made___No_operations_are_permitted_until_EndLoading_has_been_called_);
                }

                return _hasBeenValidated;
            }
        }

        /// <summary>
        /// Does this Object have any errors. Property used to surface a boolean for data binding instead of checking the Error property length or Error collections for a count.
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean HasErrors {
            get {
                return _validationErrors.Count > 0;
            }
        }

        /// <summary>
        /// Is this Object error free. Property used to surface a boolean for data binding instead of checking the Error property length or Error collections for a count.
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean HasNoErrors {
            get {
                return _validationErrors.Count == 0;
            }
        }

        /// <summary>
        /// Used by this base class internally as a method of getting the Instance Validation RulesManager.  This property uses lazy Object creation.
        /// </summary>
        /// <returns>ValidationRulesManager</returns>
        [XmlIgnore]
        ValidationRulesManager InstanceValidationRulesManager {
            get {
                if(this.IsLoading) {
                    throw new InvalidOperationException(Resources.BusinessEntityBase_STRING_END_LOADING_NEVER_CALLED_EndLoading_never_called_after_a_BeginLoading_call_was_made___No_operations_are_permitted_until_EndLoading_has_been_called_);
                }

                return _instanceValidationRulesManager ?? (_instanceValidationRulesManager = new ValidationRulesManager());
            }
        }

        /// <summary>
        /// Is this Object dirty.  Have changes been made since the Object was loaded or a new Object was constructed.  This is automatically kept track of by this base class.
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean IsDirty {
            get {

                if(this.IsLoading) {
                    throw new InvalidOperationException(Resources.BusinessEntityBase_STRING_END_LOADING_NEVER_CALLED_EndLoading_never_called_after_a_BeginLoading_call_was_made___No_operations_are_permitted_until_EndLoading_has_been_called_);
                }

                return _isDirty;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is loading.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsLoading { get; private set; }

        /// <summary>
        /// Gets a multi-line error message indicating what is wrong with this property.  Every error for this property in the Broken Rules collection is returned.
        /// </summary>
        /// <returns>String</returns>
        public String this[String columnName] {
            get {

                var sb = new StringBuilder(1024);

                foreach (ValidationError obj in this.GetBrokenRulesForProperty(columnName)) {

                    if (!(String.IsNullOrEmpty(obj.BrokenRule.RuleBase.OverrideMessage))) {
                        sb.AppendLine(obj.BrokenRule.RuleBase.OverrideMessage);

                    } else if(!String.IsNullOrWhiteSpace(obj.BrokenRule.RuleBase.CustomMessage)) {
                        sb.Append(obj.BrokenRule.RuleBase.CustomMessage);
                        sb.Append(" : ");
                        sb.AppendLine(obj.BrokenRule.RuleBase.BrokenRuleDescription);

                    } else {
                        sb.AppendLine(obj.BrokenRule.RuleBase.BrokenRuleDescription);
                    }

                    sb.AppendLine();
                }

                //this removes the last append line characters
                if (sb.Length > 4) {
                    sb.Length = sb.Length - 4;
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Enables an Exception to be thrown from the property setting when a validation error occurs
        /// </summary>
        /// <value>Boolean</value>
        /// <returns>Boolean</returns>
        public bool ThrowExceptionFromPropertySetters { get; set; }

        /// <summary>
        /// A dictionary Object of all broken rules (validation errors)
        /// </summary>
        /// <returns>Dictionary(Of String, ValidationError)</returns>
        public Dictionary<String, ValidationError> ValidatationErrors {
            get {
                if(this.IsLoading) {
                    throw new InvalidOperationException(Resources.BusinessEntityBase_STRING_END_LOADING_NEVER_CALLED_EndLoading_never_called_after_a_BeginLoading_call_was_made___No_operations_are_permitted_until_EndLoading_has_been_called_);
                }
                return _validationErrors ?? (_validationErrors = new Dictionary<String, ValidationError>());
            }
        }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessEntityBase"/> class.
        /// This is public because the Windows Phone 7 XML serializer requires it.
        /// </summary>
        protected BusinessEntityBase() {
            this.ActiveRuleSet = String.Empty;
            AddInstanceBusinessValidationRules();
            AddSharedBusinessRules();
        }

        /// <summary>
        /// Initializes the <see cref="BusinessEntityBase"/> class.
        /// </summary>
        static BusinessEntityBase() {
        }

        #endregion

        #region  Rules Methods

        /// <summary>
        /// Override this method in your business class to be notified when you need to set up business rules.  This method is only used by the sub-class and not consumers of the sub-class.
        /// Use the instance method, AddInstanceRule to in deriving classes to add instance rules to the Object.
        /// </summary>
        protected void AddInstanceBusinessValidationRules() {
        }

        /// <summary>
        /// This is used by sub-classes and classes that consume the sub-class business entity and need to add an instance rule to the list of rules to be enforced.
        /// </summary>
        public void AddInstanceRule(RuleHandler handler, RuleDescriptorBase e) {
            this.InstanceValidationRulesManager.GetRulesForProperty(e.PropertyName).List.Add(new Validator(handler, e, RuleType.Instance));
        }

        /// <summary>
        /// This code gets called on the first time this Object type is constructed
        /// </summary>
        void AddSharedBusinessRules() {
            lock (_LockObject) {

                if (!(SharedValidationRules.RulesExistFor(this.GetType()))) {

                    ValidationRulesManager mgrValidation = SharedValidationRules.GetManager(this.GetType());
                    CharacterCasingRulesManager mgrCharacterCasing = SharedCharacterCasingRules.GetManager(this.GetType());

                    foreach (PropertyInfo prop in this.GetType().GetProperties()) {

                        foreach (BaseValidatorAttribute atr in prop.GetCustomAttributes(typeof(BaseValidatorAttribute), false)) {
                            mgrValidation.AddRule(atr.Create(prop.Name), prop.Name);
                        }

                        foreach (CharacterCasingFormattingAttribute atr in prop.GetCustomAttributes(typeof(CharacterCasingFormattingAttribute), false)) {
                            mgrCharacterCasing.AddRule(prop.Name, atr.CharacterCasing);
                        }

                    }

                    AddSharedBusinessValidationRules(mgrValidation);
                    AddSharedCharacterCasingFormattingRules(mgrCharacterCasing);
                }
            }
        }

        /// <summary>
        /// Override this method in your business class to be notified when you need to set up SHARED business rules.  This method is only used by the sub-class and not consumers of the sub-class.
        /// To add shared rules to business objects to derving class properties, override this method in deriving classes and add the rules to the ValidationRulesManager.
        /// This method will only be called once; the first time the deriving class is created
        /// </summary>
        protected void AddSharedBusinessValidationRules(ValidationRulesManager mgrValidation) {
        }

        /// <summary>
        /// Override this method in your business class to be notified when you need to set up SHARED character casing rules.  This method is only used by the sub-class and not consumers of the sub-class.
        /// To add shared character case formatting to deriving class properties, override this method in deriving classes and add the rules to the CharacterCasingRulesManager.
        /// This method will only be called once; the first time the deriving class is created.
        /// </summary>
        protected void AddSharedCharacterCasingFormattingRules(CharacterCasingRulesManager mgrCharacterCasing) {
        }

        /// <summary>
        /// Validates the entity against all shared and instance rules.
        /// </summary>
        public void CheckAllRules() {

            Boolean raiseErrorPropertyChanged = false;
            ValidationRulesManager mgr = SharedValidationRules.GetManager(this.GetType());

            foreach (ValidationRulesList vrl in mgr.RulesDictionary.Values) {

                foreach (IValidationRuleMethod obj in vrl.List) {

                    if (RuleSetMatches(obj.RuleBase.RuleSet)) {

                        //remove broken rule if it exists, if not does nothing
                        if (this.ValidatationErrors.Remove(obj.RuleName)) {
                            raiseErrorPropertyChanged = true;
                        }

                        if (obj.Invoke(this) == false) {
                            raiseErrorPropertyChanged = true;
                            this.ValidatationErrors.Add(obj.RuleName, new ValidationError(obj));
                            InternalRaisePropertyChanged(obj.RuleBase.PropertyName);
                        }
                    }
                }
            }

            if (_instanceValidationRulesManager != null) {

                foreach (ValidationRulesList vrl in this.InstanceValidationRulesManager.RulesDictionary.Values) {

                    foreach (IValidationRuleMethod obj in vrl.List) {

                        if (RuleSetMatches(obj.RuleBase.RuleSet)) {

                            //remove broken rule if it exists, if not does nothing
                            if (this.ValidatationErrors.Remove(obj.RuleName)) {
                                raiseErrorPropertyChanged = true;
                            }

                            if (obj.Invoke(this) == false) {
                                raiseErrorPropertyChanged = true;
                                this.ValidatationErrors.Add(obj.RuleName, new ValidationError(obj));
                                InternalRaisePropertyChanged(obj.RuleBase.PropertyName);
                            }
                        }
                    }
                }
            }

            if (raiseErrorPropertyChanged) {
                InternalRaisePropertyChanged(_STRING_ERROR);
                InternalRaisePropertyChanged(_STRING_HASERRORS);
                InternalRaisePropertyChanged(_STRING_HASNOERRORS);
            }
        }

        /// <summary>
        /// Validates the property against all shared and instance rules for the property.
        /// </summary>
        public void CheckRulesForProperty(String propertyName) {

            Boolean raiseErrorPropertyChanged = false;
            ValidationRulesManager mgr = SharedValidationRules.GetManager(this.GetType());

            foreach (IValidationRuleMethod obj in mgr.GetRulesForProperty(propertyName).List) {

                if (RuleSetMatches(obj.RuleBase.RuleSet)) {

                    //remove broken rule if it exists, if not does nothing
                    if (this.ValidatationErrors.Remove(obj.RuleName)) {
                        raiseErrorPropertyChanged = true;
                    }

                    if (obj.Invoke(this) == false) {
                        raiseErrorPropertyChanged = true;
                        this.ValidatationErrors.Add(obj.RuleName, new ValidationError(obj));
                    }
                }
            }

            if (_instanceValidationRulesManager != null) {

                foreach (IValidationRuleMethod obj in this.InstanceValidationRulesManager.GetRulesForProperty(propertyName).List) {

                    if (RuleSetMatches(obj.RuleBase.RuleSet)) {

                        //remove broken rule if it exists, if not does nothing
                        if (this.ValidatationErrors.Remove(obj.RuleName)) {
                            raiseErrorPropertyChanged = true;
                        }

                        if (obj.Invoke(this) == false) {
                            raiseErrorPropertyChanged = true;
                            this.ValidatationErrors.Add(obj.RuleName, new ValidationError(obj));
                        }
                    }
                }
            }

            if (raiseErrorPropertyChanged) {
                InternalRaisePropertyChanged(_STRING_ERROR);
                InternalRaisePropertyChanged(_STRING_HASERRORS);
                InternalRaisePropertyChanged(_STRING_HASNOERRORS);
            }
        }

        /// <summary>
        /// A List of ValidationError objects for the Object. 
        /// </summary>
        public List<ValidationError> GetAllBrokenRules() {
            return this.ValidatationErrors.Select(obj => obj.Value).ToList();
        }

        /// <summary>
        /// A List of ValidationError objects for the property. 
        /// </summary>
        public List<ValidationError> GetBrokenRulesForProperty(String propertyName) {
            return (from obj in this.ValidatationErrors
                    where String.Compare(obj.Value.PropertyName, propertyName, StringComparison.OrdinalIgnoreCase) == 0
                    select obj.Value).ToList();
        }

        /// <summary>
        /// Runs the CheckAllRules methods and returns a Boolean indicating if the Object is valid (does it pass all Shared and Instance rules).
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean IsValid() {
            if(this.IsLoading) {
                throw new InvalidOperationException(Resources.BusinessEntityBase_STRING_END_LOADING_NEVER_CALLED_EndLoading_never_called_after_a_BeginLoading_call_was_made___No_operations_are_permitted_until_EndLoading_has_been_called_);
            }

            CheckAllRules();

            Boolean isValid = this.ValidatationErrors.Count == 0;
            _hasBeenValidated = isValid;
            InternalRaisePropertyChanged(_STRING_HASBEENVALIDATED);
            return isValid;
        }

        Boolean RuleSetMatches(String ruleSet) {

            if (String.IsNullOrWhiteSpace(this.ActiveRuleSet) || String.IsNullOrWhiteSpace(ruleSet)) {
                return true;
            }

            String[] separators = { GlobalConstants.STRING_RULESET_DELIMITER };

            return ruleSet.Split(separators, StringSplitOptions.RemoveEmptyEntries).Any(s => String.Compare(s, this.ActiveRuleSet, StringComparison.OrdinalIgnoreCase) == 0);
        }

        #endregion

        #region  Property Set and Change Notification Methods

        /// <summary> 
        /// Derived classes can override this method to execute logic after the property is set. The base implementation does nothing. 
        /// </summary> 
        /// <param name="propertyName"> 
        /// The property which was changed. 
        /// </param> 
        protected void AfterPropertyChanged(String propertyName) {
        }

        /// <summary> 
        /// Derived classes can override this method to execute logic before the property is set. The base implementation does nothing. 
        /// </summary> 
        /// <param name="propertyName"> 
        /// The property which was changed. 
        /// </param> 
        protected void BeforePropertyChanged(String propertyName) {
        }

        void InternalRaisePropertyChanged(String propertyName) {
            RaisePropertyChanged(propertyName);
            this.AfterPropertyChanged(propertyName);
        }

        /// <summary>
        /// Called by in business entity sub-classes in their property setters to set the value of the property.
        /// If the business Object is not in a loading state, this method performs validation on the property
        /// <example>Example:
        /// <code>
        ///   Set(ByVal Value As String)
        ///       MyBase.SetPropertyValue("SL_DatabaseConnection", _strSL_DatabaseConnection, Value)
        ///   End Set
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        /// <param name="currentValue">Current property value</param>
        /// <param name="newValue">New property value</param>
        protected void SetPropertyValue(String propertyName, ref String currentValue, String newValue) {

            if (currentValue == null) {

                if (newValue == null) {
                    return;
                }

            } else if (newValue != null && currentValue.Equals(newValue)) {
                return;
            }

            if(!this.IsLoading) {
                _hasBeenValidated = false;
                _isDirty = true;
                this.BeforePropertyChanged(propertyName);

                //only apply character casing rules after the Object is loaded.
                CharacterCasing characterCasing = SharedCharacterCasingRules.GetManager(this.GetType()).GetRuleForProperty(propertyName);

                currentValue = characterCasing != CharacterCasing.None ? FormatText.ApplyCharacterCasing(newValue, characterCasing) : newValue;

                CheckRulesForProperty(propertyName);
                InternalRaisePropertyChanged(propertyName);
                InternalRaisePropertyChanged(_STRING_ISDIRTY);
                InternalRaisePropertyChanged(_STRING_HASBEENVALIDATED);
                InternalRaisePropertyChanged(_STRING_ERROR);
                InternalRaisePropertyChanged(_STRING_HASERRORS);
                InternalRaisePropertyChanged(_STRING_HASNOERRORS);

                if (this.ThrowExceptionFromPropertySetters) {

                    String error = this[propertyName];

                    if (!(String.IsNullOrEmpty(error))) {
                        throw new Exception(error);
                    }

                }

                this.AfterPropertyChanged(propertyName);

            } else {
                //since we are loading, just set the value
                currentValue = newValue;
            }

        }

        /// <summary>
        /// Called by business entity sub-classes in their property setters to set the value of the property.
        /// If the business Object is not in a loading state, this method performs validation on the property
        /// <example>Example:
        /// <code>
        ///   Set(ByVal Value As String)
        ///       MyBase.SetPropertyValue("Birthday", _datBirthDay, Value)
        ///   End Set
        /// </code>
        /// </example>
        /// </summary>
        /// <typeparam name="T">Property Type</typeparam>
        /// <param name="propertyName">Property Name</param>
        /// <param name="currentValue">variable that holds the current value of the property</param>
        /// <param name="newValue">Is the Value parameter from the Setter Set.</param>
        protected void SetPropertyValue<T>(String propertyName, ref T currentValue, T newValue) {

// ReSharper disable CompareNonConstrainedGenericWithNull
            if (currentValue == null) {
// ReSharper restore CompareNonConstrainedGenericWithNull

// ReSharper disable CompareNonConstrainedGenericWithNull
                if (newValue == null) {
// ReSharper restore CompareNonConstrainedGenericWithNull
                    return;
                }

// ReSharper disable CompareNonConstrainedGenericWithNull
            } else if (newValue != null && currentValue.Equals(newValue)) {
// ReSharper restore CompareNonConstrainedGenericWithNull
                return;
            }

            if(!this.IsLoading) {
                _hasBeenValidated = false;
                _isDirty = true;
                this.BeforePropertyChanged(propertyName);
                currentValue = newValue;
                CheckRulesForProperty(propertyName);
                InternalRaisePropertyChanged(propertyName);
                InternalRaisePropertyChanged(_STRING_ISDIRTY);
                InternalRaisePropertyChanged(_STRING_HASBEENVALIDATED);
                InternalRaisePropertyChanged(_STRING_ERROR);
                InternalRaisePropertyChanged(_STRING_HASERRORS);
                InternalRaisePropertyChanged(_STRING_HASNOERRORS);

                if (this.ThrowExceptionFromPropertySetters) {

                    String error = this[propertyName];

                    if (!(String.IsNullOrEmpty(error))) {
                        throw new Exception(error);
                    }
                }

                this.AfterPropertyChanged(propertyName);

            } else {
                //if we are loading the Object then just assign the value
                currentValue = newValue;
            }
        }

        #endregion

        #region  Object Loading, Complete Loading & Persisted Methods

        /// <summary>
        /// Called when the business Object is being loaded from a database.  This saves time and processing; by passing property setter logic during loading.  After the business Object has been loaded the EndLoading MUST be called.
        /// </summary>
        public void BeginLoading() {
            this.IsLoading = true;
        }

        /// <summary>
        /// After a business Object has been loaded and the BeginLoading method was called, developers must call this method, EndLoading.  This method marks the entity IsDirty = False, HasBeenValidated = False and raises these property changed events.
        /// </summary>
        public void EndLoading() {
            this.IsLoading = false;
            _hasBeenValidated = false;
            _isDirty = false;
            InternalRaisePropertyChanged(_STRING_ISDIRTY);
            InternalRaisePropertyChanged(_STRING_HASBEENVALIDATED);
        }

        /// <summary>
        /// This method should be called by the business layer after a Valid business Object has been persisted to a database, web service, etc.  Calling this method, marks this business Object as Not Dirty.  
        /// </summary>
        public void ObjectPersisted() {
            _isDirty = false;
            InternalRaisePropertyChanged(_STRING_ISDIRTY);
        }

        #endregion

        #region  Audit Methods

        /// <summary>
        /// Used to generate a Dictionary(Of String, String) for each property decorated with the AuditAttribute.  Dictionary is property name, property value.
        /// </summary>
        /// <param name="defaultValue">Default message if no class propeties are decorated</param>
        /// <param name="dictionary">Pass an IDictionary Object that needs to be populated.  Typically this would be the Data property of an Exception Object.</param>
        /// <returns>IDictionary</returns>
        public IDictionary<String, String> ToAuditIDictionary(String defaultValue, IDictionary<String, String> dictionary) {
            return ClassMessageCreationHelper.AuditToIDictionary(this, defaultValue, dictionary);
        }

        /// <summary>
        /// Returns a String of each property decorated with the AuditAttribute.  The String displays the property name, property friendly name and property value.  This function is typically used when a developer needs to make an audit log entry.  It provides a very simple method to generate these messages.
        /// </summary>
        /// <param name="defaultValue">If no properties have been decorated with the AuditAttribute, then this message is displayed.</param>
        /// <param name="delimiter">What delimiter do you want between each property.  Defaults to comma.  Could use vbcrlf, etc.</param>
        public String ToAuditString(String defaultValue, String delimiter = GlobalConstants.STRING_DEFAULT_DELIMITER) {
            return ClassMessageCreationHelper.AuditToString(this, defaultValue, delimiter);
        }

        /// <summary>
        /// Used to generate an Dictionary(Of String, String) for each property in the class.
        /// </summary>
        /// <param name="defaultValue">Default message if no class propeties are decorated</param>
        /// <param name="dictionary">Pass an IDictionary Object that needs to be populated.  Typicaly this would be the Data property of an Exception Object.</param>
        /// <param name="sortByPropertyName">Normally sorts the output by property name.</param>
        /// <returns>IDictionary</returns>
        public IDictionary<String, String> ToClassIDictionary(String defaultValue, IDictionary<String, String> dictionary, SortByPropertyName sortByPropertyName =  SortByPropertyName.Yes) {
            return ClassMessageCreationHelper.ClassToIDictionary(this, defaultValue, dictionary, sortByPropertyName);
        }

        /// <summary>
        /// This function returns a String with each property and value in the class.  The String displays the property name, property friendly name and property value.
        /// </summary>
        /// <param name="delimiter">What delimiter do you want between each property.  Defaults to comma.  Could use Envoirnment.NewLine, etc.</param>
        /// <param name="sortByProperytName">Normally sorts the output by property name.</param>
        /// <returns>String</returns>
        public String ToClassString(String delimiter = GlobalConstants.STRING_DEFAULT_DELIMITER, SortByPropertyName sortByProperytName = SortByPropertyName.Yes) {
            return ClassMessageCreationHelper.ClassToString(this, delimiter, sortByProperytName);
        }

        #endregion
    }
}


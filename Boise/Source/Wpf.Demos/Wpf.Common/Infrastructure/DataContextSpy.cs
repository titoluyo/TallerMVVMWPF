using System;
using System.Windows;
using System.Windows.Data;

namespace Wpf.Common.Infrastructure {
    //This handy code snippet comes from Mr. WPF, Josh Smith's blog. 
    //http://blogs.infragistics.com/blogs/josh_smith/archive/2008/06/26/data-binding-the-isvisible-property-of-contextualtabgroup.aspx

    /// <summary>
    /// Represents DataContextSpy
    /// </summary>
    public class DataContextSpy : Freezable {

        /// <summary>
        /// DataContext
        /// </summary>
        public static readonly DependencyProperty DataContextProperty = FrameworkElement.DataContextProperty.AddOwner(typeof(DataContextSpy));
        
        /// <summary>
        /// Gets or sets the data context.
        /// </summary>
        /// <value>The data context.</value>
        public Object DataContext {
            get {
                return GetValue(DataContextProperty);
            }
            set {
                SetValue(DataContextProperty, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextSpy"/> class.
        /// </summary>
        public DataContextSpy() {
            BindingOperations.SetBinding(this, DataContextProperty, new Binding());
        }

        /// <summary>
        /// When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable"/> derived class.
        /// </summary>
        /// <returns>The new instance.</returns>
        protected override Freezable CreateInstanceCore() {
            throw new NotSupportedException();
        }
    }
}

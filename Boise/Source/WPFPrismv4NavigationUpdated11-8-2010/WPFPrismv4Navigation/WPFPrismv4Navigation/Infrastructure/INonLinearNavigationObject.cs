using System;

namespace WPFPrismv4Navigation.Infrastructure {

    /// <summary>
    /// This interface is the contract that a View or ViewModel can use to
    /// surface information to the application when a Region's objects are
    /// being iterated over.
    /// 
    /// This information is then displayed in the UI to allow the end user
    /// to see the currently running objects and can provide a UI Element 
    /// for navigating back to the object.  
    /// </summary>
    public interface INonLinearNavigationObject {

        /// <summary>
        /// The title is a description of the object.  This title 
        /// should easily allow an end user to identity the object 
        /// by its title.
        /// For example, for a Customer object you may want to return the First and Last Name.
        /// For an Item object you may want to return the item description.
        /// </summary>
        String Title { get; }


        /// <summary>
        /// The state describes what state the object is in.
        /// For example, adding or editing.
        /// </summary>
        String State { get; }

        /// <summary>
        /// The key is the primary key.  This is helpful in scenarios where
        /// the end user is familar with the primary key.
        /// For example, a Customer Number or Item ID.
        /// </summary>
        String Key { get; }

        /// <summary>
        /// The Uri that will allow navigating back to this object.
        /// This would include any query string parameters.
        /// This field is typically assigned in the initial OnNavigatedTo method.
        /// </summary>
        String UriString { get; }

        /// <summary>
        /// The application allows grouping of objects within a region.
        /// For example, if 3 objects are inventory views and 2 objects
        /// are sales objects, you could easily get a count of objects
        /// and display that value in the UI
        /// </summary>
        String Application { get; }

    }
}

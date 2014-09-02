using System;

namespace ThePhoneCompany.Common.Infrastructure {
    /// <summary>
    /// Short lived DTO representing <see cref="INonLinearNavigationObject"/> that is
    /// created and consumed after each navigation operation 
    /// </summary>
    public class NonLinearNavigationMetadata : INonLinearNavigationObject {

        public String Title { get; private set; }
        public String State { get; private set; }
        public String Key { get; private set; }
        public String UriString { get; private set; }
        public String Application { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonLinearNavigationMetadata"/> class.
        /// </summary>
        /// <param name="nonLinearNavigationObject">The non linear navigation object.</param>
        public NonLinearNavigationMetadata(INonLinearNavigationObject nonLinearNavigationObject) {
            this.Title = nonLinearNavigationObject.Title;
            this.State = nonLinearNavigationObject.State;
            this.Key = nonLinearNavigationObject.Key;
            this.UriString = nonLinearNavigationObject.UriString;
            this.Application = nonLinearNavigationObject.Application;
        }
    }
}

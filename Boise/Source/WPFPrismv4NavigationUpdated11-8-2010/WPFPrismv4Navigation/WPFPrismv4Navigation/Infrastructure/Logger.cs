using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace WPFPrismv4Navigation.Infrastructure {

    /// <summary>
    /// The Logger is only used for this demo application.
    /// Its purpose is to accept, store and provide log messages
    /// to the application.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class Logger {

        Int32 _order;
        Scenario _scenario;

        public enum Scenario { 
            Navagate, ItemNavigate, GoBack, CloseView, GoBackToOriginalCaller,
            NavigateUsingLink, NavigationApiThrowsException,
            NavigationTargetThrowsException,
            NavigationTargetThrowsExceptionTargetHandles
        }

        public ObservableCollection<LogItem> Items { get; private set; }

        /// <summary>
        /// Resets the Logger by clearing the Items collection.
        /// </summary>
        public void Reset() {
            _order = 0;
            this.Items.Clear();
        }

        /// <summary>
        /// Adds a log message to the log.  Notice how this method automatically adds the
        /// calling Type Name and Method Name to the log message.
        /// </summary>
        /// <param name="scenario">The scenario for this navigation request. 
        /// The scenario should only be provided on the first call to the logger for a
        /// navigation request.</param>
        /// <param name="notes">Optional addition notes for the log entry.
        /// first entry in the navigation timing cycle for this navigation request.
        /// </param>
        public void Log(Scenario scenario, String notes) {
            _scenario = scenario;
            this.Log(notes, true);
        }

        /// <summary>
        /// Adds a log message to the log.  Notice how this method automatically adds the
        /// calling Type Name and Method Name to the log message.
        /// </summary>
        /// <param name="notes">Optional addition notes for the log entry.
        /// first entry in the navigation timing cycle for this navigation request.
        /// </param>
        public void Log(String notes = "") {
            this.Log(notes, false);
        }

        void Log(String notes, Boolean isFirstInGroup) {
            var stackTrace = new StackTrace();
            _order += 1;

            String typeName = stackTrace.GetFrame(2).GetMethod().DeclaringType.Name;
            String methodName = stackTrace.GetFrame(2).GetMethod().Name;

            this.Items.Add(new LogItem(_order, this._scenario.ToString(), typeName, methodName, notes, isFirstInGroup));
        
        }

        public Logger() {
            this.Items = new ObservableCollection<LogItem>();
        }
    }
}

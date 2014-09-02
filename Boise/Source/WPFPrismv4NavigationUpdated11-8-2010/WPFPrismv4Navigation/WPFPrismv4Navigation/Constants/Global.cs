using System;

namespace WPFPrismv4Navigation.Constants {
    public class Global {

        public const String DecisionPointUserConfirmsNavigation = "Decision point: user confirms navigation";
        public const String DecisionPointAmITheTarget = "Decision point: am I the target?";
        public const String CloseJournalBackInitiated = "Close and Journal Back initiated";
        public const String JournalBackInitiated = "Journal Back initiated";
        public const String NavigationInitiated = "Navigation initiated";
        public const String NavigationRequestResultsCallBack = "NavigationRequest results call back: ";
        public const String DecisionPointKeepAliveValueReturned = "Decision point: keep alive?  Value returned: ";
        public const String RegionNavigatedTo = "Region navigated to: ";
        public const String RegionNavigatingTo = "Region navigating to: ";
        public const String RegionNavigationFailed = "Region navigation failed: ";
        public const String JournalBackInitiatedAfterException = "Journal Back initiated after exception";

        public const String Item = "item";
        public const String Viewing = "viewing";
        public const String ApplicationName = "WPF Prism v4 Navigation";
        public const String NoKey = "no key";
        public const String Throw = "Throw";
        public const String Exception = "Exception";
        public const String ThrowAndHandle = "ThrowAndHandle";
        public const String ExceptionThrownAfterNavigationCompleted = "Exception throw after navigation completed";
        public const String RepositoryExceptionMessage = "Repository Exception: server down";
        public const String ShowAll = "Show All";
        public const String ShowOnlyViewData = "Show Only View Data";
        public const String ShowOnlyViewModelData = "Show Only ViewModel Data";
        public const String View = "View";
        public const String ViewModel = "ViewModel";
        public const String NavigationError = "Navigation Error";

        /// <summary>
        /// Format String Parameters, 0 = Exception Message
        /// </summary>
        public const String ExceptionCaughtLogMessage_FormatString = "{0}, exception caught and handled";

        /// <summary>
        /// Format String Parameters, 0 = Uri string, 1 = Exception Message
        /// </summary>
        public const String NavigationFailedMessage_FormatString = "Navigation to {0}, failed. Message: {1}.";

        public static readonly String HandledExceptionMessage = String.Format("Simulate repository exception.  Message: {0}{1}{1}Pressing OK will cause this view to navigate back to the caller", Global.RepositoryExceptionMessage, Environment.NewLine);
    }
}

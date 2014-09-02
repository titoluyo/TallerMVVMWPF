using System;

namespace Ocean.Wpf.CommonDialog {

    /// <summary>
    /// Represents LogEventArgs
    /// </summary>
    public class LogEventArgs : EventArgs {

        #region  Declarations

        readonly DateTime _dateCreated;
        readonly TaskDialogResult _taskDialogResult = TaskDialogResult.None;
        readonly Int32 _buttonsDisabledDelay;
        readonly Exception _exception;
        readonly String _additionalDetailsText = String.Empty;
        readonly String _instructionHeading = String.Empty;
        readonly String _instructionText = String.Empty;
        readonly String _userName = String.Empty;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets the additional details text.
        /// </summary>
        /// <value>The additional details text.</value>
        public String AdditionalDetailsText {
            get {
                return _additionalDetailsText;
            }
        }

        /// <summary>
        /// Gets the buttons disabled delay.
        /// </summary>
        /// <value>The buttons disabled delay.</value>
        public Int32 ButtonsDisabledDelay {
            get {
                return _buttonsDisabledDelay;
            }
        }

        /// <summary>
        /// Gets the task dialog result.
        /// </summary>
        /// <value>The task dialog result.</value>
        public TaskDialogResult TaskDialogResult {
            get {
                return _taskDialogResult;
            }
        }

        /// <summary>
        /// Gets the date created.
        /// </summary>
        /// <value>The date created.</value>
        public DateTime DateCreated {
            get {
                return _dateCreated;
            }
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception {
            get {
                return _exception;
            }
        }

        /// <summary>
        /// Gets the instruction heading.
        /// </summary>
        /// <value>The instruction heading.</value>
        public String InstructionHeading {
            get {
                return _instructionHeading;
            }
        }

        /// <summary>
        /// Gets the instruction text.
        /// </summary>
        /// <value>The instruction text.</value>
        public String InstructionText {
            get {
                return _instructionText;
            }
        }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public String UserName {
            get {
                return _userName;
            }
        }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventArgs"/> class.
        /// </summary>
        /// <param name="taskDialogResult">The custom task dialog result.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="additionalDetailsText">The additional details text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="exception">The exception.</param>
        public LogEventArgs(TaskDialogResult taskDialogResult, String instructionHeading, String instructionText, String additionalDetailsText, Int32 buttonsDisabledDelay, String userName, Exception exception) {
            _taskDialogResult = taskDialogResult;
            _instructionHeading = instructionHeading;
            _instructionText = instructionText;
            _buttonsDisabledDelay = buttonsDisabledDelay;
            _additionalDetailsText = additionalDetailsText;
            _userName = userName;
            _exception = exception;
            _dateCreated = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventArgs"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="exception">The exception.</param>
        public LogEventArgs(String userName, Exception exception) {
            _userName = userName;
            _exception = exception;
            _dateCreated = DateTime.Now;
        }

        #endregion
    }
}
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Ocean.Wpf.Properties;

namespace Ocean.Wpf.CommonDialog {

    /// <summary>
    ///  Represents TaskDialog that displays a modal custom dialog box and returns the result to the caller.
    /// </summary>
    public class TaskDialog {

        //usage
        //Dim x As New TaskDialog(Nothing, Nothing)
        //    x.InstructionIcon = TaskDialogIcon.Shield
        //    x.FooterIcon = TaskDialogIcon.Information
        //    x.Buttons = TaskDialogButton.OKCancel
        //    x.DefaultButton = TaskDialogResult.OK
        //    x.Caption = "Dialog Caption"
        //    x.InstructionHeading = "This Is The Heading"
        //    x.InstructionText = "This is the text"
        //    x.FooterText = "This is the footer text"
        //    x.AdditionalDetailsText = "This is some text that can be used to explain the reason for this dialog box."
        //Dim obj As DialogResult = x.Show()
        //'MessageBox.Show(obj.ToString)
        //
        #region Declarations

        TaskDialogButton _buttons = TaskDialogButton.Ok;
        TaskDialogResult _customDialogResult = TaskDialogResult.None;
        TaskDialogResult _defaultButton = TaskDialogResult.None;
        TaskDialogIcon _footerIcon = TaskDialogIcon.None;
        TaskDialogIcon _instructionIcon = TaskDialogIcon.None;
        String _additionalDetailsText = string.Empty;
        String _caption = string.Empty;
        String _footerText = string.Empty;
        String _instructionHeading = string.Empty;
        String _instructionText = string.Empty;

        #endregion

        #region  Public Properties

        /// <summary>
        /// Gets or sets the optional text is displayed to the user when they click to Show Details expander.  Used to provide a detailed explaination to the user.
        /// </summary>
        public string AdditionalDetailsText {
            get {
                return _additionalDetailsText;
            }
            set {
                _additionalDetailsText = value;
            }
        }

        /// <summary>
        /// Gets or sets the buttons that will be displayed.  The default is the OK button.
        /// </summary>
        public TaskDialogButton Buttons {
            get {
                return _buttons;
            }
            set {
                _buttons = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of seconds that the buttons will be disabled, providing time for the user to read the dialog before dismissing it.  Assigning a value also causes a progress bar to display the elapsed time before the buttons are enabled.
        /// </summary>
        public int ButtonsDisabledDelay { get; set; }

        /// <summary>
        /// Gets or sets the dialog box window caption.  The caption is displayed in the window chrome.
        /// </summary>
        public string Caption {
            get {
                return _caption;
            }
            set {
                _caption = value;
            }
        }

        /// <summary>
        /// Gets or sets the default button for the dialog box.  This value defaults to none.
        /// </summary>
        public TaskDialogResult DefaultButton {
            get {
                return _defaultButton;
            }
            set {
                _defaultButton = value;
            }
        }

        /// <summary>
        /// Gets or sets the icon displayed in the dialog footer.  This values defaults to none.
        /// </summary>
        public TaskDialogIcon FooterIcon {
            get {
                return _footerIcon;
            }
            set {
                _footerIcon = value;
            }
        }

        /// <summary>
        /// Gets or sets the optional text displayed in the footer.
        /// </summary>
        public string FooterText {
            get {
                return _footerText;
            }
            set {
                _footerText = value;
            }
        }

        /// <summary>
        /// Gets or sets the heading text displayed in the dialog box.
        /// </summary>
        public string InstructionHeading {
            get {
                return _instructionHeading;
            }
            set {
                _instructionHeading = value;
            }
        }

        /// <summary>
        /// Gets or sets the icon displayed to the left of the instruction text.  This value defaults to none.
        /// </summary>
        public TaskDialogIcon InstructionIcon {
            get {
                return _instructionIcon;
            }
            set {
                _instructionIcon = value;
            }
        }

        /// <summary>
        /// Gets or sets the text message for the dialog.
        /// </summary>
        public string InstructionText {
            get {
                return _instructionText;
            }
            set {
                _instructionText = value;
            }
        }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDialog"/> class.
        /// </summary>
        public TaskDialog() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDialog"/> class.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <param name="instructionIcon">The instruction icon.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        public TaskDialog(String caption, String instructionHeading, String instructionText, TaskDialogButton buttons, TaskDialogResult defaultButton, TaskDialogIcon instructionIcon, Int32 buttonsDisabledDelay = 0) {
            _caption = caption;
            _instructionHeading = instructionHeading;
            _instructionText = instructionText;
            _buttons = buttons;
            _defaultButton = defaultButton;
            _instructionIcon = instructionIcon;
            this.ButtonsDisabledDelay = buttonsDisabledDelay;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDialog"/> class.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <param name="instructionIcon">The instruction icon.</param>
        /// <param name="footerIcon">The footer icon.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        public TaskDialog(String caption, String instructionHeading, String instructionText, String footerText, TaskDialogButton buttons, TaskDialogResult defaultButton, TaskDialogIcon instructionIcon, TaskDialogIcon footerIcon, Int32 buttonsDisabledDelay = 0) {
            _caption = caption;
            _instructionHeading = instructionHeading;
            _instructionText = instructionText;
            _footerText = footerText;
            _buttons = buttons;
            _defaultButton = defaultButton;
            _instructionIcon = instructionIcon;
            _footerIcon = footerIcon;
            this.ButtonsDisabledDelay = buttonsDisabledDelay;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDialog"/> class.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="additionalDetailsText">The additional details text.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <param name="instructionIcon">The instruction icon.</param>
        /// <param name="footerIcon">The footer icon.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        public TaskDialog(String caption, String instructionHeading, String instructionText, String additionalDetailsText, String footerText, TaskDialogButton buttons, TaskDialogResult defaultButton, TaskDialogIcon instructionIcon, TaskDialogIcon footerIcon, Int32 buttonsDisabledDelay = 0) {
            _caption = caption;
            _instructionHeading = instructionHeading;
            _instructionText = instructionText;
            _additionalDetailsText = additionalDetailsText;
            _footerText = footerText;
            _buttons = buttons;
            _defaultButton = defaultButton;
            _instructionIcon = instructionIcon;
            _footerIcon = footerIcon;
            this.ButtonsDisabledDelay = buttonsDisabledDelay;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <param name="customDialogIcon">The custom dialog icon.</param>
        /// <returns></returns>
        Uri GetIcon(TaskDialogIcon customDialogIcon) {

            switch (customDialogIcon) {

                case TaskDialogIcon.Information:
                    return new Uri("Information.png", UriKind.Relative);

                case TaskDialogIcon.None:
                    return null;

                case TaskDialogIcon.Question:
                    return new Uri("Question.png", UriKind.Relative);

                case TaskDialogIcon.Shield:
                    return new Uri("Shield.png", UriKind.Relative);

                case TaskDialogIcon.Stop:
                    return new Uri("Stop.png", UriKind.Relative);

                case TaskDialogIcon.Warning:
                    return new Uri("Warning.png", UriKind.Relative);

                default:
                    throw new ArgumentOutOfRangeException("customDialogIcon", customDialogIcon.ToString(), Resources.CustomTaskDialog_GetIcon_Programmer_did_not_program_for_this_icon_);
            }
        }

        void SetButtons(TaskDialogWindow win) {

            switch (this.Buttons) {

                case TaskDialogButton.Ok:
                    win.btnCancel.Visibility = Visibility.Collapsed;
                    win.btnNo.Visibility = Visibility.Collapsed;
                    win.btnYes.Visibility = Visibility.Collapsed;

                    break;
                case TaskDialogButton.OkCancel:
                    win.btnNo.Visibility = Visibility.Collapsed;
                    win.btnYes.Visibility = Visibility.Collapsed;

                    break;
                case TaskDialogButton.YesNo:
                    win.btnOK.Visibility = Visibility.Collapsed;
                    win.btnCancel.Visibility = Visibility.Collapsed;

                    break;
                case TaskDialogButton.YesNoCancel:
                    win.btnOK.Visibility = Visibility.Collapsed;

                    break;
                default:
                    throw new OverflowException(Resources.CustomTaskDialog_SetButtons_Programmer_did_not_program_for_this_selection_);
            }

            switch (this.DefaultButton) {

                case TaskDialogResult.Cancel:
                    win.btnCancel.IsDefault = true;

                    break;
                case TaskDialogResult.No:
                    win.btnNo.IsDefault = true;
                    win.btnCancel.IsCancel = true;

                    break;
                case TaskDialogResult.None:
                    //do nothing
                    win.btnCancel.IsCancel = true;

                    break;
                case TaskDialogResult.Ok:
                    win.btnOK.IsDefault = true;
                    win.btnCancel.IsCancel = true;

                    break;
                case TaskDialogResult.Yes:
                    win.btnYes.IsDefault = true;
                    win.btnCancel.IsCancel = true;

                    break;
                default:
                    throw new OverflowException(Resources.CustomTaskDialog_SetButtons_Programmer_did_not_program_for_this_selection_);
            }
        }

        /// <summary>
        ///     Shows the custom dialog described by the constructor and properties set by the caller, returns CustomDialogResult.
        /// </summary>
        /// <returns>
        ///     A CommonDialog.CustomDialogResult value.
        /// </returns>
        public TaskDialogResult Show() {

            var win = new TaskDialogWindow(this.ButtonsDisabledDelay) { tbCaption = { Text = this.Caption } };

            if (this.FooterText.Length > 0) {
                win.tbFooterText.Text = this.FooterText;

                if (this.FooterIcon != TaskDialogIcon.None) {
                    win.imgFooterIcon.Source = new BitmapImage(GetIcon(this.FooterIcon));

                } else {
                    win.imgFooterIcon.Visibility = Visibility.Collapsed;
                }

            } else {
                win.tbFooterText.Visibility = Visibility.Collapsed;
                win.imgFooterIcon.Visibility = Visibility.Collapsed;
            }

            if (this.InstructionIcon == TaskDialogIcon.None) {
                win.imgInstructionIcon.Visibility = Visibility.Collapsed;

            } else {
                win.imgInstructionIcon.Source = new BitmapImage(GetIcon(this.InstructionIcon));
            }

            win.tbInstructionText.Text = this.InstructionText;
            win.tbInstructionHeading.Text = this.InstructionHeading;

            if (this.AdditionalDetailsText.Length > 0) {
                win.tbAdditionalDetailsText.Text = this.AdditionalDetailsText;

            } else {
                win.expAdditionalDetails.Visibility = Visibility.Collapsed;
            }

            SetButtons(win);
            win.ShowInTaskbar = false;
            win.Topmost = true;
            win.ShowDialog();
            _customDialogResult = win.TaskDialogResult;
            return _customDialogResult;
        }

        #endregion
    }
}
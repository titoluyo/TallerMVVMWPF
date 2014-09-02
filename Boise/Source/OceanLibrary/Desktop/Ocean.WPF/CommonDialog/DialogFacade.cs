using System;
using System.IO;
using System.Windows;
using Ocean.Wpf.Properties;

namespace Ocean.Wpf.CommonDialog {

    /// <summary>
    /// Represents DialogFacade providing wrappers for common dialogs and logging
    /// </summary>
    public sealed class DialogFacade : IDialogFacade {

        #region  Events

        /// <summary>
        /// Occurs when log is raised.
        /// </summary>
        public event LogEventHandler Log;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public String UserName { get; set; }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogFacade"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        public DialogFacade(String userName = "") {
            this.UserName = String.IsNullOrWhiteSpace(userName) ? Environment.UserName : userName;
        }

        #endregion

        #region  Log Exception

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void LogException(Exception ex) {
            RaiseLog(ex);
        }

        #endregion

        #region  Log Messages

        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void LogMessage(String title, String message) {
            RaiseLog(TaskDialogResult.None, title, message, String.Empty, 0, null);
        }

        #endregion

        #region  Show Custom Dialog Methods

        /// <summary>
        /// Displays dialog described by the arguments
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="instructionIcon">The instruction icon.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="footerIcon">The footer icon.</param>
        /// <param name="additionalDetailsText">The additional details text.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        public TaskDialogResult AllOptionsDialog(String caption, String instructionHeading, String instructionText, TaskDialogIcon instructionIcon, String footerText, TaskDialogIcon footerIcon, String additionalDetailsText, TaskDialogButton buttons, TaskDialogResult defaultButton, Int32 buttonsDisabledDelay = 0) {
            return ShowTaskDialog(caption, instructionHeading, instructionText, instructionIcon, footerText, footerIcon, additionalDetailsText, buttons, defaultButton, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Displays exception dialog.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        public void ExceptionDialog(Exception ex, Int32 buttonsDisabledDelay = 0) {
#if DEBUG
            ShowTaskDialog(Resources.DialogFacade_ExceptionDialog_Exception, Resources.DialogFacade_ExceptionDialog_Exception, ex.Message, TaskDialogIcon.Warning, String.Empty, TaskDialogIcon.None, ex.StackTrace, TaskDialogButton.Ok, TaskDialogResult.Ok, buttonsDisabledDelay, ex);

#else
				ShowTaskDialog(Resources.DialogFacade_ExceptionDialog_Exception, Resources.DialogFacade_ExceptionDialog_Exception, ex.Message, TaskDialogIcon.Warning, String.Empty, TaskDialogIcon.None, String.Empty, TaskDialogButton.Ok, TaskDialogResult.Ok, buttonsDisabledDelay, ex);
#endif
        }

        /// <summary>
        /// Displays exception dialog.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        public void ExceptionDialog(Exception ex, String caption, String instructionHeading, Int32 buttonsDisabledDelay = 0) {
#if DEBUG
            ShowTaskDialog(caption, instructionHeading, ex.Message, TaskDialogIcon.Warning, String.Empty, TaskDialogIcon.None, ex.StackTrace, TaskDialogButton.Ok, TaskDialogResult.Ok, buttonsDisabledDelay, ex);
#else
				ShowTaskDialog(caption, instructionHeading, ex.Message, TaskDialogIcon.Warning, String.Empty, TaskDialogIcon.None, String.Empty, TaskDialogButton.Ok, TaskDialogResult.Ok, buttonsDisabledDelay, ex);
#endif
        }

        /// <summary>
        /// Exceptions the dialog.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        public void ExceptionDialog(Exception ex, String caption, String instructionHeading, String instructionText, Int32 buttonsDisabledDelay = 0) {
#if DEBUG
            ShowTaskDialog(caption, instructionHeading, instructionText, TaskDialogIcon.Warning, String.Empty, TaskDialogIcon.None, ex.StackTrace, TaskDialogButton.Ok, TaskDialogResult.OK, buttonsDisabledDelay, ex);
#else
				ShowTaskDialog(caption, instructionHeading, instructionText, TaskDialogIcon.Warning, String.Empty, TaskDialogIcon.None, String.Empty, TaskDialogButton.Ok, TaskDialogResult.Ok, buttonsDisabledDelay, ex);
#endif
        }

        /// <summary>
        /// Displays exception dialog.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="instructionIcon">The instruction icon.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="footerIcon">The footer icon.</param>
        /// <param name="additionalDetailsText">The additional details text.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        public void ExceptionDialog(Exception ex, String caption, String instructionHeading, String instructionText, TaskDialogIcon instructionIcon, String footerText, TaskDialogIcon footerIcon, String additionalDetailsText, TaskDialogButton buttons, TaskDialogResult defaultButton, Int32 buttonsDisabledDelay = 0) {
            ShowTaskDialog(caption, instructionHeading, instructionText, instructionIcon, footerText, footerIcon, additionalDetailsText, buttons, defaultButton, buttonsDisabledDelay, ex);
        }

        /// <summary>
        /// Displays message dialog.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        public void MessageDialog(String caption, String instructionHeading, String instructionText, Int32 buttonsDisabledDelay = 0) {
            ShowTaskDialog(caption, instructionHeading, instructionText, TaskDialogIcon.Information, String.Empty, TaskDialogIcon.None, String.Empty, TaskDialogButton.Ok, TaskDialogResult.Ok, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Displays message dialog.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        public void MessageDialog(String caption, String instructionHeading, String instructionText, String footerText, Int32 buttonsDisabledDelay = 0) {
            ShowTaskDialog(caption, instructionHeading, instructionText, TaskDialogIcon.Information, footerText, TaskDialogIcon.Information, String.Empty, TaskDialogButton.Ok, TaskDialogResult.Ok, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Displays message dialog.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="additionalDetailsText">The additional details text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        public void MessageDialog(String caption, String instructionHeading, String instructionText, String footerText, String additionalDetailsText, Int32 buttonsDisabledDelay = 0) {
            ShowTaskDialog(caption, instructionHeading, instructionText, TaskDialogIcon.Information, footerText, TaskDialogIcon.Information, additionalDetailsText, TaskDialogButton.Ok, TaskDialogResult.Ok, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Display save confirmation dialog
        /// </summary>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        public TaskDialogResult YesNoCancelConfirmSave(Int32 buttonsDisabledDelay = 0) {
            return ShowTaskDialog(Resources.DialogFacade_YesNoCancelConfirmSave_Save, Resources.DialogFacade_YesNoCancelConfirmSave_Confirm_Save, Resources.DialogFacade_YesNoCancelConfirmSave_Would_you_like_to_save_your_data_before_closing_this_form_, TaskDialogIcon.Question, String.Empty, TaskDialogIcon.None, String.Empty, TaskDialogButton.YesNoCancel, TaskDialogResult.No, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Display save confirmation dialog
        /// </summary>
        /// <param name="footerText">The footer text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        public TaskDialogResult YesNoCancelConfirmSave(String footerText, Int32 buttonsDisabledDelay = 0) {
            return ShowTaskDialog(Resources.DialogFacade_YesNoCancelConfirmSave_Save, Resources.DialogFacade_YesNoCancelConfirmSave_Confirm_Save, Resources.DialogFacade_YesNoCancelConfirmSave_Would_you_like_to_save_your_data_before_closing_this_form_, TaskDialogIcon.Question, footerText, TaskDialogIcon.Information, String.Empty, TaskDialogButton.YesNoCancel, TaskDialogResult.No, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Display save confirmation dialog
        /// </summary>
        /// <param name="footerText">The footer text.</param>
        /// <param name="additionalDetailsText">The additional details text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        public TaskDialogResult YesNoCancelConfirmSave(String footerText, String additionalDetailsText, Int32 buttonsDisabledDelay = 0) {
            return ShowTaskDialog(Resources.DialogFacade_YesNoCancelConfirmSave_Save, Resources.DialogFacade_YesNoCancelConfirmSave_Confirm_Save, Resources.DialogFacade_YesNoCancelConfirmSave_Would_you_like_to_save_your_data_before_closing_this_form_, TaskDialogIcon.Question, footerText, TaskDialogIcon.Information, additionalDetailsText, TaskDialogButton.YesNoCancel, TaskDialogResult.No, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Display confirmation dialog
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        public TaskDialogResult YesNoCancelDialog(String caption, String instructionHeading, String instructionText, Int32 buttonsDisabledDelay = 0) {
            return ShowTaskDialog(caption, instructionHeading, instructionText, TaskDialogIcon.Question, String.Empty, TaskDialogIcon.None, String.Empty, TaskDialogButton.YesNoCancel, TaskDialogResult.Yes, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Display confirmation dialog
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        public TaskDialogResult YesNoCancelDialog(String caption, String instructionHeading, String instructionText, String footerText, Int32 buttonsDisabledDelay = 0) {
            return ShowTaskDialog(caption, instructionHeading, instructionText, TaskDialogIcon.Question, footerText, TaskDialogIcon.Information, String.Empty, TaskDialogButton.YesNoCancel, TaskDialogResult.Yes, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Display confirmation dialog
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="additionalDetailsText">The additional details text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        public TaskDialogResult YesNoCancelDialog(String caption, String instructionHeading, String instructionText, String footerText, String additionalDetailsText, Int32 buttonsDisabledDelay = 0) {
            return ShowTaskDialog(caption, instructionHeading, instructionText, TaskDialogIcon.Question, footerText, TaskDialogIcon.Information, additionalDetailsText, TaskDialogButton.YesNoCancel, TaskDialogResult.Yes, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Display delete confirmation dialog
        /// </summary>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        public TaskDialogResult YesNoConfirmDelete(Int32 buttonsDisabledDelay = 0) {
            return ShowTaskDialog(Resources.DialogFacade_YesNoConfirmDelete_Delete, Resources.DialogFacade_YesNoConfirmDelete_Confirm_Delete, Resources.DialogFacade_YesNoConfirmDelete_Are_you_sure_you_want_to_delete_this_data_, TaskDialogIcon.Question, String.Empty, TaskDialogIcon.None, String.Empty, TaskDialogButton.YesNo, TaskDialogResult.No, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Display delete confirmation dialog
        /// </summary>
        /// <param name="footerText">The footer text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        public TaskDialogResult YesNoConfirmDelete(String footerText, Int32 buttonsDisabledDelay=0) {
            return ShowTaskDialog(Resources.DialogFacade_YesNoConfirmDelete_Delete, Resources.DialogFacade_YesNoConfirmDelete_Confirm_Delete, Resources.DialogFacade_YesNoConfirmDelete_Are_you_sure_you_want_to_delete_this_data_, TaskDialogIcon.Question, footerText, TaskDialogIcon.Information, String.Empty, TaskDialogButton.YesNo, TaskDialogResult.No, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Display delete confirmation dialog
        /// </summary>
        /// <param name="footerText">The footer text.</param>
        /// <param name="additionalDetailsText">The additional details text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        public TaskDialogResult YesNoConfirmDelete(String footerText, String additionalDetailsText, Int32 buttonsDisabledDelay=0) {
            return ShowTaskDialog(Resources.DialogFacade_YesNoConfirmDelete_Delete, Resources.DialogFacade_YesNoConfirmDelete_Confirm_Delete, Resources.DialogFacade_YesNoConfirmDelete_Are_you_sure_you_want_to_delete_this_data_, TaskDialogIcon.Question, footerText, TaskDialogIcon.Information, additionalDetailsText, TaskDialogButton.YesNo, TaskDialogResult.No, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Display confirmation dialog
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        public TaskDialogResult YesNoDialog(String caption, String instructionHeading, String instructionText, Int32 buttonsDisabledDelay=0) {
            return ShowTaskDialog(caption, instructionHeading, instructionText, TaskDialogIcon.Question, String.Empty, TaskDialogIcon.None, String.Empty, TaskDialogButton.YesNo, TaskDialogResult.Yes, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Display confirmation dialog
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        public TaskDialogResult YesNoDialog(String caption, String instructionHeading, String instructionText, String footerText, Int32 buttonsDisabledDelay=0) {
            return ShowTaskDialog(caption, instructionHeading, instructionText, TaskDialogIcon.Question, footerText, TaskDialogIcon.Information, String.Empty, TaskDialogButton.YesNo, TaskDialogResult.Yes, buttonsDisabledDelay, null);
        }

        /// <summary>
        /// Display confirmation dialog
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="additionalDetailsText">The additional details text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        public TaskDialogResult YesNoDialog(String caption, String instructionHeading, String instructionText, String footerText, String additionalDetailsText, Int32 buttonsDisabledDelay=0) {
            return ShowTaskDialog(caption, instructionHeading, instructionText, TaskDialogIcon.Question, footerText, TaskDialogIcon.Information, additionalDetailsText, TaskDialogButton.YesNo, TaskDialogResult.Yes, buttonsDisabledDelay, null);
        }

        #endregion

        #region  Show Custom Dialog

        /// <summary>
        /// Shows the custom task dialog.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        /// <returns></returns>
        public Boolean? ShowTaskDialog(Window dialog) {
            return dialog.ShowDialog();
        }

        #endregion

        #region  Show Folder Browser Methods

        /// <summary>
        /// Shows the folder browser.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="showNewFolderButton">if set to <c>true</c> [show new folder button].</param>
        /// <returns></returns>
        public String ShowFolderBrowser(String description, Boolean showNewFolderButton) {

            String strResponse = String.Empty;

            var dialog = new FolderBrowserDialog {Description = description, ShowNewFolderButton = showNewFolderButton};

            if(dialog.ShowDialog()) {
                strResponse = dialog.SelectedPath;
            }

            return strResponse;
        }

        #endregion

        #region  Show File Open Dialog Methods

        /// <summary>
        /// Shows the open file dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public String ShowOpenFileDialog(String title, String filter) {

            String strResponse = String.Empty;

            var dialog = new OpenFileDialog {CheckFileExists = true, MultiSelect = false, Filter = filter, Title = title};

            if(dialog.ShowDialog()) {
                strResponse = dialog.FileName;
            }

            return strResponse;
        }

        /// <summary>
        /// Shows the open files dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public String[] ShowOpenFilesDialog(String title, String filter) {

            String[] strResponse = null;
            
            var dialog = new OpenFileDialog {CheckFileExists = true, MultiSelect = true, Filter = filter, Title = title};

            if(dialog.ShowDialog()) {
                strResponse = dialog.FileNames;
            }

            return strResponse;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Raises the log event.
        /// </summary>
        /// <param name="TaskDialogResult">The custom task dialog result.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="additionalDetails">The additional details.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <param name="ex">The ex.</param>
        public void RaiseLog(TaskDialogResult TaskDialogResult, String instructionHeading, String instructionText, String additionalDetails, Int32 buttonsDisabledDelay, Exception ex) {
            var handler = this.Log;
            if(handler != null) {
                handler(null, new LogEventArgs(TaskDialogResult, instructionHeading, instructionText, additionalDetails, buttonsDisabledDelay, this.UserName, ex));
            }
        }

        /// <summary>
        /// Raises the log event.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void RaiseLog(Exception ex) {
            var handler = this.Log;
            if(handler != null) {
                handler(null, new LogEventArgs(this.UserName, ex));
            }
        }

        /// <summary>
        /// Shows the custom task dialog.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="instructionIcon">The instruction icon.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="footerIcon">The footer icon.</param>
        /// <param name="additionalDetailsText">The additional details text.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        TaskDialogResult ShowTaskDialog(String caption, String instructionHeading, String instructionText, TaskDialogIcon instructionIcon, String footerText, TaskDialogIcon footerIcon, String additionalDetailsText, TaskDialogButton buttons, TaskDialogResult defaultButton, Int32 buttonsDisabledDelay, Exception ex) {
            
// ReSharper disable UseObjectOrCollectionInitializer
            var dialog = new TaskDialog();
// ReSharper restore UseObjectOrCollectionInitializer
            dialog.AdditionalDetailsText = additionalDetailsText;
            dialog.Buttons = buttons;
            dialog.ButtonsDisabledDelay = buttonsDisabledDelay;
            dialog.Caption = caption;
            dialog.DefaultButton = defaultButton;
            dialog.FooterIcon = footerIcon;
            dialog.FooterText = footerText;
            dialog.InstructionHeading = instructionHeading;
            dialog.InstructionIcon = instructionIcon;
            dialog.InstructionText = instructionText;

            var result = dialog.Show();

            RaiseLog(result, instructionHeading, instructionText, additionalDetailsText, buttonsDisabledDelay, ex);

            return result;
        }

        #endregion

        #region Select Image From Disk

        /// <summary>
        /// Provides selecting an image from disk.  Image bytes and names returned in out argument.
        /// </summary>
        /// <param name="selectedBytes">The selected bytes.</param>
        /// <param name="selectedFileName">Name of the selected file.</param>
        public Boolean SelectImage(out Byte[] selectedBytes, out String selectedFileName) {

            selectedBytes = null;
            selectedFileName = String.Empty;

            var fileName = this.ShowOpenFileDialog("Select Image", "Images (*.png,*.jpg,*.gif)|*.png;*.jpg;*.gif");

            if (!String.IsNullOrEmpty(fileName)) {
                if (!File.Exists(fileName)) {
                    //this should never happen since the OpenFileDialog is supposed to protect against this, but...
                    this.MessageDialog("Missing File", "File Not Found", "Can't locate file: " + fileName);
                    return false;
                }

                var fi = new FileInfo(fileName);

                if (fi.Length > Int32.MaxValue) {
                    this.MessageDialog("Image Too Large", "Select New Image", String.Format("The selected image size is too large.  Please select an image smaller than: {0}", int.MaxValue));
                    return false;
                }

                var imgOut = new Byte[Convert.ToInt32(fi.Length) + 1];
                using (var fs = fi.OpenRead()) {
                    fs.Read(imgOut, 0, Convert.ToInt32(fi.Length));
                }
                selectedBytes = imgOut;
                selectedFileName = Path.GetFileName(fileName);
                return true;
            }
            return false;
        }

        #endregion
    }
}


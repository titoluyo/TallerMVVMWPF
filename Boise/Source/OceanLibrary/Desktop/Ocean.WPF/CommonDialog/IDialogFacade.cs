using System;
using System.Windows;

namespace Ocean.Wpf.CommonDialog {

    /// <summary>
    /// Represents IDialogFacade contract
    /// </summary>
    public interface IDialogFacade {

        /// <summary>
        /// Raises the log event.
        /// </summary>
        /// <param name="taskDialogResult">The custom task dialog result.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="additionalDetails">The additional details.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <param name="ex">The ex.</param>
        void RaiseLog(TaskDialogResult taskDialogResult, String instructionHeading, String instructionText, String additionalDetails, Int32 buttonsDisabledDelay, Exception ex);

        /// <summary>
        /// Raises the log event.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void RaiseLog(Exception ex);

        /// <summary>
        /// Occurs when log is raised.
        /// </summary>
        event LogEventHandler Log;

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        String UserName { get; set; }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void LogException(Exception ex);

        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        void LogMessage(String title, String message);

        /// <summary>
        /// Displays exception dialog.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        void ExceptionDialog(Exception ex, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Displays exception dialog.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        void ExceptionDialog(Exception ex, String caption, String instructionHeading, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Displays exception dialog.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="InstructionText">The instruction text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        void ExceptionDialog(Exception ex, String caption, String instructionHeading, String InstructionText, Int32 buttonsDisabledDelay = 0);

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
        void ExceptionDialog(Exception ex, String caption, String instructionHeading, String instructionText, TaskDialogIcon instructionIcon, String footerText, TaskDialogIcon footerIcon, String additionalDetailsText, TaskDialogButton buttons, TaskDialogResult defaultButton, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Displays message dialog.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        void MessageDialog(String caption, String instructionHeading, String instructionText, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Displays message dialog.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        void MessageDialog(String caption, String instructionHeading, String instructionText, String footerText, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Displays message dialog.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="additionalDetailsText">The additional details text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        void MessageDialog(String caption, String instructionHeading, String instructionText, String footerText, String additionalDetailsText, Int32 buttonsDisabledDelay = 0);

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
        TaskDialogResult AllOptionsDialog(String caption, String instructionHeading, String instructionText, TaskDialogIcon instructionIcon, String footerText, TaskDialogIcon footerIcon, String additionalDetailsText, TaskDialogButton buttons, TaskDialogResult defaultButton, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Display save confirmation dialog
        /// </summary>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        TaskDialogResult YesNoCancelConfirmSave(Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Display save confirmation dialog
        /// </summary>
        /// <param name="footerText">The footer text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        TaskDialogResult YesNoCancelConfirmSave(String footerText, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Display save confirmation dialog
        /// </summary>
        /// <param name="footerText">The footer text.</param>
        /// <param name="additionalDetailsText">The additional details text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        TaskDialogResult YesNoCancelConfirmSave(String footerText, String additionalDetailsText, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Display confirmation dialog
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        TaskDialogResult YesNoCancelDialog(String caption, String instructionHeading, String instructionText, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Display confirmation dialog
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        TaskDialogResult YesNoCancelDialog(String caption, String instructionHeading, String instructionText, String footerText, Int32 buttonsDisabledDelay = 0);

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
        TaskDialogResult YesNoCancelDialog(String caption, String instructionHeading, String instructionText, String footerText, String additionalDetailsText, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Display delete confirmation dialog
        /// </summary>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        TaskDialogResult YesNoConfirmDelete(Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Display delete confirmation dialog
        /// </summary>
        /// <param name="footerText">The footer text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        TaskDialogResult YesNoConfirmDelete(String footerText, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Display delete confirmation dialog
        /// </summary>
        /// <param name="footerText">The footer text.</param>
        /// <param name="additionalDetailsText">The additional details text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        TaskDialogResult YesNoConfirmDelete(String footerText, String additionalDetailsText, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Display confirmation dialog
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        TaskDialogResult YesNoDialog(String caption, String instructionHeading, String instructionText, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Display confirmation dialog
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="instructionHeading">The instruction heading.</param>
        /// <param name="instructionText">The instruction text.</param>
        /// <param name="footerText">The footer text.</param>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        /// <returns></returns>
        TaskDialogResult YesNoDialog(String caption, String instructionHeading, String instructionText, String footerText, Int32 buttonsDisabledDelay = 0);

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
        TaskDialogResult YesNoDialog(String caption, String instructionHeading, String instructionText, String footerText, String additionalDetailsText, Int32 buttonsDisabledDelay = 0);

        /// <summary>
        /// Shows the custom task dialog.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        /// <returns></returns>
        Boolean? ShowTaskDialog(Window dialog);

        /// <summary>
        /// Shows the folder browser.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="showNewFolderButton">if set to <c>true</c> [show new folder button].</param>
        /// <returns></returns>
        String ShowFolderBrowser(String description, Boolean showNewFolderButton);

        /// <summary>
        /// Shows the open file dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        String ShowOpenFileDialog(String title, String filter);

        /// <summary>
        /// Shows the open files dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        String[] ShowOpenFilesDialog(String title, String filter);

        /// <summary>
        /// Provides selecting an image from disk.  Image bytes and names returned in out argument.
        /// </summary>
        /// <param name="selectedBytes">The selected bytes.</param>
        /// <param name="selectedFileName">Name of the selected file.</param>
        Boolean SelectImage(out Byte[] selectedBytes, out String selectedFileName);
    }
}

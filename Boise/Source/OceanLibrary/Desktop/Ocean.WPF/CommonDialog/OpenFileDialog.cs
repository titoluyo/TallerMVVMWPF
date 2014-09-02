using System;
using System.Windows.Forms;
using Ocean.VistaBridgeLibrary.Dialog;
using Ocean.Wpf.Properties;

namespace Ocean.Wpf.CommonDialog {

    /// <summary>
    /// Represents OpenFileDialog
    /// </summary>
    public class OpenFileDialog {

        #region Declarations

        Boolean _checkPathExists = true;
        Boolean _validateNames = true;
        String _fileName = String.Empty;
        String[] _fileNames = { "" };
        String _filter = String.Empty;
        String _initialDirectory = String.Empty;
        String _title = Resources.OpenFileDialog__title_Select_File_To_Open;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets a value indicating whether a file dialog displays a warning if the user specifies a filename that does not exist. The default is false.
        /// </summary>
        public bool CheckFileExists { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies whether warnings are displayed if the user types invalid paths and filenames. The default is true.
        /// </summary>
        public Boolean CheckPathExists {
            get {
                return _checkPathExists;
            }
            set {
                _checkPathExists = value;
            }
        }

        /// <summary>
        /// Gets a String that is the full path of the file selected in the file dialog. The default is Empty. 
        /// </summary>
        public String FileName {
            get {
                return _fileName;
            }
        }

        /// <summary>
        /// Gets an array of String that contains one filename for each selected file. The default is an array with a single item whose value is Empty. 
        /// </summary>
        public String[] FileNames {
            get {
                return _fileNames;
            }
        }

        /// <summary>
        /// Gets or sets the current filename filter String, which determines the choices that appear in the "Files of type" box at the bottom of a file dialog.  Filter String consists of one or more pairs of String values, all separated by the pipe symbol ( | ).  Example : "Saved Report PDF Files(*.pdf)|*.pdf|All Files (*.*)|*.*"
        /// </summary>
        public String Filter {
            get {
                return _filter;
            }
            set {
                _filter = value;
            }
        }

        /// <summary>
        /// Gets or sets the initial directory displayed by the file dialog box. 
        /// </summary>
        public String InitialDirectory {
            get {
                return _initialDirectory;
            }
            set {
                _initialDirectory = value;
            }
        }

        /// <summary>
        /// Gets or sets an option indicating whether OpenFileDialog allows users to select multiple files. The default value is false.
        /// </summary>
        public bool MultiSelect { get; set; }

        /// <summary>
        /// Gets or sets the text that appears in the title bar of a file dialog. The default value is "Select File To Open"
        /// </summary>
        public String Title {
            get {
                return _title;
            }
            set {
                _title = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog accepts only valid Win32 filenames. The default values is true.
        /// </summary>
        public Boolean ValidateNames {
            get {
                return _validateNames;
            }
            set {
                _validateNames = value;
            }
        }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileDialog"/> class.
        /// </summary>
        public OpenFileDialog() { }

        #endregion

        #region  Methods

        /// <summary>
        /// Displays the open file dialog for the current operating system.  Returns true if the user selected one ore more file names.  Read the FileName or FileNames property for the selected file(s).
        /// </summary>
        public Boolean ShowDialog() {

            using(var dialogOpenFile = new VistaOpenFileDialog()) {
                dialogOpenFile.CheckFileExists = this.CheckFileExists;
                dialogOpenFile.CheckPathExists = this.CheckPathExists;
                dialogOpenFile.Filter = this.Filter;
                dialogOpenFile.InitialDirectory = this.InitialDirectory;
                dialogOpenFile.Multiselect = this.MultiSelect;
                dialogOpenFile.Title = this.Title;
                dialogOpenFile.ValidateNames = this.ValidateNames;

                DialogResult result = dialogOpenFile.ShowDialog();

                if(result == DialogResult.OK) {
                    _fileName = dialogOpenFile.FileName;

                    //user selected one or more files
                    if(this.MultiSelect) {
                        _fileNames = dialogOpenFile.FileNames;
                    }

                    return true;

                }
                //user cancelled
                return false;
            }
        }

        #endregion
    }
}
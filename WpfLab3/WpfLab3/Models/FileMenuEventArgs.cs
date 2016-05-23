﻿using System;

namespace WpfLab3.Models
{
	public delegate void FileMenuHandler(object sender, FileMenuEventArgs args);
	public class FileMenuEventArgs : EventArgs
	{
		public FileMenuEventArgs()
			: this(string.Empty)
		{

		}
		public FileMenuEventArgs(string commandName)
		{
			CommandName = CommandName;
		}
		public string CommandName
		{
			get;
			set;
		}
	}
}

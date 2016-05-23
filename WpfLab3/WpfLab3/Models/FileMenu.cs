﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfLab3.Infrastructure;
namespace WpfLab3.Models
{
	public class FileMenu : ModelBase
	{
		private ICommand _command;
		private bool _isEnabled = true;
		private ObservableCollection<FileMenu> _items;
		public string Header { get; set; }

		public string CommandParameter { get; set; }

		public ICommand Command
		{
			get
			{
				return (_command = _command ??
				                   new DelegateCommand<string>(OnMenuItemClick, (x)=> IsEnabled));
			}
		}

		public bool IsEnabled
		{
			get { return _isEnabled; }
			set
			{
				_isEnabled = value;
				OnPropertyChanged("IsEnabled");
			}
		}

		public ObservableCollection<FileMenu> Items
		{
			get
			{
				return (_items = _items ??
				                 new ObservableCollection<FileMenu>());
			}
		}

		public bool HasChildren
		{
			get { return Items.Count > 0; }
		}

		public event FileMenuHandler FileMenuClick;

		public virtual void OnFileMenuClick(object sender, FileMenuEventArgs args)
		{
			if (FileMenuClick != null)
			{
				FileMenuClick(sender, args);
			}
		}

		public virtual void OnMenuItemClick(string parameter)
		{
			OnFileMenuClick(this, new FileMenuEventArgs
			                      	{
			                      		CommandName = parameter
			                      	});
		}
	}
}
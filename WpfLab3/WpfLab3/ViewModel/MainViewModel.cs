using System.Collections.ObjectModel;
using WpfLab3.Models;

namespace WpfLab3.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private ObservableCollection<FileMenu> _menuItems;
		private string _selectedMenuItem;

		public MainViewModel()
		{
			FillMenuItems();
		}

		public ObservableCollection<FileMenu> MenuItems
		{
			get
			{
				return (_menuItems = _menuItems ??
				                     new ObservableCollection<FileMenu>());
			}
		}

		public string SelectedMenuItem
		{
			get { return _selectedMenuItem; }
			set
			{
				_selectedMenuItem = value;
				OnPropertyChanged("SelectedMenuItem");
			}
		}

		private void FillMenuItems()
		{
			//fill with dummy data.
			var file = new FileMenu {Header = "File", CommandParameter = "file"};
			file.Items.Add(new FileMenu {Header = "New/open project", CommandParameter = "newopenproject"});
			file.Items.Add(new FileMenu {Header = "Save", CommandParameter = "save"});
			var menu1 = new FileMenu {Header = "Menu1", CommandParameter = "menu1"};
			var menu2 = new FileMenu { Header = "Menu2", CommandParameter = "menu2" };
			menu1.Items.Add(menu2);
			file.Items.Add(menu1);
			file.Items.Add(new FileMenu {Header = "Exit", CommandParameter = "exit"});

			var help = new FileMenu {Header = "Help", CommandParameter = "help"};
			help.Items.Add(new FileMenu {Header = "About us", CommandParameter = "about"});

			MenuItems.Add(file);
			MenuItems.Add(help);

			foreach (var menuItem in MenuItems)
			{
				RegisterMenuItemsEventHandler(menuItem);
			}
		}

		private void RegisterMenuItemsEventHandler(FileMenu root)
		{
			foreach (var item in root.Items)
			{
				item.FileMenuClick += MenuItemFileMenuClick;
				if (item.HasChildren)
				{
					RegisterMenuItemsEventHandler(item);
				}
			}
		}

		public virtual void MenuItemFileMenuClick(object sender, FileMenuEventArgs args)
		{
			switch (args.CommandName)
			{
				case "newopenproject":
					SelectedMenuItem = "You selected : New/Open project";
					break;
				case "exit":
					SelectedMenuItem = "You selected : Exit";
					break;
				case "save":
					SelectedMenuItem = "You selected : Save";
					break;
				case "about":
					SelectedMenuItem = "You selected : about us";
					break;
			}
		}
	}
}
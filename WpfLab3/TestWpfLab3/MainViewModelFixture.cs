using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfLab3.ViewModel;

namespace TestWpfLab3
{
	[TestClass]
	public class MainViewModelFixture
	{
		private MainViewModel _viewmodel;

		[TestInitialize]
		public void MyTestInitialize()
		{
			_viewmodel = new MainViewModel();
		}

		[TestCleanup]
		public void MyTestCleanup()
		{
			_viewmodel = null;
		}

		[TestMethod]
		public void MainViewModelMenuItemsCannotBeEmpty()
		{
			int count = _viewmodel.MenuItems.Count;
			Assert.IsTrue(count > 0);
		}

		[TestMethod]
		public void MainViewModelProjectMenuItemClicked()
		{
			const string value = "newopenproject";
			string newValue = null;
			var items = _viewmodel.MenuItems;
			var item = items[0];
			item.FileMenuClick += (sender, args) => { newValue = args.CommandName; };
			item.Command.Execute(value);

			Assert.AreEqual(value, newValue);
		}
	}
}
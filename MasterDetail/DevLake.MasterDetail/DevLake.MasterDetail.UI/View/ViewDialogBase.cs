using System;
using System.Windows;
using DevLake.MasterDetail.UI.Interface;

namespace DevLake.MasterDetail.UI.View
{
	class ViewDialogBase<TView> : IModalDialog
		where TView : Window, new()
	{
		protected TView view;

		public void BindViewModel<TViewModel>(TViewModel viewModel)
		{
			GetDialog().DataContext = viewModel;
		}

		public void ShowDialog()
		{
			GetDialog().ShowDialog();
		}

		public void Close()
		{
			GetDialog().Close();
		}

		private TView GetDialog()
		{
			if (view == null)
			{
				//create the view if the view does not exist
				view = new TView();
				view.Closed += new EventHandler(view_Closed);
			}
			return view;
		}

		void view_Closed(object sender, EventArgs e)
		{
			view = null;
		}

	}
}

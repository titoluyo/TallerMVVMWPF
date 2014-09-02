using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Wpf.Common.Infrastructure;
using Wpf.Mvvm.Model;

namespace Wpf.Mvvm.WiringViewViewModel {

    public class WiredByResourceLookupViewModel : ObservableObject {

        ObservableCollection<ViewModel> _viewModels;
        ICommand _deleteCommand;

        public ICommand DeleteCommand {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand<ViewModel>(DeleteExecute)); }
        }

        public ObservableCollection<ViewModel> ViewModels {
            get { return _viewModels; }
            private set {
                _viewModels = value;
                RaisePropertyChanged("ViewModels");
            }
        }

        public WiredByResourceLookupViewModel() {
            var list = new List<ViewModel> {
                new ViewModel{Person = new Person {LastName = "Abercrombie", FirstName = "Kim"}},
                new ViewModel {Person = new Person {LastName = "Hanif", FirstName = "Kerim"}},
                new ViewModel {Person = new Person {LastName = "Perham", FirstName = "Tom"}}
                };
            this.ViewModels = new ObservableCollection<ViewModel>(list);
        }

        void DeleteExecute(ViewModel viewModel) {
            _viewModels.Remove(viewModel);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;


namespace Labb3_NET22.ViewModels
{
    class MainViewModel : ObservableObject
    {
        private readonly NavigationManager _navigationManager;


        public ObservableObject CurrentViewModel => _navigationManager.CurrentViewModel;

        public MainViewModel(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _navigationManager.CurrentViewModelChanged += CurrentViewModelChanged;
        }

        private void CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}

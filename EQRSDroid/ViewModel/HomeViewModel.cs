using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Command;
using EQRSDroid.Utilities;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace EQRSDroid.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {

        public HomeViewModel(EmergenciesConfigReader configReader,
            INavigationService navigationService)
        {
            _configReader = configReader;
            _navigationService = navigationService;
        }
        private RelayCommand _reportCrimeCommand;
        private EmergenciesConfigReader _configReader;
        private INavigationService _navigationService;

        public RelayCommand ReportCrimeCommand
        {
            get
            {
                return _reportCrimeCommand ?? (_reportCrimeCommand = new RelayCommand(() =>
                {
                    _navigationService.NavigateTo(ViewModelLocator.EmergencyListPageKey, "CRIM");
                }));
            }
        }
    }
}
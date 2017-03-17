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

        private RelayCommand<string> _reportEmergencyCommand;
        private EmergenciesConfigReader _configReader;
        private INavigationService _navigationService;
        private RelayCommand _showFirstAidTipsCommand;

        public RelayCommand<string> ReportEmergencyCommand
        {
            get
            {
                return _reportEmergencyCommand ?? (_reportEmergencyCommand = new RelayCommand<string>((typ) =>
                {
                    _navigationService.NavigateTo(ViewModelLocator.EmergencyListPageKey, typ.ToString().ToUpper());
                }));
            }
        }

        public RelayCommand ShowFirstAidTipsCommand
        {
            get
            {
                return _showFirstAidTipsCommand ?? (_showFirstAidTipsCommand = new RelayCommand(() =>
                {
                    _navigationService.NavigateTo(ViewModelLocator.TipsPageKey);
                }));
            }
        }
    }
}
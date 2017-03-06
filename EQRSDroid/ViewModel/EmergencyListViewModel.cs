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
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using EQRSDroid.Utilities;
using System.Collections.ObjectModel;

namespace EQRSDroid.ViewModel
{
    public class EmergencyListViewModel : ViewModelBase
    {
        private EmergenciesConfigReader _configReader;
        private INavigationService _navigationService;

        public ObservableCollection<string> Emergencies { get; set; }

        public EmergencyListViewModel(INavigationService navigationService,
            EmergenciesConfigReader configReader)
        {
            _navigationService = navigationService;
            _configReader = configReader;

            Emergencies = new ObservableCollection<string>();
        }

        public void LoadEmergencies(string type)
        {
            Emergencies.Clear();
            var es = _configReader.Read(type);
            foreach (var item in es)
            {
                Emergencies.Add(item);
            }
        }
    }
}
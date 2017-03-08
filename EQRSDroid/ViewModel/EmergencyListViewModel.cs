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
using GalaSoft.MvvmLight.Command;
using Plugin.Geolocator;
using Android.Util;
using Plugin.Messaging;
using Android.Telephony;
using static Android.Content.Res.Resources;

namespace EQRSDroid.ViewModel
{
    public class EmergencyListViewModel : ViewModelBase
    {
        public const string serverPhone = "+639499394644";

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
            responderCode = type;
            Emergencies.Clear();
            var es = _configReader.Read(type);
            if (es != null)
            {
                foreach (var item in es)
                {
                    Emergencies.Add(item);
                }
            }
        }

        private RelayCommand<IList<string>> _reportNowCommand;
        private string responderCode;

        /// <summary>
        /// Gets the ReportNowCommand.
        /// </summary>
        public RelayCommand<IList<string>> ReportNowCommand
        {
            get
            {
                return _reportNowCommand
                    ?? (_reportNowCommand = new RelayCommand<IList<string>>(
                    async (egs) =>
                    {
                        try
                        {
                            var locator = CrossGeolocator.Current;
                            var position = await locator.GetPositionAsync(timeoutMilliseconds: 960000);

                            var str = string.Format("{0}::{1}::{2}::{3}", responderCode.ToUpper(), string.Join(", ", egs),
                                position.Latitude, position.Longitude);

                            Log.Debug("eqrs-log", "SMS:" + str);

                            SmsManager.Default.SendTextMessage(serverPhone, null, str, null, null);
                            Log.Debug("eqrs-log", "Message was successfully sent?");
                        }
                        catch (Exception ex)
                        {
                            Log.Debug("eqrs-log", ex.Message);
                        }
                    }));
            }
        }
    }
}
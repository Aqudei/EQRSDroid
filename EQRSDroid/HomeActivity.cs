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
using GalaSoft.MvvmLight.Views;
using EQRSDroid.ViewModel;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Ioc;
using EQRSDroid.Utilities;
using Android.Locations;

namespace EQRSDroid
{
    [Activity(Label = "HomeActivity", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeActivity : ActivityBase
    {
        // Keep track of bindings to avoid premature garbage collection
        private readonly List<Binding> _bindings = new List<Binding>();



        public Button FireButton
        {
            get
            {
                return FindViewById<Button>(Resource.Id.buttonFire);
            }
        }

        public Button MedsButton
        {
            get
            {
                return FindViewById<Button>(Resource.Id.buttonMedical);
            }
        }

        public Button CrimeButton
        {
            get
            {
                return FindViewById<Button>(Resource.Id.buttonCrime);
            }
        }

        public Button NaturalDisasterButton
        {
            get
            {
                return FindViewById<Button>(Resource.Id.buttonNatDisaster);
            }
        }

        private HomeViewModel Vm
        {
            get
            {
                return App.Locator.Home;
            }
        }



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Home);

            LocationManager locationManager = GetSystemService(Context.LocationService) as LocationManager;
            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider))
            {

            }
            else
            {
                var intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                StartActivity(intent);
            }

            SimpleIoc.Default.Register(() => new EmergenciesConfigReader(Assets.Open("emergencies.json")));

            FireButton.Click += (s, e) =>
            {
                Vm.ReportEmergencyCommand.Execute(FireButton.Tag.ToString());
            };

            MedsButton.Click += (s, e) =>
            {
                Vm.ReportEmergencyCommand.Execute(MedsButton.Tag.ToString());
            };

            CrimeButton.Click += (s, e) =>
            {
                Vm.ReportEmergencyCommand.Execute(CrimeButton.Tag.ToString());
            };

            NaturalDisasterButton.Click += (s, e) =>
            {
                Vm.ReportEmergencyCommand.Execute(NaturalDisasterButton.Tag.ToString());
            };
        }
    }
}
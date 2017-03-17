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
    [Activity(Label = "EQRS", Theme = "@android:style/Theme.Light", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeActivity : ActivityBase
    {
        // Keep track of bindings to avoid premature garbage collection
        private readonly List<Binding> _bindings = new List<Binding>();

        public ImageButton FireButton
        {
            get
            {
                return FindViewById<ImageButton>(Resource.Id.buttonFire);
            }
        }



        public ImageButton MedsButton
        {
            get
            {
                return FindViewById<ImageButton>(Resource.Id.buttonMedical);
            }
        }

        public ImageButton CrimeButton
        {
            get
            {
                return FindViewById<ImageButton>(Resource.Id.buttonCrime);
            }
        }

        public ImageButton NaturalDisasterButton
        {
            get
            {
                return FindViewById<ImageButton>(Resource.Id.buttonNatDisaster);
            }
        }

        private HomeViewModel Vm
        {
            get
            {
                return App.Locator.Home;
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            LocationManager locationManager = GetSystemService(Context.LocationService) as LocationManager;
            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
                var intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                StartActivity(intent);
            }
        }

        public Button FirstAidButton
        {
            get
            {
                return FindViewById<Button>(Resource.Id.buttonFAid);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Home);

            if (SimpleIoc.Default.IsRegistered<EmergenciesConfigReader>() == false)
                SimpleIoc.Default.Register(() => new EmergenciesConfigReader(Assets.Open("emergencies.json")));

            FirstAidButton.SetCommand("Click", Vm.ShowFirstAidTipsCommand);


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

            var about = FindViewById<Button>(Resource.Id.buttonAbout);
            about.SetCommand("Click", Vm.AboutCommand);
        }
    }
}
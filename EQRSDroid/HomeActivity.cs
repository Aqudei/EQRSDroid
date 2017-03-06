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

namespace EQRSDroid
{
    [Activity(Label = "HomeActivity", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeActivity : ActivityBase
    {
        // Keep track of bindings to avoid premature garbage collection
        private readonly List<Binding> _bindings = new List<Binding>();

        Button fireBUtton, medsButton, crimeButton, disasterButton;

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

            SimpleIoc.Default.Register<EmergenciesConfigReader>(() => new EmergenciesConfigReader(Assets.Open("emergencies.json")));

            fireBUtton = FindViewById<Button>(Resource.Id.buttonFire);
            medsButton = FindViewById<Button>(Resource.Id.buttonMedical);

            crimeButton = FindViewById<Button>(Resource.Id.buttonCrime);
            crimeButton.SetCommand("Click", Vm.ReportCrimeCommand);

            disasterButton = FindViewById<Button>(Resource.Id.buttonNatDisaster);
        }
    }
}
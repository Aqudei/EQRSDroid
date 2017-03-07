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
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Helpers;
using System.Diagnostics;
using Android.Util;

namespace EQRSDroid
{
    [Activity(Label = "EmergencyListActivity")]
    public class EmergencyListActivity : ActivityBase
    {

        private ListView emergencies;
        public Button ReportButton
        {
            get
            {
                return FindViewById<Button>(Resource.Id.buttonReport);
            }
        }

        public ViewModel.EmergencyListViewModel Vm
        {
            get
            {
                return App.Locator.EmergencyList;
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EmergencyList);

            var nav = (NavigationService)SimpleIoc.Default.GetInstance<INavigationService>();
            Vm.LoadEmergencies(nav.GetAndRemoveParameter<string>(Intent));

            emergencies = FindViewById<ListView>(Resource.Id.listViewEmergencies);

            emergencies.Adapter = Vm.Emergencies.GetAdapter(EmergencyAdapter);

            ReportButton.Click += (s, e) =>
            {
                GetCheckedItemsAndPasstoVm();
            };
        }

        private void GetCheckedItemsAndPasstoVm()
        {
            var lst = new List<string>();
            for (int i = 0; i < emergencies.Count; i++)
            {
                var view = emergencies.GetChildAt(i);
                var cb = view.FindViewById<CheckBox>(Resource.Id.checkBox1);
                if (cb.Checked)
                {
                    lst.Add(cb.Text);
                }
            }
            Vm.ReportNowCommand.Execute(lst);
        }

        private View EmergencyAdapter(int position, string emergency, View convertView)
        {
            convertView = LayoutInflater.Inflate(Resource.Layout.EmergencyDetail, null);
            var cb = convertView.FindViewById<CheckBox>(Resource.Id.checkBox1);
            cb.Text = emergency;
            return convertView;
        }
    }
}
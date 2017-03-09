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
using GalaSoft.MvvmLight.Messaging;

namespace EQRSDroid
{
    [Activity(Label = "EmergencyListActivity")]
    public class EmergencyListActivity : ActivityBase
    {
        private readonly List<Binding> _bindings = new List<Binding>();

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

        public ProgressBar loading
        {
            get
            {
                return FindViewById<ProgressBar>(Resource.Id.progressBar1);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EmergencyList);

            var nav = (NavigationService)SimpleIoc.Default.GetInstance<INavigationService>();
            Vm.LoadEmergencies(nav.GetAndRemoveParameter<string>(Intent));

            emergencies = FindViewById<ListView>(Resource.Id.listViewEmergencies);

            _bindings.Add(this.SetBinding(() => Vm.IsBusy).WhenSourceChanges(() =>
            {
                if (Vm.IsBusy)
                {
                    loading.Visibility = ViewStates.Visible;
                }
                else
                {
                    loading.Visibility = ViewStates.Invisible;
                }
            }));

            emergencies.Adapter = Vm.Emergencies.GetAdapter(EmergencyAdapter);

            ReportButton.Click += (s, e) =>
            {
                GetCheckedItemsAndPasstoVm();
            };

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<NotificationMessage>(this, HandleDoneSending);
        }

        private void HandleDoneSending(NotificationMessage obj)
        {
            if (obj.Notification == "done-sending")
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Done sending");
                alert.SetMessage("Please wait for your rescue.");                
                Dialog dialog = alert.Create();
                dialog.Show();
            }
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
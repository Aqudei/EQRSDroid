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
using GalaSoft.MvvmLight.Ioc;
using EQRSDroid.Utilities;
using GalaSoft.MvvmLight.Helpers;
using EQRSDroid.Model;
using Android.Util;

namespace EQRSDroid
{
    [Activity(Label = "TipsActivity")]
    public class TipsActivity : ActivityBase, AdapterView.IOnItemClickListener
    {

        private TipsViewModel Vm
        {
            get
            {
                return App.Locator.Tips;
            }
        }



        public SearchView TipsSearch
        {
            get { return FindViewById<SearchView>(Resource.Id.searchViewTips); }

        }

        public ListView TipsListView
        {
            get { return FindViewById<ListView>(Resource.Id.listViewTips); }

        }



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Tips);

            if (SimpleIoc.Default.IsRegistered<FirstAidConfigReader>() == false)
                SimpleIoc.Default.Register(() => new FirstAidConfigReader(Assets.Open("firstaid.json")));

            TipsListView.Adapter = Vm.Tips.GetAdapter(TipsListAdapter);

            TipsSearch.QueryTextChange += (s, e) =>
            {
                Vm.DoSearch.Execute(e.NewText);
                e.Handled = true;
            };

            TipsListView.OnItemClickListener = this;
        }

        private View TipsListAdapter(int pos, Tip tip, View convertView)
        {
            convertView = LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            var cb = convertView.FindViewById<TextView>(Android.Resource.Id.Text1);
            cb.Text = tip.Title;
            return convertView;
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            Log.Debug("eqrs", "You have clicked" + Vm.Tips[position].Title);
            Vm.ItemSelectedCommand.Execute(position);
        }
    }
}
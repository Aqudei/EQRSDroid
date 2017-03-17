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
using Microsoft.Practices.ServiceLocation;
using EQRSDroid.Model;

namespace EQRSDroid
{
    [Activity(Label = "TipDetailActivity")]
    public class TipDetailActivity : ActivityBase
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TipDetail);

            var nav = (NavigationService)ServiceLocator.Current.GetInstance<INavigationService>();
            var p = nav.GetAndRemoveParameter<Tip>(Intent);

            var _titleView = FindViewById<TextView>(Resource.Id.textViewTitle);
            var _content = FindViewById<TextView>(Resource.Id.textViewContent);

            if (p != null)
            {
                _titleView.Text = p.Title;
                _content.Text = p.Content;
            }
        }
    }
}
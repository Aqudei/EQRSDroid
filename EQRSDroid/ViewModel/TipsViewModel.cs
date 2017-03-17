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
using EQRSDroid.Utilities;
using System.Collections.ObjectModel;
using EQRSDroid.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace EQRSDroid.ViewModel
{
    public class TipsViewModel : ViewModelBase
    {
        private FirstAidConfigReader _reader;

        private ObservableCollection<Tip> _tips;

        private string _filterText;

        public ObservableCollection<Tip> Tips
        {
            get { return _tips; }
            set { _tips = value; }
        }


        public TipsViewModel(FirstAidConfigReader reader,
            INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this._reader = reader;
            Tips = new ObservableCollection<Model.Tip>(reader.Tips);
        }

        private RelayCommand<string> _doSearch;

        public RelayCommand<string> DoSearch
        {
            get
            {
                return _doSearch ?? (_doSearch = new RelayCommand<string>(t =>
                {
                    Tips.Clear();
                    var tips = _reader.GetTipsByTitle(t);
                    foreach (var tip in tips)
                    {
                        Tips.Add(tip);
                    }
                }));
            }
        }

        private RelayCommand<int> _itemSelectedCommand;
        private INavigationService navigationService;

        public RelayCommand<int> ItemSelectedCommand
        {
            get
            {
                return _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<int>(t =>
                {
                    var tip = Tips[t];
                    navigationService.NavigateTo(ViewModelLocator.TipDetailKey, tip);
                }));
            }
        }

    }
}
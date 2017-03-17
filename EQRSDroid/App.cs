using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using EQRSDroid.ViewModel;

namespace EQRSDroid
{
    public static class App
    {
        private static ViewModelLocator _locator;

        public static ViewModelLocator Locator
        {
            get
            {
                if (_locator == null)
                {
                    // Initialize the MVVM Light DispatcherHelper.
                    // This needs to be called on the UI thread.
                    DispatcherHelper.Initialize();

                    // Configure and register the MVVM Light NavigationService
                    var nav = new NavigationService();
                    SimpleIoc.Default.Register<INavigationService>(() => nav);
                    nav.Configure(ViewModelLocator.SecondPageKey, typeof(SecondActivity));
                    nav.Configure(ViewModelLocator.EmergencyListPageKey, typeof(EmergencyListActivity));
                    nav.Configure(ViewModelLocator.TipsPageKey, typeof(TipsActivity));
                    nav.Configure(ViewModelLocator.TipDetailKey, typeof(TipDetailActivity));
                    nav.Configure(ViewModelLocator.AboutPageKey, typeof(AboutActivity));

                    // Register the MVVM Light DialogService
                    SimpleIoc.Default.Register<IDialogService, DialogService>();

                    _locator = new ViewModelLocator();
                }

                return _locator;
            }
        }
    }
}
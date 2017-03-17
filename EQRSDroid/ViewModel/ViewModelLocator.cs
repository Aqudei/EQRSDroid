using System.Diagnostics.CodeAnalysis;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using EQRSDroid.Design;
using EQRSDroid.Model;

namespace EQRSDroid.ViewModel
{
    /// <summary>
    /// This class contains static references to the most relevant view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// The key used by the NavigationService to go to the second page.
        /// </summary>
        public const string SecondPageKey = "SecondPage";
        public const string EmergencyListPageKey = "EmergencyListPage";
        public const string TipsPageKey = "TipsPage";
        public const string TipDetailKey = "TipDetail";

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public EmergencyListViewModel EmergencyList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EmergencyListViewModel>();
            }
        }

        public HomeViewModel Home
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomeViewModel>();
            }
        }

        public TipsViewModel Tips
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TipsViewModel>();
            }
        }

        /// <summary>
        /// This property can be used to force the application to run with design time data.
        /// </summary>
        public static bool UseDesignTimeData
        {
            get
            {
                return false;
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (!ViewModelBase.IsInDesignModeStatic
                && !UseDesignTimeData)
            {
                // Use this service in production.
                SimpleIoc.Default.Register<IDataService, DataService>();
            }
            else
            {
                // Use this service in Blend or when forcing the use of design time data.
                SimpleIoc.Default.Register<IDataService, DesignDataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<EmergencyListViewModel>();
            SimpleIoc.Default.Register<TipsViewModel>();
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}

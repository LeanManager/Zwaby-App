using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Zwaby.Views;

namespace Zwaby.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            BookCleaningCommand = new Command(async () => await OnBookCleaning());
            BookingDetailsCommand = new Command(async () => await OnBookingDetails());
            ProfileCommand = new Command(async () => await OnProfile());
        }

        public ICommand BookCleaningCommand { private set; get; }
        public ICommand BookingDetailsCommand { private set; get; }
        public ICommand ProfileCommand { private set; get; }

        private async Task OnBookCleaning()
        {
            HockeyApp.MetricsManager.TrackEvent("OnBookCleaningClicked",
                                                new Dictionary<string, string>
                                                {
                                                    {"Time", DateTime.UtcNow.ToString() }
                                                },
                                                new Dictionary<string, double>
                                                {
                                                    {"Value", 2.5 }
                                                });

            if (!string.IsNullOrWhiteSpace(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServicePrice) ||
                !string.IsNullOrWhiteSpace(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceApproximateDuration))
            {
                await Application.Current.MainPage.DisplayAlert("", "Service must be completed before booking again. " +
                                   "Alternatively, you may cancel your current booking. " +
                                   "Please call 469-995-6899 if you have any questions.", "OK");
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new ServiceLocationPage());
            }
        }

        private async Task OnBookingDetails()
        {
            HockeyApp.MetricsManager.TrackEvent("OnBookingDetailsClicked",
                                                new Dictionary<string, string>
                                                {
                                                    {"Time", DateTime.UtcNow.ToString() }
                                                },
                                                new Dictionary<string, double>
                                                {
                                                    {"Value", 2.5 }
                                                });

            if (string.IsNullOrWhiteSpace(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceDate) ||
                string.IsNullOrWhiteSpace(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceTime) ||
                string.IsNullOrWhiteSpace(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServicePrice) ||
                string.IsNullOrWhiteSpace(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceApproximateDuration) ||
                string.IsNullOrWhiteSpace(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceStreet) ||
                string.IsNullOrWhiteSpace(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceCity) ||
                string.IsNullOrWhiteSpace(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceState) ||
                string.IsNullOrWhiteSpace(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceZipCode))
            {
                await Application.Current.MainPage.DisplayAlert("", "Please complete a booking first.", "OK");
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new BookingDetailsPage());
            }
        }

        private async Task OnProfile()
        {
            HockeyApp.MetricsManager.TrackEvent("OnProfileClicked",
                                                new Dictionary<string, string>
                                                {
                                                    {"Time", DateTime.UtcNow.ToString() }
                                                },
                                                new Dictionary<string, double>
                                                {
                                                    {"Value", 2.5 }
                                                });

            await Application.Current.MainPage.Navigation.PushAsync(new ProfilePage());
        }
    }
}

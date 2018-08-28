﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Xamarin.Forms;
using XamarinForms.SQLite.SQLite;
using Zwaby.Models;
using Zwaby.Services;
using Zwaby.Utilities;
using Zwaby.ViewModels;

namespace Zwaby.Views
{
    public partial class PaymentPage : ContentPage
    {
        string totalBookingPrice, roundedDuration;

        private BookingsManager manager;

        private PricingManager pricingManager;

        private EmailService emailService;

        public PaymentPage()
        {
            InitializeComponent();

            //this.BackgroundColor = Color.FromRgb(0, 200, 255);
            this.BackgroundColor = Color.White;

            paymentLabel.FontFamily = Device.RuntimePlatform == Device.iOS ? "Cabin-Bold" : "Cabin-Bold.ttf#Cabin-Bold";

            if (Device.RuntimePlatform == Device.Android)
            {
                NavigationPage.SetHasBackButton(this, false);
            }

            ExceptionModel.ExceptionModelInstance = new ExceptionModel();

            pricingManager = new PricingManager();

            manager = new BookingsManager();

            emailService = new EmailService();

            var viewModel = new PaymentPageViewModel(new StripeRepository(), new APIRepository());

            this.BindingContext = viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            this.IsBusy = true;

            if (true)
            {
                try
                {
                    await AssignPriceAndDuration();
                }
                catch (Exception ex)
                {
                    string exception = ex.Message;
                }
                finally
                {
                    this.IsBusy = false;
                }
            }
            else
            {
                this.IsBusy = false;
                await DisplayAlert("Network connection not found", "Please try again with an active network connection.", "OK");
                await Navigation.PopAsync();
            }
        }

        private async Task AssignPriceAndDuration()
        {
            //var values = await pricingManager.GeneratePriceAndDuration(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceBedrooms,
            //BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceBathrooms,
            //BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceResidence,
            //BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceType);

            var duration = pricingManager.GetDuration(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceBedrooms,
                                                      BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceBathrooms,
                                                      BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceResidence,
                                                      BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceType);

            var price = pricingManager.CalculatePrice(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceBedrooms,
                                                      BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceBathrooms,
                                                      BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceResidence,
                                                      BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceType);

            double finalDuration = Math.Round(duration, 1);

            string serviceDuration = finalDuration.ToString();

            string finalPrice = price.ToString();

            var list = new List<string>();
            list.Add(serviceDuration);
            list.Add(finalPrice);

            roundedDuration = list[0];
            totalBookingPrice = list[1];
            durationLabel.Text = $"{durationLabel.Text} {roundedDuration} hours";
            priceLabel.Text = $"{priceLabel.Text} $ {totalBookingPrice}";
            //approximateDuration.Text = roundedDuration + " hours";
            //totalPrice.Text = "$ " + totalBookingPrice;
        }

        private PaymentPageViewModel UpdatePaymentPageViewModel()
        {
            var viewModel = (PaymentPageViewModel)this.BindingContext;

            viewModel.ServiceDuration = roundedDuration + " hours";
            // Stripe charges in cents
            var doublePrice = Double.Parse(totalBookingPrice) * 100;
            var servicePrice = (int)doublePrice;
            viewModel.ServicePrice = servicePrice;

            viewModel.CreditCardName = cardName.Text;
            viewModel.CreditCardNumber = cardNumber.Text;
            viewModel.ExpirationDate = expirationDate.Text;
            viewModel.SecurityCode = securityCode.Text;
            viewModel.BillingZipCode = billingZipCode.Text;

            return viewModel;
        }

        async void OnTermsAndConditionsClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new TermsAndConditionsPage());
        }

        async void OnFinishBookingClicked(object sender, System.EventArgs e)
        {
            HockeyApp.MetricsManager.TrackEvent("OnFinishBookingClicked",
                                                new Dictionary<string, string>
                                                {
                                                    {"Time", DateTime.UtcNow.ToString() }
                                                },
                                                new Dictionary<string, double>
                                                {
                                                    {"Value", 2.5 }
                                                });

            if (cardNumber.Text == null || expirationDate.Text == null || securityCode.Text == null)
            {
                await DisplayAlert("", "Please enter valid credit card information.", "OK");
            }
            else if ((cardNumber.Text.Length < 15 || string.IsNullOrWhiteSpace(cardNumber.Text))
                     || (!expirationDate.Text.Contains("/") || string.IsNullOrWhiteSpace(expirationDate.Text) || expirationDate.Text.Length > 5)
                     || (securityCode.Text.Length < 3 || string.IsNullOrWhiteSpace(securityCode.Text) || securityCode.Text.Length > 4))
            {
                await DisplayAlert("", "Card number must have at least 15 digits.\n"
                                       + "Expiration date must be in the sample format 07/21\n"
                                       + "Security code must have 3 or 4 digits.", "OK");
            }
            else
            {
                if (TermsAndConditions.TermsAndConditionsInstance.IsAcknowledged == true)
                {
                    finishBookingButton.IsEnabled = false;
                    this.IsBusy = true;

                    var viewModel = UpdatePaymentPageViewModel();

                    if (CrossConnectivity.Current.IsConnected)
                    {
                        await viewModel.ProcessPayment();

                        if (string.IsNullOrWhiteSpace(ExceptionModel.ExceptionModelInstance.PaymentError))
                        {
                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceApproximateDuration = roundedDuration + " hours";
                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServicePrice = totalBookingPrice + " USD";

                            var sqLiteConnection = DependencyService.Get<ISQLite>().GetConnection();

                            var customer = sqLiteConnection.Table<Customer>().First();

                            try
                            {
                                await manager.AddNewBooking(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceDate,
                                                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceTime,
                                                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServicePrice,
                                                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceApproximateDuration,
                                                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceStreet,
                                                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceCity,
                                                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceState,
                                                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceZipCode,
                                                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceResidence,
                                                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceBedrooms,
                                                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceBathrooms,
                                                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceType,
                                                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceNotes,
                                                            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceDateTime,
                                                            customer.FirstName, customer.LastName, customer.EmailAddress, customer.PhoneNumber);

                                await emailService.SendEmail(BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceDate,
                                                             BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceTime,
                                                             BookingDetailsViewModel.BookingDetailsViewModelInstance.ServicePrice,
                                                             BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceApproximateDuration,
                                                             BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceStreet,
                                                             BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceCity,
                                                             BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceState,
                                                             BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceZipCode,
                                                             BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceResidence,
                                                             BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceBedrooms,
                                                             BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceBathrooms,
                                                             BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceType,
                                                             BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceNotes,
                                                             BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceDateTime,
                                                             customer.FirstName, customer.LastName, customer.EmailAddress, customer.PhoneNumber);
                            }
                            catch (Exception ex)
                            {
                                var exception = ex.Message;
                            }
                            finally
                            {
                                sqLiteConnection.Dispose();

                                HockeyApp.MetricsManager.TrackEvent("BookingCompletedSuccessfully",
                                                    new Dictionary<string, string>
                                                    {
                                                    {"Time", DateTime.UtcNow.ToString() }
                                                    },
                                                    new Dictionary<string, double>
                                                    {
                                                    {"Value", 2.5 }
                                                    });

                                this.IsBusy = false;
                                finishBookingButton.IsEnabled = true;

                                await DisplayAlert("Success!", "Your booking has been confirmed. You will find details in 'Booking Details'", "OK");

                                await Navigation.PushAsync(new MainPage());

                                MessagingCenter.Send(new DisableBackButtonMessage(), "DisableBackButton");
                            }
                        }
                        else
                        {
                            this.IsBusy = false;
                            finishBookingButton.IsEnabled = true;
                        }
                    }
                    else
                    {
                        finishBookingButton.IsEnabled = true;
                        this.IsBusy = false;

                        await DisplayAlert("Network connection not found", "Please try again with an active network connection.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("", "Please accept the Terms and Conditions before finishing your booking.", "OK");
                }
            }
        }
    }
}

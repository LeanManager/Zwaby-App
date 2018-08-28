using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Zwaby.CustomControls;
using Zwaby.Utilities;
using Zwaby.ViewModels;

namespace Zwaby.Views
{
    public partial class MainPage : ContentPage
    {
        GradientImage homeImage;
        CircleImage zwabyImage;
        CircleImage calendarImage;
        CircleImage bookingImage;
        CircleImage profileImage;
        Label bookServiceLabel;
        Label bookingDetailsLabel;
        Label profileLabel;
        StackLayout calendarStackLayout;
        StackLayout bookingStackLayout;
        StackLayout profileStackLayout;
        ScrollView scrollView;
        Grid grid;

        private bool isHardwareBackEnabled = true;

        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new MainPageViewModel();

            CreateGrid();
            CreateViews();
            AddViewsToLayouts();
            CreateContentLayout();

            NavigationPage.SetHasBackButton(this, false);

            MessagingCenter.Subscribe<DisableBackButtonMessage>(this, "DisableBackButton", message =>
            {
                isHardwareBackEnabled = false;
            });
        }

        private void CreateGrid()
        {
            grid = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                },
                RowSpacing = 0,
            };
        }

        private void CreateViews()
        {
            homeImage = new GradientImage
            {
                Source = ImageSource.FromResource("Zwaby.Images.child-1245893_1280.jpg",
                                                  typeof(MainPage).GetTypeInfo().Assembly),
                Aspect = Aspect.AspectFit,
                StartColor = Color.FromHex("#008c49"),
                EndColor = Color.FromHex("#00d2b4")
            };

            zwabyImage = new CircleImage
            {
                Source = ImageSource.FromResource("Zwaby.Images.zwabyhomefive.png",
                                                  typeof(MainPage).GetTypeInfo().Assembly),
                Aspect = Aspect.AspectFit,
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(20, 20, 20, 20),
                CornerRadius = 4.0
            };

            calendarImage = new CircleImage
            {
                Source = "calendaricon.png",
                Style = Resources["imageIcon"] as Style
            };
            bookingImage = new CircleImage
            {
                Source = "bookingicon.png",
                Style = Resources["imageIcon"] as Style
            };
            profileImage = new CircleImage
            {
                Source = "profileicon.png",
                Style = Resources["imageIcon"] as Style
            };

            bookServiceLabel = new Label
            {
                Text = "Book Cleaning",
                FontFamily = Device.RuntimePlatform == Device.iOS ? "Cabin-Bold" : "Cabin-Bold.ttf#Cabin-Bold"
            };
            bookingDetailsLabel = new Label
            {
                Text = "Booking Details",
                FontFamily = Device.RuntimePlatform == Device.iOS ? "Cabin-Bold" : "Cabin-Bold.ttf#Cabin-Bold"
            };
            profileLabel = new Label
            {
                Text = "Profile",
                FontFamily = Device.RuntimePlatform == Device.iOS ? "Cabin-Bold" : "Cabin-Bold.ttf#Cabin-Bold"
            };
        }

        private void AddViewsToLayouts()
        {
            var bookCleaningGesture = new TapGestureRecognizer();
            bookCleaningGesture.SetBinding(TapGestureRecognizer.CommandProperty, "BookCleaningCommand", BindingMode.TwoWay);

            calendarStackLayout = new StackLayout
            {
                BackgroundColor = (Color)Resources["bookColor"],
                Orientation = StackOrientation.Horizontal,
                Spacing = 30,
                Children =
                {
                    calendarImage,
                    bookServiceLabel
                },
                GestureRecognizers = 
                {
                    bookCleaningGesture
                }
            };

            var bookingGesture = new TapGestureRecognizer();
            bookingGesture.SetBinding(TapGestureRecognizer.CommandProperty, "BookingDetailsCommand", BindingMode.TwoWay);

            bookingStackLayout = new StackLayout
            {
                BackgroundColor = (Color)Resources["bookingColor"],
                Orientation = StackOrientation.Horizontal,
                Spacing = 30,
                Children = 
                {
                    bookingImage,
                    bookingDetailsLabel
                },
                GestureRecognizers = 
                {
                    bookingGesture
                }
            };

            var profileGesture = new TapGestureRecognizer();
            profileGesture.SetBinding(TapGestureRecognizer.CommandProperty, "ProfileCommand", BindingMode.TwoWay);

            profileStackLayout = new StackLayout
            {
                BackgroundColor = (Color)Resources["profileColor"],
                Orientation = StackOrientation.Horizontal,
                Spacing = 30,
                Children = 
                {
                    profileImage,
                    profileLabel
                },
                GestureRecognizers = 
                {
                    profileGesture
                }
            };

            grid.Children.Add(calendarStackLayout, 0, 0);
            grid.Children.Add(bookingStackLayout, 0, 1);
            grid.Children.Add(profileStackLayout, 0, 2);
        }

        private void CreateContentLayout()
        {
            scrollView = new ScrollView
            {
                Content = grid
            };

            this.Content = new AbsoluteLayout
            {
                Children =
                {
                    { homeImage, new Rectangle(0, 0, 1, 0.5), AbsoluteLayoutFlags.All },
                    { zwabyImage, new Rectangle(0, 0, 1, 0.5), AbsoluteLayoutFlags.All },
                    { scrollView, new Rectangle(0, 1, 1, 0.5), AbsoluteLayoutFlags.All }
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (DateTime.Now > BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceDateTime.AddHours(8))
            {
                ClearBookingDetailsViewModel();

                // TODO: ReviewPage - Navigation.PushModalAsync(new ReviewPage());
            }
        }

        private void ClearBookingDetailsViewModel()
        {
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceDate = "";
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceTime = "";
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServicePrice = "";
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceApproximateDuration = "";
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceStreet = "";
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceCity = "";
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceState = "";
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceZipCode = "";
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceResidence = "";
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceBedrooms = "";
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceBathrooms = "";
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceType = "";
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceNotes = "";
            BookingDetailsViewModel.BookingDetailsViewModelInstance.ServiceDateTime = DateTime.Now;
        }

        // Disable Android hardware Back button functionality on this page
        protected override bool OnBackButtonPressed()
        {
            if (isHardwareBackEnabled == true)
            {
                return base.OnBackButtonPressed();
            }
            else
            {
                return true;
            }
        }
    }
}

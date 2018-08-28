using System;
using Xamarin.Forms;

namespace Zwaby.CustomControls
{
    public class CustomTimePicker : TimePicker
    {
        public CustomTimePicker()
        {
        }

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomEntry), Color.Black);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(CustomEntry), 1);

        public int BorderWidth
        {
            get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(CustomEntry), 6.0);

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty IsCurvedCornersEnabledProperty =
            BindableProperty.Create(nameof(IsCurvedCornersEnabled), typeof(bool), typeof(CustomEntry), true);

        public bool IsCurvedCornersEnabled
        {
            get => (bool)GetValue(IsCurvedCornersEnabledProperty);
            set => SetValue(IsCurvedCornersEnabledProperty, value);
        }
    }
}

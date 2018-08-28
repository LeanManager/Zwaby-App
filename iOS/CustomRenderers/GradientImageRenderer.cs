using System;
using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Zwaby.CustomControls;
using Zwaby.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(GradientImage), typeof(GradientImageRenderer))]
namespace Zwaby.iOS.CustomRenderers
{
    public class GradientImageRenderer : VisualElementRenderer<Image>
    {
        public GradientImageRenderer()
        {
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            GradientImage image = (GradientImage)this.Element;
            CGColor startColor = image.StartColor.ToCGColor();

            CGColor endColor = image.EndColor.ToCGColor();

            var gradientLayer = new CAGradientLayer();

            gradientLayer.Frame = rect;

            gradientLayer.Colors = new CGColor[] 
            { 
                startColor, endColor
            };

            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            GradientImage image = (GradientImage)this.Element;

            CGRect rect = new CGRect(0, 0, image.Width, image.Height);

            Draw(rect);
        }
    }
}

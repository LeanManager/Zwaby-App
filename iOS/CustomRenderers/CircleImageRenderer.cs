using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Zwaby.CustomControls;
using Zwaby.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(CircleImage), typeof(CircleImageRenderer))]
namespace Zwaby.iOS.CustomRenderers
{
    public class CircleImageRenderer : ImageRenderer
    {
        public CircleImageRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            CreateCircle();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VisualElement.HeightProperty.PropertyName || e.PropertyName == VisualElement.WidthProperty.PropertyName)
            {
                CreateCircle();
            }
        }

        private void CreateCircle()
        {
            try
            {
                double min = Math.Min(Element.Width, Element.Height);
                Control.Layer.CornerRadius = (float)(min / 2.0);
                Control.Layer.MasksToBounds = false;
                Control.Layer.BorderColor = ((CircleImage)this.Element).BorderColor.ToCGColor();
                Control.Layer.BorderWidth = ((CircleImage)this.Element).BorderWidth;
                Control.ClipsToBounds = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
            }
        }
    }
}

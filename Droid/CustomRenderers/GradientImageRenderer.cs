using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Zwaby.CustomControls;
using Zwaby.Droid.CustomRenderers;
using Android.Content;
using Android.Graphics;

[assembly: ExportRenderer(typeof(GradientImage), typeof(GradientImageRenderer))]
namespace Zwaby.Droid.CustomRenderers
{
    public class GradientImageRenderer : VisualElementRenderer<Image>
    {
        private Xamarin.Forms.Color StartColor { get; set; }
        private Xamarin.Forms.Color EndColor { get; set; }

        public GradientImageRenderer(Context context) : base(context)
        {
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height, 
                                                               this.StartColor.ToAndroid(), 
                                                               this.EndColor.ToAndroid(), 
                                                               Shader.TileMode.Clamp);
           
            var paint = new Android.Graphics.Paint()
            {
                Dither = true
            };

            paint.SetShader(gradient);

            canvas.DrawPaint(paint);

            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                var image = e.NewElement as GradientImage;
                this.StartColor = image.StartColor;
                this.EndColor = image.EndColor;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"Error:", ex.Message);
            }
        }
    }
}

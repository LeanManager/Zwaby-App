using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Zwaby.CustomControls;
using Zwaby.Droid.Effects;
using Zwaby.Effects;

[assembly: ResolutionGroupName("Zwaby")]
[assembly: ExportEffect(typeof(ButtonShadowEffect), "ShadowEffect")]
namespace Zwaby.Droid.Effects
{
    internal class ButtonShadowEffect : PlatformEffect
    {
        public ButtonShadowEffect()
        {
        }

        protected override void OnAttached()
        {
            // TODO: Shadow customization

            //Button button = Element as Button;
            if (Element == null)
                return;

            try
            {
                //var control = Control as Android.Support.V7.Widget.AppCompatButton;
                var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);
                if (effect != null)
                {
                    float radius = effect.Radius;
                    float distanceX = effect.DistanceX;
                    float distanceY = effect.DistanceY;
                    Android.Graphics.Color color = effect.Color.ToAndroid();
                    //Android.Graphics.Color color = Android.Graphics.Color.LightSlateGray;
                    //control.SetShadowLayer(radius, distanceX, distanceY, color);
                    //control.SetHintTextColor(Android.Graphics.Color.Black);

                    if (Element is ExtendedButton)
                    {
                        var control = Control as Android.Support.V7.Widget.AppCompatButton;
                        control.SetSupportAllCaps(false);
                    }

                    // TODO: Set TranslationZ and Elevation

                    //control.StateListAnimator = new Android.Animation.StateListAnimator();
                    //control.SetBackgroundColor(Android.Graphics.Color.Orange);
                    Control.Elevation = 12.0f;
                    //control.TranslationZ = 14.0f;
                    //Control.SetBackgroundResource(Resource.Layout.shadow);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            
        }
    }
}

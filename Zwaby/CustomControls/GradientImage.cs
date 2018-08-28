using System;
using Xamarin.Forms;

namespace Zwaby.CustomControls
{
	public class GradientImage : Image
    {
        public GradientImage()
        {
        }

        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
    }
}

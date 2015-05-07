using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.WinPhone;

namespace Todo.WinPhone
{
    class MyBoundPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);

            //this.Control.Template
            //this.Control.TextAlignment = MonoTouch.UIKit.UITextAlignment.Center;

            //this.Control.TextColor = UIColor.White;

            //this.Control.BackgroundColor = UIColor.Clear;
            //this.Control.BorderStyle = UITextBorderStyle.RoundedRect;
            //this.Layer.BorderWidth = 1.0f;
            //this.Layer.CornerRadius = 4.0f;
            //this.Layer.MasksToBounds = true;
            //this.Layer.BorderColor = UIColor.White.CGColor;
        }
    }
}

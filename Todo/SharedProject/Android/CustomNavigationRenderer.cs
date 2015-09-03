using Android.App;
using Android.Graphics.Drawables;
using Todo.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationRenderer))]

namespace Todo.Android
{
    public class CustomNavigationRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            RemoveAppIconFromActionBar();
        }

        void RemoveAppIconFromActionBar()
        {
            // http://stackoverflow.com/questions/14606294/remove-icon-logo-from-action-bar-on-android
            var actionBar = ((Activity)Context).ActionBar;
            actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid()));
        }
    }
}
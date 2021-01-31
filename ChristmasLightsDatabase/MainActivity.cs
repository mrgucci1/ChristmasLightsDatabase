using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;

namespace ChristmasLightsDatabase
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView textNum;
        int number;
        private List<addressHolder> address;
        private ListView myListView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //initialize address list
            address = new List<addressHolder>();
            //add address objects
            address.Add( new addressHolder() { addressLine = "290 Vale Street", city = "Portland", state = "ME", zipCode = "04103" });
            address.Add(new addressHolder() { addressLine = "2 Canal Road Lake", city = "Zurich", state = "IL", zipCode = "60047" });
            //initilize list view
            myListView = FindViewById<ListView>(Resource.Id.myListView);
            //apply custom adapter to listview so we can display our address's
            myListViewAdapter adapter = new myListViewAdapter(this, address);
            myListView.Adapter = adapter;
            //click listeners
            myListView.ItemClick += MyListView_ItemClick;
            myListView.LongClick += MyListView_LongClick;
        }

        private void MyListView_LongClick(object sender, Android.Views.View.LongClickEventArgs e)
        {
            //placeholder
            throw new System.NotImplementedException();
        }

        private void MyListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //placeholder
            throw new System.NotImplementedException();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
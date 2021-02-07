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
        private Button addNewAddress;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //initialize address list
            address = new List<addressHolder>();
            //add address objects
            address.Add( new addressHolder() { addressLine = "290 Vale Street", city = "Portland", state = "ME", zipCode = "04103", desc = "From the outside this house looks stylish. It has been built with brown stones and has red brick decorations. Short, wide windows add to the overall style of the house and have been added to the house in a mostly asymmetric way." });
            address.Add(new addressHolder() { addressLine = "2 Canal Road Lake", city = "Zurich", state = "IL", zipCode = "60047", desc = "From the outside this house looks grandiose. It has been built with grey stones and has blue stone decorations. Large, octagon windows allow enough light to enter the home and have been added to the house in a very symmetric way." });
            //initilize list view
            myListView = FindViewById<ListView>(Resource.Id.myListView);
            addNewAddress = FindViewById<Button>(Resource.Id.addNewAddress);
            //apply custom adapter to listview so we can display our address's
            myListViewAdapter adapter = new myListViewAdapter(this, address);
            myListView.Adapter = adapter;
            //click listeners
            myListView.ItemClick += MyListView_ItemClick;
            addNewAddress.Click += AddNewAddress_Click;
            
        }
        private void ButtonAddNew_Click(object sender, System.EventArgs e)
        {
            EditText editAddressLine = FindViewById<EditText>(Resource.Id.editAddressLine);
            EditText editCity = FindViewById<EditText>(Resource.Id.editCity);
            EditText editState = FindViewById<EditText>(Resource.Id.editState);
            EditText editZipCode = FindViewById<EditText>(Resource.Id.editZipCode);
            EditText editDesc = FindViewById<EditText>(Resource.Id.editDesc);
            //Add new address to list
            address.Add(new addressHolder() { addressLine = editAddressLine.Text, city = editCity.Text, state = editState.Text, zipCode = editZipCode.Text, desc = editDesc.Text });
        }

        private void AddNewAddress_Click(object sender, System.EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialog_AddNewAddress addNewAddress_dialog = new dialog_AddNewAddress();
            addNewAddress_dialog.Show(transaction, "dialog fragment");

            //Subscribe to on add new address event
            addNewAddress_dialog.OnAddNewAddressComplete += AddNewAddress_dialog_OnAddNewAddressComplete;
        }
        private void AddNewAddress_dialog_OnAddNewAddressComplete(object sender, OnAddNewAddress e)
        {
            //Event that is fired when they click the add new address button on the dialog fragment, add the new address to the list
            address.Add(new addressHolder() { addressLine = e.AddressLine, city = e.City, state = e.State, zipCode = e.State, desc = e.Desc });
            myListViewAdapter adapter = new myListViewAdapter(this, address);
            myListView.Adapter = adapter;
        }
        private void MyListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //pull up dialog fragment to display more address info
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialog_AddressInfo addressInfo_Dialog = new dialog_AddressInfo(address, e.Position);
            addressInfo_Dialog.Show(transaction, "dialog fragment");

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
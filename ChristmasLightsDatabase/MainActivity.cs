using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using System.ComponentModel;
using System.Threading;
using Android.Views;
using Android.Views.InputMethods;
using System.Linq;

namespace ChristmasLightsDatabase
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //
        //UI Components
        private ListView myListView;
        private TextView headerAddressLine;
        private TextView headerCity;
        private TextView headerState;
        private TextView headerZipCode;
        SwipeRefreshLayout classSwipeRefresh;
        private EditText editSearch;
        private myListViewAdapter adapter;
        //
        //Global Variables
        private bool animateBool = true;
        private bool isAnimating = false;
        const int editSearchHeight = 113;
        int number;
        private List<addressHolder> address;
        private bool addressAscending;
        private bool cityAscending;
        private bool stateAscending;
        private bool zipAscending;

        [System.Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //Reference swiperefresh layout, set color scheme
            classSwipeRefresh = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeLayout);
            classSwipeRefresh.SetColorScheme(Android.Resource.Color.HoloBlueBright, Android.Resource.Color.HoloOrangeLight, Android.Resource.Color.HoloRedLight, Android.Resource.Color.HoloGreenLight);
            //Initilize UI Components
            myListView = FindViewById<ListView>(Resource.Id.myListView);
            editSearch = FindViewById<EditText>(Resource.Id.editSearch);
            headerAddressLine = FindViewById<TextView>(Resource.Id.headerAddressLine);
            headerCity = FindViewById<TextView>(Resource.Id.headerCity);
            headerState = FindViewById<TextView>(Resource.Id.headerState);
            headerZipCode = FindViewById<TextView>(Resource.Id.headerZipCode);
            //Initialize address list
            address = new List<addressHolder>();
            //add address objects
            address.Add(new addressHolder() { addressLine = "290 Vale Street", city = "Portland", state = "ME", zipCode = "04103", desc = "From the outside this house looks stylish. It has been built with brown stones and has red brick decorations. Short, wide windows add to the overall style of the house and have been added to the house in a mostly asymmetric way." });
            address.Add(new addressHolder() { addressLine = "2 Canal Road Lake", city = "Zurich", state = "IL", zipCode = "60047", desc = "From the outside this house looks grandiose. It has been built with grey stones and has blue stone decorations. Large, octagon windows allow enough light to enter the home and have been added to the house in a very symmetric way." });
            //Adjust Search bar to be invisable and viewstates to gone
            editSearch.Alpha = 0; 
            editSearch.Visibility = ViewStates.Gone;
            //apply custom adapter to listview so we can display our address's
            adapter = new myListViewAdapter(this, address);
            myListView.Adapter = adapter;
            //Events
            myListView.ItemClick += MyListView_ItemClick;
            classSwipeRefresh.Refresh += ClassSwipeRefresh_Refresh;
            editSearch.TextChanged += EditSearch_TextChanged;
            headerAddressLine.Click += HeaderAddressLine_Click;
            headerCity.Click += HeaderCity_Click;
            headerState.Click += HeaderState_Click;
            headerZipCode.Click += HeaderZipCode_Click;

        }
        //===========================================================================================================================================================
        //Click Events
        private void HeaderCity_Click(object sender, System.EventArgs e)
        {
            List<addressHolder> filteredAddress;
            if (!cityAscending)
                filteredAddress = (from cityAll in address orderby cityAll.city select cityAll).ToList<addressHolder>();
            else
                filteredAddress = (from cityAll in address orderby cityAll.city descending select cityAll).ToList<addressHolder>();
            adapter = new myListViewAdapter(this, filteredAddress);
            myListView.Adapter = adapter;
            cityAscending = !cityAscending;
        }

        private void HeaderAddressLine_Click(object sender, System.EventArgs e)
        {
            List<addressHolder> filteredAddress;
            if (!addressAscending)
                filteredAddress = (from addressAll in address orderby addressAll.addressLine select addressAll).ToList<addressHolder>();
            else
                filteredAddress = (from addressAll in address orderby addressAll.addressLine descending select addressAll).ToList<addressHolder>();
            adapter = new myListViewAdapter(this, filteredAddress);
            myListView.Adapter = adapter;
            addressAscending = !addressAscending;
        }
        private void HeaderZipCode_Click(object sender, System.EventArgs e)
        {
            List<addressHolder> filteredAddress;
            if (!zipAscending)
                filteredAddress = (from zipAll in address orderby zipAll.zipCode select zipAll).ToList<addressHolder>();
            else
                filteredAddress = (from zipAll in address orderby zipAll.zipCode descending select zipAll).ToList<addressHolder>();
            adapter = new myListViewAdapter(this, filteredAddress);
            myListView.Adapter = adapter;
            zipAscending = !zipAscending;
        }
        private void HeaderState_Click(object sender, System.EventArgs e)
        {
            List<addressHolder> filteredAddress;
            if (!stateAscending)
                filteredAddress = (from stateAll in address orderby stateAll.state select stateAll).ToList<addressHolder>();
            else
                filteredAddress = (from stateAll in address orderby stateAll.state descending select stateAll).ToList<addressHolder>();
            adapter = new myListViewAdapter(this, filteredAddress);
            myListView.Adapter = adapter;
            stateAscending = !stateAscending;
        }
        private void MyListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //pull up dialog fragment to display more address info
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialog_AddressInfo addressInfo_Dialog = new dialog_AddressInfo(address, e.Position);
            addressInfo_Dialog.Show(transaction, "dialog fragment");

        }
        //===========================================================================================================================================================
        //Searching the list
        private void EditSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            List<addressHolder> searchedAddressHolder = (from address in address
                                                         where address.addressLine.Contains(editSearch.Text, System.StringComparison.OrdinalIgnoreCase) || address.city.Contains(editSearch.Text, System.StringComparison.OrdinalIgnoreCase)
                                                         || address.state.Contains(editSearch.Text, System.StringComparison.OrdinalIgnoreCase) || address.zipCode.Contains(editSearch.Text, System.StringComparison.OrdinalIgnoreCase)
                                                         select address).ToList<addressHolder>();
            adapter = new myListViewAdapter(this, searchedAddressHolder);
            myListView.Adapter = adapter;
        }
        //===========================================================================================================================================================
        //Action Bar Events
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //Show actionbar menu
            MenuInflater.Inflate(Resource.Menu.actionbar, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        //animate search bar
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_search:
                    //search icon has been clicked
                    if (isAnimating)
                        return true;
                    else
                    {
                        if (animateBool)
                        {
                            editSearch.Visibility = ViewStates.Visible;
                            classSwipeRefresh.Animate().TranslationYBy(editSearchHeight).SetDuration(500).Start();
                            editSearch.Animate().AlphaBy(1.0f).SetDuration(500).Start();
                        }
                        else
                        {
                            classSwipeRefresh.Animate().TranslationYBy(-editSearchHeight).SetDuration(500).Start();
                            editSearch.Animate().AlphaBy(-1.0f).SetDuration(300).Start();
                            editSearch.Visibility = ViewStates.Gone;
                        }
                        animateBool = !animateBool;
                        return true;
                    }
                case Resource.Id.action_addnew:
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    dialog_AddNewAddress addNewAddress_dialog = new dialog_AddNewAddress();
                    addNewAddress_dialog.Show(transaction, "dialog fragment");
                    //Subscribe to on add new address event
                    addNewAddress_dialog.OnAddNewAddressComplete += AddNewAddress_dialog_OnAddNewAddressComplete;
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            } 
        }
        //===========================================================================================================================================================
        //Swipe Refresh Events
        private void ClassSwipeRefresh_Refresh(object sender, System.EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork; //run on new thread
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted; //work is completed
            worker.RunWorkerAsync(); //start work!
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //do work once finished
            RunOnUiThread(() => { classSwipeRefresh.Refreshing = false; });
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Add SQL Code to refresh here, for now we are sleeping
            Thread.Sleep(3000);
        }
        //===========================================================================================================================================================
        //Add new address to list
        private void AddNewAddress_dialog_OnAddNewAddressComplete(object sender, OnAddNewAddress e)
        {
            //Event that is fired when they click the add new address button on the dialog fragment, add the new address to the list
            address.Add(new addressHolder() { addressLine = e.AddressLine, city = e.City, state = e.State, zipCode = e.State, desc = e.Desc });
            adapter = new myListViewAdapter(this, address);
            myListView.Adapter = adapter;
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
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
        SwipeRefreshLayout classSwipeRefresh;
        private EditText editSearch;
        private bool animateBool = true;
        private bool isAnimating = false;
        private FrameLayout frameLayout;

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
            //initialize address list
            address = new List<addressHolder>();
            //add address objects
            address.Add( new addressHolder() { addressLine = "290 Vale Street", city = "Portland", state = "ME", zipCode = "04103", desc = "From the outside this house looks stylish. It has been built with brown stones and has red brick decorations. Short, wide windows add to the overall style of the house and have been added to the house in a mostly asymmetric way." });
            address.Add(new addressHolder() { addressLine = "2 Canal Road Lake", city = "Zurich", state = "IL", zipCode = "60047", desc = "From the outside this house looks grandiose. It has been built with grey stones and has blue stone decorations. Large, octagon windows allow enough light to enter the home and have been added to the house in a very symmetric way." });
            //initilize list view
            myListView = FindViewById<ListView>(Resource.Id.myListView);
            addNewAddress = FindViewById<Button>(Resource.Id.addNewAddress);
            //Reference to edit text and frame layout
            editSearch = FindViewById<EditText>(Resource.Id.editSearch);
            frameLayout = FindViewById<FrameLayout>(Resource.Id.frameLayout);
            editSearch.Alpha = 0; //make edit text invisible
            //apply custom adapter to listview so we can display our address's
            myListViewAdapter adapter = new myListViewAdapter(this, address);
            myListView.Adapter = adapter;
            //click listeners
            myListView.ItemClick += MyListView_ItemClick;
            addNewAddress.Click += AddNewAddress_Click;
            classSwipeRefresh.Refresh += ClassSwipeRefresh_Refresh;
            
        }
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
                            classSwipeRefresh.Animate().TranslationYBy(editSearch.Height).SetDuration(500).Start();
                            //Using this class caused my list view to get cut in half when animating?? avoiding it for now
                            //list view is up
                            /*
                            animation anim = new animation(myListView, myListView.Height - editSearch.Height);
                            anim.Duration = 500;
                            anim.AnimationStart += Anim_AnimationStartDown; //listener for when animation has started
                            anim.AnimationEnd += Anim_AnimationEndDown;
                            myListView.StartAnimation(anim);*/
                            editSearch.Animate().AlphaBy(1.0f).SetDuration(500).Start();
                        }
                        else
                        {
                            classSwipeRefresh.Animate().TranslationYBy(-editSearch.Height).SetDuration(500).Start();
                            //Using this class caused my list view to get cut in half when animating?? avoiding it for now
                            /*animation anim = new animation(myListView, myListView.Height + editSearch.Height);
                            anim.Duration = 500;
                            anim.AnimationStart += Anim_AnimationStartUp; //listener for when animation has started
                            anim.AnimationEnd += Anim_AnimationEndUp;
                            myListView.StartAnimation(anim);*/
                            editSearch.Animate().AlphaBy(-1.0f).SetDuration(300).Start();
                        }
                        animateBool = !animateBool;
                        return true;
                    }
                default:
                    return base.OnOptionsItemSelected(item);
            }
            
        }
        //Old events, will delete if I can figure out why it breaks my list view later
        /*private void Anim_AnimationEndDown(object sender, Android.Views.Animations.Animation.AnimationEndEventArgs e)
        {
            isAnimating = false;
        }
        private void Anim_AnimationEndUp(object sender, Android.Views.Animations.Animation.AnimationEndEventArgs e)
        {
            isAnimating = false;
        }

        private void Anim_AnimationStartDown(object sender, Android.Views.Animations.Animation.AnimationStartEventArgs e)
        {
            isAnimating = true;
            editSearch.Animate().AlphaBy(1.0f).SetDuration(500).Start();
            
        }
        private void Anim_AnimationStartUp(object sender, Android.Views.Animations.Animation.AnimationStartEventArgs e)
        {
            isAnimating = true;
            editSearch.Animate().AlphaBy(-1.0f).SetDuration(300).Start();
        }*/

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
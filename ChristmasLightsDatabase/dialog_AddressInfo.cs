using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ChristmasLightsDatabase
{
    class dialog_AddressInfo : DialogFragment
    {
        private TextView myListView;
        private TextView myListView;
        private TextView myListView;
        private TextView myListView;
        private TextView myListView;
        private TextView myListView;
        private TextView myListView;
        private TextView myListView;
        List<addressHolder> myItems;
        public dialog_AddressInfo(List<addressHolder> mItems, int position)
        {
            myItems = mItems;
            //note to self, review this later: https://camposha.info/xamarin-dialog-fragment-with-simple-listview/
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.dialog_AddressInfo, container, false);
            return view;
        }
    }
}
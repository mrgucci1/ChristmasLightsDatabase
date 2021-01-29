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
    class myListViewAdapter : BaseAdapter<addressHolder>
    {
        private List<addressHolder> mItems;
        private Context mContext;
        public override int Count { get { return mItems.Count; } }
        public myListViewAdapter(Context context, List<addressHolder> items)
        {
            mItems = items;
            mContext = context;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override addressHolder this[int position]
        {
            get { return mItems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if(row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.listview_row, null, false);
            }
            TextView txtAddress = row.FindViewById<TextView>(Resource.Id.txtAddressLine);
            txtAddress.Text = mItems[position].addressLine;
            TextView txtCity = row.FindViewById<TextView>(Resource.Id.txtCity);
            txtCity.Text = mItems[position].city;
            TextView txtState = row.FindViewById<TextView>(Resource.Id.txtState);
            txtState.Text = mItems[position].state;
            TextView txtZipCode = row.FindViewById<TextView>(Resource.Id.txtZipCode);
            txtZipCode.Text = mItems[position].zipCode;

            return row;
        }




    }
}
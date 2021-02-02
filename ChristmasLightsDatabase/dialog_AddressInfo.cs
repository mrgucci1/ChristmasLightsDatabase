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
        private TextView AddressLineValue;
        private TextView CityValue;
        private TextView StateValue;
        private TextView ZipCodeValue;
        string addressLine, city, state, zipCode;
        public dialog_AddressInfo(List<addressHolder> mItems, int position)
        {
            addressLine = mItems[position].addressLine;
            city = mItems[position].city;
            state = mItems[position].state;
            zipCode = mItems[position].zipCode;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.dialog_AddressInfo, container, false);
            AddressLineValue = view.FindViewById<TextView>(Resource.Id.addressLineValue);
            AddressLineValue.Text = addressLine;
            CityValue = view.FindViewById<TextView>(Resource.Id.cityValue);
            CityValue.Text = city;
            StateValue = view.FindViewById<TextView>(Resource.Id.stateValue);
            StateValue.Text = state;
            ZipCodeValue = view.FindViewById<TextView>(Resource.Id.zipValue);
            ZipCodeValue.Text = zipCode;
            return view;
        }
    }
}
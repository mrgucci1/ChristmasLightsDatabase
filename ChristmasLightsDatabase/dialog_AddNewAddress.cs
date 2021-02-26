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
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using System.Drawing;
using Android.Graphics.Drawables;
using Java.IO;
using Android.Graphics;

namespace ChristmasLightsDatabase
{
    //Custom event args class that we can subscribe to in mainactivity class
    public class OnAddNewAddress : EventArgs
    {
        private string addressLine;
        private string city;
        private string state;
        private string zipCode;
        private string desc;
        private byte[] image;
        public string AddressLine
        {
            get { return addressLine; }
            set { addressLine = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }
        public byte[] Image
        {
            get { return image; }
            set { image = value; }
        }
        public OnAddNewAddress(string paddressLine, string pcity, string pstate, string pzipCode, string pdesc, byte[] pImage) : base()
        {
            AddressLine = paddressLine;
            City = pcity;
            State = pstate;
            ZipCode = pzipCode;
            Desc = pdesc;
            Image = pImage;
        }
    }
    public class OnAddNewPhotos : EventArgs
    {
        private byte[] imageByte;
        private ImageView imageView;
        public byte[] imageGetter
        {
            get { return imageByte; }
            set { imageByte = value; }
        }
        public ImageView imageViewGetter
        {
            get { return imageView; }
            set { imageView = value; }
        }
        public OnAddNewPhotos(ImageView pImageView) : base()
        {
            imageView = pImageView;
        }
    }
    class dialog_AddNewAddress : DialogFragment
    {
        private Button buttonAddNew;
        private Button buttonChoosePhoto;
        private EditText editAddressLine;
        private EditText editCity;
        private EditText editState;
        private EditText editZipCode;
        private EditText editDesc;
        private ImageView imageViewMain;
        //Event to broadcast, using custom even args
        public event EventHandler<OnAddNewAddress> OnAddNewAddressComplete;
        public event EventHandler<OnAddNewPhotos> OnAddNewPhotoClick;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.dialog_AddNew, container, false);
            //initialize all ui elements
            buttonAddNew = view.FindViewById<Button>(Resource.Id.buttonAddNew);
            editAddressLine = view.FindViewById<EditText>(Resource.Id.editAddressLine);
            editCity = view.FindViewById<EditText>(Resource.Id.editCity);
            editState = view.FindViewById<EditText>(Resource.Id.editState);
            editZipCode = view.FindViewById<EditText>(Resource.Id.editZipCode);
            editDesc = view.FindViewById<EditText>(Resource.Id.editDesc);
            imageViewMain = view.FindViewById<ImageView>(Resource.Id.imageViewMain);
            buttonChoosePhoto = view.FindViewById<Button>(Resource.Id.buttonChoosePhoto);
                buttonChoosePhoto.Click += ButtonChoosePhoto_Click;
            buttonAddNew.Click += ButtonAddNew_Click;

            return view;
        }

        private void ButtonChoosePhoto_Click(object sender, EventArgs e)
        {
            //Clicked choose photo, broadcase event
            OnAddNewPhotoClick.Invoke(this, new OnAddNewPhotos(imageViewMain));
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }
        private void ButtonAddNew_Click(object sender, EventArgs e)
        {
            //Clicked add new address, broadcast the event
            OnAddNewAddressComplete.Invoke(this, new OnAddNewAddress(editAddressLine.Text, editCity.Text, editState.Text, editZipCode.Text, editDesc.Text, (byte[])imageViewMain));
            //dismiss diaglog fragment
            this.Dismiss();
        }
    }
}

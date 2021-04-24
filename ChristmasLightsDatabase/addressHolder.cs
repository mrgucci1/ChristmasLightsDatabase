using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ChristmasLightsDatabase
{
    class addressHolder
    {
        //Address atributes
        public string addressLine { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string desc { get; set; }
        public string image64 { private get; set; }
        public Bitmap image
        {
            get
            {
                if(image64 != "" && image64 != null)
                {
                    byte[] imageAsBytes = Base64.Decode(image64, Base64Flags.Default);
                    return BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length);
                }
                return null;
            }
            set
            {
                image = value;
            }
        }
    }
}

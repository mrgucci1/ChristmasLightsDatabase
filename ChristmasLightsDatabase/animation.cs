using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace ChristmasLightsDatabase
{
    class animation : Animation
    {
        private View mView;
        private int originalHeight;
        private int targetHeight;
        private int growthRate;

        public animation(View view, int targetHeight)
        {
            mView = view;
            originalHeight = view.Height;
            this.targetHeight = targetHeight;
            growthRate = this.targetHeight - originalHeight;
        }

        protected override void ApplyTransformation(float interpolatedTime, Transformation t)
        {
            mView.LayoutParameters.Height = (int)(originalHeight + (growthRate * interpolatedTime));
            mView.RequestLayout(); //redraw view
        }
        public override bool WillChangeBounds()
        {
            return true;
        }

    }
}
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
    public static class extensionMethods
    {
        public static bool Contains(this string source, string checkString, StringComparison comparisonType)
        {
            return (source.IndexOf(checkString, comparisonType) >= 0);
        }
    }
}
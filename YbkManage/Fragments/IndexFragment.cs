
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace YbkManage.Fragments
{
    public class IndexFragment : BaseFragment
    {
        private LinearLayout llWrapper;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.fragment_index, container, false);

			llWrapper = (LinearLayout)view.FindViewById(Resource.Id.ll_wrapper);
			View viewItem = inflater.Inflate(Resource.Layout.index_report_item, container, false);
			llWrapper.AddView(viewItem);

			View viewItem2 = inflater.Inflate(Resource.Layout.index_report_item, container, false);
			llWrapper.AddView(viewItem2);

			return view;
        }
    }
}

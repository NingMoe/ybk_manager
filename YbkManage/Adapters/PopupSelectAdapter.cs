using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace YbkManage.Adapters
{
    public class PopupSelectAdapter : BaseAdapter<string>
    {
        private List<string> items;
        Activity activity;

        public PopupSelectAdapter(Activity context, List<string> values)
            : base()
        {
            activity = context;
            items = values;
        }

        public override string this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View v = convertView;
            if (v == null){
                v = activity.LayoutInflater.Inflate(Resource.Layout.item_popupselect1, null);
            }

            v.FindViewById<TextView>(Resource.Id.tv_name).Gravity = GravityFlags.Center;
            v.FindViewById<TextView>(Resource.Id.tv_name).Text = items[position];

            return v;
        }
    }
}

using System;
using Android.App;
using Android.Views;
using Android.Widget;

namespace YbkManage.Adapters
{
    public class PopupAdapter : BaseAdapter<string>
    {
        string[] items;
        Activity activity;

        public PopupAdapter(Activity context, string[] values)
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
            get { return items.Length; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View v = convertView;
            if (v == null)
                v = activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            v.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position];
            return v;
        }
    }
}

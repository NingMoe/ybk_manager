
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;

namespace YbkManage
{
	[Activity(Label = "CourseSelectAdapter")]
	public class CourseSelectAdapter : BaseAdapter<string>
	{
		Activity mActivity;
		private List<string> items;

		public CourseSelectAdapter(Activity context, List<string> values)
            : base()
        {
			mActivity = context;
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
			if (v == null)
			{
				v = mActivity.LayoutInflater.Inflate(Resource.Layout.item_popupselect1, null);
			}
			var tvName = v.FindViewById<TextView>(Resource.Id.tv_name);
			if (this.selectedValue.Equals(items[position]))
			{
				tvName.SetTextColor(new Color(ContextCompat.GetColor(mActivity, Resource.Color.textColorHigh)));
			}
			else
			{
				tvName.SetTextColor(new Color(ContextCompat.GetColor(mActivity, Resource.Color.textColorPrimary)));
			}
			tvName.Text = items[position];


			return v;
		}



		private string selectedValue = "";
		public void SetSelectedValue(string selectedValue)
		{
			this.selectedValue = selectedValue;
		}
	}
}


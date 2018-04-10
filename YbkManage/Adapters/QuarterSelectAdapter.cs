using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using DataEntity;

namespace YbkManage.Adapters
{
    public class QuarterSelectAdapter : BaseAdapter<QuarterEntity>
    {
		private Activity mActivity;
        private List<QuarterEntity> items;

        private string selectedValue = "";

        public QuarterSelectAdapter(Activity context, List<QuarterEntity> values)
            : base()
        {
            mActivity = context;
            items = values;
        }

        public override QuarterEntity this[int position] 
        {
            get
            {
                return items[position];
            }
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
                v = mActivity.LayoutInflater.Inflate(Resource.Layout.item_popupselect1, null);
            }
            var tvName = v.FindViewById<TextView>(Resource.Id.tv_name);
			if (this.selectedValue.Equals(items[position].QuarterName))
			{
				tvName.SetTextColor(new Color(ContextCompat.GetColor(mActivity, Resource.Color.textColorHigh)));
			}
			else
			{
				tvName.SetTextColor(new Color(ContextCompat.GetColor(mActivity, Resource.Color.textColorPrimary)));
			}
            tvName.Text = items[position].QuarterName;

            return v;
        }

        public void SetSelectedValue(string selectedValue)
        {
            this.selectedValue = selectedValue;
        }
    }
}

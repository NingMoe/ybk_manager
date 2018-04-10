using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using System.Linq;
using Android.Widget;
using DataEntity;
using Android.Support.V4.Content;

namespace YbkManage.Views
{
    public class PopWin_IndexFinancialYear : PopupWindow
    {
        private View mainView;
        private LinearLayout ll_box;

        private Context mContent;
        public List<QuarterEntity> quarterList;

        public PopWin_IndexFinancialYear(Context context,List<QuarterEntity> quarterList)
        {
            mainView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.popwin_financialyear, null);
            this.mContent = context;
            this.ContentView = mainView;
            this.quarterList = quarterList;
            initView();

            this.Height = ViewGroup.LayoutParams.WrapContent;
            this.Width = ViewGroup.LayoutParams.WrapContent;
            //this.AnimationStyle = Resource.Style.popwindow_topin;
            this.SetBackgroundDrawable(new BitmapDrawable());
        }

        private void initView()
        {
            ll_box = mainView.FindViewById<LinearLayout>(Resource.Id.ll_box);
            ll_box.RemoveAllViews();
            foreach (var quarter in quarterList)
            {
                var itemView = LayoutInflater.FromContext(this.mContent).Inflate(Resource.Layout.item_popwin_financialyear, null);
                TextView tvTitle = itemView.FindViewById<TextView>(Resource.Id.tv_year);
                tvTitle.Text = quarter.QuarterName;

                if (quarter.IsCurrent)
                {
                    
                    tvTitle.SetTextColor(new Color(ContextCompat.GetColor(this.mContent, Resource.Color.textColorHigh)));
                }

                tvTitle.Click += (sender, e) =>
                {
                    if (clickItem != null)
                    {
                        clickItem(quarter);
                    }
                };
                ll_box.AddView(itemView);
            }
        }

        public void SetSelectedColor()
        {
            var selected = this.quarterList.FirstOrDefault(i => i.IsCurrent);
            for (var i = 0; i < ll_box.ChildCount; i++)
            {
                TextView tvTitle = ll_box.GetChildAt(i).FindViewById<TextView>(Resource.Id.tv_year);
                if(tvTitle.Text == selected.QuarterName)
                {
                    tvTitle.SetTextColor(new Color(ContextCompat.GetColor(this.mContent, Resource.Color.textColorHigh))); 
                }
                else
                {
                    tvTitle.SetTextColor(new Color(ContextCompat.GetColor(this.mContent, Resource.Color.textColorPrimary)));
                }
            }
        }


        public delegate void ClickItem(QuarterEntity quarter);
        public ClickItem clickItem;

    }
}

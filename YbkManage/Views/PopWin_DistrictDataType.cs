
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;

namespace YbkManage
{
	public class PopWin_DistrictDataType : PopupWindow
	{
		private View mainView;
		private LinearLayout ll_box;

		private Context mContent;
		public Dictionary<int, string> DicDataType=new Dictionary<int, string>();
		public string SelectedText;

		public PopWin_DistrictDataType(Context context, int titleType,string selectedText)
		{
			mainView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.popwin_financialyear, null);
			this.mContent = context;
			this.ContentView = mainView;
			//根据标题类型，设置数据源
			SetDictionary(titleType,selectedText);


			this.Height = ViewGroup.LayoutParams.WrapContent;
			this.Width = ViewGroup.LayoutParams.WrapContent;
			this.AnimationStyle = Resource.Style.popwindow_topin;
			this.SetBackgroundDrawable(new BitmapDrawable());
		}

		private void initView()
		{
			ll_box = mainView.FindViewById<LinearLayout>(Resource.Id.ll_box);
			ll_box.RemoveAllViews();
			foreach (var d in DicDataType)
			{
				var itemView = LayoutInflater.FromContext(this.mContent).Inflate(Resource.Layout.item_popwin_financialyear, null);
				TextView tvTitle = itemView.FindViewById<TextView>(Resource.Id.tv_year);
				tvTitle.Text = d.Value;
				if (d.Value == SelectedText)
				{
					//tvTitle.SetTextColor(this.mContent.Resources.GetColor(Resource.Color.textColorHigh, null));
					tvTitle.SetTextColor(new Color(ContextCompat.GetColor(this.mContent, Resource.Color.textColorHigh)));
				}else
				{
					//tvTitle.SetTextColor(this.mContent.Resources.GetColor(Resource.Color.textColorPrimary, null));
					tvTitle.SetTextColor(new Color(ContextCompat.GetColor(this.mContent, Resource.Color.textColorPrimary)));
				}

				tvTitle.Click += (sender, e) =>
				{
					if (clickItem != null)
					{
						clickItem(d.Value);
					}
				};
				ll_box.AddView(itemView);
			}
		}

		//设置选中项
		public void SetSelectedColor(string selectedText)
		{
			for (var i = 0; i < ll_box.ChildCount; i++)
			{
				TextView tvTitle = ll_box.GetChildAt(i).FindViewById<TextView>(Resource.Id.tv_year);
				if (tvTitle.Text == selectedText)
				{
					//tvTitle.SetTextColor(this.mContent.Resources.GetColor(Resource.Color.textColorHigh, null));
					tvTitle.SetTextColor(new Color(ContextCompat.GetColor(this.mContent, Resource.Color.textColorHigh)));
				}
				else
				{
					//tvTitle.SetTextColor(this.mContent.Resources.GetColor(Resource.Color.textColorPrimary, null));
					tvTitle.SetTextColor(new Color(ContextCompat.GetColor(this.mContent, Resource.Color.textColorPrimary)));
				}
			}
		}

		//设置数据源
		public void SetDictionary(int titleType,string selectedText)
		{
			DicDataType.Clear();
			//预算
			if (titleType == 1)
			{
				DicDataType.Add(1, "预算");
				DicDataType.Add(2, "行课");
			}
			//累计
			else if (titleType == 2)
			{
				DicDataType.Add(1, "人次");
				DicDataType.Add(2, "预收");
				DicDataType.Add(3, "行课");
			}
			else
			{
				DicDataType.Add(1, "");

			}
			//默认选中项
            this.SelectedText =selectedText;
			//画页面
            initView();
		}

		public int GetDataType(string v)
		{
			var data=DicDataType.FirstOrDefault(p => p.Value == v);
			return data.Key;
		}


		public delegate void ClickItem(string selectedText);
		public ClickItem clickItem;


	}
}

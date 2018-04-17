using System;
using System.Collections;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using YbkManage.Activities;
using YbkManage.Adapters;
using YbkManage.Fragments;

namespace YbkManage
{
	public class DistrictMainFragment : BaseFragment
	{
		#region UIField
		private LayoutInflater layoutInflater;
		// 容器控件
		private FrameLayout mFrameLayout;

		// fragment集合
		private Hashtable fragmentHashtable = new Hashtable();
		private static Android.Support.V4.App.Fragment lastFragment;

		//预算、累计、增量、招新按钮
		private TextView tv_budge, tv_sum, tv_increase, tv_new;
		//右上角切换按钮    预算=1预算／2行课   累计=1人次／2预收／3行课
		private TextView tv_dataType;
		private PopWin_DistrictDataType pop_dataType;
		#endregion

		#region Fields
		//当前选中页的标志   1预算 2累计
		public int TitleType { get; set; }
		//预算 : 1预算／2行课    
		//累计 : 1人次／2预收／3行课
		public int DataType { get; set; }

		#endregion


		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			layoutInflater = inflater;
			View view = layoutInflater.Inflate(Resource.Layout.fragment_district, container, false);

			InitViews(view);
			InitEvents();
			//默认预算选中
			changeTextStatus(Resource.Id.tv_budge);
			TitleType = 1;
			tv_dataType.Text = "预算";
			return view;
		}

		#region 点击事件
		protected void InitEvents()
		{
			//预算
			tv_budge.Click += delegate
			{
				TitleType = 1;
				tv_dataType.Text = "预算";
				tv_dataType.Visibility = ViewStates.Visible;
				switchFragment(tv_budge);
			};
			//累计
			tv_sum.Click += delegate
			{
				TitleType = 2;
				tv_dataType.Text = "人次";
				tv_dataType.Visibility = ViewStates.Visible;
				switchFragment(tv_sum);
			};
			//增量
			tv_increase.Click += delegate
			{
				TitleType = 0;
				tv_dataType.Text = "";
				tv_dataType.Visibility = ViewStates.Gone;
				switchFragment(tv_increase);
			};
			//招新
			tv_new.Click += delegate
			{
				TitleType = 0;
				tv_dataType.Text = "";
				tv_dataType.Visibility = ViewStates.Gone;
				switchFragment(tv_new);
			};

			//查询类型
			tv_dataType.Click += delegate
			{
				SwitchDataType();
			};
		}
		#endregion

		#region 切换预算-累计-增量-招新


		protected void InitViews(View view)
		{
			mFrameLayout = view.FindViewById<FrameLayout>(Resource.Id.fl_district_content);
			tv_budge = view.FindViewById<TextView>(Resource.Id.tv_budge);
			tv_sum = view.FindViewById<TextView>(Resource.Id.tv_sum);
			tv_increase = view.FindViewById<TextView>(Resource.Id.tv_increase);
			tv_new = view.FindViewById<TextView>(Resource.Id.tv_new);
			tv_dataType = view.FindViewById<TextView>(Resource.Id.tv_datatype);

			Android.Support.V4.App.FragmentTransaction transaction = CurrActivity.SupportFragmentManager.BeginTransaction();

			int p_index = CurrActivity.Intent.GetIntExtra("p_index", 0);
			//预算
			if (p_index == 0)
			{
				BudgeFragment fragment = new BudgeFragment();
				lastFragment = fragment;
				transaction.Replace(Resource.Id.fl_district_content, fragment);
				fragmentHashtable.Add(Resource.Id.tv_budge, fragment);

				TitleType = 1;
			}
			//累计
			else if (p_index == 1)
			{
				SumAccountFragment fragment = new SumAccountFragment();
				lastFragment = fragment;
				transaction.Replace(Resource.Id.fl_district_content, fragment);
				fragmentHashtable.Add(Resource.Id.tv_sum, fragment);

				TitleType = 2;
			}
			//增量
			else if (p_index == 2)
			{
				IncreaseFragment fragment = new IncreaseFragment();
				lastFragment = fragment;
				transaction.Replace(Resource.Id.fl_district_content, fragment);
				fragmentHashtable.Add(Resource.Id.tv_increase, fragment);
			}
			//招新
			else if (p_index == 3)
			{
				NewStudentFragment fragment = new NewStudentFragment();
				lastFragment = fragment;
				transaction.Replace(Resource.Id.fl_district_content, fragment);
				fragmentHashtable.Add(Resource.Id.tv_new, fragment);
			}


			transaction.Commit();
		}



		/// <summary>
		/// 切换布局
		/// </summary>
		/// <param name="view">View.</param>
		public void switchFragment(View view)
		{
			int viewId = view.Id;
			changeTextStatus(viewId);
			Android.Support.V4.App.Fragment fragment = null;
			switch (viewId)
			{
				//预算
				case Resource.Id.tv_budge:
					fragment = (Android.Support.V4.App.Fragment)fragmentHashtable[viewId];
					if (fragment == null)
					{
						fragment = new BudgeFragment();
						fragmentHashtable.Add(viewId, fragment);
					}
					break;
				//累计
				case Resource.Id.tv_sum:
					fragment = (Android.Support.V4.App.Fragment)fragmentHashtable[viewId];
					if (fragment == null)
					{
						fragment = new SumAccountFragment();
						fragmentHashtable.Add(viewId, fragment);
					}
					break;
				//增量
				case Resource.Id.tv_increase:
					fragment = (Android.Support.V4.App.Fragment)fragmentHashtable[viewId];
					if (fragment == null)
					{
						fragment = new IncreaseFragment();
						fragmentHashtable.Add(viewId, fragment);
					}
					break;
				//招新
				case Resource.Id.tv_new:
					fragment = (Android.Support.V4.App.Fragment)fragmentHashtable[viewId];
					if (fragment == null)
					{
						fragment = new NewStudentFragment();
						fragmentHashtable.Add(viewId, fragment);
					}
					break;

			}
			switchContent(lastFragment, fragment);
		}

		/// <summary>
		/// 切换布局内容
		/// </summary>
		/// <param name="pre">Pre.</param>
		/// <param name="next">Next.</param>
		public void switchContent(Android.Support.V4.App.Fragment pre, Android.Support.V4.App.Fragment next)
		{
			if (lastFragment != next)
			{
				lastFragment = next;
				if (!next.IsAdded)
				{
					CurrActivity.SupportFragmentManager.BeginTransaction().Hide(pre).Add(Resource.Id.fl_district_content, next).Commit();
				}
				else
				{
					CurrActivity.SupportFragmentManager.BeginTransaction().Hide(pre).Show(next).Commit();
				}
			}
		}
		/// <summary>
		/// 更改按钮的选中状态
		/// </summary>
		/// <param name="viewId">View identifier.</param>
		private void changeTextStatus(int viewId)
		{
			//初始化字体颜色
			tv_budge.SetTextColor(Color.White);
			tv_sum.SetTextColor(Color.White);
			tv_increase.SetTextColor(Color.White);
			tv_new.SetTextColor(Color.White);

			//移除背景图片
			tv_budge.SetBackgroundResource(0);
			tv_sum.SetBackgroundResource(0);
			tv_increase.SetBackgroundResource(0);
			tv_new.SetBackgroundResource(0);

			switch (viewId)
			{
				case Resource.Id.tv_budge:
					tv_budge.SetTextColor(new Color(ContextCompat.GetColor(CurrContext, Resource.Color.actionbar_bg)));
					tv_budge.SetBackgroundResource(Resource.Drawable.district_top_left);
					break;
				case Resource.Id.tv_sum:
					tv_sum.SetTextColor(new Color(ContextCompat.GetColor(CurrContext, Resource.Color.actionbar_bg)));
					tv_sum.SetBackgroundResource(Resource.Drawable.district_top_center);
					break;
				case Resource.Id.tv_increase:
					tv_increase.SetTextColor(new Color(ContextCompat.GetColor(CurrContext, Resource.Color.actionbar_bg)));
					tv_increase.SetBackgroundResource(Resource.Drawable.district_top_center);
					break;
				case Resource.Id.tv_new:
					tv_new.SetTextColor(new Color(ContextCompat.GetColor(CurrContext, Resource.Color.actionbar_bg)));
					tv_new.SetBackgroundResource(Resource.Drawable.district_top_right);
					break;
				default:
					break;
			}
		}

		#endregion

		#region 切换右上角查询类型
		private void SwitchDataType()
		{
			var selectedText = tv_dataType.Text;
			if (pop_dataType == null)
			{
				pop_dataType = new PopWin_DistrictDataType(CurrActivity, TitleType,selectedText);
				pop_dataType.clickItem += new PopWin_DistrictDataType.ClickItem(DataTypeClick);
			}
			else
			{
				//重置数据源-下拉内容
				pop_dataType.SetDictionary(TitleType,selectedText);
			}

			pop_dataType.OutsideTouchable = true;
			if (pop_dataType.IsShowing)
			{
				pop_dataType.Dismiss();
			}
			else
			{
				pop_dataType.ShowAsDropDown(tv_dataType, 0, -15);
			}
		}
		//下拉选项的点击事件
		public void DataTypeClick(string selectedText)
		{
			if (pop_dataType.IsShowing)
			{
				pop_dataType.Dismiss();
			}

			pop_dataType.SetSelectedColor(selectedText);
			tv_dataType.Text = selectedText;

			//重新加载数据
			var dataType = pop_dataType.GetDataType(selectedText);
			var fragments = CurrActivity.SupportFragmentManager.Fragments;
			foreach (var f in fragments)
			{
				//预算
				if (f.IsVisible && f is BudgeFragment)
				{

					((BudgeFragment)f).dataType = dataType;
					((BudgeFragment)f).BindData();
					break;
				}
				//累计
				else if (f.IsVisible && f is SumAccountFragment)
				{
					var sumAccountFragment = (f as SumAccountFragment);
					sumAccountFragment.dataType = dataType;
					sumAccountFragment.BindData();
					break;
				}
			}
		}
		#endregion
	}
}

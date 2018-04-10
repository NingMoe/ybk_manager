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
[Activity(Label = "DistrictMain", ScreenOrientation = ScreenOrientation.Portrait)]
public class DistrictMainFragment : BaseFragment, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener, View.IOnClickListener
	{
		#region UIField
		private LayoutInflater layoutInflater;
		// 容器控件
		private FrameLayout mFrameLayout;

		// fragment集合
		private Hashtable fragmentHashtable = new Hashtable();
		private static Android.Support.V4.App.Fragment lastFragment;

		//预算、累计、增量、招新按钮
		public TextView tv_budge, tv_sum, tv_increase, tv_new;
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
			return view;
		}


		#region 点击事件  切换预算-累计-增量-招新


		protected void InitViews(View view)
		{
			mFrameLayout =view.FindViewById<FrameLayout>(Resource.Id.fl_district_content);
			tv_budge = view.FindViewById<TextView>(Resource.Id.tv_budge);
			tv_sum = view.FindViewById<TextView>(Resource.Id.tv_sum);
			tv_increase = view.FindViewById<TextView>(Resource.Id.tv_increase);
			tv_new = view.FindViewById<TextView>(Resource.Id.tv_new);

			Android.Support.V4.App.FragmentTransaction transaction = CurrActivity.SupportFragmentManager.BeginTransaction();

			int p_index = CurrActivity.Intent.GetIntExtra("p_index", 0);
			//预算
			if (p_index == 0)
			{
				BudgeFragment fragment = new BudgeFragment();
				lastFragment = fragment;
				transaction.Replace(Resource.Id.fl_district_content, fragment);
				fragmentHashtable.Add(Resource.Id.tv_budge, fragment);
			}
			//累计
			else if (p_index == 1)
			{
				SumAccountFragment fragment = new SumAccountFragment();
				lastFragment = fragment;
				transaction.Replace(Resource.Id.fl_district_content, fragment);
				fragmentHashtable.Add(Resource.Id.tv_sum, fragment);

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

		protected void InitEvents()
		{
			tv_budge.Click += delegate
			{
				switchFragment(tv_budge);
			};
			tv_sum.Click += delegate
			{
				switchFragment(tv_sum);
			};
			tv_increase.Click += delegate
			{
				switchFragment(tv_increase);
			};
			tv_new.Click += delegate
			{
				switchFragment(tv_new);
			};
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

		public void OnClick(View v)
		{
			throw new NotImplementedException();
		}

		public void OnItemClick(View itemView, int position)
		{
			throw new NotImplementedException();
		}

		public void OnItemLongClick(View itemView, int position)
		{
			throw new NotImplementedException();
		}

		public void OnRefresh()
		{
			throw new NotImplementedException();
		}
	}
}

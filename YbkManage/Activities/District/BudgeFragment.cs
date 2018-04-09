using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataEntity;
using DataService;
using xxxxxLibrary.LoadingDialog;
using xxxxxLibrary.Network;
using xxxxxLibrary.Serializer;
using xxxxxLibrary.Toast;
using YbkManage.Activities;
using YbkManage.Adapters;
using YbkManage.App;
using YbkManage.Fragments;

namespace YbkManage
{
	/// <summary>
	/// 区域-预算
	/// </summary>
	public class BudgeFragment : BaseFragment, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener, View.IOnClickListener
	{
		#region UIField
		private LayoutInflater layoutInflater;
		//预算、累计、增量、招新按钮
		public TextView tv_budge, tv_sum, tv_increase, tv_new;
// 列表页用控件
private SwipeRefreshLayout mSwipeRefreshLayout;
private RecyclerView mRecyclerView;
// 列表显示方式
private LinearLayoutManager linearLayoutManager;
// 列表适配器
private RenewReportAdapter mAdapter;
		#endregion

		#region Field
// 教学报表数据
private List<RenewInfo> teachReportList = new List<RenewInfo>();
		#endregion

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			
			layoutInflater = inflater;
			View view = layoutInflater.Inflate(Resource.Layout.fragment_budge, container, false);

			//InitViews(view);
			//LoadData();

			return view;
		}

		/// <summary>
		/// 页面控件
		/// </summary>
		protected void InitViews(View view)
		{
			tv_budge = view.FindViewById<TextView>(Resource.Id.tv_budge);
			tv_sum = view.FindViewById<TextView>(Resource.Id.tv_sum);
			tv_increase = view.FindViewById<TextView>(Resource.Id.tv_increase);
			tv_new = view.FindViewById<TextView>(Resource.Id.tv_new);

			tv_budge.SetOnClickListener(this);
			tv_sum.SetOnClickListener(this);
			tv_increase.SetOnClickListener(this);
			tv_new.SetOnClickListener(this);



			//mSwipeRefreshLayout = (SwipeRefreshLayout)view.FindViewById(Resource.Id.refresher);
			//mRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.recycler_view);

			//mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));


			//linearLayoutManager = new LinearLayoutManager(CurrActivity);
			//mAdapter = new RenewReportAdapter(CurrActivity, teachReportList);
			//mRecyclerView.SetLayoutManager(linearLayoutManager);
			//mRecyclerView.SetAdapter(mAdapter);
			//mAdapter.NotifyDataSetChanged();

			//mSwipeRefreshLayout.SetOnRefreshListener(this);
			//RecyclerViewItemOnGestureListener viewOnGestureListener = new RecyclerViewItemOnGestureListener(mRecyclerView, this);
			//mRecyclerView.AddOnItemTouchListener(new RecyclerViewItemOnItemTouchListener(mRecyclerView, viewOnGestureListener));
		}

		#region 页面点击事件处理
		/// <summary>
		/// 页面点击事件处理
		/// </summary>
		/// <param name="v">V.</param>
		public void OnClick(View v)
		{
			//预算
			if (v.Id == Resource.Id.tv_budge)
			{
				tv_budge.SetBackgroundResource(Resource.Drawable.district_top_left);
				tv_budge.SetTextColor(new Color(ContextCompat.GetColor(CurrActivity, Resource.Color.actionbar_bg)));
			}
			else if (v.Id == Resource.Id.tv_sum)
			{

			}
			else if (v.Id == Resource.Id.tv_increase)
			{

			}
			else if (v.Id == Resource.Id.tv_new)
			{
			}
		}
		#endregion


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

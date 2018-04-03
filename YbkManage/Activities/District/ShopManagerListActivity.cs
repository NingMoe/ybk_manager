
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
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

namespace YbkManage
{
	[Activity(Label = "ShopManagerListActivity", ScreenOrientation = ScreenOrientation.Portrait)]
	public class ShopManagerListActivity : AppActivity, SwipeRefreshLayout.IOnRefreshListener, IRecyclerViewItemClickListener
	{
		#region UIField
		private LinearLayout llAdd;

		// 总数
		private TextView tvCount;

		// 列表页用控件
		private SwipeRefreshLayout mSwipeRefreshLayout;
		private RecyclerView mRecyclerView;

		// 列表显示方式
		private LinearLayoutManager linearLayoutManager;
		// 列表适配器
		private ShopManagerAdapter mAdapter;


		#endregion

		#region Field
		// 教研组数据
		private List<ShopManagerList> shopManagerList = new List<ShopManagerList>();
		#endregion

		/// <summary>
		/// 指定Layout
		/// </summary>
		protected override void OnCreate(Bundle savedInstanceState)
		{
			LayoutReourceId = Resource.Layout.activity_shopmanager_list;

			base.OnCreate(savedInstanceState);
		}
		/// <summary>
		/// 初始化控件
		/// </summary>
		protected override void InitViews()
		{
			llAdd = FindViewById<LinearLayout>(Resource.Id.ll_add);
			tvCount = FindViewById<TextView>(Resource.Id.tv_count);

			mSwipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
			mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
			mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));

			linearLayoutManager = new LinearLayoutManager(CurrActivity);
			mAdapter = new ShopManagerAdapter(CurrContext, shopManagerList);
			mRecyclerView.SetLayoutManager(linearLayoutManager);
			mRecyclerView.SetAdapter(mAdapter);
			mAdapter.NotifyDataSetChanged();

			mSwipeRefreshLayout.SetOnRefreshListener(this);

			RecyclerViewItemOnGestureListener viewOnGestureListener = new RecyclerViewItemOnGestureListener(mRecyclerView, this);
			mRecyclerView.AddOnItemTouchListener(new RecyclerViewItemOnItemTouchListener(mRecyclerView, viewOnGestureListener));
		}

		/// <summary>
		/// 初始化事件
		/// </summary>
		protected override void InitEvents()
		{
			// 返回
			FindViewById<ImageButton>(Resource.Id.imgBtn_back).Click += (sender, e) =>
			{
				CurrActivity.Finish();
				OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
			};

			llAdd.Click += (sender, e) =>
			{
				Intent intent = new Intent(CurrActivity, typeof(ShopManagerAddActivity));
				StartActivity(intent);
				CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
			};
		}

		/// <summary>
		/// 获取数据
		/// </summary>
		protected override void LoadData()
		{
            OnRefresh();
		}

		/// <summary>
		/// 返回后页面刷新，执行此方法
		/// </summary>
		protected override void OnResume()
		{
			base.OnResume();
			OnRefresh();
		}



		#region BindData
		/// <summary>
		/// 刷新
		/// </summary>
		public void OnRefresh()
		{
			if (!NetUtil.CheckNetWork(CurrActivity))
			{
				ToastUtil.ShowWarningToast(CurrActivity, "网络未连接！");
				return;
			}
			else
			{
				LoadingDialogUtil.ShowLoadingDialog(CurrActivity, "获取数据中...");
				BindData();
			}
		}

		/// <summary>
		/// 绑定数据
		/// </summary>
		private void BindData()
		{
			try
			{
				var districtCode = CurrUserInfo.DistrictCode;
				var schoolId = CurrUserInfo.SchoolId;
				new Thread(new ThreadStart(() =>
				{

					shopManagerList = new MeService().GetShopManagerList(schoolId, districtCode);
					RunOnUiThread(() =>
					{
						LoadingDialogUtil.DismissLoadingDialog();
						//总人数
						tvCount.Text = CurrUserInfo.DistrictName+"店长(" + shopManagerList.Count + "人)";
						mAdapter.SetData(shopManagerList);
						mAdapter.NotifyDataSetChanged();
						mSwipeRefreshLayout.Refreshing = false;

					});


				})).Start();
			}
			catch (Exception ex)
			{
				var msg = ex.Message.ToString();
				LoadingDialogUtil.DismissLoadingDialog();
				mSwipeRefreshLayout.Refreshing = false;
			}
		}
		#endregion

		/// <summary>
		/// 行点击事件
		/// </summary>
		public void OnItemClick(View itemView, int position)
		{
			var data = shopManagerList[position];
			Intent intent = new Intent(CurrActivity, typeof(ShopManagerAddActivity));
			intent.PutExtra("areaCodes", data.AreaCodes);
			intent.PutExtra("areaNames", data.AreaNames);
			intent.PutExtra("ShopManagerJsonStr", JsonSerializer.ToJsonString(data));
			StartActivity(intent);
			CurrActivity.OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
		}
		public void OnItemLongClick(View itemView, int position)
		{
			//throw new NotImplementedException(   );
		}


	}
}

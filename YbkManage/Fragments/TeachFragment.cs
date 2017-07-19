using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Android.Graphics;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using YbkManage.Adapters;
using YbkManage.Models;
using YbkManage.Views;

namespace YbkManage.Fragments
{
    /// <summary>
    /// 教学报表页面
    /// </summary>
    public class TeachFragment : BaseFragment, SwipeRefreshLayout.IOnRefreshListener, ListAdapter.OnItemClickListener//,View.IOnScrollChangeListener
	{
        // 教研组、年级、区域 筛选按钮
		private TeachPopupButton pop_btn1, pop_btn2, pop_btn3;

        // 列表页用控件
        private SwipeRefreshLayout mSwipeRefreshLayout;
        private RecyclerView mRecyclerView;

        // 列表显示方式
        private LinearLayoutManager linearLayoutManager;
        // 列表适配器
        private ListAdapter mAdapter;

        // 教学报表数据
        private List<TeachReportEntity> teachReportList = new List<TeachReportEntity>();

        private string[] data;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.fragment_teach, container, false);

            pop_btn1 = (TeachPopupButton)view.FindViewById(Resource.Id.pop_btn1);
			pop_btn2 = (TeachPopupButton)view.FindViewById(Resource.Id.pop_btn2);
			pop_btn3 = (TeachPopupButton)view.FindViewById(Resource.Id.pop_btn3);



            View popWin1 = inflater.Inflate(Resource.Layout.popup_select1,null);
            ListView listview1 =(ListView) popWin1.FindViewById(Resource.Id.lv);
            string[] arr1 = {"item01","it em02","item03","item04","item05","item06","item07","item08","item09","item10"};
            PopupAdapter adaptera = new PopupAdapter(CurrActivity, arr1);
            listview1.Adapter = adaptera;
		    pop_btn1.setPopupView(popWin1);


            mSwipeRefreshLayout = (SwipeRefreshLayout)view.FindViewById(Resource.Id.refresher);
            mRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.recycler_view);

            mSwipeRefreshLayout.SetColorSchemeColors(Color.ParseColor("#db0000"));
			//mSwipeRefreshLayout.SetColorScheme(Resource.Color.xam_dark_blue,
						  //Resource.Color.xam_purple,
						  //Resource.Color.xam_gray,
						  //Resource.Color.xam_green);




            linearLayoutManager = new LinearLayoutManager(CurrActivity);
            mAdapter = new ListAdapter(new[] { "Brasil", "Mexico", "United States", "Canada", "United States", "Canada", "United States", "Canada", "United States", "Canada", "United States", "Canada", "United States", "Canada" });
            mRecyclerView.SetLayoutManager(linearLayoutManager);
            mRecyclerView.SetAdapter(mAdapter);
            mAdapter.NotifyDataSetChanged();

            mSwipeRefreshLayout.SetOnRefreshListener(this);
            //mSwipeRefreshLayout.SetOnScrollChangeListener(this);

            mAdapter.setOnItemClickListener(this);


            return view;
        }

        public void onItemClick(View view, int position)
        {
            
            Toast.MakeText(CurrActivity,"onItemClick",ToastLength.Short).Show();
        }

        public void onItemLongClick(View view, int position)
        {
            //NoteEntity itemData = mList.get(position);
		//	ActivityOptionsCompat options = ActivityOptionsCompat
		//					   .makeSceneTransitionAnimation(MainActivity.this, new Pair<View, String>(view.findViewById(R.id.iv_img), "cover"), new Pair<View, String>(view.findViewById(R.id.tv_tips), "title"));
		//	//                        Intent intent = new Intent(MainActivity.this, DetailActivity.class);
		//	Intent intent = new Intent(MainActivity.this, X5Activity.class);
  //                      Bundle bundle = new Bundle();
		//bundle.putSerializable("note", itemData);
                        //intent.putExtras(bundle);
                        //ActivityCompat.startActivity(MainActivity.this, intent, options.toBundle());
			Toast.MakeText(CurrActivity, "onItemLongClick", ToastLength.Short).Show();
        }

        public void OnRefresh()
        {
            mSwipeRefreshLayout.Refreshing = true;
			getData();
        }

        public void OnScrollChange(View v, int scrollX, int scrollY, int oldScrollX, int oldScrollY)
        {
            int lastvisibleItemPosition = linearLayoutManager.FindLastVisibleItemPosition();
            if(lastvisibleItemPosition +1 == mAdapter.ItemCount)
            {
                getData2();
            }

        }

		private async void getData()
		{
			//测试用
			string url = "http://www.sina.com.cn/";
			//创建一个请求
			var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
			var httpRes = (HttpWebResponse)await httpReq.GetResponseAsync();
			if (httpRes.StatusCode == HttpStatusCode.OK)
			{
				data = new[] { "Brasil", "Mexico", "United States", "Canada", "United States", "Canada", "United States", "Canada", "United States", "Canada", "United States", "Canada", "United States", "Canada" };
				mSwipeRefreshLayout.Refreshing = false;
				mAdapter.NotifyDataSetChanged();
			}
		}

		private async void getData2()
		{
			//测试用
			string url = "http://www.sina.com.cn/";
			//创建一个请求
			var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
			var httpRes = (HttpWebResponse)await httpReq.GetResponseAsync();
			if (httpRes.StatusCode == HttpStatusCode.OK)
			{
				data.Append("1111"); data.Append("1111"); data.Append("1111"); data.Append("1111"); data.Append("1111"); data.Append("1111"); data.Append("1111"); data.Append("1111");
                mSwipeRefreshLayout.Refreshing = false;
				mAdapter.NotifyDataSetChanged();
			}
		}
    }
}

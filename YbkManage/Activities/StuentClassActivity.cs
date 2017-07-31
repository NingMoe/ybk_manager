using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using DataEntity;
using xxxxxLibrary.Serializer;
using YbkManage.Adapters;
using YbkManage.Fragments;

namespace YbkManage.Activities
{
    /// <summary>
    /// 学生详情页
    /// </summary>
    [Activity(Label = "StuentClassActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class StuentClassActivity : AppActivity,ViewPager.IOnPageChangeListener
    {
        private ViewPager mViewPager;

		private StudentRenewModel currStudent;

		// 当前被选中的fragment与栏目对应的数字
		private int currentIndex;

		// 标题的集合
        private List<TextView> textviewList = new List<TextView>();
        // 标题下的下划线集合
        private List<View> viewList = new List<View>();

        // fragment的集合
        private List<Android.Support.V4.App.Fragment> fragmentList = new List<Android.Support.V4.App.Fragment>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            LayoutReourceId = Resource.Layout.activity_student_class;

			base.OnCreate(savedInstanceState);
		}

		/// <summary>
		/// 页面参数值
		/// </summary>
		protected override void InitVariables()
		{
			Bundle bundle = Intent.Extras;
			if (bundle != null)
			{
				var studentJsonStr = Intent.Extras.GetString("studentJsonStr");
				currStudent = JsonSerializer.ToObject<StudentRenewModel>(studentJsonStr);
			}
		}

        /// <summary>
        /// 页面控件
        /// </summary>
        protected override void InitViews()
        {
            FindViewById<TextView>(Resource.Id.tv_title).Text = currStudent.name;

            mViewPager = FindViewById<ViewPager>(Resource.Id.vp_list);

			textviewList.Add(FindViewById<TextView>(Resource.Id.tv_ing)); 
            textviewList.Add(FindViewById<TextView>(Resource.Id.tv_will)); 
            textviewList.Add(FindViewById<TextView>(Resource.Id.tv_end));

			viewList.Add(FindViewById<View>(Resource.Id.view_ing));
			viewList.Add(FindViewById<View>(Resource.Id.view_will));
            viewList.Add(FindViewById<View>(Resource.Id.view_end));

            fragmentList.Add(new StudentClassListFragment(currStudent.code,0));
            fragmentList.Add(new StudentClassListFragment(currStudent.code, 2));
			fragmentList.Add(new StudentClassListFragment(currStudent.code, 1));

            SimpleFragmentAdapter adapater = new SimpleFragmentAdapter(CurrActivity, this.SupportFragmentManager, fragmentList);
            mViewPager.Adapter = adapater;

        }

        /// <summary>
        /// 页面事件
        /// </summary>
        protected override void InitEvents()
        {
            // 返回
            FindViewById<ImageButton>(Resource.Id.imgBtn_back).Click += (sender, e) =>
            {
                CurrActivity.Finish();
                OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.right_out);
            };

            // 点击头部菜单
			FindViewById<TextView>(Resource.Id.tv_ing).Click += (sender, e) =>
			{
				SetSelected(0);
                mViewPager.SetCurrentItem(0, true);
			};
			FindViewById<TextView>(Resource.Id.tv_will).Click += (sender, e) =>
			{
				SetSelected(1);
				mViewPager.SetCurrentItem(1, true);
			};
			FindViewById<TextView>(Resource.Id.tv_end).Click += (sender, e) =>
			{
				SetSelected(2);
				mViewPager.SetCurrentItem(2, true);
			};

            mViewPager.AddOnPageChangeListener(this);
        }

		/// <summary>
		/// 设置显示对应ViewPager页面的对应标题的游标
		/// </summary>
		/// <param name="pagerIndex">Pager index.</param>
		public void SetSelected(int pagerIndex)
		{
            for (int i = 0; i < fragmentList.Count; i++)
			{
				var textView = textviewList[i];
                var viewLine = viewList[i];

				if (pagerIndex == i)
				{
					textView.SetTextColor(new Color(ContextCompat.GetColor(CurrContext, Resource.Color.textColorHigh)));
                    viewLine.Visibility = ViewStates.Visible;
				}
				else
				{
                    textView.SetTextColor(new Color(ContextCompat.GetColor(CurrContext, Resource.Color.textColorPrimary)));
                    viewLine.Visibility = ViewStates.Gone;
				}
			}
		}

        public void OnPageScrollStateChanged(int state)
        {
            //throw new NotImplementedException();
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            //throw new NotImplementedException();
        }

        public void OnPageSelected(int position)
        {
            SetSelected(position);
            currentIndex = position;
        }
    }
}

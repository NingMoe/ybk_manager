using System.Collections;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;

using YbkManage.Fragments;

namespace YbkManage.Activities
{
    /// <summary>
    /// 首页
    /// </summary>
    [Activity(Label = "Main", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Main : AppActivity
    {
        // 容器控件
        private FrameLayout mFrameLayout;

        // fragment集合
        private Hashtable fragmentHashtable = new Hashtable();
        private static Android.Support.V4.App.Fragment lastFragment;

        private TextView tv_index, tv_teach, tv_mine;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            LayoutReourceId = Resource.Layout.activity_main;
            base.OnCreate(savedInstanceState);
        }


        protected override void InitViews()
        {
            mFrameLayout = (FrameLayout)FindViewById(Resource.Id.fl_content);
            tv_index = (TextView)FindViewById(Resource.Id.tv_index);
            tv_teach = (TextView)FindViewById(Resource.Id.tv_teach);
            tv_mine = (TextView)FindViewById(Resource.Id.tv_mine);

            Android.Support.V4.App.FragmentTransaction transaction = SupportFragmentManager.BeginTransaction();

			int p_index = Intent.GetIntExtra("p_index", 0);
            if (p_index == 1)
            {
                TeachFragment fragment = new TeachFragment();
                lastFragment = fragment;
                transaction.Replace(Resource.Id.fl_content, fragment);
                fragmentHashtable.Add(Resource.Id.tv_teach, fragment);

            }
            else if (p_index == 2)
            {
                MineFragment fragment = new MineFragment();
                lastFragment = fragment;
                transaction.Replace(Resource.Id.fl_content, fragment);
                fragmentHashtable.Add(Resource.Id.tv_mine, fragment);
            }
            else
            {
                IndexFragment fragment = new IndexFragment();
                lastFragment = fragment;
                transaction.Replace(Resource.Id.fl_content, fragment);
                fragmentHashtable.Add(Resource.Id.tv_index, fragment);
            }

            transaction.Commit();
        }

        protected override void InitEvents()
        {
            tv_index.Click += delegate
            {
                switchFragment(tv_index);
            };
            tv_teach.Click += delegate
            {
                switchFragment(tv_teach);
            };
            tv_mine.Click += delegate
            {
                switchFragment(tv_mine);
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
                case Resource.Id.tv_teach:
                    fragment = (Android.Support.V4.App.Fragment)fragmentHashtable[viewId];
                    if (fragment == null)
                    {
                        fragment = new TeachFragment();
                        fragmentHashtable.Add(viewId, fragment);
                    }
                    break;
                case Resource.Id.tv_mine:
                    fragment = (Android.Support.V4.App.Fragment)fragmentHashtable[viewId];
                    if (fragment == null)
                    {
                        fragment = new MineFragment();
                        fragmentHashtable.Add(viewId, fragment);
                    }
                    break;
                default:
                    fragment = (Android.Support.V4.App.Fragment)fragmentHashtable[viewId];
                    if (fragment == null)
                    {
                        fragment = new IndexFragment();
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
                    SupportFragmentManager.BeginTransaction().Hide(pre).Add(Resource.Id.fl_content, next).Commit();
                }
                else
                {
                    SupportFragmentManager.BeginTransaction().Hide(pre).Show(next).Commit();
                }
            }
        }

        private void changeTextStatus(int viewId)
        {
            tv_index.SetTextColor(Color.ParseColor("#333333"));
            tv_teach.SetTextColor(Color.ParseColor("#333333"));
            tv_mine.SetTextColor(Color.ParseColor("#333333"));

            tv_index.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.menu_index, 0, 0);
            tv_teach.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.menu_teach, 0, 0);
            tv_mine.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.menu_mine, 0, 0);
            switch (viewId)
            {
                case Resource.Id.tv_teach:
                    tv_teach.SetTextColor(new Color(ContextCompat.GetColor(CurrContext, Resource.Color.menu_txt_color)));
                    tv_teach.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.menu_teach_on, 0, 0);
                    break;
                case Resource.Id.tv_mine:
                    tv_mine.SetTextColor(new Color(ContextCompat.GetColor(CurrContext, Resource.Color.menu_txt_color)));
                    tv_mine.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.menu_mine_on, 0, 0);
                    break;
                case Resource.Id.tv_index:
                    tv_index.SetTextColor(new Color(ContextCompat.GetColor(CurrContext, Resource.Color.menu_txt_color)));
                    tv_index.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.menu_index_on, 0, 0);
                    break;
                default:
                    break;
            }
        }

    }
}

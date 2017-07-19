using System.Collections;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;

using YbkManage.Fragments;

namespace YbkManage.Activities
{
    [Activity(Label = "Main", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Main : AppActivity
    {
        // 容器控件
        private FrameLayout mFrameLayout;

		// fragment集合
        private Hashtable fragmentHashtable = new Hashtable();
		private static Fragment lastFragment;

        private TextView tv_index, tv_teach, tv_mine;

        protected override void OnCreate(Bundle savedInstanceState)
		{
           
			//initSystemBar(Resource.Color.actionbar_bg);
            SetContentView(Resource.Layout.activity_main);

            base.OnCreate(savedInstanceState);

			

			FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
            int p_index = Intent.GetIntExtra("p_index",0); 
			if (p_index == 1)
			{
				TeachFragment fragment = new TeachFragment();
				lastFragment = fragment;
                fragmentTransaction.Replace(Resource.Id.fl_content, fragment);
                fragmentHashtable.Add(Resource.Id.tv_teach, fragment);

				//changeTextStatus(R.id.tv_live);
			}
			else if (p_index == 2)
			{
				MineFragment fragment = new MineFragment();
				lastFragment = fragment;
				fragmentTransaction.Replace(Resource.Id.fl_content, fragment);
				fragmentHashtable.Add(Resource.Id.tv_mine, fragment);

				//changeTextStatus(R.id.tv_mine);
			}
			else
			{
				IndexFragment fragment = new IndexFragment();
				lastFragment = fragment;
				fragmentTransaction.Replace(Resource.Id.fl_content, fragment);
				fragmentHashtable.Add(Resource.Id.tv_index, fragment);

				//switchFragment(R.id.tv_news);
			}

            fragmentTransaction.Commit();
		}


		protected override void InitViews()
		{
            mFrameLayout = (FrameLayout)FindViewById(Resource.Id.fl_content);
			tv_index = (TextView)FindViewById(Resource.Id.tv_index);
			tv_teach = (TextView)FindViewById(Resource.Id.tv_teach);
			tv_mine = (TextView)FindViewById(Resource.Id.tv_mine);
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

		/*
	   * 切换布局
	   */
        public void switchFragment(View view)
		{
            int viewId = view.Id;
			changeTextStatus(viewId);
			Fragment fragment = null;
            switch (viewId)
			{
                case Resource.Id.tv_teach:
                    fragment = (Fragment)fragmentHashtable[viewId];
					if (fragment == null)
					{
						fragment = new TeachFragment();
						fragmentHashtable.Add(viewId, fragment);
					}
					break;
                case Resource.Id.tv_mine:
					fragment = (Fragment)fragmentHashtable[viewId];
					if (fragment == null)
					{
						fragment = new MineFragment();
						fragmentHashtable.Add(viewId, fragment);
					}
					break;
				default:
					fragment = (Fragment)fragmentHashtable[viewId];
					if (fragment == null)
					{
						fragment = new IndexFragment();
						fragmentHashtable.Add(viewId, fragment);
					}
					break;
			}
			switchContent(lastFragment, fragment);
		}

		/*
		 * 切换布局内容
		 */
		public void switchContent(Fragment pre, Fragment next)
		{
			if (lastFragment != next)
			{
				lastFragment = next;
				//FragmentManager fragmentManager = getSupportFragmentManager();
				FragmentTransaction transaction = FragmentManager.BeginTransaction();
                if (!next.IsAdded)
				{
                    transaction.Hide(pre).Add(Resource.Id.fl_content, next).Commit();
				}
				else
				{
					transaction.Hide(pre).Show(next).Commit();
				}
			}
		}

		private void changeTextStatus(int viewId)
		{
			tv_index.SetTextColor(Android.Graphics.Color.ParseColor("#333333"));
			tv_teach.SetTextColor(Android.Graphics.Color.ParseColor("#333333"));
            tv_mine.SetTextColor(Android.Graphics.Color.ParseColor("#333333"));

            tv_index.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.menu_index, 0, 0);
            tv_teach.SetCompoundDrawablesWithIntrinsicBounds(0,Resource.Drawable.menu_teach, 0, 0);
            tv_mine.SetCompoundDrawablesWithIntrinsicBounds(0,Resource.Drawable.menu_mine, 0, 0);
			switch (viewId)
			{
                case Resource.Id.tv_teach:
                    tv_teach.SetTextColor(this.Resources.GetColor(Resource.Color.menu_txt_color));
                    tv_teach.SetCompoundDrawablesWithIntrinsicBounds(0,Resource.Drawable.menu_teach_on, 0, 0);
					break;
				case Resource.Id.tv_mine:
					tv_mine.SetTextColor(Resources.GetColor(Resource.Color.menu_txt_color));
                    tv_mine.SetCompoundDrawablesWithIntrinsicBounds(0,Resource.Drawable.menu_mine_on, 0, 0);
					break;
				case Resource.Id.tv_index:
					tv_index.SetTextColor(Resources.GetColor(Resource.Color.menu_txt_color));
                    tv_index.SetCompoundDrawablesWithIntrinsicBounds(0,Resource.Drawable.menu_index_on, 0, 0);
					break;
				default:
					break;
			}
		}

	}
}

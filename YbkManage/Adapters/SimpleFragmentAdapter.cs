using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V4.App;

namespace YbkManage.Adapters
{
    public class SimpleFragmentAdapter:FragmentPagerAdapter
    {
		private Context mContext;

        private List<Fragment> fragmentList;

        private FragmentManager fm;

        public SimpleFragmentAdapter(Context context,FragmentManager fm, List<Fragment> list):base(fm)
        {
            this.fm = fm;
            this.mContext = context;
            this.fragmentList = list;
        }

        public override Fragment GetItem(int position)
        {
            return fragmentList[position];
        }

		public override int Count
		{
			get
			{
				return fragmentList.Count;
			}
		}
    }
}

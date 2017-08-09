using System;
using Android.Support.V7.Widget;
using Android.Views;

namespace YbkManage.Adapters
{
    public interface IRecyclerViewItemClickListener
    {
		// 普通Click点击
		void OnItemClick(View itemView, int position);

        //长按Click点击
        void OnItemLongClick(View itemView, int position);
    }

}
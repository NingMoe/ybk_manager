using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace YbkManage.Adapters
{
    /// <summary>
    /// 手势监听器类
    /// </summary>
	public class RecyclerViewItemOnGestureListener : GestureDetector.SimpleOnGestureListener
	{
		private RecyclerView m_RecyclerView;
		private IRecyclerViewItemClickListener m_IRecyclerViewClickListerner;
		public RecyclerViewItemOnGestureListener(RecyclerView rv, IRecyclerViewItemClickListener listener)
		{
			m_RecyclerView = rv;
			m_IRecyclerViewClickListerner = listener;
		}

        /// <summary>
        /// 单击事件
        /// </summary>
        /// <returns><c>true</c>, if single tap up was oned, <c>false</c> otherwise.</returns>
        /// <param name="e">E.</param>
		public override bool OnSingleTapUp(MotionEvent e)
		{
			View child = m_RecyclerView.FindChildViewUnder(e.GetX(), e.GetY());
			if (child != null)
			{
				RecyclerView.ViewHolder vh = m_RecyclerView.GetChildViewHolder(child);
				int position = vh.LayoutPosition;
				if (m_IRecyclerViewClickListerner != null)
				{
					m_IRecyclerViewClickListerner.OnItemClick(child, position);
				}
			}
			return true;
		}

		/// <summary>
		/// 长按事件
		/// </summary>
		/// <param name="e">E.</param>
		public override void OnLongPress(MotionEvent e)
		{
			View child = m_RecyclerView.FindChildViewUnder(e.GetX(), e.GetY());
			if (child != null)
			{
				RecyclerView.ViewHolder vh = m_RecyclerView.GetChildViewHolder(child);
				int position = vh.LayoutPosition;

				if (m_IRecyclerViewClickListerner != null)
				{
					m_IRecyclerViewClickListerner.OnItemLongClick(child, position);
				}
			}
		}
	}
}
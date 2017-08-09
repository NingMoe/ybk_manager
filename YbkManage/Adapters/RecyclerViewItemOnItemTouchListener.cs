using System;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;

namespace YbkManage.Adapters
{
	/// <summary>
	/// Item的触摸监听器类
	/// </summary>
	public class RecyclerViewItemOnItemTouchListener : RecyclerView.SimpleOnItemTouchListener
	{

		private RecyclerView m_RecyclerView;
		private GestureDetectorCompat m_GestureDetector;

		public RecyclerViewItemOnItemTouchListener(RecyclerView rv, RecyclerViewItemOnGestureListener simpleOnGestureListener)
		{
			m_RecyclerView = rv;
			m_GestureDetector = new GestureDetectorCompat(rv.Context, simpleOnGestureListener);
		}

		public override bool OnInterceptTouchEvent(RecyclerView rv, MotionEvent e)
		{
			if (m_GestureDetector.OnTouchEvent(e))
				return true;
			else
				return false;
		}

		public override void OnTouchEvent(RecyclerView rv, MotionEvent e)
		{

		}
	}
}
package md5f78e4fcb18e467f99034745a00b936d7;


public class RecyclerViewItemOnItemTouchListener
	extends android.support.v7.widget.RecyclerView.SimpleOnItemTouchListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInterceptTouchEvent:(Landroid/support/v7/widget/RecyclerView;Landroid/view/MotionEvent;)Z:GetOnInterceptTouchEvent_Landroid_support_v7_widget_RecyclerView_Landroid_view_MotionEvent_Handler\n" +
			"n_onTouchEvent:(Landroid/support/v7/widget/RecyclerView;Landroid/view/MotionEvent;)V:GetOnTouchEvent_Landroid_support_v7_widget_RecyclerView_Landroid_view_MotionEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("YbkManage.Adapters.RecyclerViewItemOnItemTouchListener, YbkManage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RecyclerViewItemOnItemTouchListener.class, __md_methods);
	}


	public RecyclerViewItemOnItemTouchListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == RecyclerViewItemOnItemTouchListener.class)
			mono.android.TypeManager.Activate ("YbkManage.Adapters.RecyclerViewItemOnItemTouchListener, YbkManage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public RecyclerViewItemOnItemTouchListener (android.support.v7.widget.RecyclerView p0, md5f78e4fcb18e467f99034745a00b936d7.RecyclerViewItemOnGestureListener p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == RecyclerViewItemOnItemTouchListener.class)
			mono.android.TypeManager.Activate ("YbkManage.Adapters.RecyclerViewItemOnItemTouchListener, YbkManage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Support.V7.Widget.RecyclerView, Xamarin.Android.Support.v7.RecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null:YbkManage.Adapters.RecyclerViewItemOnGestureListener, YbkManage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0, p1 });
	}


	public boolean onInterceptTouchEvent (android.support.v7.widget.RecyclerView p0, android.view.MotionEvent p1)
	{
		return n_onInterceptTouchEvent (p0, p1);
	}

	private native boolean n_onInterceptTouchEvent (android.support.v7.widget.RecyclerView p0, android.view.MotionEvent p1);


	public void onTouchEvent (android.support.v7.widget.RecyclerView p0, android.view.MotionEvent p1)
	{
		n_onTouchEvent (p0, p1);
	}

	private native void n_onTouchEvent (android.support.v7.widget.RecyclerView p0, android.view.MotionEvent p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

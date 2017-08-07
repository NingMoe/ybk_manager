package md5aef17806c436d105b8e2fc9f2f039f1b;


public class TeacherListActivity
	extends md5aef17806c436d105b8e2fc9f2f039f1b.AppActivity
	implements
		mono.android.IGCUserPeer,
		android.support.v4.widget.SwipeRefreshLayout.OnRefreshListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onRefresh:()V:GetOnRefreshHandler:Android.Support.V4.Widget.SwipeRefreshLayout/IOnRefreshListenerInvoker, Xamarin.Android.Support.v4\n" +
			"";
		mono.android.Runtime.register ("YbkManage.Activities.TeacherListActivity, YbkManage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TeacherListActivity.class, __md_methods);
	}


	public TeacherListActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TeacherListActivity.class)
			mono.android.TypeManager.Activate ("YbkManage.Activities.TeacherListActivity, YbkManage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onRefresh ()
	{
		n_onRefresh ();
	}

	private native void n_onRefresh ();

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

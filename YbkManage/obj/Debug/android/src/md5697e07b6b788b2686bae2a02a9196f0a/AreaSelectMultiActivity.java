package md5697e07b6b788b2686bae2a02a9196f0a;


public class AreaSelectMultiActivity
	extends md5aef17806c436d105b8e2fc9f2f039f1b.AppActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("YbkManage.AreaSelectMultiActivity, YbkManage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AreaSelectMultiActivity.class, __md_methods);
	}


	public AreaSelectMultiActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == AreaSelectMultiActivity.class)
			mono.android.TypeManager.Activate ("YbkManage.AreaSelectMultiActivity, YbkManage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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

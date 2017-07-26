package md561c0e7010d4f8fb6bd7f0f259909cf54;


public class MyLoadingDialog
	extends android.app.ProgressDialog
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_show:()V:GetShowHandler\n" +
			"";
		mono.android.Runtime.register ("xxxxxLibrary.LoadingDialog.MyLoadingDialog, xxxxxLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyLoadingDialog.class, __md_methods);
	}


	public MyLoadingDialog (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == MyLoadingDialog.class)
			mono.android.TypeManager.Activate ("xxxxxLibrary.LoadingDialog.MyLoadingDialog, xxxxxLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public MyLoadingDialog (android.content.Context p0, int p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == MyLoadingDialog.class)
			mono.android.TypeManager.Activate ("xxxxxLibrary.LoadingDialog.MyLoadingDialog, xxxxxLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void show ()
	{
		n_show ();
	}

	private native void n_show ();

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

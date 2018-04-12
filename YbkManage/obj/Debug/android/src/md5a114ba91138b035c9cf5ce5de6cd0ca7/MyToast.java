package md5a114ba91138b035c9cf5ce5de6cd0ca7;


public class MyToast
	extends android.widget.Toast
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("xxxxxLibrary.Toast.MyToast, xxxxxLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyToast.class, __md_methods);
	}


	public MyToast (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == MyToast.class)
			mono.android.TypeManager.Activate ("xxxxxLibrary.Toast.MyToast, xxxxxLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

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

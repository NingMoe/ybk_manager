package md5aef17806c436d105b8e2fc9f2f039f1b;


public class TeacherRoleSelectActivity
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
		mono.android.Runtime.register ("YbkManage.Activities.TeacherRoleSelectActivity, YbkManage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TeacherRoleSelectActivity.class, __md_methods);
	}


	public TeacherRoleSelectActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TeacherRoleSelectActivity.class)
			mono.android.TypeManager.Activate ("YbkManage.Activities.TeacherRoleSelectActivity, YbkManage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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

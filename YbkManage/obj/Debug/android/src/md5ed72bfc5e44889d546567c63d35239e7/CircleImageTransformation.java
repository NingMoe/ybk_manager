package md5ed72bfc5e44889d546567c63d35239e7;


public class CircleImageTransformation
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.squareup.picasso.Transformation
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_key:()Ljava/lang/String;:GetGetKeyHandler:Square.Picasso.ITransformationInvoker, Square.Picasso\n" +
			"n_transform:(Landroid/graphics/Bitmap;)Landroid/graphics/Bitmap;:GetTransform_Landroid_graphics_Bitmap_Handler:Square.Picasso.ITransformationInvoker, Square.Picasso\n" +
			"";
		mono.android.Runtime.register ("YbkManage.App.CircleImageTransformation, YbkManage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CircleImageTransformation.class, __md_methods);
	}


	public CircleImageTransformation () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CircleImageTransformation.class)
			mono.android.TypeManager.Activate ("YbkManage.App.CircleImageTransformation, YbkManage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public CircleImageTransformation (com.squareup.picasso.Picasso p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == CircleImageTransformation.class)
			mono.android.TypeManager.Activate ("YbkManage.App.CircleImageTransformation, YbkManage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Square.Picasso.Picasso, Square.Picasso, Version=2.5.2.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public java.lang.String key ()
	{
		return n_key ();
	}

	private native java.lang.String n_key ();


	public android.graphics.Bitmap transform (android.graphics.Bitmap p0)
	{
		return n_transform (p0);
	}

	private native android.graphics.Bitmap n_transform (android.graphics.Bitmap p0);

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

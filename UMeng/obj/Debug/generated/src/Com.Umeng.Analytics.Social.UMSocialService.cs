using System;
using System.Collections.Generic;
using Android.Runtime;

namespace Com.Umeng.Analytics.Social {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMSocialService']"
	[global::Android.Runtime.Register ("com/umeng/analytics/social/UMSocialService", DoNotGenerateAcw=true)]
	public abstract partial class UMSocialService : global::Java.Lang.Object {

		internal static IntPtr java_class_handle;
		internal static IntPtr class_ref {
			get {
				return JNIEnv.FindClass ("com/umeng/analytics/social/UMSocialService", ref java_class_handle);
			}
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return typeof (UMSocialService); }
		}

		protected UMSocialService (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		static IntPtr id_ctor;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMSocialService']/constructor[@name='UMSocialService' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe UMSocialService ()
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (Handle != IntPtr.Zero)
				return;

			try {
				if (GetType () != typeof (UMSocialService)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "()V"),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "()V");
					return;
				}

				if (id_ctor == IntPtr.Zero)
					id_ctor = JNIEnv.GetMethodID (class_ref, "<init>", "()V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor);
			} finally {
			}
		}

		static IntPtr id_share_Landroid_content_Context_arrayLcom_umeng_analytics_social_UMPlatformData_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMSocialService']/method[@name='share' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='com.umeng.analytics.social.UMPlatformData...']]"
		[Register ("share", "(Landroid/content/Context;[Lcom/umeng/analytics/social/UMPlatformData;)V", "")]
		public static unsafe void Share (global::Android.Content.Context p0, params global:: Com.Umeng.Analytics.Social.UMPlatformData[] p1)
		{
			if (id_share_Landroid_content_Context_arrayLcom_umeng_analytics_social_UMPlatformData_ == IntPtr.Zero)
				id_share_Landroid_content_Context_arrayLcom_umeng_analytics_social_UMPlatformData_ = JNIEnv.GetStaticMethodID (class_ref, "share", "(Landroid/content/Context;[Lcom/umeng/analytics/social/UMPlatformData;)V");
			IntPtr native_p1 = JNIEnv.NewArray (p1);
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_share_Landroid_content_Context_arrayLcom_umeng_analytics_social_UMPlatformData_, __args);
			} finally {
				if (p1 != null) {
					JNIEnv.CopyArray (native_p1, p1);
					JNIEnv.DeleteLocalRef (native_p1);
				}
			}
		}

		static IntPtr id_share_Landroid_content_Context_Ljava_lang_String_arrayLcom_umeng_analytics_social_UMPlatformData_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMSocialService']/method[@name='share' and count(parameter)=3 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='com.umeng.analytics.social.UMPlatformData...']]"
		[Register ("share", "(Landroid/content/Context;Ljava/lang/String;[Lcom/umeng/analytics/social/UMPlatformData;)V", "")]
		public static unsafe void Share (global::Android.Content.Context p0, string p1, params global:: Com.Umeng.Analytics.Social.UMPlatformData[] p2)
		{
			if (id_share_Landroid_content_Context_Ljava_lang_String_arrayLcom_umeng_analytics_social_UMPlatformData_ == IntPtr.Zero)
				id_share_Landroid_content_Context_Ljava_lang_String_arrayLcom_umeng_analytics_social_UMPlatformData_ = JNIEnv.GetStaticMethodID (class_ref, "share", "(Landroid/content/Context;Ljava/lang/String;[Lcom/umeng/analytics/social/UMPlatformData;)V");
			IntPtr native_p1 = JNIEnv.NewString (p1);
			IntPtr native_p2 = JNIEnv.NewArray (p2);
			try {
				JValue* __args = stackalloc JValue [3];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				__args [2] = new JValue (native_p2);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_share_Landroid_content_Context_Ljava_lang_String_arrayLcom_umeng_analytics_social_UMPlatformData_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				if (p2 != null) {
					JNIEnv.CopyArray (native_p2, p2);
					JNIEnv.DeleteLocalRef (native_p2);
				}
			}
		}

	}

	[global::Android.Runtime.Register ("com/umeng/analytics/social/UMSocialService", DoNotGenerateAcw=true)]
	internal partial class UMSocialServiceInvoker : UMSocialService {

		public UMSocialServiceInvoker (IntPtr handle, JniHandleOwnership transfer) : base (handle, transfer) {}

		protected override global::System.Type ThresholdType {
			get { return typeof (UMSocialServiceInvoker); }
		}

	}

}

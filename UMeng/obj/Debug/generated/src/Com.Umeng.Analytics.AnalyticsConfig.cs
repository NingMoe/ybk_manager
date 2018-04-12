using System;
using System.Collections.Generic;
using Android.Runtime;

namespace Com.Umeng.Analytics {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']"
	[global::Android.Runtime.Register ("com/umeng/analytics/AnalyticsConfig", DoNotGenerateAcw=true)]
	public partial class AnalyticsConfig : global::Java.Lang.Object {


		static IntPtr ACTIVITY_DURATION_OPEN_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/field[@name='ACTIVITY_DURATION_OPEN']"
		[Register ("ACTIVITY_DURATION_OPEN")]
		public static bool ActivityDurationOpen {
			get {
				if (ACTIVITY_DURATION_OPEN_jfieldId == IntPtr.Zero)
					ACTIVITY_DURATION_OPEN_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "ACTIVITY_DURATION_OPEN", "Z");
				return JNIEnv.GetStaticBooleanField (class_ref, ACTIVITY_DURATION_OPEN_jfieldId);
			}
			set {
				if (ACTIVITY_DURATION_OPEN_jfieldId == IntPtr.Zero)
					ACTIVITY_DURATION_OPEN_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "ACTIVITY_DURATION_OPEN", "Z");
				try {
					JNIEnv.SetStaticField (class_ref, ACTIVITY_DURATION_OPEN_jfieldId, value);
				} finally {
				}
			}
		}

		static IntPtr CATCH_EXCEPTION_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/field[@name='CATCH_EXCEPTION']"
		[Register ("CATCH_EXCEPTION")]
		public static bool CatchException {
			get {
				if (CATCH_EXCEPTION_jfieldId == IntPtr.Zero)
					CATCH_EXCEPTION_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "CATCH_EXCEPTION", "Z");
				return JNIEnv.GetStaticBooleanField (class_ref, CATCH_EXCEPTION_jfieldId);
			}
			set {
				if (CATCH_EXCEPTION_jfieldId == IntPtr.Zero)
					CATCH_EXCEPTION_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "CATCH_EXCEPTION", "Z");
				try {
					JNIEnv.SetStaticField (class_ref, CATCH_EXCEPTION_jfieldId, value);
				} finally {
				}
			}
		}

		static IntPtr GPU_RENDERER_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/field[@name='GPU_RENDERER']"
		[Register ("GPU_RENDERER")]
		public static string GpuRenderer {
			get {
				if (GPU_RENDERER_jfieldId == IntPtr.Zero)
					GPU_RENDERER_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "GPU_RENDERER", "Ljava/lang/String;");
				IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, GPU_RENDERER_jfieldId);
				return JNIEnv.GetString (__ret, JniHandleOwnership.TransferLocalRef);
			}
			set {
				if (GPU_RENDERER_jfieldId == IntPtr.Zero)
					GPU_RENDERER_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "GPU_RENDERER", "Ljava/lang/String;");
				IntPtr native_value = JNIEnv.NewString (value);
				try {
					JNIEnv.SetStaticField (class_ref, GPU_RENDERER_jfieldId, native_value);
				} finally {
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}

		static IntPtr GPU_VENDER_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/field[@name='GPU_VENDER']"
		[Register ("GPU_VENDER")]
		public static string GpuVender {
			get {
				if (GPU_VENDER_jfieldId == IntPtr.Zero)
					GPU_VENDER_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "GPU_VENDER", "Ljava/lang/String;");
				IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, GPU_VENDER_jfieldId);
				return JNIEnv.GetString (__ret, JniHandleOwnership.TransferLocalRef);
			}
			set {
				if (GPU_VENDER_jfieldId == IntPtr.Zero)
					GPU_VENDER_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "GPU_VENDER", "Ljava/lang/String;");
				IntPtr native_value = JNIEnv.NewString (value);
				try {
					JNIEnv.SetStaticField (class_ref, GPU_VENDER_jfieldId, native_value);
				} finally {
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}

		static IntPtr kContinueSessionMillis_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/field[@name='kContinueSessionMillis']"
		[Register ("kContinueSessionMillis")]
		public static long KContinueSessionMillis {
			get {
				if (kContinueSessionMillis_jfieldId == IntPtr.Zero)
					kContinueSessionMillis_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "kContinueSessionMillis", "J");
				return JNIEnv.GetStaticLongField (class_ref, kContinueSessionMillis_jfieldId);
			}
			set {
				if (kContinueSessionMillis_jfieldId == IntPtr.Zero)
					kContinueSessionMillis_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "kContinueSessionMillis", "J");
				try {
					JNIEnv.SetStaticField (class_ref, kContinueSessionMillis_jfieldId, value);
				} finally {
				}
			}
		}

		static IntPtr mWrapperType_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/field[@name='mWrapperType']"
		[Register ("mWrapperType")]
		public static string MWrapperType {
			get {
				if (mWrapperType_jfieldId == IntPtr.Zero)
					mWrapperType_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "mWrapperType", "Ljava/lang/String;");
				IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, mWrapperType_jfieldId);
				return JNIEnv.GetString (__ret, JniHandleOwnership.TransferLocalRef);
			}
			set {
				if (mWrapperType_jfieldId == IntPtr.Zero)
					mWrapperType_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "mWrapperType", "Ljava/lang/String;");
				IntPtr native_value = JNIEnv.NewString (value);
				try {
					JNIEnv.SetStaticField (class_ref, mWrapperType_jfieldId, native_value);
				} finally {
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}

		static IntPtr mWrapperVersion_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/field[@name='mWrapperVersion']"
		[Register ("mWrapperVersion")]
		public static string MWrapperVersion {
			get {
				if (mWrapperVersion_jfieldId == IntPtr.Zero)
					mWrapperVersion_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "mWrapperVersion", "Ljava/lang/String;");
				IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, mWrapperVersion_jfieldId);
				return JNIEnv.GetString (__ret, JniHandleOwnership.TransferLocalRef);
			}
			set {
				if (mWrapperVersion_jfieldId == IntPtr.Zero)
					mWrapperVersion_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "mWrapperVersion", "Ljava/lang/String;");
				IntPtr native_value = JNIEnv.NewString (value);
				try {
					JNIEnv.SetStaticField (class_ref, mWrapperVersion_jfieldId, native_value);
				} finally {
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}

		static IntPtr sEncrypt_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/field[@name='sEncrypt']"
		[Register ("sEncrypt")]
		public static bool SEncrypt {
			get {
				if (sEncrypt_jfieldId == IntPtr.Zero)
					sEncrypt_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "sEncrypt", "Z");
				return JNIEnv.GetStaticBooleanField (class_ref, sEncrypt_jfieldId);
			}
			set {
				if (sEncrypt_jfieldId == IntPtr.Zero)
					sEncrypt_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "sEncrypt", "Z");
				try {
					JNIEnv.SetStaticField (class_ref, sEncrypt_jfieldId, value);
				} finally {
				}
			}
		}

		static IntPtr sLatentWindow_jfieldId;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/field[@name='sLatentWindow']"
		[Register ("sLatentWindow")]
		public static int SLatentWindow {
			get {
				if (sLatentWindow_jfieldId == IntPtr.Zero)
					sLatentWindow_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "sLatentWindow", "I");
				return JNIEnv.GetStaticIntField (class_ref, sLatentWindow_jfieldId);
			}
			set {
				if (sLatentWindow_jfieldId == IntPtr.Zero)
					sLatentWindow_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "sLatentWindow", "I");
				try {
					JNIEnv.SetStaticField (class_ref, sLatentWindow_jfieldId, value);
				} finally {
				}
			}
		}
		internal static IntPtr java_class_handle;
		internal static IntPtr class_ref {
			get {
				return JNIEnv.FindClass ("com/umeng/analytics/AnalyticsConfig", ref java_class_handle);
			}
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return typeof (AnalyticsConfig); }
		}

		protected AnalyticsConfig (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		static IntPtr id_ctor;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/constructor[@name='AnalyticsConfig' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe AnalyticsConfig ()
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (Handle != IntPtr.Zero)
				return;

			try {
				if (GetType () != typeof (AnalyticsConfig)) {
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

		static IntPtr id_getAppkey_Landroid_content_Context_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/method[@name='getAppkey' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("getAppkey", "(Landroid/content/Context;)Ljava/lang/String;", "")]
		public static unsafe string GetAppkey (global::Android.Content.Context p0)
		{
			if (id_getAppkey_Landroid_content_Context_ == IntPtr.Zero)
				id_getAppkey_Landroid_content_Context_ = JNIEnv.GetStaticMethodID (class_ref, "getAppkey", "(Landroid/content/Context;)Ljava/lang/String;");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				string __ret = JNIEnv.GetString (JNIEnv.CallStaticObjectMethod  (class_ref, id_getAppkey_Landroid_content_Context_, __args), JniHandleOwnership.TransferLocalRef);
				return __ret;
			} finally {
			}
		}

		static IntPtr id_getChannel_Landroid_content_Context_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/method[@name='getChannel' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("getChannel", "(Landroid/content/Context;)Ljava/lang/String;", "")]
		public static unsafe string GetChannel (global::Android.Content.Context p0)
		{
			if (id_getChannel_Landroid_content_Context_ == IntPtr.Zero)
				id_getChannel_Landroid_content_Context_ = JNIEnv.GetStaticMethodID (class_ref, "getChannel", "(Landroid/content/Context;)Ljava/lang/String;");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				string __ret = JNIEnv.GetString (JNIEnv.CallStaticObjectMethod  (class_ref, id_getChannel_Landroid_content_Context_, __args), JniHandleOwnership.TransferLocalRef);
				return __ret;
			} finally {
			}
		}

		static IntPtr id_getLocation;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/method[@name='getLocation' and count(parameter)=0]"
		[Register ("getLocation", "()[D", "")]
		public static unsafe double[] GetLocation ()
		{
			if (id_getLocation == IntPtr.Zero)
				id_getLocation = JNIEnv.GetStaticMethodID (class_ref, "getLocation", "()[D");
			try {
				return (double[]) JNIEnv.GetArray (JNIEnv.CallStaticObjectMethod  (class_ref, id_getLocation), JniHandleOwnership.TransferLocalRef, typeof (double));
			} finally {
			}
		}

		static IntPtr id_getSDKVersion_Landroid_content_Context_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/method[@name='getSDKVersion' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("getSDKVersion", "(Landroid/content/Context;)Ljava/lang/String;", "")]
		public static unsafe string GetSDKVersion (global::Android.Content.Context p0)
		{
			if (id_getSDKVersion_Landroid_content_Context_ == IntPtr.Zero)
				id_getSDKVersion_Landroid_content_Context_ = JNIEnv.GetStaticMethodID (class_ref, "getSDKVersion", "(Landroid/content/Context;)Ljava/lang/String;");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				string __ret = JNIEnv.GetString (JNIEnv.CallStaticObjectMethod  (class_ref, id_getSDKVersion_Landroid_content_Context_, __args), JniHandleOwnership.TransferLocalRef);
				return __ret;
			} finally {
			}
		}

		static IntPtr id_getSecretKey_Landroid_content_Context_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/method[@name='getSecretKey' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("getSecretKey", "(Landroid/content/Context;)Ljava/lang/String;", "")]
		public static unsafe string GetSecretKey (global::Android.Content.Context p0)
		{
			if (id_getSecretKey_Landroid_content_Context_ == IntPtr.Zero)
				id_getSecretKey_Landroid_content_Context_ = JNIEnv.GetStaticMethodID (class_ref, "getSecretKey", "(Landroid/content/Context;)Ljava/lang/String;");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				string __ret = JNIEnv.GetString (JNIEnv.CallStaticObjectMethod  (class_ref, id_getSecretKey_Landroid_content_Context_, __args), JniHandleOwnership.TransferLocalRef);
				return __ret;
			} finally {
			}
		}

		static IntPtr id_getVerticalType_Landroid_content_Context_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='AnalyticsConfig']/method[@name='getVerticalType' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("getVerticalType", "(Landroid/content/Context;)I", "")]
		public static unsafe int GetVerticalType (global::Android.Content.Context p0)
		{
			if (id_getVerticalType_Landroid_content_Context_ == IntPtr.Zero)
				id_getVerticalType_Landroid_content_Context_ = JNIEnv.GetStaticMethodID (class_ref, "getVerticalType", "(Landroid/content/Context;)I");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				int __ret = JNIEnv.CallStaticIntMethod  (class_ref, id_getVerticalType_Landroid_content_Context_, __args);
				return __ret;
			} finally {
			}
		}

	}
}

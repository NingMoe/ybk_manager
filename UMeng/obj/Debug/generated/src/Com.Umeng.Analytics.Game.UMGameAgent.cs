using System;
using System.Collections.Generic;
using Android.Runtime;

namespace Com.Umeng.Analytics.Game {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']"
	[global::Android.Runtime.Register ("com/umeng/analytics/game/UMGameAgent", DoNotGenerateAcw=true)]
	public partial class UMGameAgent : global::Com.Umeng.Analytics.MobclickAgent {

		internal static new IntPtr java_class_handle;
		internal static new IntPtr class_ref {
			get {
				return JNIEnv.FindClass ("com/umeng/analytics/game/UMGameAgent", ref java_class_handle);
			}
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return typeof (UMGameAgent); }
		}

		protected UMGameAgent (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		static IntPtr id_ctor;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/constructor[@name='UMGameAgent' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe UMGameAgent ()
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (Handle != IntPtr.Zero)
				return;

			try {
				if (GetType () != typeof (UMGameAgent)) {
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

		static IntPtr id_bonus_DI;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='bonus' and count(parameter)=2 and parameter[1][@type='double'] and parameter[2][@type='int']]"
		[Register ("bonus", "(DI)V", "")]
		public static unsafe void Bonus (double p0, int p1)
		{
			if (id_bonus_DI == IntPtr.Zero)
				id_bonus_DI = JNIEnv.GetStaticMethodID (class_ref, "bonus", "(DI)V");
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (p1);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_bonus_DI, __args);
			} finally {
			}
		}

		static IntPtr id_bonus_Ljava_lang_String_IDI;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='bonus' and count(parameter)=4 and parameter[1][@type='java.lang.String'] and parameter[2][@type='int'] and parameter[3][@type='double'] and parameter[4][@type='int']]"
		[Register ("bonus", "(Ljava/lang/String;IDI)V", "")]
		public static unsafe void Bonus (string p0, int p1, double p2, int p3)
		{
			if (id_bonus_Ljava_lang_String_IDI == IntPtr.Zero)
				id_bonus_Ljava_lang_String_IDI = JNIEnv.GetStaticMethodID (class_ref, "bonus", "(Ljava/lang/String;IDI)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JValue* __args = stackalloc JValue [4];
				__args [0] = new JValue (native_p0);
				__args [1] = new JValue (p1);
				__args [2] = new JValue (p2);
				__args [3] = new JValue (p3);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_bonus_Ljava_lang_String_IDI, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static IntPtr id_buy_Ljava_lang_String_ID;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='buy' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='int'] and parameter[3][@type='double']]"
		[Register ("buy", "(Ljava/lang/String;ID)V", "")]
		public static unsafe void Buy (string p0, int p1, double p2)
		{
			if (id_buy_Ljava_lang_String_ID == IntPtr.Zero)
				id_buy_Ljava_lang_String_ID = JNIEnv.GetStaticMethodID (class_ref, "buy", "(Ljava/lang/String;ID)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JValue* __args = stackalloc JValue [3];
				__args [0] = new JValue (native_p0);
				__args [1] = new JValue (p1);
				__args [2] = new JValue (p2);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_buy_Ljava_lang_String_ID, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static IntPtr id_exchange_DLjava_lang_String_DILjava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='exchange' and count(parameter)=5 and parameter[1][@type='double'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='double'] and parameter[4][@type='int'] and parameter[5][@type='java.lang.String']]"
		[Register ("exchange", "(DLjava/lang/String;DILjava/lang/String;)V", "")]
		public static unsafe void Exchange (double p0, string p1, double p2, int p3, string p4)
		{
			if (id_exchange_DLjava_lang_String_DILjava_lang_String_ == IntPtr.Zero)
				id_exchange_DLjava_lang_String_DILjava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "exchange", "(DLjava/lang/String;DILjava/lang/String;)V");
			IntPtr native_p1 = JNIEnv.NewString (p1);
			IntPtr native_p4 = JNIEnv.NewString (p4);
			try {
				JValue* __args = stackalloc JValue [5];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				__args [2] = new JValue (p2);
				__args [3] = new JValue (p3);
				__args [4] = new JValue (native_p4);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_exchange_DLjava_lang_String_DILjava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p4);
			}
		}

		static IntPtr id_failLevel_Ljava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='failLevel' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("failLevel", "(Ljava/lang/String;)V", "")]
		public static unsafe void FailLevel (string p0)
		{
			if (id_failLevel_Ljava_lang_String_ == IntPtr.Zero)
				id_failLevel_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "failLevel", "(Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (native_p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_failLevel_Ljava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static IntPtr id_finishLevel_Ljava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='finishLevel' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("finishLevel", "(Ljava/lang/String;)V", "")]
		public static unsafe void FinishLevel (string p0)
		{
			if (id_finishLevel_Ljava_lang_String_ == IntPtr.Zero)
				id_finishLevel_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "finishLevel", "(Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (native_p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_finishLevel_Ljava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static IntPtr id_init_Landroid_content_Context_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='init' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("init", "(Landroid/content/Context;)V", "")]
		public static unsafe void Init (global::Android.Content.Context p0)
		{
			if (id_init_Landroid_content_Context_ == IntPtr.Zero)
				id_init_Landroid_content_Context_ = JNIEnv.GetStaticMethodID (class_ref, "init", "(Landroid/content/Context;)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_init_Landroid_content_Context_, __args);
			} finally {
			}
		}

		static IntPtr id_onEvent_Ljava_lang_String_Ljava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='onEvent' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String']]"
		[Register ("onEvent", "(Ljava/lang/String;Ljava/lang/String;)V", "")]
		public static unsafe void OnEvent (string p0, string p1)
		{
			if (id_onEvent_Ljava_lang_String_Ljava_lang_String_ == IntPtr.Zero)
				id_onEvent_Ljava_lang_String_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "onEvent", "(Ljava/lang/String;Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (native_p0);
				__args [1] = new JValue (native_p1);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onEvent_Ljava_lang_String_Ljava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		static IntPtr id_onSocialEvent_Landroid_content_Context_arrayLcom_umeng_analytics_social_UMPlatformData_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='onSocialEvent' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='com.umeng.analytics.social.UMPlatformData...']]"
		[Register ("onSocialEvent", "(Landroid/content/Context;[Lcom/umeng/analytics/social/UMPlatformData;)V", "")]
		public static unsafe void OnSocialEvent (global::Android.Content.Context p0, params global:: Com.Umeng.Analytics.Social.UMPlatformData[] p1)
		{
			if (id_onSocialEvent_Landroid_content_Context_arrayLcom_umeng_analytics_social_UMPlatformData_ == IntPtr.Zero)
				id_onSocialEvent_Landroid_content_Context_arrayLcom_umeng_analytics_social_UMPlatformData_ = JNIEnv.GetStaticMethodID (class_ref, "onSocialEvent", "(Landroid/content/Context;[Lcom/umeng/analytics/social/UMPlatformData;)V");
			IntPtr native_p1 = JNIEnv.NewArray (p1);
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onSocialEvent_Landroid_content_Context_arrayLcom_umeng_analytics_social_UMPlatformData_, __args);
			} finally {
				if (p1 != null) {
					JNIEnv.CopyArray (native_p1, p1);
					JNIEnv.DeleteLocalRef (native_p1);
				}
			}
		}

		static IntPtr id_onSocialEvent_Landroid_content_Context_Ljava_lang_String_arrayLcom_umeng_analytics_social_UMPlatformData_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='onSocialEvent' and count(parameter)=3 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='com.umeng.analytics.social.UMPlatformData...']]"
		[Register ("onSocialEvent", "(Landroid/content/Context;Ljava/lang/String;[Lcom/umeng/analytics/social/UMPlatformData;)V", "")]
		public static unsafe void OnSocialEvent (global::Android.Content.Context p0, string p1, params global:: Com.Umeng.Analytics.Social.UMPlatformData[] p2)
		{
			if (id_onSocialEvent_Landroid_content_Context_Ljava_lang_String_arrayLcom_umeng_analytics_social_UMPlatformData_ == IntPtr.Zero)
				id_onSocialEvent_Landroid_content_Context_Ljava_lang_String_arrayLcom_umeng_analytics_social_UMPlatformData_ = JNIEnv.GetStaticMethodID (class_ref, "onSocialEvent", "(Landroid/content/Context;Ljava/lang/String;[Lcom/umeng/analytics/social/UMPlatformData;)V");
			IntPtr native_p1 = JNIEnv.NewString (p1);
			IntPtr native_p2 = JNIEnv.NewArray (p2);
			try {
				JValue* __args = stackalloc JValue [3];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				__args [2] = new JValue (native_p2);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onSocialEvent_Landroid_content_Context_Ljava_lang_String_arrayLcom_umeng_analytics_social_UMPlatformData_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				if (p2 != null) {
					JNIEnv.CopyArray (native_p2, p2);
					JNIEnv.DeleteLocalRef (native_p2);
				}
			}
		}

		static IntPtr id_pay_DDI;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='pay' and count(parameter)=3 and parameter[1][@type='double'] and parameter[2][@type='double'] and parameter[3][@type='int']]"
		[Register ("pay", "(DDI)V", "")]
		public static unsafe void Pay (double p0, double p1, int p2)
		{
			if (id_pay_DDI == IntPtr.Zero)
				id_pay_DDI = JNIEnv.GetStaticMethodID (class_ref, "pay", "(DDI)V");
			try {
				JValue* __args = stackalloc JValue [3];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (p1);
				__args [2] = new JValue (p2);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_pay_DDI, __args);
			} finally {
			}
		}

		static IntPtr id_pay_DLjava_lang_String_IDI;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='pay' and count(parameter)=5 and parameter[1][@type='double'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='int'] and parameter[4][@type='double'] and parameter[5][@type='int']]"
		[Register ("pay", "(DLjava/lang/String;IDI)V", "")]
		public static unsafe void Pay (double p0, string p1, int p2, double p3, int p4)
		{
			if (id_pay_DLjava_lang_String_IDI == IntPtr.Zero)
				id_pay_DLjava_lang_String_IDI = JNIEnv.GetStaticMethodID (class_ref, "pay", "(DLjava/lang/String;IDI)V");
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JValue* __args = stackalloc JValue [5];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				__args [2] = new JValue (p2);
				__args [3] = new JValue (p3);
				__args [4] = new JValue (p4);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_pay_DLjava_lang_String_IDI, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		static IntPtr id_setPlayerLevel_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='setPlayerLevel' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("setPlayerLevel", "(I)V", "")]
		public static unsafe void SetPlayerLevel (int p0)
		{
			if (id_setPlayerLevel_I == IntPtr.Zero)
				id_setPlayerLevel_I = JNIEnv.GetStaticMethodID (class_ref, "setPlayerLevel", "(I)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_setPlayerLevel_I, __args);
			} finally {
			}
		}

		static IntPtr id_setTraceSleepTime_Z;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='setTraceSleepTime' and count(parameter)=1 and parameter[1][@type='boolean']]"
		[Register ("setTraceSleepTime", "(Z)V", "")]
		public static unsafe void SetTraceSleepTime (bool p0)
		{
			if (id_setTraceSleepTime_Z == IntPtr.Zero)
				id_setTraceSleepTime_Z = JNIEnv.GetStaticMethodID (class_ref, "setTraceSleepTime", "(Z)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_setTraceSleepTime_Z, __args);
			} finally {
			}
		}

		static IntPtr id_startLevel_Ljava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='startLevel' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("startLevel", "(Ljava/lang/String;)V", "")]
		public static unsafe void StartLevel (string p0)
		{
			if (id_startLevel_Ljava_lang_String_ == IntPtr.Zero)
				id_startLevel_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "startLevel", "(Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (native_p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_startLevel_Ljava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static IntPtr id_use_Ljava_lang_String_ID;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.game']/class[@name='UMGameAgent']/method[@name='use' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='int'] and parameter[3][@type='double']]"
		[Register ("use", "(Ljava/lang/String;ID)V", "")]
		public static unsafe void Use (string p0, int p1, double p2)
		{
			if (id_use_Ljava_lang_String_ID == IntPtr.Zero)
				id_use_Ljava_lang_String_ID = JNIEnv.GetStaticMethodID (class_ref, "use", "(Ljava/lang/String;ID)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JValue* __args = stackalloc JValue [3];
				__args [0] = new JValue (native_p0);
				__args [1] = new JValue (p1);
				__args [2] = new JValue (p2);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_use_Ljava_lang_String_ID, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

	}
}

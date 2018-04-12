using System;
using System.Collections.Generic;
using Android.Runtime;

namespace Com.Umeng.Analytics {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']"
	[global::Android.Runtime.Register ("com/umeng/analytics/MobclickAgent", DoNotGenerateAcw=true)]
	public partial class MobclickAgent : global::Java.Lang.Object {

		// Metadata.xml XPath class reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.EScenarioType']"
		[global::Android.Runtime.Register ("com/umeng/analytics/MobclickAgent$EScenarioType", DoNotGenerateAcw=true)]
		public sealed partial class EScenarioType : global::Java.Lang.Enum {


			static IntPtr E_UM_ANALYTICS_OEM_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.EScenarioType']/field[@name='E_UM_ANALYTICS_OEM']"
			[Register ("E_UM_ANALYTICS_OEM")]
			public static global::Com.Umeng.Analytics.MobclickAgent.EScenarioType EUmAnalyticsOem {
				get {
					if (E_UM_ANALYTICS_OEM_jfieldId == IntPtr.Zero)
						E_UM_ANALYTICS_OEM_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "E_UM_ANALYTICS_OEM", "Lcom/umeng/analytics/MobclickAgent$EScenarioType;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, E_UM_ANALYTICS_OEM_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.MobclickAgent.EScenarioType> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}

			static IntPtr E_UM_GAME_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.EScenarioType']/field[@name='E_UM_GAME']"
			[Register ("E_UM_GAME")]
			public static global::Com.Umeng.Analytics.MobclickAgent.EScenarioType EUmGame {
				get {
					if (E_UM_GAME_jfieldId == IntPtr.Zero)
						E_UM_GAME_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "E_UM_GAME", "Lcom/umeng/analytics/MobclickAgent$EScenarioType;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, E_UM_GAME_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.MobclickAgent.EScenarioType> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}

			static IntPtr E_UM_GAME_OEM_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.EScenarioType']/field[@name='E_UM_GAME_OEM']"
			[Register ("E_UM_GAME_OEM")]
			public static global::Com.Umeng.Analytics.MobclickAgent.EScenarioType EUmGameOem {
				get {
					if (E_UM_GAME_OEM_jfieldId == IntPtr.Zero)
						E_UM_GAME_OEM_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "E_UM_GAME_OEM", "Lcom/umeng/analytics/MobclickAgent$EScenarioType;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, E_UM_GAME_OEM_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.MobclickAgent.EScenarioType> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}

			static IntPtr E_UM_NORMAL_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.EScenarioType']/field[@name='E_UM_NORMAL']"
			[Register ("E_UM_NORMAL")]
			public static global::Com.Umeng.Analytics.MobclickAgent.EScenarioType EUmNormal {
				get {
					if (E_UM_NORMAL_jfieldId == IntPtr.Zero)
						E_UM_NORMAL_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "E_UM_NORMAL", "Lcom/umeng/analytics/MobclickAgent$EScenarioType;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, E_UM_NORMAL_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.MobclickAgent.EScenarioType> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}
			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/umeng/analytics/MobclickAgent$EScenarioType", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (EScenarioType); }
			}

			internal EScenarioType (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_toValue;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.EScenarioType']/method[@name='toValue' and count(parameter)=0]"
			[Register ("toValue", "()I", "")]
			public unsafe int ToValue ()
			{
				if (id_toValue == IntPtr.Zero)
					id_toValue = JNIEnv.GetMethodID (class_ref, "toValue", "()I");
				try {
					return JNIEnv.CallIntMethod  (Handle, id_toValue);
				} finally {
				}
			}

			static IntPtr id_valueOf_Ljava_lang_String_;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.EScenarioType']/method[@name='valueOf' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
			[Register ("valueOf", "(Ljava/lang/String;)Lcom/umeng/analytics/MobclickAgent$EScenarioType;", "")]
			public static unsafe global::Com.Umeng.Analytics.MobclickAgent.EScenarioType ValueOf (string p0)
			{
				if (id_valueOf_Ljava_lang_String_ == IntPtr.Zero)
					id_valueOf_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "valueOf", "(Ljava/lang/String;)Lcom/umeng/analytics/MobclickAgent$EScenarioType;");
				IntPtr native_p0 = JNIEnv.NewString (p0);
				try {
					JValue* __args = stackalloc JValue [1];
					__args [0] = new JValue (native_p0);
					global::Com.Umeng.Analytics.MobclickAgent.EScenarioType __ret = global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.MobclickAgent.EScenarioType> (JNIEnv.CallStaticObjectMethod  (class_ref, id_valueOf_Ljava_lang_String_, __args), JniHandleOwnership.TransferLocalRef);
					return __ret;
				} finally {
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}

			static IntPtr id_values;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.EScenarioType']/method[@name='values' and count(parameter)=0]"
			[Register ("values", "()[Lcom/umeng/analytics/MobclickAgent$EScenarioType;", "")]
			public static unsafe global::Com.Umeng.Analytics.MobclickAgent.EScenarioType[] Values ()
			{
				if (id_values == IntPtr.Zero)
					id_values = JNIEnv.GetStaticMethodID (class_ref, "values", "()[Lcom/umeng/analytics/MobclickAgent$EScenarioType;");
				try {
					return (global::Com.Umeng.Analytics.MobclickAgent.EScenarioType[]) JNIEnv.GetArray (JNIEnv.CallStaticObjectMethod  (class_ref, id_values), JniHandleOwnership.TransferLocalRef, typeof (global::Com.Umeng.Analytics.MobclickAgent.EScenarioType));
				} finally {
				}
			}

		}

		// Metadata.xml XPath class reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.UMAnalyticsConfig']"
		[global::Android.Runtime.Register ("com/umeng/analytics/MobclickAgent$UMAnalyticsConfig", DoNotGenerateAcw=true)]
		public partial class UMAnalyticsConfig : global::Java.Lang.Object {


			static IntPtr mAppkey_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.UMAnalyticsConfig']/field[@name='mAppkey']"
			[Register ("mAppkey")]
			public string MAppkey {
				get {
					if (mAppkey_jfieldId == IntPtr.Zero)
						mAppkey_jfieldId = JNIEnv.GetFieldID (class_ref, "mAppkey", "Ljava/lang/String;");
					IntPtr __ret = JNIEnv.GetObjectField (Handle, mAppkey_jfieldId);
					return JNIEnv.GetString (__ret, JniHandleOwnership.TransferLocalRef);
				}
				set {
					if (mAppkey_jfieldId == IntPtr.Zero)
						mAppkey_jfieldId = JNIEnv.GetFieldID (class_ref, "mAppkey", "Ljava/lang/String;");
					IntPtr native_value = JNIEnv.NewString (value);
					try {
						JNIEnv.SetField (Handle, mAppkey_jfieldId, native_value);
					} finally {
						JNIEnv.DeleteLocalRef (native_value);
					}
				}
			}

			static IntPtr mChannelId_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.UMAnalyticsConfig']/field[@name='mChannelId']"
			[Register ("mChannelId")]
			public string MChannelId {
				get {
					if (mChannelId_jfieldId == IntPtr.Zero)
						mChannelId_jfieldId = JNIEnv.GetFieldID (class_ref, "mChannelId", "Ljava/lang/String;");
					IntPtr __ret = JNIEnv.GetObjectField (Handle, mChannelId_jfieldId);
					return JNIEnv.GetString (__ret, JniHandleOwnership.TransferLocalRef);
				}
				set {
					if (mChannelId_jfieldId == IntPtr.Zero)
						mChannelId_jfieldId = JNIEnv.GetFieldID (class_ref, "mChannelId", "Ljava/lang/String;");
					IntPtr native_value = JNIEnv.NewString (value);
					try {
						JNIEnv.SetField (Handle, mChannelId_jfieldId, native_value);
					} finally {
						JNIEnv.DeleteLocalRef (native_value);
					}
				}
			}

			static IntPtr mContext_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.UMAnalyticsConfig']/field[@name='mContext']"
			[Register ("mContext")]
			public global::Android.Content.Context MContext {
				get {
					if (mContext_jfieldId == IntPtr.Zero)
						mContext_jfieldId = JNIEnv.GetFieldID (class_ref, "mContext", "Landroid/content/Context;");
					IntPtr __ret = JNIEnv.GetObjectField (Handle, mContext_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Android.Content.Context> (__ret, JniHandleOwnership.TransferLocalRef);
				}
				set {
					if (mContext_jfieldId == IntPtr.Zero)
						mContext_jfieldId = JNIEnv.GetFieldID (class_ref, "mContext", "Landroid/content/Context;");
					IntPtr native_value = JNIEnv.ToLocalJniHandle (value);
					try {
						JNIEnv.SetField (Handle, mContext_jfieldId, native_value);
					} finally {
						JNIEnv.DeleteLocalRef (native_value);
					}
				}
			}

			static IntPtr mIsCrashEnable_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.UMAnalyticsConfig']/field[@name='mIsCrashEnable']"
			[Register ("mIsCrashEnable")]
			public bool MIsCrashEnable {
				get {
					if (mIsCrashEnable_jfieldId == IntPtr.Zero)
						mIsCrashEnable_jfieldId = JNIEnv.GetFieldID (class_ref, "mIsCrashEnable", "Z");
					return JNIEnv.GetBooleanField (Handle, mIsCrashEnable_jfieldId);
				}
				set {
					if (mIsCrashEnable_jfieldId == IntPtr.Zero)
						mIsCrashEnable_jfieldId = JNIEnv.GetFieldID (class_ref, "mIsCrashEnable", "Z");
					try {
						JNIEnv.SetField (Handle, mIsCrashEnable_jfieldId, value);
					} finally {
					}
				}
			}

			static IntPtr mType_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.UMAnalyticsConfig']/field[@name='mType']"
			[Register ("mType")]
			public global::Com.Umeng.Analytics.MobclickAgent.EScenarioType MType {
				get {
					if (mType_jfieldId == IntPtr.Zero)
						mType_jfieldId = JNIEnv.GetFieldID (class_ref, "mType", "Lcom/umeng/analytics/MobclickAgent$EScenarioType;");
					IntPtr __ret = JNIEnv.GetObjectField (Handle, mType_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.MobclickAgent.EScenarioType> (__ret, JniHandleOwnership.TransferLocalRef);
				}
				set {
					if (mType_jfieldId == IntPtr.Zero)
						mType_jfieldId = JNIEnv.GetFieldID (class_ref, "mType", "Lcom/umeng/analytics/MobclickAgent$EScenarioType;");
					IntPtr native_value = JNIEnv.ToLocalJniHandle (value);
					try {
						JNIEnv.SetField (Handle, mType_jfieldId, native_value);
					} finally {
						JNIEnv.DeleteLocalRef (native_value);
					}
				}
			}
			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/umeng/analytics/MobclickAgent$UMAnalyticsConfig", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (UMAnalyticsConfig); }
			}

			protected UMAnalyticsConfig (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_Lcom_umeng_analytics_MobclickAgent_EScenarioType_Z;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.UMAnalyticsConfig']/constructor[@name='MobclickAgent.UMAnalyticsConfig' and count(parameter)=5 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.lang.String'] and parameter[4][@type='com.umeng.analytics.MobclickAgent.EScenarioType'] and parameter[5][@type='boolean']]"
			[Register (".ctor", "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Lcom/umeng/analytics/MobclickAgent$EScenarioType;Z)V", "")]
			public unsafe UMAnalyticsConfig (global::Android.Content.Context p0, string p1, string p2, global::Com.Umeng.Analytics.MobclickAgent.EScenarioType p3, bool p4)
				: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				IntPtr native_p1 = JNIEnv.NewString (p1);
				IntPtr native_p2 = JNIEnv.NewString (p2);
				try {
					JValue* __args = stackalloc JValue [5];
					__args [0] = new JValue (p0);
					__args [1] = new JValue (native_p1);
					__args [2] = new JValue (native_p2);
					__args [3] = new JValue (p3);
					__args [4] = new JValue (p4);
					if (GetType () != typeof (UMAnalyticsConfig)) {
						SetHandle (
								global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Lcom/umeng/analytics/MobclickAgent$EScenarioType;Z)V", __args),
								JniHandleOwnership.TransferLocalRef);
						global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Lcom/umeng/analytics/MobclickAgent$EScenarioType;Z)V", __args);
						return;
					}

					if (id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_Lcom_umeng_analytics_MobclickAgent_EScenarioType_Z == IntPtr.Zero)
						id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_Lcom_umeng_analytics_MobclickAgent_EScenarioType_Z = JNIEnv.GetMethodID (class_ref, "<init>", "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Lcom/umeng/analytics/MobclickAgent$EScenarioType;Z)V");
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_Lcom_umeng_analytics_MobclickAgent_EScenarioType_Z, __args),
							JniHandleOwnership.TransferLocalRef);
					JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_Lcom_umeng_analytics_MobclickAgent_EScenarioType_Z, __args);
				} finally {
					JNIEnv.DeleteLocalRef (native_p1);
					JNIEnv.DeleteLocalRef (native_p2);
				}
			}

			static IntPtr id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_Lcom_umeng_analytics_MobclickAgent_EScenarioType_;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.UMAnalyticsConfig']/constructor[@name='MobclickAgent.UMAnalyticsConfig' and count(parameter)=4 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.lang.String'] and parameter[4][@type='com.umeng.analytics.MobclickAgent.EScenarioType']]"
			[Register (".ctor", "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Lcom/umeng/analytics/MobclickAgent$EScenarioType;)V", "")]
			public unsafe UMAnalyticsConfig (global::Android.Content.Context p0, string p1, string p2, global::Com.Umeng.Analytics.MobclickAgent.EScenarioType p3)
				: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				IntPtr native_p1 = JNIEnv.NewString (p1);
				IntPtr native_p2 = JNIEnv.NewString (p2);
				try {
					JValue* __args = stackalloc JValue [4];
					__args [0] = new JValue (p0);
					__args [1] = new JValue (native_p1);
					__args [2] = new JValue (native_p2);
					__args [3] = new JValue (p3);
					if (GetType () != typeof (UMAnalyticsConfig)) {
						SetHandle (
								global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Lcom/umeng/analytics/MobclickAgent$EScenarioType;)V", __args),
								JniHandleOwnership.TransferLocalRef);
						global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Lcom/umeng/analytics/MobclickAgent$EScenarioType;)V", __args);
						return;
					}

					if (id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_Lcom_umeng_analytics_MobclickAgent_EScenarioType_ == IntPtr.Zero)
						id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_Lcom_umeng_analytics_MobclickAgent_EScenarioType_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Lcom/umeng/analytics/MobclickAgent$EScenarioType;)V");
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_Lcom_umeng_analytics_MobclickAgent_EScenarioType_, __args),
							JniHandleOwnership.TransferLocalRef);
					JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_Lcom_umeng_analytics_MobclickAgent_EScenarioType_, __args);
				} finally {
					JNIEnv.DeleteLocalRef (native_p1);
					JNIEnv.DeleteLocalRef (native_p2);
				}
			}

			static IntPtr id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_;
			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent.UMAnalyticsConfig']/constructor[@name='MobclickAgent.UMAnalyticsConfig' and count(parameter)=3 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.lang.String']]"
			[Register (".ctor", "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V", "")]
			public unsafe UMAnalyticsConfig (global::Android.Content.Context p0, string p1, string p2)
				: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				if (Handle != IntPtr.Zero)
					return;

				IntPtr native_p1 = JNIEnv.NewString (p1);
				IntPtr native_p2 = JNIEnv.NewString (p2);
				try {
					JValue* __args = stackalloc JValue [3];
					__args [0] = new JValue (p0);
					__args [1] = new JValue (native_p1);
					__args [2] = new JValue (native_p2);
					if (GetType () != typeof (UMAnalyticsConfig)) {
						SetHandle (
								global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V", __args),
								JniHandleOwnership.TransferLocalRef);
						global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V", __args);
						return;
					}

					if (id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_ == IntPtr.Zero)
						id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V");
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_, __args),
							JniHandleOwnership.TransferLocalRef);
					JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_, __args);
				} finally {
					JNIEnv.DeleteLocalRef (native_p1);
					JNIEnv.DeleteLocalRef (native_p2);
				}
			}

		}

		internal static IntPtr java_class_handle;
		internal static IntPtr class_ref {
			get {
				return JNIEnv.FindClass ("com/umeng/analytics/MobclickAgent", ref java_class_handle);
			}
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return typeof (MobclickAgent); }
		}

		protected MobclickAgent (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		static IntPtr id_ctor;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/constructor[@name='MobclickAgent' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe MobclickAgent ()
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (Handle != IntPtr.Zero)
				return;

			try {
				if (GetType () != typeof (MobclickAgent)) {
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

		static IntPtr id_enableEncrypt_Z;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='enableEncrypt' and count(parameter)=1 and parameter[1][@type='boolean']]"
		[Register ("enableEncrypt", "(Z)V", "")]
		public static unsafe void EnableEncrypt (bool p0)
		{
			if (id_enableEncrypt_Z == IntPtr.Zero)
				id_enableEncrypt_Z = JNIEnv.GetStaticMethodID (class_ref, "enableEncrypt", "(Z)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_enableEncrypt_Z, __args);
			} finally {
			}
		}

		static IntPtr id_onEvent_Landroid_content_Context_Ljava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onEvent' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String']]"
		[Register ("onEvent", "(Landroid/content/Context;Ljava/lang/String;)V", "")]
		public static unsafe void OnEvent (global::Android.Content.Context p0, string p1)
		{
			if (id_onEvent_Landroid_content_Context_Ljava_lang_String_ == IntPtr.Zero)
				id_onEvent_Landroid_content_Context_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "onEvent", "(Landroid/content/Context;Ljava/lang/String;)V");
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onEvent_Landroid_content_Context_Ljava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		static IntPtr id_onEvent_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onEvent' and count(parameter)=3 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.lang.String']]"
		[Register ("onEvent", "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V", "")]
		public static unsafe void OnEvent (global::Android.Content.Context p0, string p1, string p2)
		{
			if (id_onEvent_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_ == IntPtr.Zero)
				id_onEvent_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "onEvent", "(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V");
			IntPtr native_p1 = JNIEnv.NewString (p1);
			IntPtr native_p2 = JNIEnv.NewString (p2);
			try {
				JValue* __args = stackalloc JValue [3];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				__args [2] = new JValue (native_p2);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onEvent_Landroid_content_Context_Ljava_lang_String_Ljava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p2);
			}
		}

		static IntPtr id_onEvent_Landroid_content_Context_Ljava_lang_String_Ljava_util_Map_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onEvent' and count(parameter)=3 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.util.Map&lt;java.lang.String, java.lang.String&gt;']]"
		[Register ("onEvent", "(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;)V", "")]
		public static unsafe void OnEvent (global::Android.Content.Context p0, string p1, global::System.Collections.Generic.IDictionary<string, string> p2)
		{
			if (id_onEvent_Landroid_content_Context_Ljava_lang_String_Ljava_util_Map_ == IntPtr.Zero)
				id_onEvent_Landroid_content_Context_Ljava_lang_String_Ljava_util_Map_ = JNIEnv.GetStaticMethodID (class_ref, "onEvent", "(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;)V");
			IntPtr native_p1 = JNIEnv.NewString (p1);
			IntPtr native_p2 = global::Android.Runtime.JavaDictionary<string, string>.ToLocalJniHandle (p2);
			try {
				JValue* __args = stackalloc JValue [3];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				__args [2] = new JValue (native_p2);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onEvent_Landroid_content_Context_Ljava_lang_String_Ljava_util_Map_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p2);
			}
		}

		static IntPtr id_onEvent_Landroid_content_Context_Ljava_util_List_ILjava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onEvent' and count(parameter)=4 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.util.List&lt;java.lang.String&gt;'] and parameter[3][@type='int'] and parameter[4][@type='java.lang.String']]"
		[Register ("onEvent", "(Landroid/content/Context;Ljava/util/List;ILjava/lang/String;)V", "")]
		public static unsafe void OnEvent (global::Android.Content.Context p0, global::System.Collections.Generic.IList<string> p1, int p2, string p3)
		{
			if (id_onEvent_Landroid_content_Context_Ljava_util_List_ILjava_lang_String_ == IntPtr.Zero)
				id_onEvent_Landroid_content_Context_Ljava_util_List_ILjava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "onEvent", "(Landroid/content/Context;Ljava/util/List;ILjava/lang/String;)V");
			IntPtr native_p1 = global::Android.Runtime.JavaList<string>.ToLocalJniHandle (p1);
			IntPtr native_p3 = JNIEnv.NewString (p3);
			try {
				JValue* __args = stackalloc JValue [4];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				__args [2] = new JValue (p2);
				__args [3] = new JValue (native_p3);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onEvent_Landroid_content_Context_Ljava_util_List_ILjava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p3);
			}
		}

		static IntPtr id_onEventValue_Landroid_content_Context_Ljava_lang_String_Ljava_util_Map_I;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onEventValue' and count(parameter)=4 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.util.Map&lt;java.lang.String, java.lang.String&gt;'] and parameter[4][@type='int']]"
		[Register ("onEventValue", "(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;I)V", "")]
		public static unsafe void OnEventValue (global::Android.Content.Context p0, string p1, global::System.Collections.Generic.IDictionary<string, string> p2, int p3)
		{
			if (id_onEventValue_Landroid_content_Context_Ljava_lang_String_Ljava_util_Map_I == IntPtr.Zero)
				id_onEventValue_Landroid_content_Context_Ljava_lang_String_Ljava_util_Map_I = JNIEnv.GetStaticMethodID (class_ref, "onEventValue", "(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;I)V");
			IntPtr native_p1 = JNIEnv.NewString (p1);
			IntPtr native_p2 = global::Android.Runtime.JavaDictionary<string, string>.ToLocalJniHandle (p2);
			try {
				JValue* __args = stackalloc JValue [4];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				__args [2] = new JValue (native_p2);
				__args [3] = new JValue (p3);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onEventValue_Landroid_content_Context_Ljava_lang_String_Ljava_util_Map_I, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p2);
			}
		}

		static IntPtr id_onKillProcess_Landroid_content_Context_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onKillProcess' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("onKillProcess", "(Landroid/content/Context;)V", "")]
		public static unsafe void OnKillProcess (global::Android.Content.Context p0)
		{
			if (id_onKillProcess_Landroid_content_Context_ == IntPtr.Zero)
				id_onKillProcess_Landroid_content_Context_ = JNIEnv.GetStaticMethodID (class_ref, "onKillProcess", "(Landroid/content/Context;)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onKillProcess_Landroid_content_Context_, __args);
			} finally {
			}
		}

		static IntPtr id_onPageEnd_Ljava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onPageEnd' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("onPageEnd", "(Ljava/lang/String;)V", "")]
		public static unsafe void OnPageEnd (string p0)
		{
			if (id_onPageEnd_Ljava_lang_String_ == IntPtr.Zero)
				id_onPageEnd_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "onPageEnd", "(Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (native_p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onPageEnd_Ljava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static IntPtr id_onPageStart_Ljava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onPageStart' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("onPageStart", "(Ljava/lang/String;)V", "")]
		public static unsafe void OnPageStart (string p0)
		{
			if (id_onPageStart_Ljava_lang_String_ == IntPtr.Zero)
				id_onPageStart_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "onPageStart", "(Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (native_p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onPageStart_Ljava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static IntPtr id_onPause_Landroid_content_Context_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onPause' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("onPause", "(Landroid/content/Context;)V", "")]
		public static unsafe void OnPause (global::Android.Content.Context p0)
		{
			if (id_onPause_Landroid_content_Context_ == IntPtr.Zero)
				id_onPause_Landroid_content_Context_ = JNIEnv.GetStaticMethodID (class_ref, "onPause", "(Landroid/content/Context;)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onPause_Landroid_content_Context_, __args);
			} finally {
			}
		}

		static IntPtr id_onProfileSignIn_Ljava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onProfileSignIn' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("onProfileSignIn", "(Ljava/lang/String;)V", "")]
		public static unsafe void OnProfileSignIn (string p0)
		{
			if (id_onProfileSignIn_Ljava_lang_String_ == IntPtr.Zero)
				id_onProfileSignIn_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "onProfileSignIn", "(Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (native_p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onProfileSignIn_Ljava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static IntPtr id_onProfileSignIn_Ljava_lang_String_Ljava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onProfileSignIn' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String']]"
		[Register ("onProfileSignIn", "(Ljava/lang/String;Ljava/lang/String;)V", "")]
		public static unsafe void OnProfileSignIn (string p0, string p1)
		{
			if (id_onProfileSignIn_Ljava_lang_String_Ljava_lang_String_ == IntPtr.Zero)
				id_onProfileSignIn_Ljava_lang_String_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "onProfileSignIn", "(Ljava/lang/String;Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (native_p0);
				__args [1] = new JValue (native_p1);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onProfileSignIn_Ljava_lang_String_Ljava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		static IntPtr id_onProfileSignOff;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onProfileSignOff' and count(parameter)=0]"
		[Register ("onProfileSignOff", "()V", "")]
		public static unsafe void OnProfileSignOff ()
		{
			if (id_onProfileSignOff == IntPtr.Zero)
				id_onProfileSignOff = JNIEnv.GetStaticMethodID (class_ref, "onProfileSignOff", "()V");
			try {
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onProfileSignOff);
			} finally {
			}
		}

		static IntPtr id_onResume_Landroid_content_Context_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onResume' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("onResume", "(Landroid/content/Context;)V", "")]
		public static unsafe void OnResume (global::Android.Content.Context p0)
		{
			if (id_onResume_Landroid_content_Context_ == IntPtr.Zero)
				id_onResume_Landroid_content_Context_ = JNIEnv.GetStaticMethodID (class_ref, "onResume", "(Landroid/content/Context;)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_onResume_Landroid_content_Context_, __args);
			} finally {
			}
		}

		static IntPtr id_onSocialEvent_Landroid_content_Context_arrayLcom_umeng_analytics_social_UMPlatformData_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onSocialEvent' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='com.umeng.analytics.social.UMPlatformData...']]"
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
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='onSocialEvent' and count(parameter)=3 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='com.umeng.analytics.social.UMPlatformData...']]"
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

		static IntPtr id_openActivityDurationTrack_Z;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='openActivityDurationTrack' and count(parameter)=1 and parameter[1][@type='boolean']]"
		[Register ("openActivityDurationTrack", "(Z)V", "")]
		public static unsafe void OpenActivityDurationTrack (bool p0)
		{
			if (id_openActivityDurationTrack_Z == IntPtr.Zero)
				id_openActivityDurationTrack_Z = JNIEnv.GetStaticMethodID (class_ref, "openActivityDurationTrack", "(Z)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_openActivityDurationTrack_Z, __args);
			} finally {
			}
		}

		static IntPtr id_reportError_Landroid_content_Context_Ljava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='reportError' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String']]"
		[Register ("reportError", "(Landroid/content/Context;Ljava/lang/String;)V", "")]
		public static unsafe void ReportError (global::Android.Content.Context p0, string p1)
		{
			if (id_reportError_Landroid_content_Context_Ljava_lang_String_ == IntPtr.Zero)
				id_reportError_Landroid_content_Context_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "reportError", "(Landroid/content/Context;Ljava/lang/String;)V");
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_reportError_Landroid_content_Context_Ljava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		static IntPtr id_reportError_Landroid_content_Context_Ljava_lang_Throwable_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='reportError' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.Throwable']]"
		[Register ("reportError", "(Landroid/content/Context;Ljava/lang/Throwable;)V", "")]
		public static unsafe void ReportError (global::Android.Content.Context p0, global::Java.Lang.Throwable p1)
		{
			if (id_reportError_Landroid_content_Context_Ljava_lang_Throwable_ == IntPtr.Zero)
				id_reportError_Landroid_content_Context_Ljava_lang_Throwable_ = JNIEnv.GetStaticMethodID (class_ref, "reportError", "(Landroid/content/Context;Ljava/lang/Throwable;)V");
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (p1);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_reportError_Landroid_content_Context_Ljava_lang_Throwable_, __args);
			} finally {
			}
		}

		static IntPtr id_setCatchUncaughtExceptions_Z;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='setCatchUncaughtExceptions' and count(parameter)=1 and parameter[1][@type='boolean']]"
		[Register ("setCatchUncaughtExceptions", "(Z)V", "")]
		public static unsafe void SetCatchUncaughtExceptions (bool p0)
		{
			if (id_setCatchUncaughtExceptions_Z == IntPtr.Zero)
				id_setCatchUncaughtExceptions_Z = JNIEnv.GetStaticMethodID (class_ref, "setCatchUncaughtExceptions", "(Z)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_setCatchUncaughtExceptions_Z, __args);
			} finally {
			}
		}

		static IntPtr id_setCheckDevice_Z;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='setCheckDevice' and count(parameter)=1 and parameter[1][@type='boolean']]"
		[Register ("setCheckDevice", "(Z)V", "")]
		public static unsafe void SetCheckDevice (bool p0)
		{
			if (id_setCheckDevice_Z == IntPtr.Zero)
				id_setCheckDevice_Z = JNIEnv.GetStaticMethodID (class_ref, "setCheckDevice", "(Z)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_setCheckDevice_Z, __args);
			} finally {
			}
		}

		static IntPtr id_setDebugMode_Z;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='setDebugMode' and count(parameter)=1 and parameter[1][@type='boolean']]"
		[Register ("setDebugMode", "(Z)V", "")]
		public static unsafe void SetDebugMode (bool p0)
		{
			if (id_setDebugMode_Z == IntPtr.Zero)
				id_setDebugMode_Z = JNIEnv.GetStaticMethodID (class_ref, "setDebugMode", "(Z)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_setDebugMode_Z, __args);
			} finally {
			}
		}

		static IntPtr id_setLatencyWindow_J;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='setLatencyWindow' and count(parameter)=1 and parameter[1][@type='long']]"
		[Register ("setLatencyWindow", "(J)V", "")]
		public static unsafe void SetLatencyWindow (long p0)
		{
			if (id_setLatencyWindow_J == IntPtr.Zero)
				id_setLatencyWindow_J = JNIEnv.GetStaticMethodID (class_ref, "setLatencyWindow", "(J)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_setLatencyWindow_J, __args);
			} finally {
			}
		}

		static IntPtr id_setLocation_DD;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='setLocation' and count(parameter)=2 and parameter[1][@type='double'] and parameter[2][@type='double']]"
		[Register ("setLocation", "(DD)V", "")]
		public static unsafe void SetLocation (double p0, double p1)
		{
			if (id_setLocation_DD == IntPtr.Zero)
				id_setLocation_DD = JNIEnv.GetStaticMethodID (class_ref, "setLocation", "(DD)V");
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (p1);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_setLocation_DD, __args);
			} finally {
			}
		}

		static IntPtr id_setOpenGLContext_Ljavax_microedition_khronos_opengles_GL10_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='setOpenGLContext' and count(parameter)=1 and parameter[1][@type='javax.microedition.khronos.opengles.GL10']]"
		[Register ("setOpenGLContext", "(Ljavax/microedition/khronos/opengles/GL10;)V", "")]
		public static unsafe void SetOpenGLContext (global::Javax.Microedition.Khronos.Opengles.IGL10 p0)
		{
			if (id_setOpenGLContext_Ljavax_microedition_khronos_opengles_GL10_ == IntPtr.Zero)
				id_setOpenGLContext_Ljavax_microedition_khronos_opengles_GL10_ = JNIEnv.GetStaticMethodID (class_ref, "setOpenGLContext", "(Ljavax/microedition/khronos/opengles/GL10;)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_setOpenGLContext_Ljavax_microedition_khronos_opengles_GL10_, __args);
			} finally {
			}
		}

		static IntPtr id_setScenarioType_Landroid_content_Context_Lcom_umeng_analytics_MobclickAgent_EScenarioType_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='setScenarioType' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='com.umeng.analytics.MobclickAgent.EScenarioType']]"
		[Register ("setScenarioType", "(Landroid/content/Context;Lcom/umeng/analytics/MobclickAgent$EScenarioType;)V", "")]
		public static unsafe void SetScenarioType (global::Android.Content.Context p0, global::Com.Umeng.Analytics.MobclickAgent.EScenarioType p1)
		{
			if (id_setScenarioType_Landroid_content_Context_Lcom_umeng_analytics_MobclickAgent_EScenarioType_ == IntPtr.Zero)
				id_setScenarioType_Landroid_content_Context_Lcom_umeng_analytics_MobclickAgent_EScenarioType_ = JNIEnv.GetStaticMethodID (class_ref, "setScenarioType", "(Landroid/content/Context;Lcom/umeng/analytics/MobclickAgent$EScenarioType;)V");
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (p1);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_setScenarioType_Landroid_content_Context_Lcom_umeng_analytics_MobclickAgent_EScenarioType_, __args);
			} finally {
			}
		}

		static IntPtr id_setSecret_Landroid_content_Context_Ljava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='setSecret' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String']]"
		[Register ("setSecret", "(Landroid/content/Context;Ljava/lang/String;)V", "")]
		public static unsafe void SetSecret (global::Android.Content.Context p0, string p1)
		{
			if (id_setSecret_Landroid_content_Context_Ljava_lang_String_ == IntPtr.Zero)
				id_setSecret_Landroid_content_Context_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "setSecret", "(Landroid/content/Context;Ljava/lang/String;)V");
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_setSecret_Landroid_content_Context_Ljava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		static IntPtr id_setSessionContinueMillis_J;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='setSessionContinueMillis' and count(parameter)=1 and parameter[1][@type='long']]"
		[Register ("setSessionContinueMillis", "(J)V", "")]
		public static unsafe void SetSessionContinueMillis (long p0)
		{
			if (id_setSessionContinueMillis_J == IntPtr.Zero)
				id_setSessionContinueMillis_J = JNIEnv.GetStaticMethodID (class_ref, "setSessionContinueMillis", "(J)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_setSessionContinueMillis_J, __args);
			} finally {
			}
		}

		static IntPtr id_startWithConfigure_Lcom_umeng_analytics_MobclickAgent_UMAnalyticsConfig_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics']/class[@name='MobclickAgent']/method[@name='startWithConfigure' and count(parameter)=1 and parameter[1][@type='com.umeng.analytics.MobclickAgent.UMAnalyticsConfig']]"
		[Register ("startWithConfigure", "(Lcom/umeng/analytics/MobclickAgent$UMAnalyticsConfig;)V", "")]
		public static unsafe void StartWithConfigure (global::Com.Umeng.Analytics.MobclickAgent.UMAnalyticsConfig p0)
		{
			if (id_startWithConfigure_Lcom_umeng_analytics_MobclickAgent_UMAnalyticsConfig_ == IntPtr.Zero)
				id_startWithConfigure_Lcom_umeng_analytics_MobclickAgent_UMAnalyticsConfig_ = JNIEnv.GetStaticMethodID (class_ref, "startWithConfigure", "(Lcom/umeng/analytics/MobclickAgent$UMAnalyticsConfig;)V");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				JNIEnv.CallStaticVoidMethod  (class_ref, id_startWithConfigure_Lcom_umeng_analytics_MobclickAgent_UMAnalyticsConfig_, __args);
			} finally {
			}
		}

	}
}

using System;
using System.Collections.Generic;
using Android.Runtime;

namespace Com.Umeng.Analytics.Social {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData']"
	[global::Android.Runtime.Register ("com/umeng/analytics/social/UMPlatformData", DoNotGenerateAcw=true)]
	public partial class UMPlatformData : global::Java.Lang.Object {

		// Metadata.xml XPath class reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.GENDER']"
		[global::Android.Runtime.Register ("com/umeng/analytics/social/UMPlatformData$GENDER", DoNotGenerateAcw=true)]
		public partial class GENDER : global::Java.Lang.Enum {


			static IntPtr FEMALE_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.GENDER']/field[@name='FEMALE']"
			[Register ("FEMALE")]
			public static global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER Female {
				get {
					if (FEMALE_jfieldId == IntPtr.Zero)
						FEMALE_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "FEMALE", "Lcom/umeng/analytics/social/UMPlatformData$GENDER;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, FEMALE_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}

			static IntPtr MALE_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.GENDER']/field[@name='MALE']"
			[Register ("MALE")]
			public static global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER Male {
				get {
					if (MALE_jfieldId == IntPtr.Zero)
						MALE_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "MALE", "Lcom/umeng/analytics/social/UMPlatformData$GENDER;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, MALE_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}

			static IntPtr value_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.GENDER']/field[@name='value']"
			[Register ("value")]
			public int Value {
				get {
					if (value_jfieldId == IntPtr.Zero)
						value_jfieldId = JNIEnv.GetFieldID (class_ref, "value", "I");
					return JNIEnv.GetIntField (Handle, value_jfieldId);
				}
				set {
					if (value_jfieldId == IntPtr.Zero)
						value_jfieldId = JNIEnv.GetFieldID (class_ref, "value", "I");
					try {
						JNIEnv.SetField (Handle, value_jfieldId, value);
					} finally {
					}
				}
			}
			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/umeng/analytics/social/UMPlatformData$GENDER", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (GENDER); }
			}

			protected GENDER (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_valueOf_Ljava_lang_String_;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.GENDER']/method[@name='valueOf' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
			[Register ("valueOf", "(Ljava/lang/String;)Lcom/umeng/analytics/social/UMPlatformData$GENDER;", "")]
			public static unsafe global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER ValueOf (string p0)
			{
				if (id_valueOf_Ljava_lang_String_ == IntPtr.Zero)
					id_valueOf_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "valueOf", "(Ljava/lang/String;)Lcom/umeng/analytics/social/UMPlatformData$GENDER;");
				IntPtr native_p0 = JNIEnv.NewString (p0);
				try {
					JValue* __args = stackalloc JValue [1];
					__args [0] = new JValue (native_p0);
					global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER __ret = global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER> (JNIEnv.CallStaticObjectMethod  (class_ref, id_valueOf_Ljava_lang_String_, __args), JniHandleOwnership.TransferLocalRef);
					return __ret;
				} finally {
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}

			static IntPtr id_values;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.GENDER']/method[@name='values' and count(parameter)=0]"
			[Register ("values", "()[Lcom/umeng/analytics/social/UMPlatformData$GENDER;", "")]
			public static unsafe global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER[] Values ()
			{
				if (id_values == IntPtr.Zero)
					id_values = JNIEnv.GetStaticMethodID (class_ref, "values", "()[Lcom/umeng/analytics/social/UMPlatformData$GENDER;");
				try {
					return (global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER[]) JNIEnv.GetArray (JNIEnv.CallStaticObjectMethod  (class_ref, id_values), JniHandleOwnership.TransferLocalRef, typeof (global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER));
				} finally {
				}
			}

		}

		// Metadata.xml XPath class reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.UMedia']"
		[global::Android.Runtime.Register ("com/umeng/analytics/social/UMPlatformData$UMedia", DoNotGenerateAcw=true)]
		public partial class UMedia : global::Java.Lang.Enum {


			static IntPtr DOUBAN_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.UMedia']/field[@name='DOUBAN']"
			[Register ("DOUBAN")]
			public static global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia Douban {
				get {
					if (DOUBAN_jfieldId == IntPtr.Zero)
						DOUBAN_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "DOUBAN", "Lcom/umeng/analytics/social/UMPlatformData$UMedia;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, DOUBAN_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}

			static IntPtr RENREN_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.UMedia']/field[@name='RENREN']"
			[Register ("RENREN")]
			public static global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia Renren {
				get {
					if (RENREN_jfieldId == IntPtr.Zero)
						RENREN_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "RENREN", "Lcom/umeng/analytics/social/UMPlatformData$UMedia;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, RENREN_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}

			static IntPtr SINA_WEIBO_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.UMedia']/field[@name='SINA_WEIBO']"
			[Register ("SINA_WEIBO")]
			public static global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia SinaWeibo {
				get {
					if (SINA_WEIBO_jfieldId == IntPtr.Zero)
						SINA_WEIBO_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "SINA_WEIBO", "Lcom/umeng/analytics/social/UMPlatformData$UMedia;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, SINA_WEIBO_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}

			static IntPtr TENCENT_QQ_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.UMedia']/field[@name='TENCENT_QQ']"
			[Register ("TENCENT_QQ")]
			public static global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia TencentQq {
				get {
					if (TENCENT_QQ_jfieldId == IntPtr.Zero)
						TENCENT_QQ_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "TENCENT_QQ", "Lcom/umeng/analytics/social/UMPlatformData$UMedia;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, TENCENT_QQ_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}

			static IntPtr TENCENT_QZONE_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.UMedia']/field[@name='TENCENT_QZONE']"
			[Register ("TENCENT_QZONE")]
			public static global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia TencentQzone {
				get {
					if (TENCENT_QZONE_jfieldId == IntPtr.Zero)
						TENCENT_QZONE_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "TENCENT_QZONE", "Lcom/umeng/analytics/social/UMPlatformData$UMedia;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, TENCENT_QZONE_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}

			static IntPtr TENCENT_WEIBO_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.UMedia']/field[@name='TENCENT_WEIBO']"
			[Register ("TENCENT_WEIBO")]
			public static global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia TencentWeibo {
				get {
					if (TENCENT_WEIBO_jfieldId == IntPtr.Zero)
						TENCENT_WEIBO_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "TENCENT_WEIBO", "Lcom/umeng/analytics/social/UMPlatformData$UMedia;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, TENCENT_WEIBO_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}

			static IntPtr WEIXIN_CIRCLE_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.UMedia']/field[@name='WEIXIN_CIRCLE']"
			[Register ("WEIXIN_CIRCLE")]
			public static global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia WeixinCircle {
				get {
					if (WEIXIN_CIRCLE_jfieldId == IntPtr.Zero)
						WEIXIN_CIRCLE_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "WEIXIN_CIRCLE", "Lcom/umeng/analytics/social/UMPlatformData$UMedia;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, WEIXIN_CIRCLE_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}

			static IntPtr WEIXIN_FRIENDS_jfieldId;

			// Metadata.xml XPath field reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.UMedia']/field[@name='WEIXIN_FRIENDS']"
			[Register ("WEIXIN_FRIENDS")]
			public static global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia WeixinFriends {
				get {
					if (WEIXIN_FRIENDS_jfieldId == IntPtr.Zero)
						WEIXIN_FRIENDS_jfieldId = JNIEnv.GetStaticFieldID (class_ref, "WEIXIN_FRIENDS", "Lcom/umeng/analytics/social/UMPlatformData$UMedia;");
					IntPtr __ret = JNIEnv.GetStaticObjectField (class_ref, WEIXIN_FRIENDS_jfieldId);
					return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia> (__ret, JniHandleOwnership.TransferLocalRef);
				}
			}
			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/umeng/analytics/social/UMPlatformData$UMedia", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (UMedia); }
			}

			protected UMedia (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static IntPtr id_valueOf_Ljava_lang_String_;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.UMedia']/method[@name='valueOf' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
			[Register ("valueOf", "(Ljava/lang/String;)Lcom/umeng/analytics/social/UMPlatformData$UMedia;", "")]
			public static unsafe global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia ValueOf (string p0)
			{
				if (id_valueOf_Ljava_lang_String_ == IntPtr.Zero)
					id_valueOf_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "valueOf", "(Ljava/lang/String;)Lcom/umeng/analytics/social/UMPlatformData$UMedia;");
				IntPtr native_p0 = JNIEnv.NewString (p0);
				try {
					JValue* __args = stackalloc JValue [1];
					__args [0] = new JValue (native_p0);
					global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia __ret = global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia> (JNIEnv.CallStaticObjectMethod  (class_ref, id_valueOf_Ljava_lang_String_, __args), JniHandleOwnership.TransferLocalRef);
					return __ret;
				} finally {
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}

			static IntPtr id_values;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData.UMedia']/method[@name='values' and count(parameter)=0]"
			[Register ("values", "()[Lcom/umeng/analytics/social/UMPlatformData$UMedia;", "")]
			public static unsafe global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia[] Values ()
			{
				if (id_values == IntPtr.Zero)
					id_values = JNIEnv.GetStaticMethodID (class_ref, "values", "()[Lcom/umeng/analytics/social/UMPlatformData$UMedia;");
				try {
					return (global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia[]) JNIEnv.GetArray (JNIEnv.CallStaticObjectMethod  (class_ref, id_values), JniHandleOwnership.TransferLocalRef, typeof (global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia));
				} finally {
				}
			}

		}

		internal static IntPtr java_class_handle;
		internal static IntPtr class_ref {
			get {
				return JNIEnv.FindClass ("com/umeng/analytics/social/UMPlatformData", ref java_class_handle);
			}
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return typeof (UMPlatformData); }
		}

		protected UMPlatformData (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		static IntPtr id_ctor_Lcom_umeng_analytics_social_UMPlatformData_UMedia_Ljava_lang_String_;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData']/constructor[@name='UMPlatformData' and count(parameter)=2 and parameter[1][@type='com.umeng.analytics.social.UMPlatformData.UMedia'] and parameter[2][@type='java.lang.String']]"
		[Register (".ctor", "(Lcom/umeng/analytics/social/UMPlatformData$UMedia;Ljava/lang/String;)V", "")]
		public unsafe UMPlatformData (global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia p0, string p1)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (Handle != IntPtr.Zero)
				return;

			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (native_p1);
				if (GetType () != typeof (UMPlatformData)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "(Lcom/umeng/analytics/social/UMPlatformData$UMedia;Ljava/lang/String;)V", __args),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (Handle, "(Lcom/umeng/analytics/social/UMPlatformData$UMedia;Ljava/lang/String;)V", __args);
					return;
				}

				if (id_ctor_Lcom_umeng_analytics_social_UMPlatformData_UMedia_Ljava_lang_String_ == IntPtr.Zero)
					id_ctor_Lcom_umeng_analytics_social_UMPlatformData_UMedia_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "<init>", "(Lcom/umeng/analytics/social/UMPlatformData$UMedia;Ljava/lang/String;)V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor_Lcom_umeng_analytics_social_UMPlatformData_UMedia_Ljava_lang_String_, __args),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (Handle, class_ref, id_ctor_Lcom_umeng_analytics_social_UMPlatformData_UMedia_Ljava_lang_String_, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		static Delegate cb_getGender;
#pragma warning disable 0169
		static Delegate GetGetGenderHandler ()
		{
			if (cb_getGender == null)
				cb_getGender = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetGender);
			return cb_getGender;
		}

		static IntPtr n_GetGender (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Umeng.Analytics.Social.UMPlatformData __this = global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.Gender);
		}
#pragma warning restore 0169

		static Delegate cb_setGender_Lcom_umeng_analytics_social_UMPlatformData_GENDER_;
#pragma warning disable 0169
		static Delegate GetSetGender_Lcom_umeng_analytics_social_UMPlatformData_GENDER_Handler ()
		{
			if (cb_setGender_Lcom_umeng_analytics_social_UMPlatformData_GENDER_ == null)
				cb_setGender_Lcom_umeng_analytics_social_UMPlatformData_GENDER_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SetGender_Lcom_umeng_analytics_social_UMPlatformData_GENDER_);
			return cb_setGender_Lcom_umeng_analytics_social_UMPlatformData_GENDER_;
		}

		static void n_SetGender_Lcom_umeng_analytics_social_UMPlatformData_GENDER_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Umeng.Analytics.Social.UMPlatformData __this = global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER p0 = global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.Gender = p0;
		}
#pragma warning restore 0169

		static IntPtr id_getGender;
		static IntPtr id_setGender_Lcom_umeng_analytics_social_UMPlatformData_GENDER_;
		public virtual unsafe global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER Gender {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData']/method[@name='getGender' and count(parameter)=0]"
			[Register ("getGender", "()Lcom/umeng/analytics/social/UMPlatformData$GENDER;", "GetGetGenderHandler")]
			get {
				if (id_getGender == IntPtr.Zero)
					id_getGender = JNIEnv.GetMethodID (class_ref, "getGender", "()Lcom/umeng/analytics/social/UMPlatformData$GENDER;");
				try {

					if (GetType () == ThresholdType)
						return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER> (JNIEnv.CallObjectMethod  (Handle, id_getGender), JniHandleOwnership.TransferLocalRef);
					else
						return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.GENDER> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getGender", "()Lcom/umeng/analytics/social/UMPlatformData$GENDER;")), JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData']/method[@name='setGender' and count(parameter)=1 and parameter[1][@type='com.umeng.analytics.social.UMPlatformData.GENDER']]"
			[Register ("setGender", "(Lcom/umeng/analytics/social/UMPlatformData$GENDER;)V", "GetSetGender_Lcom_umeng_analytics_social_UMPlatformData_GENDER_Handler")]
			set {
				if (id_setGender_Lcom_umeng_analytics_social_UMPlatformData_GENDER_ == IntPtr.Zero)
					id_setGender_Lcom_umeng_analytics_social_UMPlatformData_GENDER_ = JNIEnv.GetMethodID (class_ref, "setGender", "(Lcom/umeng/analytics/social/UMPlatformData$GENDER;)V");
				try {
					JValue* __args = stackalloc JValue [1];
					__args [0] = new JValue (value);

					if (GetType () == ThresholdType)
						JNIEnv.CallVoidMethod  (Handle, id_setGender_Lcom_umeng_analytics_social_UMPlatformData_GENDER_, __args);
					else
						JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setGender", "(Lcom/umeng/analytics/social/UMPlatformData$GENDER;)V"), __args);
				} finally {
				}
			}
		}

		static Delegate cb_isValid;
#pragma warning disable 0169
		static Delegate GetIsValidHandler ()
		{
			if (cb_isValid == null)
				cb_isValid = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, bool>) n_IsValid);
			return cb_isValid;
		}

		static bool n_IsValid (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Umeng.Analytics.Social.UMPlatformData __this = global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.IsValid;
		}
#pragma warning restore 0169

		static IntPtr id_isValid;
		public virtual unsafe bool IsValid {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData']/method[@name='isValid' and count(parameter)=0]"
			[Register ("isValid", "()Z", "GetIsValidHandler")]
			get {
				if (id_isValid == IntPtr.Zero)
					id_isValid = JNIEnv.GetMethodID (class_ref, "isValid", "()Z");
				try {

					if (GetType () == ThresholdType)
						return JNIEnv.CallBooleanMethod  (Handle, id_isValid);
					else
						return JNIEnv.CallNonvirtualBooleanMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "isValid", "()Z"));
				} finally {
				}
			}
		}

		static Delegate cb_getMeida;
#pragma warning disable 0169
		static Delegate GetGetMeidaHandler ()
		{
			if (cb_getMeida == null)
				cb_getMeida = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetMeida);
			return cb_getMeida;
		}

		static IntPtr n_GetMeida (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Umeng.Analytics.Social.UMPlatformData __this = global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.Meida);
		}
#pragma warning restore 0169

		static IntPtr id_getMeida;
		public virtual unsafe global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia Meida {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData']/method[@name='getMeida' and count(parameter)=0]"
			[Register ("getMeida", "()Lcom/umeng/analytics/social/UMPlatformData$UMedia;", "GetGetMeidaHandler")]
			get {
				if (id_getMeida == IntPtr.Zero)
					id_getMeida = JNIEnv.GetMethodID (class_ref, "getMeida", "()Lcom/umeng/analytics/social/UMPlatformData$UMedia;");
				try {

					if (GetType () == ThresholdType)
						return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia> (JNIEnv.CallObjectMethod  (Handle, id_getMeida), JniHandleOwnership.TransferLocalRef);
					else
						return global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData.UMedia> (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getMeida", "()Lcom/umeng/analytics/social/UMPlatformData$UMedia;")), JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}

		static Delegate cb_getName;
#pragma warning disable 0169
		static Delegate GetGetNameHandler ()
		{
			if (cb_getName == null)
				cb_getName = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetName);
			return cb_getName;
		}

		static IntPtr n_GetName (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Umeng.Analytics.Social.UMPlatformData __this = global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.Name);
		}
#pragma warning restore 0169

		static Delegate cb_setName_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetSetName_Ljava_lang_String_Handler ()
		{
			if (cb_setName_Ljava_lang_String_ == null)
				cb_setName_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SetName_Ljava_lang_String_);
			return cb_setName_Ljava_lang_String_;
		}

		static void n_SetName_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Umeng.Analytics.Social.UMPlatformData __this = global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.Name = p0;
		}
#pragma warning restore 0169

		static IntPtr id_getName;
		static IntPtr id_setName_Ljava_lang_String_;
		public virtual unsafe string Name {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData']/method[@name='getName' and count(parameter)=0]"
			[Register ("getName", "()Ljava/lang/String;", "GetGetNameHandler")]
			get {
				if (id_getName == IntPtr.Zero)
					id_getName = JNIEnv.GetMethodID (class_ref, "getName", "()Ljava/lang/String;");
				try {

					if (GetType () == ThresholdType)
						return JNIEnv.GetString (JNIEnv.CallObjectMethod  (Handle, id_getName), JniHandleOwnership.TransferLocalRef);
					else
						return JNIEnv.GetString (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getName", "()Ljava/lang/String;")), JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData']/method[@name='setName' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
			[Register ("setName", "(Ljava/lang/String;)V", "GetSetName_Ljava_lang_String_Handler")]
			set {
				if (id_setName_Ljava_lang_String_ == IntPtr.Zero)
					id_setName_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "setName", "(Ljava/lang/String;)V");
				IntPtr native_value = JNIEnv.NewString (value);
				try {
					JValue* __args = stackalloc JValue [1];
					__args [0] = new JValue (native_value);

					if (GetType () == ThresholdType)
						JNIEnv.CallVoidMethod  (Handle, id_setName_Ljava_lang_String_, __args);
					else
						JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setName", "(Ljava/lang/String;)V"), __args);
				} finally {
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}

		static Delegate cb_getUsid;
#pragma warning disable 0169
		static Delegate GetGetUsidHandler ()
		{
			if (cb_getUsid == null)
				cb_getUsid = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetUsid);
			return cb_getUsid;
		}

		static IntPtr n_GetUsid (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Umeng.Analytics.Social.UMPlatformData __this = global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.Usid);
		}
#pragma warning restore 0169

		static IntPtr id_getUsid;
		public virtual unsafe string Usid {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData']/method[@name='getUsid' and count(parameter)=0]"
			[Register ("getUsid", "()Ljava/lang/String;", "GetGetUsidHandler")]
			get {
				if (id_getUsid == IntPtr.Zero)
					id_getUsid = JNIEnv.GetMethodID (class_ref, "getUsid", "()Ljava/lang/String;");
				try {

					if (GetType () == ThresholdType)
						return JNIEnv.GetString (JNIEnv.CallObjectMethod  (Handle, id_getUsid), JniHandleOwnership.TransferLocalRef);
					else
						return JNIEnv.GetString (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getUsid", "()Ljava/lang/String;")), JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}

		static Delegate cb_getWeiboId;
#pragma warning disable 0169
		static Delegate GetGetWeiboIdHandler ()
		{
			if (cb_getWeiboId == null)
				cb_getWeiboId = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetWeiboId);
			return cb_getWeiboId;
		}

		static IntPtr n_GetWeiboId (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Umeng.Analytics.Social.UMPlatformData __this = global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.WeiboId);
		}
#pragma warning restore 0169

		static Delegate cb_setWeiboId_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetSetWeiboId_Ljava_lang_String_Handler ()
		{
			if (cb_setWeiboId_Ljava_lang_String_ == null)
				cb_setWeiboId_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SetWeiboId_Ljava_lang_String_);
			return cb_setWeiboId_Ljava_lang_String_;
		}

		static void n_SetWeiboId_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Umeng.Analytics.Social.UMPlatformData __this = global::Java.Lang.Object.GetObject<global::Com.Umeng.Analytics.Social.UMPlatformData> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.WeiboId = p0;
		}
#pragma warning restore 0169

		static IntPtr id_getWeiboId;
		static IntPtr id_setWeiboId_Ljava_lang_String_;
		public virtual unsafe string WeiboId {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData']/method[@name='getWeiboId' and count(parameter)=0]"
			[Register ("getWeiboId", "()Ljava/lang/String;", "GetGetWeiboIdHandler")]
			get {
				if (id_getWeiboId == IntPtr.Zero)
					id_getWeiboId = JNIEnv.GetMethodID (class_ref, "getWeiboId", "()Ljava/lang/String;");
				try {

					if (GetType () == ThresholdType)
						return JNIEnv.GetString (JNIEnv.CallObjectMethod  (Handle, id_getWeiboId), JniHandleOwnership.TransferLocalRef);
					else
						return JNIEnv.GetString (JNIEnv.CallNonvirtualObjectMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "getWeiboId", "()Ljava/lang/String;")), JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
			// Metadata.xml XPath method reference: path="/api/package[@name='com.umeng.analytics.social']/class[@name='UMPlatformData']/method[@name='setWeiboId' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
			[Register ("setWeiboId", "(Ljava/lang/String;)V", "GetSetWeiboId_Ljava_lang_String_Handler")]
			set {
				if (id_setWeiboId_Ljava_lang_String_ == IntPtr.Zero)
					id_setWeiboId_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "setWeiboId", "(Ljava/lang/String;)V");
				IntPtr native_value = JNIEnv.NewString (value);
				try {
					JValue* __args = stackalloc JValue [1];
					__args [0] = new JValue (native_value);

					if (GetType () == ThresholdType)
						JNIEnv.CallVoidMethod  (Handle, id_setWeiboId_Ljava_lang_String_, __args);
					else
						JNIEnv.CallNonvirtualVoidMethod  (Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "setWeiboId", "(Ljava/lang/String;)V"), __args);
				} finally {
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}

	}
}

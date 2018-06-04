using System;
namespace DataService
{
	public class Config
	{
		public Config()
		{

		}
		#region 测试
		//public static string UpocManagerUserUrl = "http://xytest.staff.xdf.cn/ApiMember0311/";
		//public static string UpocCommonUrl = "http://xytest.staff.xdf.cn/ApiClass/";
		//public static string SpocApiUrl = "http://xytext.staff.xdf.cn/upoc/";
		////续班api（正式环境）
		//public static string RenewApiUrl = "http://xuban.xdf.cn/renew/index/";
		////用户头像api（正式环境）
		//public static string AvatarApiUrl = "http://hudong.dev.staff.xdf.cn/API_Avatar/imgUrl";

		//public static string AppId = "5001";
		//public static string AppKey = "v5appkey_test";

		//public static string EncodeKey = "u2_test_aesK_test_test_test_test";
		//#region 是否开启友盟，true-是；false-否
		//public static bool UMengIsOpen = false;
		//#endregion

		////优播课管理端H5页面
		//public static string UpocManagerH5 = "http://xytest.staff.xdf.cn/upocmanagerh5/";

		#endregion

		#region 正式
		public static string UpocManagerUserUrl = "http://Member.i.xdf.cn/";
		public static string UpocCommonUrl = "http://Class.i.xdf.cn/";
		public static string SpocApiUrl = "http://i.xdf.cn/upoc/";
		//续班api（正式环境）
		public static string RenewApiUrl = "http://xuban.xdf.cn/renew/index/";
		//用户头像api（正式环境）
		public static string AvatarApiUrl = "http://my.xdf.cn/API_Avatar/imgUrl";

		public static string AppId = "1004";
		public static string AppKey = "v5appkey_xf_865fn$xa";

		public static string EncodeKey = "en32xamarindses81be0dc39232-mtfs";

		#region 是否开启友盟，true-是；false-否
		public static bool UMengIsOpen = true;
		#endregion

		//优播课管理端H5页面
		public static string UpocManagerH5 = "http://i.xdf.cn/upocmanagerh5/";
		#endregion

	}
}

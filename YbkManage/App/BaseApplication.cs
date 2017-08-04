using System;
using System.Collections.Generic;
using Android.App;
using Android.Runtime;
using DataEntity;

namespace YbkManage.App
{
	#if DEBUG
	    [Application(Debuggable = true)]
	#else
		[Application(Debuggable = false)]
	#endif
    public class BaseApplication : Application
    {
        /// <summary>
        /// http请求帮助类
        /// </summary>
        //public IHttwrapClient HttpHelper ;


        private static BaseApplication singletonApplication;

        public static BaseApplication GetInstance()
        {
            return singletonApplication;
        }


        public BaseApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            singletonApplication = this;

            //IHttwrapConfiguration configuration = new HttwrapConfiguration(AppConfig.API_Get_HOST());
            //HttpHelper = new HttwrapClient(configuration);
        }

        // 报表的筛选条件
        public List<QuarterEntity> quarterList;
        public List<GradeEntity> gradeList;
        public List<DistrictEntity> districtList;
    }
}

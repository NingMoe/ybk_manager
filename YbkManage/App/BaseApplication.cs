using System;
using Android.App;
using Android.Runtime;

namespace YbkManage.App
{
    [Application]
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


    }
}

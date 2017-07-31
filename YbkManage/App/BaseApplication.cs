using System;
using System.Collections.Generic;
using Android.App;
using Android.Runtime;
using DataEntity;
using YbkManage.Models;

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

		/// <summary>
		/// 教研组
		/// </summary>
		private static List<TeacherScopeEntity> teacherScopeList = new List<TeacherScopeEntity>();
		public List<TeacherScopeEntity> GetTeacherScopeList()
		{
			if (teacherScopeList == null || teacherScopeList.Count == 0)
			{
				teacherScopeList = new List<TeacherScopeEntity>();
				teacherScopeList.Add(new TeacherScopeEntity { Id = 152, ScopeName = "高中化学" });
				teacherScopeList.Add(new TeacherScopeEntity { Id = 153, ScopeName = "高中生物" });
				teacherScopeList.Add(new TeacherScopeEntity { Id = 154, ScopeName = "高中数学" });
				teacherScopeList.Add(new TeacherScopeEntity { Id = 155, ScopeName = "高中文综" });
				teacherScopeList.Add(new TeacherScopeEntity { Id = 156, ScopeName = "高中物理" });
				teacherScopeList.Add(new TeacherScopeEntity { Id = 157, ScopeName = "高中英语" });
				teacherScopeList.Add(new TeacherScopeEntity { Id = 158, ScopeName = "高中语文" });
				teacherScopeList.Add(new TeacherScopeEntity { Id = 159, ScopeName = "优才教育" });
			}
			return teacherScopeList;
		}

		/// <summary>
		/// 教师角色
		/// </summary>
		private static List<TeacherRoleEntity> teacherRoleList = new List<TeacherRoleEntity>();
		public List<TeacherRoleEntity> GetTeacherRoleList()
		{
			if (teacherRoleList == null || teacherRoleList.Count == 0)
			{
				teacherRoleList = new List<TeacherRoleEntity>();
				teacherRoleList.Add(new TeacherRoleEntity { RoleId = 2, RoleName = "教师" });
				teacherRoleList.Add(new TeacherRoleEntity { RoleId = 22, RoleName = "教学区长" });
				teacherRoleList.Add(new TeacherRoleEntity { RoleId = 23, RoleName = "教学主管" });
			}
			return teacherRoleList;
		}

		// 报表的筛选条件
		public List<QuarterEntity> quarterList;
		public List<GradeEntity> gradeList;
		public List<DistrictEntity> districtList;
    }
}

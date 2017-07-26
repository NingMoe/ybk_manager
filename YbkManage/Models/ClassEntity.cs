using System;
namespace YbkManage.Models
{
    /// <summary>
    /// 班级信息
    /// </summary>
    public class ClassEntity
    {
        /// <summary>
        /// 班级Id
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// 所属学校Id
        /// </summary>
        /// <value>The school identifier.</value>
		public int SchoolId { get; set; }

        /// <summary>
        /// 班级编码
        /// </summary>
        /// <value>The class code.</value>
		public string ClassCode { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        /// <value>The name of the class.</value>
		public string ClassName { get; set; }

        /// <summary>
        /// 开课时间
        /// </summary>
        /// <value>The begin date.</value>
		public string BeginDate { get; set; }

		/// <summary>
		/// 结课时间
		/// </summary>
		/// <value>The end date.</value>
		public string EndDate { get; set; }

		/// <summary>
		/// 上课地点
		/// </summary>
		/// <value>The print address.</value>
		public string PrintAddress { get; set; }

		public string PrintTime { get; set; }

        /// <summary>
        /// 上课老师
        /// </summary>
        /// <value>The teacher names.</value>
		public string TeacherNames { get; set; }

		public int Type { get; set; }
    }
}

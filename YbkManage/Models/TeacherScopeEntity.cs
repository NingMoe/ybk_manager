using System;
namespace YbkManage.Models
{
    /// <summary>
    /// 教研组
    /// </summary>
    public class TeacherScopeEntity
    {
		/// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

		/// <summary>
        /// 所属学校Id
        /// </summary>
        /// <value>The school identifier.</value>
        public int SchoolId { get; set; }

		/// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
		public string ScopeName { get; set; }

		/// <summary>
        /// 教师数量
        /// </summary>
        /// <value>The teacher count.</value>
        public int TeacherCount { get; set; }
    }
}

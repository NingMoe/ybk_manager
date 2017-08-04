using System;
using System.ComponentModel;

namespace DataEntity
{
	public enum UserType
	{
		[Description("学生")]
		Student = 1,

		[Description("教师")]
		Teacher = 2,

		[Description("访客")]
		Guest = 5,

		[Description("家长")]
		Parent = 9,

		[Description("教学经理")]
		TeacherManager = 24,

		[Description("教学主管")]
		TeacherDirector = 23,

		[Description("教学区长")]
		TeacherArea = 22,

		[Description("助教主管")]
		AssistantDirector = 122,

		[Description("助教组长")]
		AssistantLeader = 121,

		[Description("助教")]
		Staff = 120,

		[Description("一级管理者")]
		Manager = 200
	}

}

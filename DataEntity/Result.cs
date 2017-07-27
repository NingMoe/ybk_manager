using System;
namespace DataEntity
{
	/// <summary>
	/// 接口返回值结构
	/// </summary>
	[Serializable]
	public class Result
	{
		public int State { get; set; }
		public object Data { get; set; }
		public string Error { get; set; }
		public int DataCount { get; set; }
	}
	/// <summary>
	/// 接口返回值结构-泛型
	/// </summary>
	[Serializable]
	public class Result<T>
	{
		public int State { get; set; }
		public T Data { get; set; }
		public string Error { get; set; }
		public int DataCount { get; set; }
	}
}

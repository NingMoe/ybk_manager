using System;
namespace DataEntity
{
	[Serializable]
	public class ShopManagerList
	{
		public string Name { get; set; }
		public string UserId { get; set; }
		public string Email { get; set; }
		public string AreaCodes { get; set; }
		public string AreaNames { get; set; }
		public string Avatar { get; set; }

	}
}

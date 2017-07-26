using System;
namespace YbkManage.Models
{
    public class QuarterEntity
    {
		public int Year { get; set; }
		public int Quarter { get; set; }
        public string QuarterName { get; set; }
        public bool IsCurrent { get; set; }
    }
}

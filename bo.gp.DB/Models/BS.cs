using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace bo.gp.DB.Models
{
	[Table("BS")]
	public class BS
	{
		public Int64 UserID { get; set; }
		public Int64 eventTime { get; set; }
		public int eventCode { get; set; } = 56;
		public int tnaEvent { get; set; } = 0;
		public string Code { get; set; }
		public string IP { get; set; }

		public override string ToString()
		{
			return $"{string.Format("{0,-15}", IP.ToString())}|{string.Format("{0,-10}", UserID.ToString())}|{eventCode}|{eventTime}|{tnaEvent}|{string.Format("{0,-10}", Code)}|";
		}

	}
}

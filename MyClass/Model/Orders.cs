using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace MyClass.Model
{
	[Table("Orders")]
	public class Orders
	{
		[Key]
		public int Id { get; set; }
		public int UserID { get; set; }
		[Required]
		public string ReceiveraAddress { get; set; }
		[Required]
		public int ReceiverPhone { get; set; }
		public string Note { get; set; }
		public DateTime CreateAt { get; set; }
		public int CreateBy { get; set; }
		public DateTime UpdateAt { get; set; }
		public int UpdateBy { get; set; }
		public int Status { get; set; }
	}
}

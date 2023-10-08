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
	[Table("OrderDetails")]
	public class OrderDetails
	{
		[Key]
		public int Id { get; set; }
		public int OrderID { get; set; }
		public int ProductID { get; set; }
		public decimal Price { get; set; }
		public decimal Amount { get; set; }
	}
}

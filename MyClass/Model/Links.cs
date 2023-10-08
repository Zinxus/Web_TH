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
	[Table("Links")]
	public class Links
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public int TableID { get; set; }
		public string Type { get; set; }
	}
}

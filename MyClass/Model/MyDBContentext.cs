using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
	public class MyDBContentext : DbContext
	{
		public MyDBContentext() : base("name = strConnect") { }
		public DbSet<Categories> Categories { get; set; }
		public DbSet<Contacts> Contacts { get; set; }
		public DbSet<Links> Links { get; set; }
		public DbSet<Menus> Menus { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }
		public DbSet<Orders> Orders { get; set; }
		public DbSet<Pots> Pots { get; set; }
		public DbSet<Products> Products { get; set; }
		public DbSet<Sliders> Sliders { get; set; }
		public DbSet<Suppliers> Suppliers { get; set; }
		public DbSet<Topics> Topics { get; set; }
		public DbSet<Users> Users { get; set; }
		
	}
}

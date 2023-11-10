using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
	public class SuppliersDao
	{
		private MyDBContext db = new MyDBContext();
		//Select *From ...
		public List<Suppliers> getlist()
		{
			return db.Suppliers.ToList();
		}

		//Index chi coi stasus 1,2
		public List<Suppliers> getlist(string status = "ALL")
		{
			List<Suppliers> list = null;
			switch (status)
			{
				case "Index"://1 2
					{
						list = db.Suppliers.Where(m => m.Status != 0).ToList();
						break;
					}
				case "Trash"://0
					{
						list = db.Suppliers.Where(m => m.Status == 0).ToList();
						break;
					}
				default:
					{
						list = db.Suppliers.ToList();
						break;
					}
			}
			return list;
		}
		//details
		public Suppliers getRow(int? id)
		{
			if (id == null)
			{
				return null;
			}
			else
			{
				return db.Suppliers.Find(id);
			}
		}
		//create
		public int Insert(Suppliers row)
		{
			db.Suppliers.Add(row);
			return db.SaveChanges();
		}
		//Edit
		public int Update(Suppliers row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//Delete
		public int Delete(Suppliers row)
		{
			db.Suppliers.Remove(row);
			return db.SaveChanges();
		}
	}
}


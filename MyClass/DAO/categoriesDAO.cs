using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
	public class categoriesDAO
	{
		private MyDBContext db = new MyDBContext();
		//Select *From ...
		public List<Categories> getlist()
		{
			return db.Categories.ToList();
		}

		//Index chi coi stasus 1,2
		public List<Categories> getlist(string status = "ALL")
		{
			List<Categories> list = null;
			switch (status)
			{
				case "Index"://1 2
					{
						list = db.Categories.Where(m => m.Status !=0).ToList();
						break;
					}
				case "Trash"://0
					{
						list = db.Categories.Where(m => m.Status == 0).ToList();
						break;
					}
				default:
					{
						list = db.Categories.ToList();
						break;
					}
			}
			return list;
		}
		//details
		public Categories getRow(int id) 
		{
			if (id == null)
			{
				return null;
			}
			else
			{
				return db.Categories.Find(id);
			}
		}
		//create
		public int Insert(Categories row) 
		{
			db.Categories.Add(row);
			return db.SaveChanges();
		}
		//Edit
		public int Update(Categories row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//Delete
		public int Delete(Categories row)
		{
			db.Categories.Remove(row);
			return db.SaveChanges();
		}
	}
}

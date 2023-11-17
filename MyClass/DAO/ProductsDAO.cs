using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
	public class ProductsDAO
	{
		private MyDBContext db = new MyDBContext();
		//Select *From ...
		public List<Products> getlist()
		{
			return db.Products.ToList();
		}

		//Index chi coi stasus 1,2
		public List<Products> getlist(string status = "ALL")
		{
			List<Products> list = null;
			switch (status)
			{
				case "Index"://1 2
					{
						list = db.Products.Where(m => m.Status != 0).ToList();
						break;
					}
				case "Trash"://0
					{
						list = db.Products.Where(m => m.Status == 0).ToList();
						break;
					}
				default:
					{
						list = db.Products.ToList();
						break;
					}
			}
			return list;
		}
		//details
		public Products getRow(int? id)
		{
			if (id == null)
			{
				return null;
			}
			else
			{
				return db.Products.Find(id);
			}
		}
		//create
		public int Insert(Products row)
		{
			db.Products.Add(row);
			return db.SaveChanges();
		}
		//Edit
		public int Update(Products row)
		{
			db.Entry(row).State = EntityState.Modified;
			return db.SaveChanges();
		}
		//Delete
		public int Delete(Products row)
		{
			db.Products.Remove(row);
			return db.SaveChanges();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Model;
using Web_TH.Library;

namespace Web_TH.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {

		categoriesDAO categoriesDAO = new categoriesDAO();

		//////////////////////////////////////////////////////////////////////////////////////
		//INDEX
		// GET: Admin/Category
		public ActionResult Index()
        {
            return View(categoriesDAO.getlist());
        }
		//////////////////////////////////////////////////////////////////////////////////////
		//Details
		// GET: Admin/Category/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				//thong bao that bai
				TempData["message"] =new XMessage("","Không tồn tại loại sản phẩm");
				return RedirectToAction("Index");
			}
			//truy van id
			Categories categories = categoriesDAO.getRow((int)id);

			if (id == null)
			{

				

				return RedirectToAction("Index");
			}
			return View(categories);
		}
		//////////////////////////////////////////////////////////////////////////////////////
		//CREATE
		// GET: Admin/Category/Create
		public ActionResult Create()
		{
			ViewBag.ListCat = new SelectList(categoriesDAO.getlist("Index"), "Id", "Name");
			ViewBag.ListOrder = new SelectList(categoriesDAO.getlist("Index"), "Order", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Categories categories)
		{
			if (ModelState.IsValid)
			{
				//Xu ly tu dong: CreateAt
				categories.CreateAt = DateTime.Now;
				//Xu ly tu dong: UpdateAt
				categories.UpdateAt = DateTime.Now;
				//Xu ly tu dong: ParentId
				if (categories.ParentId == null)
				{
					categories.ParentId = 0;
				}
				//Xu ly tu dong: Order
				if (categories.Order == null)
				{
					categories.Order = 1;
				}
				else
				{
					categories.Order += 1;
				}
				//Xu ly tu dong: Slug
				categories.Slug = XString.Str_Slug(categories.Name);

				//Chen them dong cho DB
				categoriesDAO.Insert(categories);
				//thong bao danh muc san pham thanh cong
				TempData["message"] = new XMessage("success","tạo mới loại sản phẩm thành công");
				//tro ve trang index
				return RedirectToAction("Index");
			}
			ViewBag.ListCat = new SelectList(categoriesDAO.getlist("Index"), "Id", "Name");
			ViewBag.ListOrder = new SelectList(categoriesDAO.getlist("Index"), "Order", "Name");
			return View(categories);
		}
		//////////////////////////////////////////////////////////////////////////////////////
		//Edit
		// GET: Admin/Category/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{

				//thong bao that bai
				TempData["message"] = new XMessage("", "Không tìm thấy loại sản phẩm");
				return RedirectToAction("Index");
			}
			Categories categories = categoriesDAO.getRow((int)id);
			if (categories == null)
			{
				return HttpNotFound();
			}
			ViewBag.ListCat = new SelectList(categoriesDAO.getlist("Index"), "Id", "Name");
			ViewBag.ListOrder = new SelectList(categoriesDAO.getlist("Index"), "Order", "Name");
			return View(categories);
		}

		// POST: Admin/Category/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Categories categories)
		{
			if (ModelState.IsValid)
			{
				
				//Xu ly tu dong: Slug
				categories.Slug = XString.Str_Slug(categories.Name);
				//xu ly tu dong: ParentID
				if(categories.ParentId == null)
				{
					categories.ParentId = 0;
				}
				//xu ly tu dong: Order
				if(categories.Order == null)
				{
					categories.Order = 1;
				}
				else
				{
					categories.Order += 1;
				}
				//xu ly tu dong: UpdeateAt
				categories.UpdateAt = DateTime.Now;

				//cập nhật mẫu tin
				categoriesDAO.Update(categories);

				//thong bao thanh cong
				TempData["message"] = TempData["message"] = new XMessage("success","Cập nhật loại hàng thành công");
				return RedirectToAction("Index");
			}
			ViewBag.ListCat = new SelectList(categoriesDAO.getlist("Index"), "Id", "Name");
			ViewBag.ListOrder = new SelectList(categoriesDAO.getlist("Index"), "Order", "Name");
			return View(categories);
		}
		//////////////////////////////////////////////////////////////////////////////////////
		//Delete
		// GET: Admin/Category/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{

				//thong bao that bai
				TempData["message"] = new XMessage("danger", "Xoa mau tin thất bại");
				return RedirectToAction("Index");
			}
			Categories categories = categoriesDAO.getRow((int)id);
			if (categories == null)
			{

				//thong bao that bai
				TempData["message"] = new XMessage("danger", "Xoa mau tin thất bại");
				return RedirectToAction("Index");
			}
			return View(categories);
		}

		// POST: Admin/Category/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Categories categories = categoriesDAO.getRow(id);
			categoriesDAO.Delete(categories);

			//thong bao thanh cong
			TempData["message"] = new XMessage("success", "Xoa mau tin thanh cong");
			return RedirectToAction("Trash");
		}
		
		//////////////////////////////////////////////////////////////////////////////////////
		//STATUS
		// GET: Admin/Category/Status/5
		public ActionResult Status(int? id)
		{

			if (id == null)
			{
				//thong bao that bai
				TempData["message"] = new XMessage("danger","Cập nhật trạng thái thất bại");
				return RedirectToAction("Index");
			}
			//truy van id
			Categories categories = categoriesDAO.getRow((int)id);

			if (id == null)
			{

				//thong bao cap nhat trang thai thanh cong
				TempData["message"] = TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");

				return RedirectToAction("Index");
			}
			else
			{
				//chuyen doi trang thai cua Satus tu 1<->2
				categories.Status = (categories.Status == 1) ? 2 : 1;

				//cap nhat gia tri UpdateAt
				categories.UpdateAt = DateTime.Now;

				//cap nhat lai DB
				categoriesDAO.Update(categories);

				//thong bao cap nhat trang thai thanh cong
				TempData["message"] = TempData["message"] = new XMessage("success","Cập nhật trạng thái thành công");

				return RedirectToAction("Index");
			}
		}
		//////////////////////////////////////////////////////////////////////////////////////
		//DelTrash
		// GET: Admin/Category/Delete/5
		public ActionResult DelTrash(int? id)
		{

			if (id == null)
			{
				//thong bao that bai
				TempData["message"] = new XMessage("danger", "Khong tim thay mau tin");
				return RedirectToAction("Index");
			}
			//truy van id
			Categories categories = categoriesDAO.getRow((int)id);

			if (categories == null)
			{

				//thong bao cap nhat trang thai thanh cong
				TempData["message"] = TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thành công");

				return RedirectToAction("Index");
			}
			else
			{
				//chuyen doi trang thai cua Satus tu 1,2+>0: khong hien thi o index
				categories.Status = 0;

				//cap nhat gia tri UpdateAt
				categories.UpdateAt = DateTime.Now;

				//cap nhat lai DB
				categoriesDAO.Update(categories);

				//thong bao cap nhat trang thai thanh cong
				TempData["message"] = TempData["message"] = new XMessage("success", "Xoa mau tin thanh cong");

				return RedirectToAction("Index");
			}
		}

		//////////////////////////////////////////////////////////////////////////////////////
		//TRASH
		// GET: Admin/Trash
		public ActionResult Trash()
		{
			return View(categoriesDAO.getlist("Trash"));
		}

		//////////////////////////////////////////////////////////////////////////////////////
		//RECOVER
		// GET: Admin/Recover/Status/5
		public ActionResult Recover(int? id)
		{

			if (id == null)
			{
				//thong bao that bai
				TempData["message"] = new XMessage("danger", "Phuc hoi mau tin that bai");
				return RedirectToAction("Index");
			}
			//truy van id
			Categories categories = categoriesDAO.getRow((int)id);

			if (id == null)
			{

				//thong bao cap nhat trang thai thanh cong
				TempData["message"] = TempData["message"] = new XMessage("danger", "Phuc hoi mau tin that bai");

				return RedirectToAction("Index");
			}
			else
			{
				//chuyen doi trang thai cua Satus tu 1<->2
				categories.Status = 2;

				//cap nhat gia tri UpdateAt
				categories.UpdateAt = DateTime.Now;

				//cap nhat lai DB
				categoriesDAO.Update(categories);

				//thong bao phuc hoi du lieu thanh cong
				TempData["message"] = ("Phuc hoi mau tin thành công");

				return RedirectToAction("Index");
			}
		}

	}
}

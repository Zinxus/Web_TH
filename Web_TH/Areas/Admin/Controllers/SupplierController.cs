using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Model;
using Web_TH.Library;

namespace Web_TH.Areas.Admin.Controllers
{
    public class SupplierController : Controller
    {
        SuppliersDao suppliersDao = new SuppliersDao();

        // GET: Admin/Supplier
        public ActionResult Index()
        {
            return View(suppliersDao.getlist("Index"));
        }

        // GET: Admin/Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
				TempData["message"] = new XMessage("", "Không tồn tại nhà cung cấp");
				return RedirectToAction("Index");
			}
            Suppliers suppliers = suppliersDao.getRow((int)id);
            if (suppliers == null)
            {
				TempData["message"] = new XMessage("", "Không tồn tại nhà cung cấp");
			}
            return View(suppliers);
        }
		///////////////////////////////////////////////////////////////////////////
		// GET: Admin/Supplier/Create
		public ActionResult Create()
		{
			ViewBag.OrderList = new SelectList(suppliersDao.getlist("Index"), "Order", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Suppliers suppliers)
		{
			if (ModelState.IsValid)
			{
				//xu ly tu dong cho cac truong: Slug, CreateAt/By, UpdateAt/By, Oder
				//Xu ly tu dong: CreateAt
				suppliers.CreateAt = DateTime.Now;
				//Xu ly tu dong: UpdateAt
				suppliers.UpdateAt = DateTime.Now;
				//Xu ly tu dong: CreateBy
				suppliers.CreateBy = Convert.ToInt32(Session["UserId"]);
				//Xu ly tu dong: UpdateBy
				suppliers.UpdateBy = Convert.ToInt32(Session["UserId"]);
				//Xu ly tu dong: Order
				if (suppliers.Order == null)
				{
					suppliers.Order = 1;
				}
				else
				{
					suppliers.Order += 1;
				}
				//Xu ly tu dong: Slug
				suppliers.Slug = XString.Str_Slug(suppliers.Name);

				//xu ly cho phan upload hinh anh
				var img = Request.Files["img"];//lay thong tin file
				if (img.ContentLength != 0)
				{
					string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
					//kiem tra tap tin co hay khong
					if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
					{
						string slug = suppliers.Slug;
						//ten file = Slug + phan mo rong cua tap tin
						string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
						suppliers.Image = imgName;//abc-def-1.jpg
												  //upload hinh
						string PathDir = "~/Public/img/supplier/";
						string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
						img.SaveAs(PathFile);
					}
				}//ket thuc phan upload hinh anh

				//chen mau tin vao DB
				suppliersDao.Insert(suppliers);
				//thong bao tao mau tin thanh cong
				TempData["message"] = new XMessage("success", "Tạo mới nhà cung cấp thành công");
				return RedirectToAction("Index");
			}
			ViewBag.OrderList = new SelectList(suppliersDao.getlist("Index"), "Order", "Name");
			return View(suppliers);
		}
		//////////////////////////////////////////////////////////////////////////////////////
		//Edit
		// GET: Admin/Supplier/Edit/5
		public ActionResult Edit(int? id)
        {
			if (id == null)
			{
				//thong bao that bai
				TempData["message"] = new XMessage("danger", "Không tồn tại nhà cung cấp");
				return RedirectToAction("Index");
			}
			Suppliers suppliers = suppliersDao.getRow(id);
			if (suppliers == null)
			{
				//thong bao that bai
				TempData["message"] = new XMessage("danger", "Không tồn tại nhà cung cấp");
				return RedirectToAction("Index");
			}
			ViewBag.ListOrder = new SelectList(suppliersDao.getlist("Index"), "Order", "Name");
			return View(suppliers);
		}

        // POST: Admin/Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Image,Slug,Order,Fullname,Phone,Email,UrlSite,MetaDesc,MetaKey,CreateAt,CreateBy,UpdateAt,UpdateBy,Status")] Suppliers suppliers)
        {
			if (ModelState.IsValid)
			{
				//xu ly tu dong cho cac truong: CreateAt/By, UpdateAt/By, Slug, OrderBy

				//Xu ly tu dong cho: UpdateAt
				suppliers.UpdateAt = DateTime.Now;

				//Xu ly tu dong cho: Order
				if (suppliers.Order == null)
				{
					suppliers.Order = 1;
				}
				else
				{
					suppliers.Order += 1;
				}

				//Xu ly tu dong cho: Slug
				suppliers.Slug = XString.Str_Slug(suppliers.Name);

				//xu ly cho phan upload hinh anh
				var img = Request.Files["img"];//lay thong tin file
				string PathDir = "~/Public/img/supplier";
				if (img.ContentLength != 0)
				{
					//Xu ly cho muc xoa hinh anh
					if (suppliers.Image != null)
					{
						string DelPath = Path.Combine(Server.MapPath(PathDir), suppliers.Image);
						System.IO.File.Delete(DelPath);
					}

					string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
					//kiem tra tap tin co hay khong
					if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
					{
						string slug = suppliers.Slug;
						//ten file = Slug + phan mo rong cua tap tin
						string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
						suppliers.Image = imgName;
						//upload hinh
						string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
						img.SaveAs(PathFile);
					}

				}//ket thuc phan upload hinh anh

				//cap nhat mau tin vao DB
				suppliersDao.Update(suppliers);
				//thong bao thanh cong
				TempData["message"] = new XMessage("success", "Cập nhật mẩu tin thành công");
				return RedirectToAction("Index");
			}
			return View(suppliers);
		}
		//////////////////////////////////////////////////////////////////////////////////////
		// GET: Admin/Supplier/Delete/5
		public ActionResult Delete(int? id)
        {
			if (id == null)
			{
				//hien thong bao loi
				TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
				return RedirectToAction("Index");
			}
			Suppliers suppliers = suppliersDao.getRow((int)id);
			if (suppliers == null)
			{
				//hien thong bao loi
				TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
				return RedirectToAction("Index");
			}
			return View(suppliers);
		}

        // POST: Admin/Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			Suppliers suppliers = suppliersDao.getRow(id);
			//xu ly cho phan upload hinh anh
			var img = Request.Files["img"];//lay thong tin file
			string PathDir = "~/Public/img/supplier";
			//xoa mau tin ra khoi DB
			if (suppliersDao.Delete(suppliers) == 1)
			{
				//Xu ly cho muc xoa hinh anh
				if (suppliers.Image != null)
				{
					string DelPath = Path.Combine(Server.MapPath(PathDir), suppliers.Image);
					System.IO.File.Delete(DelPath);
				}
			}
			//hien thong baothanh cong
			TempData["message"] = new XMessage("danger", "Xóa nhà cung cấp thành công");
			return RedirectToAction("Index");
		}

		//////////////////////////////////////////////////////////////////////////////////////
		//STATUS
		// GET: Admin/Supplier/Status/5
		public ActionResult Status(int? id)
		{

			if (id == null)
			{
				//thong bao that bai
				TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
				return RedirectToAction("Index");
			}
			//truy van id
			Suppliers suppliers = suppliersDao.getRow((int)id);

			if (id == null)
			{

				//thong bao cap nhat trang thai thanh cong
				TempData["message"] = TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");

				return RedirectToAction("Index");
			}
			else
			{
				//chuyen doi trang thai cua Satus tu 1<->2
				suppliers.Status = (suppliers.Status == 1) ? 2 : 1;

				//cap nhat gia tri UpdateAt
				suppliers.UpdateAt = DateTime.Now;

				//cap nhat lai DB
				suppliersDao.Update(suppliers);

				//thong bao cap nhat trang thai thanh cong
				TempData["message"] = TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");

				return RedirectToAction("Index");
			}
		}

		/// //////////////////////////////////////////////////////////////////////////////////
		// GET: Admin/Supplier/DelTrash/5
		//Chuyen mau tin dang o trang thai status la 1/2 thanh 0: khong hien thi o trang INDEX
		public ActionResult DelTrash(int? id)
		{
			if (id == null)
			{
				//thong bao that bai
				TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
				return RedirectToAction("Index");
			}
			Suppliers suppliers = suppliersDao.getRow((int)id);
			if (suppliers == null)
			{
				//thong bao thay doi status that bai
				TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
				return RedirectToAction("Index");
			}
			//cap nhat mot so thong tin cho DB (id==id)
			//UpdateAt
			suppliers.UpdateAt = DateTime.Now;
			//UpdateBy
			suppliers.UpdateBy = Convert.ToInt32(Session["UserID"]);
			//Status
			suppliers.Status = 0;
			//Update DB
			suppliersDao.Update(suppliers);

			//thong bao thay doi status thanh cong
			TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");
			return RedirectToAction("Index");
		}
		/// //////////////////////////////////////////////////////////////////////////////////
		// TRASH
		public ActionResult Trash()
		{
			return View(suppliersDao.getlist("Trash"));//status =0
		}
		//////////////////////////////////////////////////////////////////////////////////////
		//RECOVER
		// GET: Admin/Supplier/Recover/5
		public ActionResult Recover(int? id)
		{

			if (id == null)
			{
				//thong bao that bai
				TempData["message"] = new XMessage("danger", "Phuc hoi mau tin that bai");
				return RedirectToAction("Index");
			}
			//truy van id
			Suppliers suppliers = suppliersDao.getRow((int)id);

			if (id == null)
			{

				//thong bao cap nhat trang thai thanh cong
				TempData["message"] = TempData["message"] = new XMessage("danger", "Phuc hoi mau tin that bai");

				return RedirectToAction("Index");
			}
			else
			{
				//chuyen doi trang thai cua Satus tu 1<->2
				suppliers.Status = 2;

				//cap nhat gia tri UpdateAt
				suppliers.UpdateAt = DateTime.Now;

				//cap nhat lai DB
				suppliersDao.Update(suppliers);

				//thong bao phuc hoi du lieu thanh cong
				TempData["message"] = ("Phuc hoi mau tin thành công");

				return RedirectToAction("Index");
			}
		}
	}
}

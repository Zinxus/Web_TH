﻿using MyClass.DAO;
using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Web_TH.Controllers
{
    public class ModuleController : Controller
    {
		MenusDAO menusDAO = new MenusDAO();
		///////////////////////////////////////////////////////////////////
		//GET: Module
		public ActionResult MainMenu()
		{
			List<Menus> list = menusDAO.getListByParentId(0);
			return PartialView("MainMenu",list);
		}
	}
}
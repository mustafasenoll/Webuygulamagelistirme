using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCNhibernate.Nhibernate;
using NHibernate.Linq;

namespace MVCNhibernate.Controllers
{
    public class DepartmanlarController : Controller
    {
        // GET: Departmanlar
        public ActionResult Index()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                var departmanlar = session.Query<Models.Departmanlar>().Fetch(x => x.Calisanlar).ToList();
                return View(departmanlar);
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Departmanlar departman)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                session.SaveOrUpdate(departman);
                session.Flush();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                var departman = session.Query<Models.Departmanlar>().FirstOrDefault(x => x.Departman_ID == id);
                return View(departman);
            }
        }
        [HttpPost]
        public ActionResult Edit(int id, Models.Departmanlar departman)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                var yeniDepartman = session.Query<Models.Departmanlar>().FirstOrDefault(x => x.Departman_ID == id);
                yeniDepartman.Departman_Ad = departman.Departman_Ad;
                yeniDepartman.Telefon = departman.Telefon;
                session.SaveOrUpdate(yeniDepartman);
                session.Flush();
            }
            return RedirectToAction("Index");
        }
    }
}
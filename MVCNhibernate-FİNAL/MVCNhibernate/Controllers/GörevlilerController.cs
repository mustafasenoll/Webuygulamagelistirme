using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using System.Web;
using System.Web.Mvc;
using MVCNhibernate.Nhibernate;

namespace MVCNhibernate.Controllers
{
    public class GörevlilerController : Controller

    {
        // GET: Görevliler
        public IList<int> departmanlariAl()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                var departmanListe = session.CreateSQLQuery("SELECT Departman_ID FROM Departmanlar").List<int>();
                session.Flush();
                return departmanListe;
            }
        }

        [Obsolete]
        public ActionResult Index()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                var görevliler = session.Query<Models.Görevliler>().ToList();
                return View(görevliler);
            }
        }

        public ActionResult Delete(int id)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                var görevli = session.Query<Models.Görevliler>().FirstOrDefault(x => x.Personel_ID == id);
                session.Delete(görevli);
                session.Flush();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                var görevli = session.Query<Models.Görevliler>().FirstOrDefault(x => x.Personel_ID == id);
                return View(görevli);
            }
        }
        [HttpPost]
        public ActionResult Edit(int id, Models.Görevliler görevli)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                var yeniGörevli = session.Query<Models.Görevliler>().FirstOrDefault(x => x.Personel_ID == id);
                yeniGörevli.Ad_Soyad = görevli.Ad_Soyad;
                yeniGörevli.TC_Kimlik = görevli.TC_Kimlik;
                yeniGörevli.Unvan = görevli.Unvan;
                session.SaveOrUpdate(yeniGörevli);
                session.Flush();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Görevliler görevli)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                session.SaveOrUpdate(görevli);
                session.Flush();
            }
            return RedirectToAction("Index");
        }
    }
}
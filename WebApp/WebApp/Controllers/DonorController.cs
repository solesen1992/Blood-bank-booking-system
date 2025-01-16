using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class DonorController : Controller
    {
        // GET: DonorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DonorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DonorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DonorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DonorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DonorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DonorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DonorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

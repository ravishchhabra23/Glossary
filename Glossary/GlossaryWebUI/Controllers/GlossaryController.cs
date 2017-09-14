using GlossaryWebUI.Helpers;
using GlossaryWebUI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;

namespace GlossaryWebUI.Controllers
{
    public class GlossaryController : Controller
    {
        private IApiRestClient _apiRestClient;
        private readonly ISessionHelper _sessionHelper;
        

        public GlossaryController(ISessionHelper sessionHelper, IApiRestClient apiRestClient)
        {
            _sessionHelper = sessionHelper;
            _apiRestClient = apiRestClient;
        }
        public ActionResult Index(int? page = 1)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            Task <IEnumerable<GlossaryModel>> glossary = _apiRestClient.GetGlossaries();
            glossary.Wait();
            return View(glossary.Result.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            GlossaryModel glossary = null;
            return View(glossary);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GlossaryModel glossaryModel)
        {
            if (string.IsNullOrEmpty(glossaryModel.Term))
                ModelState.AddModelError("Term", "Term is Required.");
            if (string.IsNullOrEmpty(glossaryModel.Definition))
                ModelState.AddModelError("Definition", "Term Definition is Required.");
            if (ModelState.IsValid)
            {
                Task<GlossaryModel> glossary = _apiRestClient.CreateGlossary(glossaryModel);
                glossary.Wait();

            }
            else
                return View(glossaryModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            int id = Convert.ToInt32(HttpContext.Request.RequestContext.RouteData.Values["id"].ToString());
            Task<GlossaryModel> glossary = _apiRestClient.GetGlossary(id);
            glossary.Wait();
            return View(glossary.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GlossaryModel glossaryModel,int id)
        {
            if (string.IsNullOrEmpty(glossaryModel.Term))
                ModelState.AddModelError("Term", "Term is Required.");
            if (string.IsNullOrEmpty(glossaryModel.Definition))
                ModelState.AddModelError("Definition", "Term Definition is Required.");
            if (ModelState.IsValid)
            {
                Task<string> glossary = _apiRestClient.UpdateGlossary(id, glossaryModel);
                glossary.Wait();
                
            }
            else
                return View(glossaryModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete()
        {
            
            int id = Convert.ToInt32(HttpContext.Request.RequestContext.RouteData.Values["id"].ToString()); 
            Task<GlossaryModel> glossary = _apiRestClient.GetGlossary(id);
            glossary.Wait();
            return View(glossary.Result);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Task<string> glossary = _apiRestClient.DeleteGlossary(id);
            glossary.Wait();
            var result = glossary.Result;
            return RedirectToAction("Index");
        }
	}
}
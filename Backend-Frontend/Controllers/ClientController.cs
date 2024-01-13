using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Test.DataAccess.Repository;
using Test.DataAccess.Repository.IRepository;
using Test.EntityFramework.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Backend_Frontend.Controllers
{
    public class ClientController : Controller
    {

       
        private readonly IClientDataAccess _client;
        private readonly IMaritalRepository _status;
        private readonly IWebHostEnvironment _webHostEnvironment; //inject this to arraive for pathRoot
        public ClientController(IClientDataAccess client, IMaritalRepository status, IWebHostEnvironment webHostEnvironment)
        {
            _client = client;
            _status = status;
            _webHostEnvironment = webHostEnvironment;
        }
        public ActionResult Index()
        {
            List<ClientStatusVM> clientAll = _client.GetAllClient();
            
           
            return View(clientAll);
                //PartialView("~/Views/Shared/_SearchIndex.cshtml", clientAll);
        }
        

        [HttpGet]
        public PartialViewResult Search(string searchterm)
        {
            
            
                List<ClientStatusVM> clientVM = _client.SearchClientByName(searchterm);


                return PartialView("d", clientVM);
            
        }


        [HttpGet]
        public IActionResult Create()
        {
          
            ViewBag.statusList = _status.GetAllStatus();
            return View();
        }
        [HttpGet]
        public IActionResult AddEdit(int id, Client x)
        {
           
            if (id == null || id == 0)
            {
                //create
               
                ViewBag.statusList = _status.GetAllStatus();
                return View(x);
            }
            else
            {
                //update
                Client clientobj = _client.GetById(id);

                ViewBag.statusList = _status.GetAllStatus();
                return View(clientobj);
            }

        }
        [HttpPost]
        public IActionResult AddEdit(int id, Client clientobj, IFormFile? files)
        {
            
       
            if (ModelState.IsValid)
            {
                if (clientobj.Id == 0)
                {
                    _client.Add(clientobj);
                    _client.save();
                }
                else
                {
                    _client.Edit(id,clientobj);
                    _client.save();
                }

              
                if (files != null)
                {
                    string pathroot = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(files.FileName);
                    string pathproduct = Path.Combine(pathroot, @"images\Client\");

                    if (!string.IsNullOrEmpty(clientobj.Iamge))
                    {
                        //delete old image
                        var oldImagepath = Path.Combine(pathroot, clientobj.Iamge.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagepath))
                        {
                            System.IO.File.Delete(oldImagepath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(pathproduct, fileName), FileMode.Create))
                    {
                        files.CopyTo(fileStream);
                    }
                    clientobj.Iamge = @"images\Client\" + fileName;
                }

                _client.Edit(id, clientobj);
                _client.save();

                return RedirectToAction("Index");
            }
            else
            {
                //list
                ViewBag.statusList = _status.GetAllStatus();
                return View(clientobj);
            }
        
}
        [HttpPost]
        public IActionResult Create(Client clientobj, IFormFile? files)
        {
            if (ModelState.IsValid)
            {
                string pathroot = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(files.FileName);
                    string pathproduct = Path.Combine(pathroot, @"images\Client\");
                    using (var fileStream = new FileStream(Path.Combine(pathproduct, fileName), FileMode.Create))
                    {
                        files.CopyTo(fileStream);
                    }
                    clientobj.Iamge = @"images\CLient\" + fileName;
                }
                _client.Add(clientobj);
                _client.save();
              
                //TempData["Success"] = "Product Created Successfully";
                return RedirectToAction("Index");

            }
            return View();

        }

        //public IActionResult Delete(int id)
        //{
        //    if (id == null || id == 0)
        //    { return NotFound(); }

        //    Client Clientobj = _client.GetById(id);
        //    ViewBag.statusList = _status.GetAllStatus();
        //    if (Clientobj == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(Clientobj);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int id) 
        //{

        //    ViewBag.statusList = _status.GetAllStatus();
        //    _client.Remove(id);

        //    return RedirectToAction("Index");
        //}
        //[HttpPost]
        //public IActionResult Delete(int clientId)
        //{
        //    // Perform the deletion logic here, based on the provided clientId
        //    // ...
        //    var ClientDeleted = _client.GetById(clientId);
        //    if (ClientDeleted == null)
        //    {
        //      //  return Json(new { success = false, message = "Error while deleting" });
        //         return NotFound();
        //    }
          
        //    _client.Remove(ClientDeleted);
        //    _client.save();

        //    return RedirectToAction("Index");

            
        //}
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(string searchterm)
        {
            List <ClientStatusVM> objclientlist = _client.GetAllClient();
            
            return Json(new { data = objclientlist });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var ClientDeleted = _client.GetById(id);
            if (ClientDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
                // return NotFound();
            }
            var oldpathimage = Path.Combine(_webHostEnvironment.WebRootPath, ClientDeleted.Iamge.Trim('\\'));
            if (System.IO.File.Exists(oldpathimage))
            {
                System.IO.File.Delete(oldpathimage);
            }
            _client.Remove(ClientDeleted);
            _client.save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZOLLA.TestTask.Models;

namespace ZOLLA.TestTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly int size = 5;



        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddClient(Client client)
        {
            object data;

            try
            {
                using (var uow = new UnitOfWork())
                {
                    uow.Clients.Create(client);
                    uow.Save();
                }

                data = new { };
            }
            catch (Exception exception)
            {
                data = new
                {
                    message = exception.Message,
                };
            }

            return Json(data);
        }

        [HttpPost]
        public JsonResult DeleteClient(int clientId)
        {
            object data;

            try
            {
                using (var uow = new UnitOfWork())
                {
                    uow.Clients.Delete(clientId);
                    uow.Save();
                }

                data = new { };
            }
            catch (Exception exception)
            {
                data = new
                {
                    message = exception.Message,
                };
            }

            return Json(data);
        }

        [HttpPost]
        public JsonResult EditClient(Client client)
        {
            object data;

            try
            {
                using (var uow = new UnitOfWork())
                {
                    uow.Clients.Update(client);
                    uow.Save();
                }

                data = new { };
            }
            catch (Exception exception)
            {
                data = new
                {
                    message = exception.Message,
                };
            }

            return Json(data);
        }

        [HttpGet]
        public JsonResult GetClients(int page = 1)
        {
            object data;

            try
            {
                var clients = new List<Client>();
                using (var uow = new UnitOfWork())
                {
                    clients = new List<Client>(uow.Clients.Get());
                }

                page = page.Restrict(1, (int)Math.Ceiling((decimal)(clients?.Count ?? 0) / size));

                data = new
                {
                    clients = clients.Skip((page - 1) * size).Take(size),
                    pageInfo = new PageInfo()
                    {
                        Page = page,
                        Size = size,
                        TotalSize = clients?.Count ?? 0
                    }
                };
            }
            catch (Exception exception)
            {
                data = new
                {
                    message = exception.Message,
                };
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
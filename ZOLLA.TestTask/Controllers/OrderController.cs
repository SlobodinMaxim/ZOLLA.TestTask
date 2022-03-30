using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZOLLA.TestTask.Models;

namespace ZOLLA.TestTask.Controllers
{
    public class OrderController : Controller
    {
        private readonly int size = 3;



        [HttpPost]
        public JsonResult Add(Order order)
        {
            object data;

            try
            {
                using (var uow = new UnitOfWork())
                {
                    uow.Orders.Create(order);
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
        public JsonResult Delete(int orderId)
        {
            object data;

            try
            {
                using (var uow = new UnitOfWork())
                {
                    uow.Orders.Delete(orderId);
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
        public JsonResult Edit(Order order)
        {
            object data;

            try
            {
                using (var uow = new UnitOfWork())
                {
                    uow.Orders.Update(order);
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
        public JsonResult Get(int clientId, int page = 1)
        {
            object data;

            try
            {
                var orders = new List<Order>();
                using (var uow = new UnitOfWork())
                {
                    orders = new List<Order>(uow.Orders.Get().Where(o => o.ClientId == clientId));
                }

                page = page.Restrict(1, (int)Math.Ceiling((decimal)(orders?.Count ?? 0) / size));

                data = new
                {
                    orders = orders.Skip((page - 1) * size).Take(size),
                    pageInfo = new PageInfo()
                    {
                        Page = page,
                        Size = size,
                        TotalSize = orders?.Count ?? 0
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
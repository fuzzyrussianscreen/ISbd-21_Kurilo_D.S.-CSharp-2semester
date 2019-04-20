using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PizzeriaRestApi.Controllers
{
    public class ReptController : ApiController
    {
        private readonly IReptService _service;
        public ReptController(IReptService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetStoragesLoad()
        {
            var list = _service.GetStoragesLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public IHttpActionResult GetCustomerIndents(ReptBindingModel model)
        {
            var list = _service.GetCustomerIndents(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));

            }
            return Ok(list);
        }
        [HttpPost]
        public void SavePizzaPrice(ReptBindingModel model)
        {
            _service.SavePizzaPrice(model);
        }
        [HttpPost]
        public void SaveStoragesLoad(ReptBindingModel model)
        {
            _service.SaveStoragesLoad(model);
        }
        [HttpPost]
        public void SaveCustomerIndents(ReptBindingModel model)
        {
            _service.SaveCustomerIndents(model);
        }
    }
}

﻿using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PizzeriaRestApi.Controllers
{
    public class PerformerController : ApiController
    {
        private readonly IPerformerService _service;
        public PerformerController(IPerformerService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }
        [HttpPost]
        public void AddElement(PerformerBindingModel model)
        {
            _service.AddElement(model);
        }
        [HttpPost]
        public void UpdElement(PerformerBindingModel model)
        {
            _service.UpdElement(model);
        }
        [HttpPost]
        public void DelElement(PerformerBindingModel model)
        {
            _service.DelElement(model.Id);
        }

    }
}

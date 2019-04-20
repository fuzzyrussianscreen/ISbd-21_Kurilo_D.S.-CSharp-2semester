using PizzeriaRestApi.Services;
using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.Interfaces;
using PizzeriaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PizzeriaRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;
        private readonly IPerformerService _servicePerformer;

        public MainController(IMainService service, IPerformerService servicePerformer)
        {
            _service = service;
            _servicePerformer = servicePerformer;
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

        [HttpPost]
        public void CreateIndent(IndentBindingModel model)
        {
            _service.CreateIndent(model);
        }

        [HttpPost]
        public void PayIndent(IndentBindingModel model)
        {
            _service.PayIndent(model);
        }

        [HttpPost]
        public void PutIngredientOnStorage(StorageIngredientBindingModel model)
        {
            _service.PutIngredientOnStorage(model);
        }

        [HttpPost]
        public void StartWork()
        {
            List<IndentViewModel> orders = _service.GetFreeIndents();
            foreach (var order in orders)
            {
                PerformerViewModel impl = _servicePerformer.GetFreeWorker();
                if (impl == null)
                {
                    throw new Exception("Нет сотрудников");
                }
                new WorkPerformer(_service, _servicePerformer, impl.Id, order.Id);
            }
        }
    }
}

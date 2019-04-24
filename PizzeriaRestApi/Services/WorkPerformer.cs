using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace PizzeriaRestApi.Services
{
    public class WorkPerformer
    {
        private readonly IMainService _service;
        private readonly IPerformerService _servicePerformer;
        private readonly int _implementerId;
        private readonly int _orderId;
        // семафор
        static Semaphore _sem = new Semaphore(3, 3);
        Thread myThread;
        public WorkPerformer(IMainService service, IPerformerService
       servicePerformer, int implementerId, int orderId)
        {
            _service = service;
            _servicePerformer = servicePerformer;
            _implementerId = implementerId;
            _orderId = orderId;
            try
            {
                _service.TakeIndentInWork(new IndentBindingModel
                {
                    Id = _orderId,
                    PerformerId = _implementerId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            myThread = new Thread(Work);
            myThread.Start();
        }
        public void Work()
        {
            try
            {
                // забиваем мастерскую
                _sem.WaitOne();
                // Типа выполняем
                Thread.Sleep(10000);
                _service.FinishIndent(new IndentBindingModel
                {
                    Id = _orderId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // освобождаем мастерскую
                _sem.Release();
            }
        }
    }
}
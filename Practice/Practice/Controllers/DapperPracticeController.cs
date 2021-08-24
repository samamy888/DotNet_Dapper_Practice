using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Practice.DB;
using Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DapperPracticeController : Controller
    {
        private readonly ILogger<DapperPracticeController> _logger;
        private readonly Repository _repository;
        public DapperPracticeController(ILogger<DapperPracticeController> logger, Repository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        [HttpPost]
        public JsonResult GetAll()
        {
            var data = _repository.GetAll();
            return Json(new ResultModel {  Data = data ,});
        }
        [HttpPost]
        public JsonResult Add(TestModel FormModel)
        {
            var data = _repository.Add(FormModel);
            return Json(new ResultModel { IsSuccess = data >= 1 , });
        }
        [HttpPost]
        public JsonResult GetByID(int ID)
        {
            var data = _repository.GetByID(ID);
            return Json(new ResultModel {  Data = data, });
        }
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            var data = _repository.Delete(ID);
            return Json(new ResultModel { IsSuccess = data >= 1, });
        }
        [HttpPost]
        public JsonResult Update(TestModel FormModel)
        {
            var data = _repository.Update(FormModel);
            return Json(new ResultModel { IsSuccess = data >= 1, });
        }
    }
}

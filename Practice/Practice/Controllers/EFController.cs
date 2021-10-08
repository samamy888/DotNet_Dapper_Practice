using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Practice.Models;
using Practice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EFController : Controller
    {
        private readonly ILogger<CRUDController> _logger;
        private readonly EFService _EFService;
        public EFController(ILogger<CRUDController> logger, EFService EFService)
        {
            _logger = logger;
            _EFService = EFService;
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            var data = _EFService.GetAllAsync();
            return Json(new ResultModel { Data = data, });
        }
        [HttpPost]
        public JsonResult Add(TestModel FormModel)
        {
            var data = _EFService.Add(FormModel);
            return Json(new ResultModel { IsSuccess = data >= 1, });
        }
        [HttpGet]
        public JsonResult GetByID([FromBody] int ID)
        {
            var data = _EFService.GetByID(ID);
            return Json(new ResultModel { Data = data, });
        }
        [HttpDelete]
        public JsonResult Delete([FromBody] int ID)
        {
            var data = _EFService.Delete(ID);
            return Json(new ResultModel { IsSuccess = data >= 1, });
        }
        [HttpPut]
        public JsonResult Update(TestModel FormModel)
        {
            var data = _EFService.Update(FormModel);
            return Json(new ResultModel { IsSuccess = data >= 1, });
        }
    }
}

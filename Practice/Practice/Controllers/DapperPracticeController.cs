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
        public IEnumerable<TestModel> GetAll()
        {
            var data = _repository.GetAll();
            return data;
        }
        [HttpPost]
        public bool Add(TestModel FormModel)
        {
            var data = _repository.Add(FormModel);
            return data >= 1;
        }
        [HttpPost]
        public TestModel GetByID(int ID)
        {
            var data = _repository.GetByID(ID);
            return data;
        }
        [HttpPost]
        public bool Delete(int ID)
        {
            var data = _repository.Delete(ID);
            return data >= 1;
        }
        [HttpPost]
        public bool Update(TestModel FormModel)
        {
            var data = _repository.Update(FormModel);
            return data >= 1;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Practice.EF;
using Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice.Service
{
    public class EFService
    {
        private IConfiguration _config;
        private readonly EFContext _eFContext;

        public EFService(IConfiguration config, EFContext eFContext)
        {
            _config = config;
            _eFContext = eFContext;
        }
        public int Add(TestModel testModel)
        {
            _eFContext.EFTable.Add(testModel);
            return _eFContext.SaveChanges();
        }

        public IEnumerable<TestModel> GetAllAsync()
        {
            return _eFContext.EFTable.AsEnumerable(); ;
        }

        public TestModel GetByID(int ID)
        {
            return _eFContext.EFTable.OrderBy(x => x.ID).First();
        }

        public int Delete(int ID)
        {
            TestModel testModel = _eFContext.EFTable.FirstOrDefault(x => x.ID == ID);
            _eFContext.EFTable.Remove(testModel);
            return _eFContext.SaveChanges();
        }

        public int Update(TestModel testModel)
        {
            TestModel model = _eFContext.EFTable.FirstOrDefault(x => x.ID == testModel.ID);
            model.Name = testModel.Name;
            return _eFContext.SaveChanges();
        }
    }
}

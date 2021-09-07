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
        public EFService(IConfiguration config)
        {
            _config = config;
        }
        public int Add(TestModel testModel)
        {
            using (var db = new EFContext(_config))
            {
                db.EFTable.Add(testModel);
                return db.SaveChanges();
            }
        }

        public IEnumerable<TestModel> GetAllAsync()
        {
            IEnumerable<TestModel> result;
            using (var db = new EFContext(_config))
            {
                result =  db.EFTable.ToList();
            }
            return result;
        }

        public TestModel GetByID(int ID)
        {
            using (var db = new EFContext(_config))
            {
                return db.EFTable.OrderBy(x => x.ID).First();
            }
        }

        public int Delete(int ID)
        {
            using (var db = new EFContext(_config))
            {
                TestModel testModel = new TestModel() { ID = ID };
                db.EFTable.Attach(testModel);
                db.EFTable.Remove(testModel);
                return db.SaveChanges();
            }
        }

        public int Update(TestModel testModel)
        {
            using (var db = new EFContext(_config))
            {
                TestModel model = db.EFTable.OrderBy(x => x.ID).First();
                model.Name = testModel.Name;
                return db.SaveChanges();
            }
        }
    }
}

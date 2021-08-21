using System;
using System.Collections.Generic;
using System.Text;
using KONTAKTOR.DA.Models;

namespace KONTAKTOR.DA.Mongo.Repository
{
    public class EmployeeRepository : BaseMongoRepository<Employee>
    {
        public EmployeeRepository(MongoConnectionOptions settings) : base(settings)
        {
        }
    }
}

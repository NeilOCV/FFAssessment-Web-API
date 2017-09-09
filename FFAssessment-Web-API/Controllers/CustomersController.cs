using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DL;

namespace FFAssessment_Web_API.Controllers
{
    //[Authorize]
    public class CustomersController : ApiController
    {
        // GET api/Customers
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/Customers/5
        public string Get(int id)
        {
            return "Customers";
        }

        // POST api/Customers
        public void Post([FromBody]Customer customer)
        {
            using(DBContext context=new DBContext())
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }

        // PUT api/Customers/5
        public void Put(Customer customer)
        {
            
        }

        // DELETE api/Customers/5
        public void Delete(int id)
        {
        }
    }
}

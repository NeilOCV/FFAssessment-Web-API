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
        public IEnumerable<Customer> Get()
        {
            using (DBContext context = new DBContext())
            {
                return context.Customers.ToList();
            }
        }

        // GET api/Customers/5
        public Customer Get(int id)
        {
            using (DBContext context = new DBContext())
            {
                return context.Customers.FirstOrDefault(c => c.id == id);
            }
        }

        // POST /api/Customers
        public HttpResponseMessage Post([FromBody]Customer customer)
        {
            try
            {
                using (DBContext context = new DBContext())
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, customer);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + customer.id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT /api/Customers/5
        public HttpResponseMessage Put(int id, [FromBody]Customer customer)
        {
            try
            {
                using (DBContext context = new DBContext())
                {
                    

                    var entity = context.Customers.FirstOrDefault(c => c.id == id);
                    if (entity == null) //Tried to get something that does not exist.
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Customer with id " + id.ToString() + " not found.  Nothing to update!");
                    }
                    else
                    {
                        entity.name = customer.name;
                        entity.latitude = customer.latitude;
                        entity.longetude = customer.longetude;
                        
                        context.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // DELETE /api/Customers/5

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (DBContext context = new DBContext())
                {
                    #region Check to see if the customer has children before deleting
                    var children = context.Contacts.FirstOrDefault(o => o.customer_id == id);
                    if (children != null)
                        return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Customer with id " + id.ToString() + " still has contacts.  Please delete them first.");
                    #endregion

                    var entity = context.Customers.FirstOrDefault(c => c.id == id);
                    if (entity == null)//Nothig found to delete
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Customer with id " + id.ToString() + " not found.  Nothing deleted.");
                    else
                    {
                        context.Customers.Remove(entity);
                        context.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, "Customer with id " + id.ToString() + " deleted.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

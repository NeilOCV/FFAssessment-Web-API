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
    public class ContactsController : ApiController
    {
        // GET /api/Contacts
        public IEnumerable<Contact> Get()
        {
            using (DBContext context = new DBContext())
            {
                return context.Contacts.ToList();
            }
        }

        // GET /api/Contacts/5
        public Contact Get(int id)
        {
            using (DBContext context = new DBContext())
            {
                return context.Contacts.FirstOrDefault(c => c.id == id);
            }
        }

        // POST /api/Contacts
        public HttpResponseMessage Post([FromBody]Contact contact)
        {
            try
            {
                using (DBContext context = new DBContext())
                {
                    context.Contacts.Add(contact);
                    context.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, contact);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + contact.id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT /api/Contacts/5
        public HttpResponseMessage Put(int id, [FromBody]Contact contact)
        {
            try
            {
                using (DBContext context = new DBContext())
                {
                    var entity = context.Contacts.FirstOrDefault(c => c.id == id);
                    if (entity == null) //Tried to get something that does not exist.
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact with id " + id.ToString() + " not found.  Nothing to update!");
                    }
                    else
                    {
                        entity.name = contact.name;
                        entity.number = contact.number;
                        entity.email = contact.email;
                        entity.customer_id = contact.customer_id;

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

        // DELETE /api/Contacts/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (DBContext context = new DBContext())
                {
                    var entity = context.Contacts.FirstOrDefault(c => c.id == id);
                    if (entity == null)//Nothig found to delete
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact with id " + id.ToString() + " not found.  Nothing deleted.");
                    else
                    {
                        context.Contacts.Remove(entity);
                        context.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, "Entity with id " + id.ToString() + " deleted.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }
    }
}

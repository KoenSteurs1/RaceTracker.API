using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KartingApplication.Models;
using System.Text;

namespace KartingApplication.Controllers
{
    public class DriverController : ApiController
    {
        // GET api/driver
        public IEnumerable<Driver> Get()
        {
            using (KartingEntities entities = new KartingEntities())
            {
                return entities.Driver.ToList<Driver>();
            }
        }

        // GET api/driver/5
        public Driver Get(int id)
        {
            using (KartingEntities entities = new KartingEntities())
            {
                return entities.Driver.SingleOrDefault<Driver>(b => b.Id == id);
            }
        }

        // POST api/driver
        public HttpResponseMessage Post(Driver value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (KartingEntities entities = new KartingEntities())
                    {
                        entities.Driver.Add(value);
                        entities.SaveChanges();

                        var response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(value.Id.ToString(), Encoding.UTF8, "application/json");
                        return response;
                        //return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "invalid New Value");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/driver/5
        public HttpResponseMessage Put(int id, Driver value)
        {
            try
            {
                using (KartingEntities entities = new KartingEntities())
                {
                    Driver drv = entities.Driver.SingleOrDefault<Driver>(b => b.Id == id);
                    // update all the fields
                    drv.FirstName = value.FirstName;
                    drv.LastName = value.LastName;
                    drv.BirthDate = value.BirthDate;
                    // flush to disk
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/driver/5
        public void Delete(int id)
        {
        }
    }
}
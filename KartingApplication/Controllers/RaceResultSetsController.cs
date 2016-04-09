using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using KartingApplication.Models;

namespace KartingApplication.Controllers
{
    public class RaceResultSetsController : ApiController
    {
        private KartingEntities db = new KartingEntities();

        // GET: api/RaceResultSets
        public IQueryable<RaceResultSet> GetRaceResultSet()
        {
            return db.RaceResultSet.Include(b => b.Driver);
        }

        // GET: api/RaceResultSets/5
        [ResponseType(typeof(RaceResultSet))]
        public IHttpActionResult GetRaceResultSet(int id)
        {
            RaceResultSet raceResultSet = db.RaceResultSet.Find(id);
            //RaceResultSet raceResultSet = db.RaceResultSet.Include(b => b.Driver).SingleOrDefault(b => b.Id == id);
            
            if (raceResultSet == null)
            {
                return NotFound();
            }

            return Ok(raceResultSet);
        }

        // PUT: api/RaceResultSets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRaceResultSet(int id, RaceResultSet raceResultSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != raceResultSet.Id)
            {
                return BadRequest();
            }

            db.Entry(raceResultSet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceResultSetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RaceResultSets
        [ResponseType(typeof(RaceResultSet))]
        public IHttpActionResult PostRaceResultSet(RaceResultSet raceResultSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RaceResultSet.Add(raceResultSet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = raceResultSet.Id }, raceResultSet);
        }

        // DELETE: api/RaceResultSets/5
        [ResponseType(typeof(RaceResultSet))]
        public IHttpActionResult DeleteRaceResultSet(int id)
        {
            RaceResultSet raceResultSet = db.RaceResultSet.Find(id);
            if (raceResultSet == null)
            {
                return NotFound();
            }

            db.RaceResultSet.Remove(raceResultSet);
            db.SaveChanges();

            return Ok(raceResultSet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RaceResultSetExists(int id)
        {
            return db.RaceResultSet.Count(e => e.Id == id) > 0;
        }
    }
}
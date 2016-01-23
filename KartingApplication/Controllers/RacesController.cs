﻿using System;
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
    public class RacesController : ApiController
    {
        private KartingEntities db = new KartingEntities();

        // GET: api/Races
        public IEnumerable<Race> Get()
        {
            using (KartingEntities entities = new KartingEntities())
            {
                return entities.Race.ToList<Race>();
            }
        }

        // GET: api/Races/5
        [ResponseType(typeof(Race))]
        public IHttpActionResult GetRace(int id)
        {
            Race race = db.Race.Find(id);
            if (race == null)
            {
                return NotFound();
            }

            return Ok(race);
        }

        // PUT: api/Races/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRace(int id, Race race)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != race.ID)
            {
                return BadRequest();
            }

            db.Entry(race).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceExists(id))
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

        // POST: api/Races
        [ResponseType(typeof(Race))]
        public IHttpActionResult PostRace(Race race)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Race.Add(race);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RaceExists(race.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = race.ID }, race);
        }

        // DELETE: api/Races/5
        [ResponseType(typeof(Race))]
        public IHttpActionResult DeleteRace(int id)
        {
            Race race = db.Race.Find(id);
            if (race == null)
            {
                return NotFound();
            }

            db.Race.Remove(race);
            db.SaveChanges();

            return Ok(race);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RaceExists(int id)
        {
            return db.Race.Count(e => e.ID == id) > 0;
        }
    }
}
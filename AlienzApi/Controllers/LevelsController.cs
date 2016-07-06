using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AlienzApi.Models;
using AlienzApi.Models.GameModels;
using AlienzApi.Models.Interfaces;

namespace AlienzApi.Controllers
{
    public class LevelsController : ApiController
    {
        private IAlienzApiContext db = new AlienzApiContext();

        public LevelsController() { }

        public LevelsController(IAlienzApiContext context)
        {
            db = context;
        }

        // GET: api/Levels
        public IQueryable<Level> GetLevels()
        {
            return db.Levels;
        }

        // GET: api/Levels/5
        [ResponseType(typeof(Level))]
        public async Task<IHttpActionResult> GetLevel(int id)
        {
            Level level = await db.Levels.FindAsync(id);
            if (level == null)
            {
                return NotFound();
            }

            return Ok(level);
        }

        // PUT: api/Levels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLevel(int id, Level level)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != level.Id)
            {
                return BadRequest();
            }
            
            db.MarkAsModified(level);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LevelExists(id))
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

        // POST: api/Levels
        [ResponseType(typeof(Level))]
        public async Task<IHttpActionResult> PostLevel(Level level)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Levels.Add(level);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = level.Id }, level);
        }

        // DELETE: api/Levels/5
        //[ResponseType(typeof(Level))]
        //public async Task<IHttpActionResult> DeleteLevel(int id)
        //{
        //    Level level = await db.Levels.FindAsync(id);
        //    if (level == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Levels.Remove(level);
        //    await db.SaveChangesAsync();

        //    return Ok(level);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LevelExists(int id)
        {
            return db.Levels.Count(e => e.Id == id) > 0;
        }
    }
}
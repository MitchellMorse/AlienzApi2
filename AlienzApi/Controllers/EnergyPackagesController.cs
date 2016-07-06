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

namespace AlienzApi.Controllers
{
    public class EnergyPackagesController : ApiController
    {
        private AlienzApiContext db = new AlienzApiContext();

        // GET: api/EnergyPackages
        public IQueryable<EnergyPackage> GetEnergyPackages()
        {
            return db.EnergyPackages;
        }

        // GET: api/EnergyPackages/5
        [ResponseType(typeof(EnergyPackage))]
        public async Task<IHttpActionResult> GetEnergyPackage(int id)
        {
            EnergyPackage energyPackage = await db.EnergyPackages.FindAsync(id);
            if (energyPackage == null)
            {
                return NotFound();
            }

            return Ok(energyPackage);
        }

        // PUT: api/EnergyPackages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEnergyPackage(int id, EnergyPackage energyPackage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != energyPackage.Id)
            {
                return BadRequest();
            }

            db.Entry(energyPackage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnergyPackageExists(id))
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

        // POST: api/EnergyPackages
        [ResponseType(typeof(EnergyPackage))]
        public async Task<IHttpActionResult> PostEnergyPackage(EnergyPackage energyPackage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EnergyPackages.Add(energyPackage);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = energyPackage.Id }, energyPackage);
        }

        // DELETE: api/EnergyPackages/5
        [ResponseType(typeof(EnergyPackage))]
        public async Task<IHttpActionResult> DeleteEnergyPackage(int id)
        {
            EnergyPackage energyPackage = await db.EnergyPackages.FindAsync(id);
            if (energyPackage == null)
            {
                return NotFound();
            }

            db.EnergyPackages.Remove(energyPackage);
            await db.SaveChangesAsync();

            return Ok(energyPackage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnergyPackageExists(int id)
        {
            return db.EnergyPackages.Count(e => e.Id == id) > 0;
        }
    }
}
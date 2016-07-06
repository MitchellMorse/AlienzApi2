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
using AlienzApi.Models.DTO;
using AlienzApi.Models.GameModels;
using AlienzApi.Models.Interfaces;

namespace AlienzApi.Controllers
{
    public class PlayersController : ApiController
    {
        private IAlienzApiContext db = new AlienzApiContext();

        // GET: api/Players
        public IQueryable<Player> GetPlayers()
        {
            return db.Players;
        }

        // GET: api/Players/5
        [ResponseType(typeof(Player))]
        public async Task<IHttpActionResult> GetPlayer(int id)
        {
            Player player = await db.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        //[ResponseType(typeof(GameStartDto))]
        //public async Task<IHttpActionResult> GetPlayerGameStart(int id)
        //{
        //    //get furthest level player has attempted from LevelAttempt, with highest score for that level
        //}

        // PUT: api/Players/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlayer(int id, Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != player.Id)
            {
                return BadRequest();
            }
            
            db.MarkAsModified(player);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/Players
        [ResponseType(typeof(Player))]
        public async Task<IHttpActionResult> PostPlayer(Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Players.Add(player);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = player.Id }, player);
        }

        // DELETE: api/Players/5
        //[ResponseType(typeof(Player))]
        //public async Task<IHttpActionResult> DeletePlayer(int id)
        //{
        //    Player player = await db.Players.FindAsync(id);
        //    if (player == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Players.Remove(player);
        //    await db.SaveChangesAsync();

        //    return Ok(player);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerExists(int id)
        {
            return db.Players.Count(e => e.Id == id) > 0;
        }

        //private Level GetPlayerNextUncompletedLevel(int playerId)
        //{
        //    LevelAttempt highestCompletedLevelAttempt =
        //        db.LevelAttempts.Where(l => l.PlayerId == playerId && l.Completed)
        //            .OrderBy(l => l.Level.World)
        //            .ThenBy(l => l.Level.SequenceInWorld)
        //            .FirstOrDefault();
        //}
    }
}
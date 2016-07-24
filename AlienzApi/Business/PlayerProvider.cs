using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlienzApi.Models;
using AlienzApi.Models.DTO;
using AlienzApi.Models.GameModels;
using AlienzApi.Models.Interfaces;

namespace AlienzApi.Business
{
    public class PlayerProvider
    {
        private const int _maxPlayerLives = 5; //TODO: need to have a better way to handle max player lives

        private IAlienzApiContext db = new AlienzApiContext();

        public PlayerProvider(IAlienzApiContext dbContext)
        {
            db = dbContext;
        }

        public GameStartDto GetGameStartDto(int playerId)
        {
            GameStartDto gameStart = new GameStartDto();
            LevelProvider levelProvider = new LevelProvider(db);

            Level nextNonCompletedLevel = levelProvider.GetNextNonCompleteLevel(playerId);
            gameStart.AllLevelsInCurrentWorld = levelProvider.GetAllLevelsInWorld(nextNonCompletedLevel.World);
            gameStart.PlayerLives = GetCurrentPlayerLives(playerId);
            //gameStart.PowerupCount = GetPlayerCurrentPowerups(playerId);

            return gameStart;
        }

        public int GetCurrentPlayerLives(int playerId)
        {
            DateTime currentTime = DateTime.Now;
            List<PlayerDeath> playerDeaths =
                db.PlayerDeaths.Where(d => d.PlayerId == playerId && d.IrreleventTime >= currentTime).ToList();

            int currentPlayerLives = _maxPlayerLives - playerDeaths.Count;
            return currentPlayerLives > -1 ? currentPlayerLives : 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>Dictionary where the key is the powerup name and the int is the quantity the player currently has available</returns>
        public Dictionary<string, int> GetPlayerCurrentPowerups(int playerId)
        {
            var query = (from ep in db.EnergyPurchases.Where(ep => ep.PlayerId == playerId)
                         join epi in db.EnergyPurchaseableItems.Where(epi => epi.PowerupId != null) on
                             ep.EnergyPurchaseableItemId equals epi.Id
                         join p in db.Powerups on epi.PowerupId equals p.Id
                         group p by new { p.Id, p.Name, epi.Quantity } into pGrouped
                         orderby pGrouped.Key
                         select new { pGrouped.Key.Name, Quantity = pGrouped.Sum(p => pGrouped.Key.Quantity) }
                );

            List<KeyValuePair<string, int>> powerups = query.AsEnumerable().Select(i => new KeyValuePair<string, int>(i.Name, i.Quantity)).ToList();

            var powerupsSummed = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> powerup in powerups)
            {
                if (!powerupsSummed.ContainsKey(powerup.Key))
                {
                    powerupsSummed[powerup.Key] = powerup.Value;
                }
                else
                {
                    powerupsSummed[powerup.Key] += powerup.Value;
                }
            }

            return powerupsSummed;
        }
    }
}
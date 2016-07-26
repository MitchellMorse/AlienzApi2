using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            gameStart.PowerupCount = GetPlayerCurrentPowerups(playerId);

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
            Player player = db.Players.Where(p => p.Id == playerId)
                .Include(p => p.PlayerPowerupUsages.Select(pu => pu.Powerup))
                .Include(p => p.EnergyPurchases.Select(ep => ep.EnergyPurchaseableItem.Powerups))
                .Single();

            Dictionary<string, int> availablePowerups = new Dictionary<string, int>();

            foreach (
                var energyPurchase in
                    player.EnergyPurchases.Where(
                        e => e.EnergyPurchaseableItem.PowerupId != null && e.EnergyPurchaseableItem.PowerupId > 0))
            {
                if (!availablePowerups.ContainsKey(energyPurchase.EnergyPurchaseableItem.Powerups.Name))
                {
                    availablePowerups[energyPurchase.EnergyPurchaseableItem.Powerups.Name] =
                        energyPurchase.EnergyPurchaseableItem.Quantity;
                }
                else
                {
                    availablePowerups[energyPurchase.EnergyPurchaseableItem.Powerups.Name] +=
                        energyPurchase.EnergyPurchaseableItem.Quantity;
                }
            }

            foreach (
                var powerupUsage in player.PlayerPowerupUsages.Where(pu => pu.PowerupId > 0))
            {
                availablePowerups[powerupUsage.Powerup.Name] -= 1;
            }

            return availablePowerups;
        }
    }
}
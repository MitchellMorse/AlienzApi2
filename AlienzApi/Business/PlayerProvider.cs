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

        //private ICollection<KeyValuePair<string, int>> GetPlayerCurrentPowerups(int playerId)
        //{
            
        //}
    }
}
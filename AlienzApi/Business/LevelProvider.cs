using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using AlienzApi.Models;
using AlienzApi.Models.GameModels;
using AlienzApi.Models.Interfaces;

namespace AlienzApi.Business
{
    public class LevelProvider
    {
        private IAlienzApiContext db = new AlienzApiContext();

        public LevelProvider(IAlienzApiContext dbContext)
        {
            db = dbContext;
        }

        public IOrderedQueryable<Level> GetOrderedLevels()
        {
            return db.Levels.Where(l => l.Active).OrderBy(l => l.World)
                .ThenBy(l => l.SequenceInWorld);
        }

        public Level GetNextNonCompleteLevel(int playerId)
        {
            Level nextLevel;
            LevelAttempt highestCompletedLevelAttempt = GetHighestCompletedLevelAttemptForPlayer(playerId);

            IOrderedQueryable<Level> orderedLevels = GetOrderedLevels();

            if (highestCompletedLevelAttempt == null)
            {
                //the player has not completed any levels, so just get the first
                nextLevel = orderedLevels.First(l => l.Active);
            }
            else
            {
                //check to see if there is another level in the current world
                nextLevel =
                    orderedLevels
                        .SingleOrDefault(
                            l =>
                                l.World == highestCompletedLevelAttempt.Level.World &&
                                l.SequenceInWorld == highestCompletedLevelAttempt.Level.SequenceInWorld + 1);

                if (nextLevel == null)
                {
                    //check to see if there is a next world
                    nextLevel =
                        orderedLevels
                            .SingleOrDefault(
                                l =>
                                    l.World == highestCompletedLevelAttempt.Level.World + 1 &&
                                    l.SequenceInWorld == 1);
                }

                if (nextLevel == null)
                {
                    //the player has completed all levels, so just return the last level
                    nextLevel =
                        orderedLevels
                            .Last();
                }
            }

            return nextLevel;
        }
        
        public LevelAttempt GetHighestCompletedLevelAttemptForPlayer(int playerId)
        {
            LevelAttempt highestCompletedLevelAttempt =
                db.LevelAttempts.Include(l => l.Level).Where(l => l.PlayerId == playerId && l.Completed)
                    .OrderByDescending(l => l.Level.World)
                    .ThenByDescending(l => l.Level.SequenceInWorld)
                    .FirstOrDefault();

            return highestCompletedLevelAttempt;
        }
    }
}
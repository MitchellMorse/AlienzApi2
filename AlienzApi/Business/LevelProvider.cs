using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using AlienzApi.Models;
using AlienzApi.Models.DTO;
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
                        orderedLevels.OrderByDescending(o => o.World).ThenByDescending(o => o.SequenceInWorld)
                            .First();
                }
            }

            return nextLevel;
        }

        public ICollection<LevelDto> GetAllLevelsInWorld(int worldId)
        {
            return (from level in db.Levels
                join tier1Info in db.TierScoreRewards on new {LevelId = level.Id, TierNumber = 1} equals
                    new {tier1Info.LevelId, tier1Info.TierNumber}
                join tier1AwardReason in db.AwardReasons on tier1Info.AwardReasonId equals tier1AwardReason.Id
                join tier2Info in db.TierScoreRewards on new {LevelId = level.Id, TierNumber = 2} equals
                    new {tier2Info.LevelId, tier2Info.TierNumber}
                join tier2AwardReason in db.AwardReasons on tier2Info.AwardReasonId equals tier2AwardReason.Id
                join tier3Info in db.TierScoreRewards on new {LevelId = level.Id, TierNumber = 3} equals
                    new {tier3Info.LevelId, tier3Info.TierNumber}
                join tier3AwardReason in db.AwardReasons on tier3Info.AwardReasonId equals tier3AwardReason.Id
                from levelAttempts in
                    db.LevelAttempts.Where(
                        la =>
                            la.LevelId == level.Id &&
                            !db.LevelAttempts.Any(
                                laHigher =>
                                    laHigher.Id != la.Id && laHigher.LevelId == la.LevelId && (laHigher.Score > la.Score || (laHigher.Score == la.Score && laHigher.Id > la.Id))))
                        .DefaultIfEmpty()
                where level.World == worldId
                select new LevelDto()
                {
                    PlayerHighScore = levelAttempts != null ? levelAttempts.Score : 0,
                    Sequence = level.SequenceInWorld,
                    StartingFuel = level.StartingFuel,
                    StartingTimeSeconds = level.StartingTime,
                    Tier1Reward = tier1AwardReason.EnergyRewardAmount,
                    Tier1Score = tier1Info.Score,
                    Tier2Reward = tier2AwardReason.EnergyRewardAmount,
                    Tier2Score = tier2Info.Score,
                    Tier3Reward = tier3AwardReason.EnergyRewardAmount,
                    Tier3Score = tier3Info.Score
                }).ToList();
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
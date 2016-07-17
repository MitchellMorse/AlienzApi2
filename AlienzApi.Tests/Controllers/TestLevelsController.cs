using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AlienzApi.Business;
using AlienzApi.Controllers;
using AlienzApi.Models;
using AlienzApi.Models.DTO;
using AlienzApi.Models.GameModels;
using AlienzApi.Models.Interfaces;
using AlienzApi.Tests.DbSets;
using AlienzApi.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlienzApi.Tests.Controllers
{
    [TestClass]
    public class TestLevelsController : AlienzTester
    {
        #region AliensTester

        protected override IAlienzApiContext DbContext => new AlienzApiContext();

        #endregion

        [TestCleanup]
        public void CleanupTests()
        {
            CleanTests();
        }

        [TestInitialize]
        public void InitTests()
        {
            InitializeDbContext();
            ResetLists();
        }

        [TestMethod]
        public async Task PutProduct_ShouldReturnStatusCode()
        {
            var controller = new LevelsController(new TestAlienzApiContext());

            var item = GetDemoLevel();

            var result = await controller.PutLevel(item.Id, item) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public async void PutProduct_ShouldFail_WhenDifferentID()
        {
            var controller = new LevelsController(new TestAlienzApiContext());

            var item = GetDemoLevel();
            var badresult = await controller.PutLevel(999, item) as StatusCodeResult;
            Assert.IsInstanceOfType(badresult, typeof(BadRequestResult));
        }

        [TestMethod]
        public async void GetProduct_ShouldReturnProductWithSameID()
        {
            var context = new TestAlienzApiContext();
            context.Levels.Add(GetDemoLevel());

            var controller = new LevelsController(context);
            var result = await controller.GetLevel(3) as OkNegotiatedContentResult<Level>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.Id);
        }

        [TestMethod]
        public void GetProducts_ShouldReturnAllProducts()
        {
            var context = new TestAlienzApiContext();
            context.Levels.Add(new Level { Id = 1, World = 1, SequenceInWorld = 1 });
            context.Levels.Add(new Level { Id = 2, World = 1, SequenceInWorld = 2 });
            context.Levels.Add(new Level { Id = 3, World = 1, SequenceInWorld = 3 });

            var controller = new LevelsController(context);
            var result = controller.GetLevels() as TestLevelDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        #region GetHighestCompletedLevelAttemptForPlayer
        [TestMethod]
        public void GetHighestCompletedLevelAttemptForPlayer_OneCompletedSecondNotTried()
        {
            //Arrange
            Level level1 = GetTestLevel();
            Level level2 = GetTestLevel(sequenceInWorld: 2);
            Level level3 = GetTestLevel(sequenceInWorld: 3);
            Player player = GetTestPlayer();
            LevelAttempt savedAttempt = GetTestLevelAttempt(player.Id, level1.Id, completed: true);

            LevelProvider provider = new LevelProvider(_context);

            //Act
            LevelAttempt attempt = provider.GetHighestCompletedLevelAttemptForPlayer(player.Id);

            //Assert
            Assert.AreEqual(savedAttempt.Id, attempt.Id);
        }

        [TestMethod]
        public void GetHighestCompletedLevelAttemptForPlayer_NoneTried()
        {
            //Arrange
            Level level1 = GetTestLevel();
            Player player = GetTestPlayer();

            LevelProvider provider = new LevelProvider(_context);

            //Act
            LevelAttempt attempt = provider.GetHighestCompletedLevelAttemptForPlayer(player.Id);

            //Assert
            Assert.AreEqual(null, attempt);
        }

        [TestMethod]
        public void GetHighestCompletedLevelAttemptForPlayer_AllCompleted()
        {
            //Arrange
            Level level1 = GetTestLevel();
            Level level2 = GetTestLevel(sequenceInWorld: 2);
            Player player = GetTestPlayer();

            LevelAttempt savedAttempt1 = GetTestLevelAttempt(player.Id, level1.Id, completed: true);
            LevelAttempt savedAttempt2 = GetTestLevelAttempt(player.Id, level2.Id, completed: true);

            LevelProvider provider = new LevelProvider(_context);

            //Act
            LevelAttempt attempt = provider.GetHighestCompletedLevelAttemptForPlayer(player.Id);

            //Assert
            Assert.AreEqual(savedAttempt2.Id, attempt.Id);
        }
        #endregion

        #region GetNextNonCompleteLevel

        [TestMethod]
        public void GetNextNonCompleteLevel_OneLevelCompleted()
        {
            //Arrange
            Level level1 = GetTestLevel();
            Level level2 = GetTestLevel(sequenceInWorld:2);
            Level level3 = GetTestLevel(sequenceInWorld:3);
            Player player = GetTestPlayer();
            LevelAttempt savedAttempt = GetTestLevelAttempt(player.Id, level1.Id, completed: true);
            LevelProvider provider = new LevelProvider(_context);

            //Act
            Level nextLevel = provider.GetNextNonCompleteLevel(player.Id);

            //Assert
            Assert.AreEqual(level2.Id, nextLevel.Id);
        }

        [TestMethod]
        public void GetNextNonCompleteLevel_AllLevelsCompleted()
        {
            //Arrange
            Level level1 = GetTestLevel();
            Level level2 = GetTestLevel(sequenceInWorld: 2);
            Player player = GetTestPlayer();
            LevelAttempt savedAttempt = GetTestLevelAttempt(player.Id, level1.Id, completed: true);
            LevelAttempt savedAttempt2 = GetTestLevelAttempt(player.Id, level2.Id, completed: true);

            LevelProvider provider = new LevelProvider(_context);

            //Act
            Level nextLevel = provider.GetNextNonCompleteLevel(player.Id);

            //Assert
            Assert.AreEqual(level2.Id, nextLevel.Id);
        }
        #endregion

        #region GetOrderedLevels
        [TestMethod]
        public void GetOrderedLevels_MultipleLevelsInTwoWorlds()
        {
            //Arrange
            Level level1 = GetTestLevel(10000, 2);
            Level level2 = GetTestLevel();
            Level level3 = GetTestLevel(10000);

            LevelProvider provider = new LevelProvider(_context);

            //Act
            List<Level> levels = provider.GetOrderedLevels().ToList();

            //Assert
            int level1Index = levels.FindIndex(l => l.Id == level1.Id);
            int level2Index = levels.FindIndex(l => l.Id == level2.Id);
            int level3Index = levels.FindIndex(l => l.Id == level3.Id);

            Assert.IsTrue(level1Index > level2Index && level1Index > level3Index && level2Index < level3Index);
        }
        #endregion

        #region GetAllLevelsInWorld

        [TestMethod]
        public void GetAllLevelsInWorld_3LevelsNoAttempts()
        {
            //Arrange
            Level level1 = GetTestLevel();
            Level level2 = GetTestLevel(sequenceInWorld: 2);
            Level level3 = GetTestLevel(sequenceInWorld: 3);
            Setup3TiersForLevel(level1.Id);
            Setup3TiersForLevel(level2.Id);
            Setup3TiersForLevel(level3.Id);

            LevelProvider provider = new LevelProvider(_context);

            //Act
            ICollection<LevelDto> levels = provider.GetAllLevelsInWorld(_testWorldId);

            //Assert
            int expectedCount = 3;
            int expectedTier1Reward = 1;
            int expectedTier2Reward = 2;
            int expectedTier3Reward = 3;
            Assert.AreEqual(expectedCount, levels.Count);
            Assert.AreEqual(expectedTier1Reward, levels.Single(l => l.Sequence == 1).Tier1Reward);
            Assert.AreEqual(expectedTier2Reward, levels.Single(l => l.Sequence == 2).Tier2Reward);
            Assert.AreEqual(expectedTier3Reward, levels.Single(l => l.Sequence == 1).Tier3Reward);
        }

        [TestMethod]
        public void GetAllLevelsInWorld_3LevelsLevel1Completed2Attempts()
        {
            //Arrange
            Level level1 = GetTestLevel();
            Level level2 = GetTestLevel(sequenceInWorld: 2);
            Level level3 = GetTestLevel(sequenceInWorld: 3);
            Setup3TiersForLevel(level1.Id);
            Setup3TiersForLevel(level2.Id);
            Setup3TiersForLevel(level3.Id);
            Player player = GetTestPlayer();
            LevelAttempt attemptFailed = GetTestLevelAttempt(player.Id, level1.Id, score: 543, timeSeconds: 543,
                completed: false, timesDied:5);
            LevelAttempt attemptSuccess = GetTestLevelAttempt(player.Id, level1.Id, score: 1022, timeSeconds: 325,
                completed: true);

            LevelProvider provider = new LevelProvider(_context);

            //Act
            ICollection<LevelDto> levels = provider.GetAllLevelsInWorld(_testWorldId);

            //Assert
            int expectedCount = 3;
            int expectedLevel1HighScore = 1022;
            int expectedLevel2HighScore = 0;
            Assert.AreEqual(expectedCount, levels.Count);
            Assert.AreEqual(expectedLevel1HighScore, levels.Single(l => l.Sequence == 1).PlayerHighScore);
            Assert.AreEqual(expectedLevel2HighScore, levels.Single(l => l.Sequence == 2).PlayerHighScore);
        }

        [TestMethod]
        public void GetAllLevelsInWorld_3LevelsLevel1CompletedTie()
        {
            //Arrange
            Level level1 = GetTestLevel();
            Level level2 = GetTestLevel(sequenceInWorld: 2);
            Level level3 = GetTestLevel(sequenceInWorld: 3);
            Setup3TiersForLevel(level1.Id);
            Setup3TiersForLevel(level2.Id);
            Setup3TiersForLevel(level3.Id);
            Player player = GetTestPlayer();
            LevelAttempt attemptFailed = GetTestLevelAttempt(player.Id, level1.Id, score: 1022, timeSeconds: 543,
                completed: true);
            LevelAttempt attemptSuccess = GetTestLevelAttempt(player.Id, level1.Id, score: 1022, timeSeconds: 325,
                completed: true);

            LevelProvider provider = new LevelProvider(_context);

            //Act
            ICollection<LevelDto> levels = provider.GetAllLevelsInWorld(_testWorldId);

            //Assert
            int expectedCount = 3;
            int expectedLevel1HighScore = 1022;
            int expectedLevel2HighScore = 0;
            Assert.AreEqual(expectedCount, levels.Count);
            Assert.AreEqual(expectedLevel1HighScore, levels.Single(l => l.Sequence == 1).PlayerHighScore);
            Assert.AreEqual(expectedLevel2HighScore, levels.Single(l => l.Sequence == 2).PlayerHighScore);
        }
        #endregion

        Level GetDemoLevel()
        {
            return new Level()
            {
                Id = 3, World = _testWorldId, SequenceInWorld = 3, StartingTime = 100, StartingFuel = 100
            };
        }
    }
}

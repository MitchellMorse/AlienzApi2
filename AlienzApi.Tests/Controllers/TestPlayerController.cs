using System;
using AlienzApi.Business;
using AlienzApi.Models;
using AlienzApi.Models.GameModels;
using AlienzApi.Models.Interfaces;
using AlienzApi.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlienzApi.Tests.Controllers
{
    [TestClass]
    public class TestPlayerController : AlienzTester
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

        #region GetCurrentPlayerLives
        [TestMethod]
        public void GetCurrentPlayerLives_NoDeaths()
        {
            //Arrange
            Player player = GetTestPlayer();

            PlayerProvider provider = new PlayerProvider(_context);

            //Act
            int lives = provider.GetCurrentPlayerLives(player.Id);

            //Assert
            int expectedLives = 5;
            Assert.AreEqual(expectedLives, lives);
        }

        [TestMethod]
        public void GetCurrentPlayerLives_1Death()
        {
            //Arrange
            Player player = GetTestPlayer();
            Level level = GetTestLevel();
            LevelAttempt attempt = GetTestLevelAttempt(player.Id, level.Id);
            PlayerDeath death = GetTestPlayerDeath(player.Id, attempt.Id);

            PlayerProvider provider = new PlayerProvider(_context);

            //Act
            int lives = provider.GetCurrentPlayerLives(player.Id);

            //Assert
            int expectedLives = 4;
            Assert.AreEqual(expectedLives, lives);
        }

        [TestMethod]
        public void GetCurrentPlayerLives_MaxDeaths()
        {
            //Arrange
            Player player = GetTestPlayer();
            Level level = GetTestLevel();
            LevelAttempt attempt = GetTestLevelAttempt(player.Id, level.Id);
            PlayerDeath death = GetTestPlayerDeath(player.Id, attempt.Id);
            GetTestPlayerDeath(player.Id, attempt.Id);
            GetTestPlayerDeath(player.Id, attempt.Id);
            GetTestPlayerDeath(player.Id, attempt.Id);
            GetTestPlayerDeath(player.Id, attempt.Id);

            PlayerProvider provider = new PlayerProvider(_context);

            //Act
            int lives = provider.GetCurrentPlayerLives(player.Id);

            //Assert
            int expectedLives = 0;
            Assert.AreEqual(expectedLives, lives);
        }

        [TestMethod]
        public void GetCurrentPlayerLives_IrrelevantDeath()
        {
            //Arrange
            Player player = GetTestPlayer();
            Level level = GetTestLevel();
            LevelAttempt attempt = GetTestLevelAttempt(player.Id, level.Id);
            PlayerDeath death = GetTestPlayerDeath(player.Id, attempt.Id, DateTime.Now.AddMinutes(-1));

            PlayerProvider provider = new PlayerProvider(_context);

            //Act
            int lives = provider.GetCurrentPlayerLives(player.Id);

            //Assert
            int expectedLives = 5;
            Assert.AreEqual(expectedLives, lives);
        }
        #endregion
    }
}

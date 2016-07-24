using System;
using System.Collections.Generic;
using System.Linq;
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

        #region GetPlayerCurrentPowerups

        [TestMethod]
        public void GetPlayerCurrentPowerups_NoPowerups()
        {
            //Arrange
            Player player = GetTestPlayer();

            PlayerProvider provider = new PlayerProvider(_context);

            //Act
            ICollection<KeyValuePair<string, int>> powerups = provider.GetPlayerCurrentPowerups(player.Id);

            //Assert
            int expectedCount = 0;
            Assert.AreEqual(expectedCount, powerups.Count);
        }

        [TestMethod]
        public void GetPlayerCurrentPowerups_Bought6Speed9JumpUsedNone()
        {
            //Arrange
            Player player = GetTestPlayer();
            GetTestEnergyPurchase(player.Id, 3);
            GetTestEnergyPurchase(player.Id, 3);
            GetTestEnergyPurchase(player.Id, 5);
            GetTestEnergyPurchase(player.Id, 6);

            PlayerProvider provider = new PlayerProvider(_context);

            //Act
            ICollection<KeyValuePair<string, int>> powerups = provider.GetPlayerCurrentPowerups(player.Id);

            //Assert
            int expectedCount = 2;
            int expectedSpeedCount = 6;
            int expectedJumpCount = 9;
            Assert.AreEqual(expectedCount, powerups.Count);
            Assert.AreEqual(expectedSpeedCount, powerups.Where(p => p.Key == "Speed").Sum(p => p.Value));
            Assert.AreEqual(expectedJumpCount, powerups.Where(p => p.Key == "Jump").Sum(p => p.Value));
        }

        [TestMethod]
        public void GetPlayerCurrentPowerups_Bought6Speed9JumpUsed1Speed3Jumps()
        {
            //Arrange
            Player player = GetTestPlayer();
            Level level = GetTestLevel();
            LevelAttempt attempt = GetTestLevelAttempt(player.Id, level.Id);
            GetTestEnergyPurchase(player.Id, 3);
            GetTestEnergyPurchase(player.Id, 3);
            GetTestEnergyPurchase(player.Id, 5);
            GetTestEnergyPurchase(player.Id, 6);
            GetTestPlayerPowerupUsage(player.Id, 1, attempt.Id);
            GetTestPlayerPowerupUsage(player.Id, 2, attempt.Id);
            GetTestPlayerPowerupUsage(player.Id, 2, attempt.Id);
            GetTestPlayerPowerupUsage(player.Id, 2, attempt.Id);

            PlayerProvider provider = new PlayerProvider(_context);

            //Act
            ICollection<KeyValuePair<string, int>> powerups = provider.GetPlayerCurrentPowerups(player.Id);

            //Assert
            int expectedCount = 2;
            int expectedSpeedCount = 5;
            int expectedJumpCount = 6;
            Assert.AreEqual(expectedCount, powerups.Count);
            Assert.AreEqual(expectedSpeedCount, powerups.Where(p => p.Key == "Speed").Sum(p => p.Value));
            Assert.AreEqual(expectedJumpCount, powerups.Where(p => p.Key == "Jump").Sum(p => p.Value));
        }

        [TestMethod]
        public void GetPlayerCurrentPowerups_Bought6Speed9JumpUsedAllSpeeds()
        {
            //Arrange
            Player player = GetTestPlayer();
            Level level = GetTestLevel();
            LevelAttempt attempt = GetTestLevelAttempt(player.Id, level.Id);
            GetTestEnergyPurchase(player.Id, 3);
            GetTestEnergyPurchase(player.Id, 3);
            GetTestEnergyPurchase(player.Id, 5);
            GetTestEnergyPurchase(player.Id, 6);
            GetTestPlayerPowerupUsage(player.Id, 1, attempt.Id);
            GetTestPlayerPowerupUsage(player.Id, 1, attempt.Id);
            GetTestPlayerPowerupUsage(player.Id, 1, attempt.Id);
            GetTestPlayerPowerupUsage(player.Id, 1, attempt.Id);
            GetTestPlayerPowerupUsage(player.Id, 1, attempt.Id);
            GetTestPlayerPowerupUsage(player.Id, 1, attempt.Id);
            GetTestPlayerPowerupUsage(player.Id, 2, attempt.Id);
            GetTestPlayerPowerupUsage(player.Id, 2, attempt.Id);
            GetTestPlayerPowerupUsage(player.Id, 2, attempt.Id);

            PlayerProvider provider = new PlayerProvider(_context);

            //Act
            ICollection<KeyValuePair<string, int>> powerups = provider.GetPlayerCurrentPowerups(player.Id);

            //Assert
            int expectedCount = 2;
            int expectedSpeedCount = 0;
            int expectedJumpCount = 6;
            Assert.AreEqual(expectedCount, powerups.Count);
            Assert.AreEqual(expectedSpeedCount, powerups.Where(p => p.Key == "Speed").Sum(p => p.Value));
            Assert.AreEqual(expectedJumpCount, powerups.Where(p => p.Key == "Jump").Sum(p => p.Value));
        }
        #endregion
    }
}

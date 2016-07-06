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
using AlienzApi.Models.GameModels;
using AlienzApi.Tests.DbSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlienzApi.Tests.Controllers
{
    [TestClass]
    public class TestLevelsController
    {
        private List<int> LevelAttemptIdsToCleanup { get; set; }
        private List<int> LevelIdsToCleanup { get; set; }
        private List<int> PlayerIdsToCleanup { get; set; }

        private void ResetLists()
        {
            LevelAttemptIdsToCleanup = new List<int>();
            LevelIdsToCleanup = new List<int>();
            PlayerIdsToCleanup = new List<int>();
        }

        [TestInitialize]
        public void InitTests()
        {
            ResetLists();
        }

        [TestCleanup]
        public void CleanupTests()
        {
            var db = new AlienzApiContext();

            if (LevelAttemptIdsToCleanup.Any())
            {
                var levelAttempts = db.LevelAttempts.Where(l => LevelAttemptIdsToCleanup.Contains(l.Id));

                foreach (var attempt in levelAttempts)
                {
                    db.LevelAttempts.Remove(attempt);
                }

                db.SaveChanges();
            }

            if (LevelIdsToCleanup.Any())
            {
                var levels = db.Levels.Where(l => LevelIdsToCleanup.Contains(l.Id));

                foreach (var level in levels)
                {
                    db.Levels.Remove(level);
                }

                db.SaveChanges();
            }

            if (PlayerIdsToCleanup.Any())
            {
                var players = db.Players.Where(l => PlayerIdsToCleanup.Contains(l.Id));

                foreach (var player in players)
                {
                    db.Players.Remove(player);
                }

                db.SaveChanges();
            }

            ResetLists();
        }

        //[TestMethod]
        //public async void PostProduct_ShouldReturnSameProduct()
        //{
        //    var controller = new LevelsController(new TestAlienzApiContext());

        //    var item = GetDemoLevel();

        //    var result =
        //        await controller.PostLevel(item) as CreatedAtRouteNegotiatedContentResult<Level>;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.RouteName, "DefaultApi");
        //    Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
        //    Assert.AreEqual(result.Content.World, item.World);
        //    Assert.AreEqual(result.Content.SequenceInWorld, item.SequenceInWorld);
        //}

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

        [TestMethod]
        public void GetHighestCompletedLevelAttemptForPlayer_OneCompletedSecondNotTried()
        {
            var context = new AlienzApiContext();
            
            Level level1 = context.Levels.Add(new Level
            {
                World = 1,
                SequenceInWorld = 1,
                StartingFuel = 100,
                StartingTime = 500,
                Active = true,
                IsBlockingLevel = false
            });

            Level level2 = context.Levels.Add(new Level
            {
                World = 1,
                SequenceInWorld = 2,
                StartingFuel = 100,
                StartingTime = 500,
                Active = true,
                IsBlockingLevel = false
            });

            Level level3 = context.Levels.Add(new Level
            {
                World = 1,
                SequenceInWorld = 3,
                StartingFuel = 100,
                StartingTime = 500,
                Active = true,
                IsBlockingLevel = false
            });

            context.SaveChanges();
            LevelIdsToCleanup.AddRange(new List<int>() { level1.Id, level2.Id, level3.Id });

            Player player = context.Players.Add(new Player());
            context.SaveChanges();

            PlayerIdsToCleanup.AddRange(new List<int>() { player.Id });

            LevelAttempt savedAttempt = context.LevelAttempts.Add(new LevelAttempt
            {
                LevelId = level1.Id,
                PlayerId = player.Id,
                Date = DateTime.Now,
                TimesDied = 0,
                Score = 100,
                TimeSeconds = 100,
                Completed = true
            });

            context.SaveChanges();
            LevelAttemptIdsToCleanup.AddRange(new List<int>() { savedAttempt.Id });

            LevelProvider provider = new LevelProvider(context);
            LevelAttempt attempt = provider.GetHighestCompletedLevelAttemptForPlayer(player.Id);

            Assert.AreEqual(savedAttempt.Id, attempt.Id);
        }

        //[TestMethod]
        //public async void DeleteProduct_ShouldReturnOK()
        //{
        //    var context = new TestAlienzApiContext();
        //    var item = GetDemoLevel();
        //    context.Levels.Add(item);

        //    var controller = new LevelsController(context);
        //    var result = await controller.DeleteLevel(3) as OkNegotiatedContentResult<Level>;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(item.Id, result.Content.Id);
        //}

        Level GetDemoLevel()
        {
            return new Level()
            {
                Id = 3, World = 1, SequenceInWorld = 3, StartingTime = 100, StartingFuel = 100
            };
        }
    }
}

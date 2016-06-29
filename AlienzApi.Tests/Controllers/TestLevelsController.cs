using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AlienzApi.Controllers;
using AlienzApi.Models.GameModels;
using AlienzApi.Tests.DbSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlienzApi.Tests.Controllers
{
    [TestClass]
    public class TestLevelsController
    {
        [TestMethod]
        public async void PostProduct_ShouldReturnSameProduct()
        {
            var controller = new LevelsController(new TestAlienzApiContext());

            var item = GetDemoLevel();

            var result =
                await controller.PostLevel(item) as CreatedAtRouteNegotiatedContentResult<Level>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.World, item.World);
            Assert.AreEqual(result.Content.SequenceInWorld, item.SequenceInWorld);
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
        public void PutProduct_ShouldFail_WhenDifferentID()
        {
            var controller = new LevelsController(new TestAlienzApiContext());

            var badresult = controller.PutLevel(999, GetDemoLevel());
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
        public async void DeleteProduct_ShouldReturnOK()
        {
            var context = new TestAlienzApiContext();
            var item = GetDemoLevel();
            context.Levels.Add(item);

            var controller = new LevelsController(context);
            var result = await controller.DeleteLevel(3) as OkNegotiatedContentResult<Level>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.Id, result.Content.Id);
        }

        Level GetDemoLevel()
        {
            return new Level()
            {
                Id = 3, World = 1, SequenceInWorld = 3, StartingTime = 100, StartingFuel = 100, Tier3Score = 300, Tier3Reward = 1, Tier2Reward = 1, Tier1Reward = 1,
                    Tier2Score = 200
            };
        }
    }
}

using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Http;
using GlossaryWebUI.Helpers;
using System.Net;

namespace GlossaryWebUI.Tests.Controllers
{
    [TestClass]
    public class GlossaryControllerTest
    {
        
        [TestMethod]
        public void Index()
        {
            Mock<GlossaryWebAPI.Controllers.GlossaryController> mock = new Mock<GlossaryWebAPI.Controllers.GlossaryController>();
            var glossaries = new HttpResponseMessage(HttpStatusCode.OK);
            mock.Setup(m => m.GetTerms()).Returns(glossaries);

            IApiRestClient apiclient = new ApiRestClient(null);
            GlossaryWebUI.Controllers.GlossaryController controller = new GlossaryWebUI.Controllers.GlossaryController(null, apiclient);
            ActionResult result = controller.Index() as ActionResult;
            var expected = mock.Object;
            Assert.IsNotNull(result);
        }
    }
}

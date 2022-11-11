using AvigilonAlarmDemoApp.BusinessLayer.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvigilonAlarmDemoApp.BusinessLayer.Model;
using AvigilonAlarmDemoApp.DataAccesslayer.Manager;
using AvigilonAlarmDemoApp.DataAccesslayer.DataContracts;

namespace AvigilonAlarmDemoApp.UnitTests.BusinessLayer
{
    /// <summary>
    /// class for Unit test cases for Businesslogic
    /// </summary>
    [TestClass]
    public class AvigilonWebEndPointServiceBusinessLogicTest
    {
        private Mock<AvigilonWebEndpointServiceManager> avigilonWebEndPointService = new Mock<AvigilonWebEndpointServiceManager>();
        private AvigilonWebEndPointServiceBusinessLogic avigilonWebEndPointBusinessService = new AvigilonWebEndPointServiceBusinessLogic();

        #region Test Data

        /// <summary>
        /// Mocked alarm response
        /// </summary>
        /// <returns></returns>
        private List<AlarmModel> GetMockedAlarms()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mocked login response
        /// </summary>
        /// <returns></returns>
        private LoginResponseContract GetMockedLoginResponseContract()
        {
            return new LoginResponseContract
            {
                result = new LoginResultContract
                {
                    session = "test"
                },
                status = "200"
            };
        }

        #endregion

        /// <summary>
        /// Test method to verfy for null session
        /// </summary>
        [TestMethod]
        public void IsAuthenticatedForNullSessionTest()
        {

            var expected = false;
            var actual = avigilonWebEndPointBusinessService.IsAuthenticated();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method to verify for login
        /// </summary>
        [TestMethod]
        public void LoginToAvigilonWebEndpointTest()
        {
            avigilonWebEndPointService.Setup(s =>
            s.LoginToAvigilonWebEndpoint(It.IsAny<LoginRequestContract>()))
            .Returns(GetMockedLoginResponseContract());
            var loginModel = new LoginRequestModel
            {
                ClientId = "",
                UserName = "",
                Password = ""
            };
            var expected = true;
            var actual = avigilonWebEndPointBusinessService.LoginToAvigilonWebEndpoint(loginModel);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method to verify null login
        /// </summary>
        [TestMethod]
        public void LoginToAvigilonWebEndpointForNullResponseTest()
        {
            LoginResponseContract loginResponse = null;
            avigilonWebEndPointService.Setup(s =>
            s.LoginToAvigilonWebEndpoint(It.IsAny<LoginRequestContract>()))
            .Returns(loginResponse);
            var loginModel = new LoginRequestModel
            {
                ClientId = "",
                UserName = "",
                Password = ""
            };
            var expected = false;
            var actual = avigilonWebEndPointBusinessService.LoginToAvigilonWebEndpoint(loginModel);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method to verify QueryAvigilonWebEndpointForAlarms
        /// </summary>
        [TestMethod]
        public void QueryAvigilonWebEndpointForAlarmsTest()
        {
            var expected = false;
            var actual = avigilonWebEndPointBusinessService.QueryAvigilonWebEndpointForAlarms();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method to verify QueryAvigilonWebEndpointForUpdatingAlarms
        /// </summary>
        [TestMethod]
        public void QueryAvigilonWebEndpointForUpdatingAlarmStatusTest()
        {
            AvigilonAlarmDemoApp.BusinessLayer.Model.AlarmModel selectedAlarm = null;
            string comment = "";
            AvigilonAlarmDemoApp.BusinessLayer.Model.ActionsModelType action = (AvigilonAlarmDemoApp.BusinessLayer.Model.ActionsModelType)((int)AvigilonAlarmDemoApp.BusinessLayer.Model.ActionsModelType.UNCLAIM);
            var expected = false;
            var actual = avigilonWebEndPointBusinessService.QueryAvigilonWebEndpointForUpdatingAlarmStatus(selectedAlarm,
             comment,
             action);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method to verify LogoutFromAvigilonWebEndpoint
        /// </summary>
        [TestMethod]
        public void LogoutFromAvigilonWebEndpointTest()
        {
            var expected = false;
            var actual = avigilonWebEndPointBusinessService.LogoutFromAvigilonWebEndpoint();
            Assert.AreEqual(expected, actual);
        }
        
    }
}

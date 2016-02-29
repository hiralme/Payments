using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WCFClient.CreditcardPaymentService;
using WCFClient;
using Microsoft.Practices.Unity;

namespace WCFClient.Tests
{
    [TestClass]
    public class WcfClientTest
    {
        [TestMethod]
        public void MockWhatsYourId()
        {
            string val = "59bb09f7-df66-45d0-9bcc-79c2d4d2ced1";
            string actualRetVal;

            // mock wcf interface
            Mock<IPaymentService> wcfMock = new Mock<IPaymentService>();

            // setup for wcf service WhatsYourId method
            wcfMock.Setup<string>(s => s.WhatsYourId()).Returns(val);

            // create object to register with IoC
            IPaymentService wcfMockObject = wcfMock.Object;

            // register instance
            UnityHelper.IoC = new UnityContainer();
            UnityHelper.IoC.RegisterInstance<IPaymentService>(wcfMockObject);

            // create ServiceAgent object using parameterized constructor (for unit test)
            PaymentServiceAgent serviceAgent = new PaymentServiceAgent(true);

            // method call to be tested
            actualRetVal = serviceAgent.GetID();

            // verify if the expected method called during test or not
            wcfMock.Verify(s => s.WhatsYourId(), Times.Exactly(1));

            Assert.AreEqual("59bb09f7-df66-45d0-9bcc-79c2d4d2ced1", actualRetVal, "Not same.");
        }
        
    }
}
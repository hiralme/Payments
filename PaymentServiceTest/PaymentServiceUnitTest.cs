using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payments;

namespace PaymentServiceTest
{
    [TestClass]
    public class PaymentServiceUnitTest
    {
        private IPaymentService _paymentService = null;

        [TestInitialize]
        public void setup()
        {
            _paymentService = new PaymentService();
        }


        [TestMethod]        
        public void TestWhatsYourId_success()
        {
            string result = _paymentService.WhatsYourId();
            Assert.AreEqual<string>("59bb09f7-df66-45d0-9bcc-79c2d4d2ced1", result);
        }

        [TestMethod]
        public void TestWhatsYourId_fail()
        {
            
            string result = _paymentService.WhatsYourId();
            Assert.AreNotEqual<string>("59bb09f7-df66-45d0-9bcc-79c2d4d2ced2", result);
        }

        [TestMethod]
        public void TestIsCardNumberValid_success()
        {
            bool result = _paymentService.IsCardNumberValid("4012888888881881");
            Assert.AreEqual<bool>(true, result);
        }
        [TestMethod]
        public void TestIsCardNumberValid_fail()
        {
            bool result = _paymentService.IsCardNumberValid("4012888888881882");
            Assert.AreNotEqual<bool>(true, result);
        }
        [TestMethod]
        public void TestIsCardNumberValid_fail_second_Scenario()
        {
            bool result = _paymentService.IsCardNumberValid("1222");
            Assert.AreNotEqual<bool>(true, result);
        }

        [TestMethod]
        public void TestIsValidPaymentAmount_success()
        {
            bool result = _paymentService.IsValidPaymentAmount(999);
            Assert.AreEqual<bool>(true, result);
        }
        [TestMethod]
        public void TestIsValidPaymentAmount_fail()
        {
            bool result = _paymentService.IsValidPaymentAmount(98);
            Assert.AreNotEqual<bool>(true, result);
        }
        [TestMethod]
        public void TestIsValidPaymentAmount_fail_second_scenario()
        {
            bool result = _paymentService.IsValidPaymentAmount(99999999);
            Assert.AreNotEqual<bool>(true, result);
        }

        [TestMethod]
        public void TestCanMakePaymentWithCard_success()
        {
            bool result = _paymentService.CanMakePaymentWithCard("4012888888881881",3,2016);
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void TestCanMakePaymentWithCard_fail()
        {
            Exception expectedEx = null;

            try
            {
                bool result = _paymentService.CanMakePaymentWithCard("4012888888881881", 1, 2016);
            }
            catch (Exception ex)
            {
                expectedEx = ex;
            }

            Assert.IsNotNull(expectedEx);
        }

        [TestMethod]
        public void TestCanMakePaymentWithCard_fail_second_Scenario()
        {
            Exception expectedEx = null;

            try
            {
                bool result = _paymentService.CanMakePaymentWithCard("40128888888818AA", 2, 2016);
            }
            catch (Exception ex)
            {
                expectedEx = ex;
            }

            Assert.IsNotNull(expectedEx);
        }

        [TestMethod]
        public void TestCanMakePaymentWithCard_fail_fourth_Scenario()
        {
            Exception expectedEx = null;

            try
            {
                bool result = _paymentService.CanMakePaymentWithCard("4012888888881882", 2, 2016);
            }
            catch (Exception ex)
            {
                expectedEx = ex;
            }

            Assert.IsNotNull(expectedEx);
        }


        [TestMethod]
        public void TestCanMakePaymentWithCard_fail_third_Scenario()
        {
            Exception expectedEx = null;
            
            try
            {
                bool result = _paymentService.CanMakePaymentWithCard("4012888888881881", 1, 201);
            }
            catch (Exception ex)
            {
                expectedEx = ex;
            }

            Assert.IsNotNull(expectedEx);
        }

    }
}

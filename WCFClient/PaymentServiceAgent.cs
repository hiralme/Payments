using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCFClient.CreditcardPaymentService;
using Microsoft.Practices.Unity;

namespace WCFClient
{
    public class PaymentServiceAgent
    {
        IPaymentService paymentService;

        public PaymentServiceAgent()
        {
            this.paymentService = new PaymentServiceClient();
        }

        public PaymentServiceAgent(bool isUnitTest)
        {
            this.paymentService = UnityHelper.IoC.Resolve<IPaymentService>();
        }

        public string GetID()
        {
          return  this.paymentService.WhatsYourId();
        }
    }
}
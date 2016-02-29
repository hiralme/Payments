using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using Utilities;
using Common;

namespace Payments
{
   
    public class PaymentService : IPaymentService
    {
        public string WhatsYourId()
        {
            try
                {
                string candidateId = GlobalConstants.CANDIDATE_ID;
                return candidateId;
            }
            catch (FaultException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool IsCardNumberValid(string cardNumber)
        {
            try {
                Regex cardnoregex = new Regex(@"^\d{16}$");
                Match cardnomatch = cardnoregex.Match(cardNumber.ToString());
                if (cardnomatch.Success)
                {
                    return Mod10Check.IsCardNumberValid(cardNumber);
                }
                return false;
            }
            catch (FaultException ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        public bool IsValidPaymentAmount(long amount)
        {
            try
            {
                long amountout;

                if (long.TryParse(amount.ToString(), out amountout))
                {
                    if (amountout > GlobalConstants.MIN_AMOUNT && amountout < GlobalConstants.MAX_AMOUNT)
                        return true;
                }
                return false;
            }
            catch (FaultException ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        public bool CanMakePaymentWithCard(string cardNumber, int expiryMonth, int expiryYear)
        {
            try
            {
                //Validate CardNumber
                if (!IsCardNumberValid(cardNumber))
                {
                    FaultContainer excetionDetails = new FaultContainer();
                    excetionDetails.Description = "Payment can't be made as credit card number entered is invalid";
                    excetionDetails.ErrorCode = "INVALID_CC_CARD_NUMBER";
                    throw new FaultException<FaultContainer>(excetionDetails);
                }
                //Validate Expiry Month
                if (expiryMonth < 1 || expiryMonth > 12)
                {
                    FaultContainer excetionDetails = new FaultContainer();
                    excetionDetails.Description = "Payment can't be made as expiry month is invalid";
                    excetionDetails.ErrorCode = "INVALID_EXPIRY_MONTH";
                    throw new FaultException<FaultContainer>(excetionDetails);
                }

                //Validate Expiry Year
                Regex regex = new Regex(@"^\d{4}$");
                Match match = regex.Match(expiryYear.ToString());
                if (match.Success)
                {
                    DateTime dt;
                    if (DateTime.TryParse(string.Format("{0}/{1}/{2}", DateTime.DaysInMonth(expiryYear, expiryMonth).ToString(), expiryMonth, expiryYear), out dt))
                    {
                        if (dt < DateTime.Now)
                        {
                            FaultContainer excetionDetails = new FaultContainer();
                            excetionDetails.Description = "Payment can't be made as expiry date is not current or future date";
                            excetionDetails.ErrorCode = "INVALID_EXPIRY_DATE";
                            throw new FaultException<FaultContainer>(excetionDetails);
                        }
                    }
                }
                else
                {
                    FaultContainer excetionDetails = new FaultContainer();
                    excetionDetails.Description = "Payment can't be made as expiry year is invalid";
                    excetionDetails.ErrorCode = "INVALID_EXPIRY_YEAR";
                    throw new FaultException<FaultContainer>(excetionDetails);
                }

                return true;
        }
            catch (FaultException<FaultContainer> e)
            {
                FaultContainer fault = new FaultContainer();
                fault.Description = e.Detail.Description;
                fault.ErrorCode = e.Detail.ErrorCode;

                throw new FaultException<FaultContainer>(fault,e.Detail.ErrorCode);
            }
            catch (FaultException ex)
            {                
                throw new FaultException(ex.Message);
            }
        }

    }
}

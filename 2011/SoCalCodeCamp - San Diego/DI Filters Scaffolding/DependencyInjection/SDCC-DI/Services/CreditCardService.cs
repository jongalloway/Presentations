using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SDCC_DI.Services
{
    public class CreditCardService : ICreditCardService
    {
        public bool ChargeCreditCart(string creditcard, decimal price)
        {
            //Pre-validate
            if (creditcard.Length != 16)
                return false;

            // ****************************************
            // Call out to credit card processing API
            // ****************************************

            Debug.WriteLine("Charged {0} to credit card {1}", price, creditcard);
            return true;
        }
    }
}

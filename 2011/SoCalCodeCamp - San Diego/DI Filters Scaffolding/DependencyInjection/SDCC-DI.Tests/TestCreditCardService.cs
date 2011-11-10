using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDCC_DI.Services;

namespace SDCC_DI.Tests
{
    class TestCreditCardService : ICreditCardService
    {
        public bool ChargeCreditCart(string creditcard, decimal price)
        {
            return (creditcard == "4111111111111111" & price < 100);
        }
    }
}

using System;
namespace SDCC_DI.Services
{
    public interface ICreditCardService
    {
        bool ChargeCreditCart(string creditcard, decimal price);
    }
}

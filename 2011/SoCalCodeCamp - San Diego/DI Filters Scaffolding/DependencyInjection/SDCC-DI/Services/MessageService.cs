using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDCC_DI.Services;

namespace SDCC_DI
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "This is a your message, created on: " + DateTime.Now.ToShortTimeString();
        }
    }
}
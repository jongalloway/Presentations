using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDCC_DI.Services;

namespace SDCC_DI.Tests
{
    class TestMessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Test message";
        }
    }
}

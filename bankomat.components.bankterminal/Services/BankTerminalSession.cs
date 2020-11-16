using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace bankomat.components.bankterminal
{
    public class BankTerminalSession : IBankTerminalSession
    {
        public Guid SessionId { get; set; }

        public bool Start()
        {
            SessionId = Guid.NewGuid();
            return true;
        }

        public bool End()
        {
            SessionId = Guid.Empty;
            return true;
        }
    }
}
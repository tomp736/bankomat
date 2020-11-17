using System;
using System.Threading.Tasks;

namespace bankomat.components.bankterminal
{
    public interface IBankTerminalSession
    {
        Guid SessionId { get; set; }
        bool Start();
        int PinAttempts {get; set;}
        bool End();
    }
}
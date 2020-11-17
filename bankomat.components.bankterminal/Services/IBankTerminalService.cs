using System;
using System.Threading.Tasks;
using bankomat.components.keypad;

namespace bankomat.components.bankterminal
{
    public interface IBankTerminalService
    {

        #region Session

        Task<bool> StartTerminalSession();
        Task<(bool valid, string message)> GetSessionState();
        Task<bool> EndTerminalSession();

        #endregion

        Task<(bool valid, string message, Guid nextStepId)> SubmitCard(string card);
        Task<(bool valid, string message, Guid nextStepId)> SubmitPin(string pin);
        Task<(bool valid, string message, Guid nextStepId, int amount)> SubmitWithdrawAmount(string amount);
        Task<(bool valid, string message, Guid nextStepId)> SubmitPrintReceipt();
    }
}
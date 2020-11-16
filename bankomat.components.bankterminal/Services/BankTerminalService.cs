using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace bankomat.components.bankterminal
{
    public class BankTerminalService : IBankTerminalService
    {
        private IBankTerminalSession _bankTerminalSession;        

        public BankTerminalService(IBankTerminalSession bankTerminalSession)
        {
            _bankTerminalSession = bankTerminalSession;
        }

        #region Session

        public Task<bool> StartTerminalSession()
        {
            bool valid = _bankTerminalSession.Start();
            return Task.FromResult(valid);
        }

        public Task<(bool valid, string message)> GetSessionState()
        {
            bool valid = _bankTerminalSession.SessionId != Guid.Empty;
            if(valid)
            {
                return Task.FromResult((valid, $"SessionId: {_bankTerminalSession.SessionId}"));
            }
            else
            {
                return Task.FromResult((valid, "Session Invalid"));
            }
        }

        public Task<bool> EndTerminalSession()
        {
            bool valid = _bankTerminalSession.End();
            return Task.FromResult(valid);
        }

        #endregion

        #region Card

        public async Task<(bool, string, Guid)> SubmitCard(string card)
        {            
            return await Task.FromResult((true,"", Guid.Empty));
        }

        #endregion

        #region Pin
        public int attempt = 0;
        public int maxAttempt = 3;
        public async Task<(bool, string, Guid)> SubmitPin(string pin)
        {
            bool valid = true;
            string message = "";
            Guid nextStep = Guid.Empty;

            if(PinValid(pin))
            {                
            }
            else
            {
                valid = false;
                if(attempt >= maxAttempt)
                {
                    message = $"Invalid Pin. {maxAttempt - attempt} Tries Remaining";
                    nextStep = BankTerminalStep.ExitStepId;
                }  
                else
                {

                }                              
            }
            return await Task.FromResult((valid, message, nextStep));
        }

        public bool PinValid(string pinNumber)
        {
            if(pinNumber == "1234")
            {
                return true;
            }
            attempt++;
            return false;
        }

#endregion        

        #region Amount

        public async Task<(bool, string, Guid, int)> SubmitWithdrawAmount(string entry)
        {
            bool valid = true;
            string message = string.Empty;
            int iAmount = 0;

            string amount = entry;
            decimal balance = 500;
            if(!int.TryParse(amount, out iAmount))
            {
                valid = false;
                message = $"{amount} is not a valid amount.";
            }
            else if(iAmount > balance)
            {
                valid = false;
                message = $"{amount} is greater than present balance {balance}.";
            }

            return await Task.FromResult((valid, message, Guid.Empty, iAmount));
        }

        #endregion

        #region Receipt

        public async Task<(bool valid, string message, Guid nextStepId)> SubmitPrintReceipt()
        {
            return await Task.FromResult((true, "", Guid.Empty));
        }

        #endregion
        
    }
}
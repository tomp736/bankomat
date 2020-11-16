using System.Threading.Tasks;
using bankomat.components.keypad;

namespace bankomat.components.bankterminal
{
    public class BankTerminalEnterPinStepViewModel : BankTerminalStepViewModel
    {
        internal BankTerminalEnterPinStepViewModel(IBankTerminalService bankTerminalService) 
            : base(bankTerminalService)
        {
            StepId = BankTerminalStep.EnterPinStepId;
            Title = "Enter Pin";
            Content = "";
            KeyPadMode = KeyPadMode.Password;
            TimeOut = 10;
            TimeOutStep = BankTerminalStep.ExitStepId;
        }
        
        public async override Task<SubmitStepResponse> SubmitStep(string entry)
        {
            SubmitStepResponse response = new SubmitStepResponse();
            var submitPinResponse = await _bankTerminalService.SubmitPin(entry);
            if (submitPinResponse.valid)
            {
                response.IsValid = true;
                response.NextStep = BankTerminalStep.EnterAmountStepId;
            }
            else
            {
                response.IsValid = false;
                response.ErrorMessage = $"Invalid Pin. { maxAttempt - attempt } Tries Remaining";

                if(attempt >= maxAttempt)
                {
                    response.IsValid = false;
                    response.NextStep = BankTerminalStep.ExitStepId;
                }
            }
            return await Task.FromResult(response);
        }

        public int attempt = 0;
        public int maxAttempt = 3;

        public bool PinValid(string pinNumber)
        {
            if(pinNumber == "1234")
            {
                return true;
            }
            attempt++;
            return false;
        }
    }
}
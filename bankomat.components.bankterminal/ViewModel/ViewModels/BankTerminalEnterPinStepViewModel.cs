using System;
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
            KeyPadMaxChars = 4;
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
                response.ErrorMessage = submitPinResponse.message;
                if(submitPinResponse.nextStepId != Guid.Empty)
                {
                    response.NextStep = submitPinResponse.nextStepId;
                }
            }
            return await Task.FromResult(response);
        }
    }
}
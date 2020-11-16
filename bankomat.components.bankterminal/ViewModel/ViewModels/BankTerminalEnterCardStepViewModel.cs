using System.Threading.Tasks;
using bankomat.components.keypad;

namespace bankomat.components.bankterminal
{
    public class BankTerminalEnterCardStepViewModel : BankTerminalStepViewModel
    {
        internal BankTerminalEnterCardStepViewModel(IBankTerminalService bankTerminalService) 
            : base(bankTerminalService)        
        {
            StepId = BankTerminalStep.EnterCardStepId;
            Title = "Enter Card";
            Content = "";
            KeyPadMode = KeyPadMode.Password;
            TimeOut = 10;
            TimeOutStep = BankTerminalStep.ExitStepId;
        }
        
        public async override Task<SubmitStepResponse> SubmitStep(string entry)
        {
            SubmitStepResponse response = new SubmitStepResponse();
            var cardResponse = await _bankTerminalService.SubmitCard(entry);
            if (cardResponse.valid)
            {
                response.IsValid = true;
                response.NextStep = BankTerminalStep.EnterPinStepId;
            }
            else
            {
                response.IsValid = false;
                response.ErrorMessage = "Card invalid.";
            }
            return await Task.FromResult(response);
        }
    }
}
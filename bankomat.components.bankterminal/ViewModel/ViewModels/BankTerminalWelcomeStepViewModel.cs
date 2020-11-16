using System.Threading.Tasks;
using bankomat.components.keypad;

namespace bankomat.components.bankterminal
{
    public class BankTerminalWelcomeStepViewModel : BankTerminalStepViewModel
    {
        internal BankTerminalWelcomeStepViewModel(IBankTerminalService bankTerminalService) 
            : base(bankTerminalService)
        {
            StepId = BankTerminalStep.WelcomeStepId;
            Title = "Welcome";
            Content = "Press right arrow to continue";
            KeyPadMode = KeyPadMode.YesNo;    
            KeyPadMaxChars = 10;
        }

        public async override Task<SubmitStepResponse> SubmitStep(string entry)
        {
            SubmitStepResponse response = new SubmitStepResponse();
            bool sessionStarted = await _bankTerminalService.StartTerminalSession();
            if(sessionStarted)
            {
                response.IsValid = true;
                response.NextStep = BankTerminalStep.EnterCardStepId;
            }
            return await Task.FromResult(response);
        }
    }
}
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
            KeyPadMode = KeyPadMode.Yes;    
            KeyPadMaxChars = 10;
        }

        public override async Task<KeyPadEntryResponse> OnKeyPadEntry(KeyPadEntry keyPadEntry)
        {
            bool isValid = false;
            if(keyPadEntry.IsYes())
            {
                isValid = true;
            }
            return await Task.FromResult(new KeyPadEntryResponse() { IsValid = isValid });
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
using System.Threading.Tasks;
using bankomat.components.keypad;

namespace bankomat.components.bankterminal
{
    public class BankTerminalCheckDoneStepViewModel : BankTerminalStepViewModel
    {
        internal BankTerminalCheckDoneStepViewModel(IBankTerminalService bankTerminalService) 
            : base(bankTerminalService)
        {
            StepId = BankTerminalStep.CheckDoneStepId;
            Title = "Would you like to perform another transaction?";
            Content = "";
            KeyPadMode = KeyPadMode.YesNo;
            KeyPadMaxChars = 10;
            TimeOut = 10;
            TimeOutStep = BankTerminalStep.ExitStepId;
        }

        public override async Task<KeyPadEntryResponse> OnKeyPadEntry(KeyPadEntry keyPadEntry)
        {
            bool isValid = false;
            if(keyPadEntry.IsNo())
            {
                isValid = true;
                IsDone = false;
            }
            else if(keyPadEntry.IsYes())
            {
                isValid = true;
                IsDone = true;
            }
            return await Task.FromResult(new KeyPadEntryResponse() { IsValid = isValid });
        }

        public bool? IsDone { get; set; }

        public async override Task<SubmitStepResponse> SubmitStep(string entry)
        {
            SubmitStepResponse response = new SubmitStepResponse();
            if(!IsDone.HasValue)
            {
                response.IsValid = false;
                response.ErrorMessage = "Must be selected";
            }
            else
            {
                if(IsDone.Value)
                {
                    response.IsValid = true;
                    response.NextStep = BankTerminalStep.EnterAmountStepId; 
                }
                else
                {
                    response.IsValid = true;
                    response.NextStep = BankTerminalStep.ExitStepId; 
                }
            }
            return await Task.FromResult(response);
        }
    }
}
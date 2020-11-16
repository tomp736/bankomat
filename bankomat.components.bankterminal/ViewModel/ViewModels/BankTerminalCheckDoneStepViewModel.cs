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

        public override async Task OnKeyPadEntry(KeyPadEntry keyPadEntry)
        {
            if(keyPadEntry.IsBack())
            {
                IsDone = false;
            }
            else if(keyPadEntry.IsNext())
            {
                IsDone = true;
            }
            await Task.CompletedTask;
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
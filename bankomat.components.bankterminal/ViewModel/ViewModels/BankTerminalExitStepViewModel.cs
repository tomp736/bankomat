using System;
using System.Threading.Tasks;
using bankomat.components.keypad;

namespace bankomat.components.bankterminal
{
    public class BankTerminalExitStepViewModel : BankTerminalStepViewModel
    {
        public BankTerminalExitStepViewModel(IBankTerminalService bankTerminalService) 
            : base(bankTerminalService)
        {
            StepId = BankTerminalStep.CheckDoneStepId;
            Title = "Goodbye";
            Content = "";
            KeyPadMode = KeyPadMode.YesNo;
            TimeOut = 3;
            TimeOutStep = BankTerminalStep.WelcomeStepId;
        }   

        public async override Task<SubmitStepResponse> SubmitStep(string entry)
        {
            SubmitStepResponse response = new SubmitStepResponse();
            await _bankTerminalService.EndTerminalSession();
            return await Task.FromResult(response);
        }
    }
}
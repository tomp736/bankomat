using System;
using System.Threading.Tasks;
using bankomat.components.keypad;

namespace bankomat.components.bankterminal
{
    public class BankTerminalEnterAmountStepViewModel : BankTerminalStepViewModel
    {
        internal BankTerminalEnterAmountStepViewModel(IBankTerminalService bankTerminalService) 
            : base(bankTerminalService)
        {
            StepId = BankTerminalStep.EnterAmountStepId;
            Title = "Enter Amount";
            Content = "";
            KeyPadMode = KeyPadMode.Amount;
            TimeOut = 30;
            TimeOutStep = BankTerminalStep.ExitStepId;
        }

        public async override Task<SubmitStepResponse> SubmitStep(string entry)
        {
            SubmitStepResponse response = new SubmitStepResponse();
            var validateAmountResult = await _bankTerminalService.SubmitWithdrawAmount(entry);

            if(!validateAmountResult.valid)
            {
                response.ErrorMessage = validateAmountResult.message;
                response.NextStep = Guid.Empty;
            }
            else
            {
                response.NextStep = BankTerminalStep.PrintReceiptStepId;
            }
            return await Task.FromResult(response);
        }
    }
}
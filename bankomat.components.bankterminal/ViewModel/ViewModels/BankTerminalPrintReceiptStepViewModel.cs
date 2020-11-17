using System.Threading.Tasks;
using bankomat.components.keypad;

namespace bankomat.components.bankterminal
{
    public class BankTerminalPrintReceiptStepViewModel : BankTerminalStepViewModel
    {
        internal BankTerminalPrintReceiptStepViewModel(IBankTerminalService bankTerminalService) 
            : base(bankTerminalService)
        {
            StepId = BankTerminalStep.PrintReceiptStepId;
            Title = "Print Receipt?";
            Content = "";
            KeyPadMode = KeyPadMode.YesNo;
            KeyPadMaxChars = 10;
            TimeOut = 10;
            TimeOutStep = BankTerminalStep.ExitStepId;
        }
        private bool? PrintReceiptSelection { get; set; }

        public override async Task<KeyPadEntryResponse> OnKeyPadEntry(KeyPadEntry keyPadEntry)
        {
            bool isValid = false;
            if(keyPadEntry.IsNo())
            {
                isValid = true;
                PrintReceiptSelection = false;
            }
            else if(keyPadEntry.IsYes())
            {
                isValid = true;
                PrintReceiptSelection = true;
            }
            return await Task.FromResult(new KeyPadEntryResponse() { IsValid = isValid });
        }

        public async override Task<SubmitStepResponse> SubmitStep(string entry)
        {
            SubmitStepResponse response = new SubmitStepResponse();
            if (!PrintReceiptSelection.HasValue)
            {
                response.IsValid = false;
                response.ErrorMessage = "Must be selected";
            }
            else
            {
                if(PrintReceiptSelection.Value)
                {
                    // Print Receipt
                }
                response.NextStep = BankTerminalStep.CheckDoneStepId; 
            }
            return await Task.FromResult(response);
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using bankomat.components;
using bankomat.components.keypad;
using bankomat.components.terminalscreen;

namespace bankomat.components.bankterminal
{
    public class BankTerminalStepViewModel
    {
        protected IBankTerminalService _bankTerminalService;

        internal BankTerminalStepViewModel(IBankTerminalService bankTerminalService)
        {
            this._bankTerminalService = bankTerminalService;
        }

        public Guid StepId { get; init; }
        public string Title { get; init; }
        public string Content { get; init; }
        
        public KeyPadMode KeyPadMode { get; init; }
        public int KeyPadMaxChars { get; init; }

        public int TimeOut { get; init;}
        public Guid TimeOutStep { get; init; }
        

        public string AlertMessage { get; set;}
        public AlertType AlertType { get; set;}
        
                
        public virtual async Task<KeyPadEntryResponse> OnKeyPadEntry(KeyPadEntry keyPadEntry)
        {
            return await Task.FromResult(new KeyPadEntryResponse() { IsValid = true });
        }

        public virtual async Task<ValidateStepResponse> ValidateStep(string entry)
        {
            return await Task.FromResult(new ValidateStepResponse());
        }

        public virtual async Task<SubmitStepResponse> SubmitStep(string entry)
        {
            return await Task.FromResult(new SubmitStepResponse());
        }

        public async Task OnTimeout(IBankTerminalService bankTerminalService)
        {
            await bankTerminalService.EndTerminalSession();
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using bankomat.components;
using bankomat.components.keypad;
using bankomat.components.terminalscreen;

namespace bankomat.components.bankterminal
{
    public class BankTerminalBase : ComponentBase
    {
        [Inject]
        protected ILoggerService loggerService { get; set; }
        [Inject]
        protected IBankTerminalService bankTerminalService { get; set; }
        [Inject]
        protected IBankTerminalSession bankTerminalSession { get; set; }
        [Inject]
        protected IBankTerminalStepViewModelFactory bankTerminalStepViewModelFactory { get; set; }

        protected BankTerminalStepViewModel bankTerminalStepViewModel { get; set; } 


        protected TerminalScreen terminalScreen;
        protected KeyPad keyPad;
        protected Alert.Alert alert;      
          
        protected ValidateStepResponse ValidationResponse { get; set; }


        protected async override Task OnInitializedAsync()
        {
            await bankTerminalService.StartTerminalSession();
            await GoToStep(BankTerminalStep.WelcomeStepId);
            await base.OnInitializedAsync();
        }

        public virtual async Task OnKeyPadEntry(KeyPadEntry keyPadEntry)
        {
            var result = bankTerminalStepViewModel.OnKeyPadEntry(keyPadEntry);
            if((keyPadEntry == KeyPadEntry.Left && keyPad.KeyPadValue == string.Empty)
                || keyPadEntry == KeyPadEntry.Right)
            {
                await ProcessStep();
            }
            await OnUserActivity();
            await Task.CompletedTask;
        }

        public async Task ProcessStep()
        {
            loggerService.WriteConsole("ValidateStep");
            ValidateStepResponse validateResponse = await ValidateStep();
            ValidationResponse = validateResponse;
            if (validateResponse.IsValid)
            {
                loggerService.WriteConsole("SubmitStep");
                SubmitStepResponse submitStepResponse = await SubmitStep();
                if(submitStepResponse.IsValid)
                {
                    loggerService.WriteConsole("GoToStep");
                    await GoToStep(submitStepResponse.NextStep);
                }
            }          
        }

        public virtual async Task<ValidateStepResponse> ValidateStep()
        {
            var result = await bankTerminalStepViewModel.ValidateStep(keyPad.KeyPadValue);
            return await Task.FromResult(result);
        }

        public virtual async Task<SubmitStepResponse> SubmitStep()
        {
            var result = await bankTerminalStepViewModel.SubmitStep(keyPad.KeyPadValue);
            return await Task.FromResult(result);
        }
        
        protected async Task GoToStep(Guid stepId)
        {
            if(stepId != Guid.Empty)
            {        
                bankTerminalStepViewModel = bankTerminalStepViewModelFactory.CreateViewModel(stepId);                
                if(bankTerminalStepViewModel != null)
                {
                    if(keyPad != null)
                    {
                        keyPad.Clear();
                    }
                }
                ValidationResponse = null;
            }
            await OnUserActivity();
            StateHasChanged();
            await Task.CompletedTask;
        }

        private Task timer = null;
        protected TimeSpan timeLeft = new TimeSpan(0,0,0);
        public async Task OnUserActivity()
        {
            timeLeft = new TimeSpan(0,0,bankTerminalStepViewModel.TimeOut);
            if(timer == null || timer.IsCompleted)
            {
                timer = Task.Run(Timer);
                await timer;
            }
        }
        public async Task Timer()
        {
            while(timeLeft > new TimeSpan())
            {
                await Task.Delay(1000);
                timeLeft = timeLeft.Subtract(new TimeSpan(0,0,1));
                StateHasChanged();
            }
            await bankTerminalStepViewModel.OnTimeout(bankTerminalService);
            await GoToStep(bankTerminalStepViewModel.TimeOutStep);
        }
    }
}
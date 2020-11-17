using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using bankomat.components;
using bankomat.components.keypad;
using bankomat.components.terminalscreen;
using System.Threading;

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
            timer = Timer();
            await base.OnInitializedAsync();
        }

        public virtual async Task OnKeyPadEntry(KeyPadEntry keyPadEntry)
        {
            var result = await bankTerminalStepViewModel.OnKeyPadEntry(keyPadEntry);
            if(result.IsValid)
            {
                if(keyPadEntry.IsCancel())
                {
                    await ProcessCancel();
                    return;
                }
                
                // update timeout on number press
                if(keyPadEntry.IsNumber())
                {
                    UpdateTimeout();
                }

                // process step if next pressed
                if(keyPadEntry.IsYes())
                {
                    await ProcessStep();
                }

                // process step if back pressed when YesNoCancel
                if(keyPad.KeyPadMode == KeyPadMode.YesNoCancel || 
                    keyPad.KeyPadMode == KeyPadMode.YesNo && 
                    keyPadEntry.IsNo())
                {
                    await ProcessStep();
                }
                    
            }            
        }

        public async Task ProcessCancel()
        {
            await GoToStep(BankTerminalStep.ExitStepId);
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
                
                if(submitStepResponse.NextStep != Guid.Empty)
                {
                    alert.Clear();
                    loggerService.WriteConsole("GoToStep");
                    await GoToStep(submitStepResponse.NextStep);
                }
                else
                {
                    alert.ShowAlert(AlertType.Warning, submitStepResponse.ErrorMessage);
                }
            }          
        }

        public virtual async Task<ValidateStepResponse> ValidateStep()
        {
            var result = await bankTerminalStepViewModel.ValidateStep(keyPad.KeyPadValue);
            // if(result.IsValid)
            // {
            //     alert.Clear();
            // }
            // else
            // {
            //     alert.ShowAlert(AlertType.Warning, result.ErrorMessage);
            // }
            return await Task.FromResult(result);
        }

        public virtual async Task<SubmitStepResponse> SubmitStep()
        {
            var result = await bankTerminalStepViewModel.SubmitStep(keyPad.KeyPadValue);
            // if(result.IsValid)
            // {
            //     alert.Clear();
            // }
            // else
            // {
            //     alert.ShowAlert(AlertType.Warning, result.ErrorMessage);
            // }
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
            UpdateTimeout();            
            StateHasChanged();
            await Task.CompletedTask;
        }

        private Task timer;
        protected TimeSpan timeoutTimeSpan = new TimeSpan(0,0,0);
        public void UpdateTimeout()
        {      
            timeoutTimeSpan = new TimeSpan(0,0,bankTerminalStepViewModel.TimeOut);
        }
        public async Task Timer()
        {
            StateHasChanged();
            // while(timeoutTimeSpan > new TimeSpan())
            while(true)
            {
                await Task.Delay(1000);
                if(timeoutTimeSpan.Seconds > 0)
                {
                    timeoutTimeSpan = timeoutTimeSpan.Subtract(new TimeSpan(0,0,1));
                    StateHasChanged();
                }
                if(timeoutTimeSpan.Seconds == 0)
                {
                    await bankTerminalStepViewModel.OnTimeout(bankTerminalService);
                    await GoToStep(bankTerminalStepViewModel.TimeOutStep);
                }
            }
        }
    }
}
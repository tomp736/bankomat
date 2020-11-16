using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using bankomat.components;
using bankomat.components.keypad;
using bankomat.components.terminalscreen;

namespace bankomat.components.bankterminal
{
    public class BankTerminalStepViewModelFactory : IBankTerminalStepViewModelFactory
    {
        private ILoggerService _loggerService;
        private IBankTerminalService _bankTerminalService;
        public BankTerminalStepViewModelFactory(ILoggerService loggerService, IBankTerminalService bankTerminalService)
        {
            _loggerService = loggerService;
            _bankTerminalService = bankTerminalService;
        }

        public BankTerminalStepViewModel CreateViewModel(Guid stepId)
        {
            switch (stepId)
            {
                case var r when (r == BankTerminalStep.WelcomeStepId):
                    return new BankTerminalWelcomeStepViewModel(_bankTerminalService);
                case var r when (r == BankTerminalStep.EnterCardStepId):
                    return new BankTerminalEnterCardStepViewModel(_bankTerminalService);
                case var r when (r == BankTerminalStep.EnterPinStepId):
                    return new BankTerminalEnterPinStepViewModel(_bankTerminalService);
                case var r when (r == BankTerminalStep.EnterAmountStepId):
                    return new BankTerminalEnterAmountStepViewModel(_bankTerminalService);
                case var r when (r == BankTerminalStep.PrintReceiptStepId):
                    return new BankTerminalPrintReceiptStepViewModel(_bankTerminalService);
                case var r when (r == BankTerminalStep.CheckDoneStepId):
                    return new BankTerminalCheckDoneStepViewModel(_bankTerminalService);
                case var r when (r == BankTerminalStep.ExitStepId):
                    return new BankTerminalExitStepViewModel(_bankTerminalService);
            }
            return new BankTerminalWelcomeStepViewModel(_bankTerminalService);
        }
    }
}
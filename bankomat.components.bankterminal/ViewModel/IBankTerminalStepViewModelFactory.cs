using System;

namespace bankomat.components.bankterminal
{
    public interface IBankTerminalStepViewModelFactory
    {
        BankTerminalStepViewModel CreateViewModel(Guid stepId);
    }
}
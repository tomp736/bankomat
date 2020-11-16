using System;

namespace bankomat.components.bankterminal
{
    public static class BankTerminalStep
    {
        #region stepids
        public static Guid WelcomeStepId = Guid.Parse("8ccbe540-349b-42e9-b0f3-db439abdfeb7");
        public static Guid EnterCardStepId = Guid.Parse("1931a867-4eed-4685-b1de-e3576130b486");
        public static Guid EnterPinStepId = Guid.Parse("22cd5ad4-be85-4d4c-a1c9-ba6212ddc279");
        public static Guid EnterAmountStepId = Guid.Parse("8fe792cb-3516-46a0-8e4a-b2a96111dc07");
        public static Guid PrintReceiptStepId = Guid.Parse("27f89dce-4580-459f-afb2-ff39185b0db8");
        public static Guid CheckDoneStepId = Guid.Parse("86123f4b-054c-4547-ad00-d4efd5819476");
        public static Guid ExitStepId = Guid.Parse("d61da753-9e10-4909-811c-4625090d488b");

        #endregion
    }
}
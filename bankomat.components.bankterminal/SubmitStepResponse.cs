using System;

namespace bankomat.components
{
    public class SubmitStepResponse
    {
        public bool IsValid { get; set; } = true;
        public Guid NextStep { get; internal set; }
        public string ErrorMessage { get; internal set; }
    }
}
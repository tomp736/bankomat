using System;

namespace bankomat.components
{
    public class ValidateStepResponse
    {
        public bool IsValid { get; set; } = true;
        public Guid NextStep {get; set;}
        public string ErrorMessage { get; internal set; }
    }
}
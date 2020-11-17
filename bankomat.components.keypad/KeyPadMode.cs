using System;

namespace bankomat.components.keypad
{
    public enum KeyPadMode 
    {
        Disabled = -3,
        Yes = -2,
        YesNo = -1,
        YesNoCancel = 0,
        Text = 1,
        Amount = 2,
        Password = 3,
    }
}
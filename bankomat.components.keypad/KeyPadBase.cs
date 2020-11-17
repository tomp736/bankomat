using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace bankomat.components.keypad
{
    public class KeyPadBase : ComponentBase
    {
        [Parameter]
        public String KeyPadValue { get; set; }
        [Parameter]
        public EventCallback<KeyPadEntry> OnKeyPadEntry { get; set; }
        [Parameter]
        public KeyPadMode KeyPadMode { get; set; }
        [Parameter]
        public int KeyPadMaxChars { get; set; }

        protected bool IsDisabled(KeyPadEntry keyPadEntry)
        {
            bool disabled = false;
            switch (KeyPadMode)
            {
                case KeyPadMode.Disabled:
                    disabled = true;
                    break;
                case KeyPadMode.YesNoCancel:
                    disabled = keyPadEntry.IsNumber();
                    break;
                case KeyPadMode.YesNo:
                    disabled = keyPadEntry.IsNumber() || keyPadEntry.IsCancel();
                    break;
                case KeyPadMode.Yes:
                    disabled = keyPadEntry.IsNumber() || keyPadEntry.IsCancel() || keyPadEntry.IsNo();
                    break;
                case KeyPadMode.Amount:
                case KeyPadMode.Text:
                case KeyPadMode.Password:
                    if (keyPadEntry.IsNo() && KeyPadValue.Length == 0)
                    {
                        return true;
                    }
                    break;
            }
            return disabled;
        }

        protected async Task OnButtonClick(KeyPadEntry keyPadEntry)
        {
            switch (keyPadEntry)
            {
                case KeyPadEntry.Yes:

                    break;
            }

            switch (KeyPadMode)
            {
                case KeyPadMode.Disabled:
                    break;
                case KeyPadMode.YesNoCancel:
                case KeyPadMode.YesNo:
                case KeyPadMode.Yes:
                    await OnKeyPadEntry.InvokeAsync(keyPadEntry);
                    break;
                case KeyPadMode.Text:
                case KeyPadMode.Amount:
                case KeyPadMode.Password:
                    // update keypad display
                    if (keyPadEntry.IsNumber())
                    {
                        if (KeyPadValue.Length < KeyPadMaxChars)
                        {
                            string tempValue = KeyPadValue;
                            tempValue += (int)keyPadEntry;
                            if (KeyPadMode == KeyPadMode.Amount)
                            {
                                int tempIntValue = int.Parse(tempValue);
                                KeyPadValue = tempIntValue.ToString();
                            }
                            else
                            {
                                KeyPadValue = tempValue;
                            }
                        }
                        else
                        {
                            // max length exceeded
                        }
                    }
                    if (keyPadEntry.IsNo() && KeyPadValue != null && KeyPadValue.Length > 0)
                    {
                        KeyPadValue = KeyPadValue.Substring(0, KeyPadValue.Length - 1);
                    }
                    await OnKeyPadEntry.InvokeAsync(keyPadEntry);
                    break;
            }
        }

        public void Clear()
        {
            KeyPadValue = string.Empty;
        }
    }
}
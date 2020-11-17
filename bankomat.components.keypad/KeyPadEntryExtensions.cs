namespace bankomat.components.keypad
{
    public static class KeyPadEntryExtensions
    {
        public static bool IsNumber(this KeyPadEntry keyPadEntry)
        {
            if(keyPadEntry == KeyPadEntry.One ||
                keyPadEntry == KeyPadEntry.Two ||
                keyPadEntry == KeyPadEntry.Three ||
                keyPadEntry == KeyPadEntry.Four ||
                keyPadEntry == KeyPadEntry.Five ||
                keyPadEntry == KeyPadEntry.Six ||
                keyPadEntry == KeyPadEntry.Seven ||
                keyPadEntry == KeyPadEntry.Eight ||
                keyPadEntry == KeyPadEntry.Nine ||
                keyPadEntry == KeyPadEntry.Zero
            )
            {
                return true;
            }
            return false;
        }
        public static bool IsNo(this KeyPadEntry keyPadEntry)
        {
            if(keyPadEntry == KeyPadEntry.Left)
            {
                return true;
            }
            return false;
        }
        public static bool IsYes(this KeyPadEntry keyPadEntry)
        {
            if(keyPadEntry == KeyPadEntry.Yes)
            {
                return true;
            }
            return false;
        }
        public static bool IsCancel(this KeyPadEntry keyPadEntry)
        {
            if(keyPadEntry == KeyPadEntry.Cancel)
            {
                return true;
            }
            return false;
        }
    }
}
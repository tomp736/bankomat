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
        public static bool IsBack(this KeyPadEntry keyPadEntry)
        {
            if(keyPadEntry == KeyPadEntry.Left)
            {
                return true;
            }
            return false;
        }
        public static bool IsNext(this KeyPadEntry keyPadEntry)
        {
            if(keyPadEntry == KeyPadEntry.Right)
            {
                return true;
            }
            return false;
        }
    }
}
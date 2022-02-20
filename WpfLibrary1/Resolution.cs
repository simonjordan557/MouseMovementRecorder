using System;

namespace WpfLibrary1
{
    public static class Resolution
    {
        public static int Height()
        {
            return (int)System.Windows.SystemParameters.PrimaryScreenHeight;
        }

        public static int Width()
        {
            return (int)System.Windows.SystemParameters.PrimaryScreenWidth;
        }
    }
}

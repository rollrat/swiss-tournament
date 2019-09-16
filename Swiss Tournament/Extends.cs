namespace Swiss_Tournament
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public static class Extends
    {
        public static void Post(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        public static T Send<T>(this Control control, Func<T> func) => 
            (control.InvokeRequired ? ((T) control.Invoke(func)) : func());

        public static int ToInt32(this string str) => 
            Convert.ToInt32(str);
    }
}


using System;
using System.Windows.Input;

namespace Maui.Barcode.Extensions
{
    public static class CommandExtension
    {
        public static void Raise(this ICommand comand, object parameter)
        {
            if (comand != null && comand.CanExecute(parameter)) {
                comand.Execute(parameter);
            }
        }

        public static void Raise(this ICommand comand)
        {
            Raise(comand, EventArgs.Empty);
        }
    }
}


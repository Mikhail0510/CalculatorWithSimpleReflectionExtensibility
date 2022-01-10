using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator_.BusinessLogic
{
    internal static class Notificator
    {
        public static void DisplayMessage(string message)
        {
            MessageBox.Show(message, "Notification", MessageBoxButton.OK);
        }

        public static void DisplayError(string message)
        {
            MessageBox.Show(message, "Internal Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

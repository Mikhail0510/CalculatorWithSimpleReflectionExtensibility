using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_.BusinessLogic
{
    internal static class DefaultConstants
    {
        public const string Initial = "0";
        public const string Error = "ERROR";
        public const int ColumnLimit = 3;
        public const int RowLimit = 6;
        public const string Dot = ".";
    }

    internal static class DefaultMathOperators
    {
        public const char Plus = '+';
        public const char Minus = '-';
        public const char Divider = '/';
        public const char Multiply = '*';
        public const char Equal = '=';

        public static char[] ReturnListOfMathOperators()
        {
            return new char[] { Plus, Minus, Divider, Multiply };
        }
    }

    internal static class DefaultDigits
    {
        public static List<char> Digits = new List<char>()
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };
    }
}

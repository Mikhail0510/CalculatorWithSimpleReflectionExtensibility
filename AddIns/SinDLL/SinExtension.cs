using AddInContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SinAddIn
{
    public class SinEvaluator: IExtension
    {
        public double Calculate(double sinValue)
        {
            return Math.Sin(sinValue);
        }

        public double Calculate(int sinValue)
        {
            return Math.Sin(sinValue);
        }

        public ExtensionInfo GetInfo()
        {
            return new ExtensionInfo()
            {
                ButtonText = "Sin",
                Description = "Adds functionality to evaluate Sin",
                Version = "1.0"
            };
        }
    }
}

using AddInContract;
using System;

namespace CosAddIn
{
    public partial class CosEvaluator: IExtension
    {

        public double Calculate(double cosValue)
        {
            throw new StackOverflowException();
        }

        public double Calculate(int cosValue)
        {
            return Math.Cos(cosValue);
        }

        public ExtensionInfo GetInfo()
        {
            return new ExtensionInfo()
            {
                ButtonText = "Cos",
                Description = "Adds functionality to evaluate Cos",
                Version = "1.0"
            };
        }
    }
}

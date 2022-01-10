using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator_;

namespace Calculator_.BusinessLogic
{
    internal class MathExecutor
    {
        private static StringBuilder commandString = new StringBuilder();
        private static StringBuilder currentNumberString = new StringBuilder(DefaultConstants.Initial);
        private static bool SlushCurrentStringDown = false;

        public static string GetCurrentNumberString
        {
            get
            {
                if(currentNumberString.ToString() != DefaultConstants.Error)
                {
                    return currentNumberString.ToString();
                }
                else
                {
                    EmptyCurrentNumberStringAndSetInitial();
                    return currentNumberString.ToString();
                }
                
            }
            set
            {
                currentNumberString.Clear();
                currentNumberString.Append(value);
            }
        }

        public string GetCommandStringValue
        {
            get
            {
                return commandString.ToString();
            }
        }

        public bool ShouldClear {
            get
            {
                return SlushCurrentStringDown;
            }
            set
            {
                SlushCurrentStringDown = value;
            }
        }

        public void EvaluateExpression()
        {
            if (SlushCurrentStringDown)
            {
                EmptyAllStringsAndSetInitialValue();
            }
            else
            {
                commandString.Append(currentNumberString.ToString());
                currentNumberString.Clear();
                try
                {
                    currentNumberString.Append(CSharpScript.EvaluateAsync(commandString.ToString()).Result);
                }
                catch
                {
                    currentNumberString.Clear();
                    currentNumberString.Append(DefaultConstants.Error);
                }
                SlushCurrentStringDown = true;
            }
        }

        public static void EmptyAllStringsAndSetInitialValue()
        {
            EmptyCommandStringAndSetInitial();
            EmptyCurrentNumberStringAndSetInitial();
            SlushCurrentStringDown = false;
        }

        public static void EmptyCurrentNumberStringAndSetInitial()
        {
            currentNumberString.Clear();
            currentNumberString.Append(DefaultConstants.Initial);
        }

        public static void EmptyCommandStringAndSetInitial()
        {
            commandString.Clear();
        }

        public void RemoveLastChar()
        {
            if (SlushCurrentStringDown)
            {
                EmptyAllStringsAndSetInitialValue();
            }
            else
            {
                string val = currentNumberString.ToString();
                if (val != DefaultConstants.Initial)
                {
                    currentNumberString.Clear();
                    if (val.Length > 1)
                    {
                        currentNumberString.Append(val.Substring(0, val.Length - 1));
                    }
                    else
                    {
                        EmptyCurrentNumberStringAndSetInitial();
                    }
                }
                else
                {
                    val = commandString.ToString();
                    if (val.Length > 1)
                    {
                        commandString.Clear();
                        commandString.Append(val.Substring(0, val.Length - 1));
                    }
                }
            }
            
                
        }

        public void PushDot()
        {
            if (SlushCurrentStringDown)
            {
                EmptyAllStringsAndSetInitialValue();
            }
            if (currentNumberString.ToString() == String.Empty)
            {
                if (IsLastCharMathOperator())
                {
                    currentNumberString.Clear();
                }
                currentNumberString.Append("0.0");

            }
            else if (currentNumberString.ToString() == DefaultConstants.Initial) {
                currentNumberString.Append(DefaultConstants.Dot);
            }
            else if (!currentNumberString.ToString().Contains("."))
            {
                currentNumberString.Append(".");
            }

        }

        public void PushDigit(int digit)
        {
            if (SlushCurrentStringDown)
            {
                EmptyAllStringsAndSetInitialValue();
            }
            ReplaceZeroWithNumberOrAppendDigit(digit);
        }

        public void PushMathOperator(char mathOperator)
        {
            if (IsLastCharMathOperator())
            {
                string stringValue = commandString.ToString();
                commandString.Clear();
                commandString.Append(stringValue.Substring(0, stringValue.Length - 1));
                commandString.Append(mathOperator);
            }
            else
            {
                CorrectCurrentValueToGetCorrectResultWithWholeNumber();
                currentNumberString.Append(mathOperator);
                commandString.Append(currentNumberString.ToString());
                EmptyCurrentNumberStringAndSetInitial();
            }            
        }

        private void CorrectCurrentValueToGetCorrectResultWithWholeNumber()
        {
            if (currentNumberString.ToString().Last().ToString() == DefaultConstants.Dot)
            {
                currentNumberString.Append(DefaultConstants.Initial);
            }
            if (!currentNumberString.ToString().Contains(DefaultConstants.Dot))
            {
                currentNumberString.Append(".0");
            }
        }

        private void ReplaceZeroWithNumberOrAppendDigit(int digit)
        {
            if(currentNumberString.ToString() == DefaultConstants.Initial)
            {
                currentNumberString.Clear();
            }
            currentNumberString.Append(digit);
        }

        private bool IsLastCharMathOperator()
        {
            int testResult;
            if(currentNumberString.ToString() != DefaultConstants.Initial)
            {
                string chr = currentNumberString.ToString().Last().ToString();
                if(chr != DefaultConstants.Dot) {
                    if (Int32.TryParse(chr, out testResult))
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                if (commandString.ToString().Length > 0)
                {
                    string chr = commandString.ToString().Last().ToString();
                    if (Int32.TryParse(chr, out testResult))
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
            
            
        }

        public bool DoesStringContainMathOperator()
        {
            
            char[] mathOperators = DefaultMathOperators.ReturnListOfMathOperators();
            for (int i=0; i < mathOperators.Length; i++)
            {
                if(GetCommandStringValue.Contains(mathOperators[i]))
                    return true;
            }
            return false;
        }

    }
}

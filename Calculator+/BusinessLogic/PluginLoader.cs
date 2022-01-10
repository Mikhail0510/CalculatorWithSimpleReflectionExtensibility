using AddInContract;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Calculator_.BusinessLogic
{
    internal class PlugInLoader
    {
        private RefreshCurrentValue RefreshCurrentValue;
        private ReplaceExtensionButton ReplaceButton;

        public PlugInLoader(RefreshCurrentValue refreshCurrentValue, ReplaceExtensionButton replaceExtensionButton)
        {
            RefreshCurrentValue = refreshCurrentValue;
            ReplaceButton = replaceExtensionButton;
        }

        public Button Load(ExtensionState eXstate)
        {
            Assembly _Assembly = Assembly.LoadFile(eXstate.FullPath);
            var types = _Assembly.GetTypes()?.ToList();
            var type = types?.Find(a => typeof(IExtension).IsAssignableFrom(a));
            var exType = (IExtension)Activator.CreateInstance(type);
            Button newBtn = new Button();
            newBtn.Content = exType.GetInfo().ButtonText;
            newBtn.Name = "Button" + newBtn.Content;
            newBtn.Click += delegate (object sender, RoutedEventArgs e)
            {
                Execute(exType.Calculate, eXstate, RefreshCurrentValue);
            };
            Notificator.DisplayMessage("Plugin for " + newBtn.Content + " has been loaded.");
            return newBtn;
        }

        public void Execute(Func<double, double> method, ExtensionState exState, RefreshCurrentValue refresh)
        {
            double num;
            try
            {
                try
                {
                    string str = MathExecutor.GetCurrentNumberString;//.Replace('.', ',');
                    num = Double.Parse(str);
                    num = method(num);
                    MathExecutor.EmptyAllStringsAndSetInitialValue();
                    MathExecutor.GetCurrentNumberString = num.ToString();
                }
                catch (Exception)
                {
                    throw new PluginException(exState.FullPath);
                }
            }
            catch (PluginException)
            {
                MathExecutor.EmptyAllStringsAndSetInitialValue();
                MathExecutor.GetCurrentNumberString = DefaultConstants.Error;
                Notificator.DisplayError("Error in " + exState.FullPath);
                PlugInLoader loader = new PlugInLoader(RefreshCurrentValue, ReplaceButton);
                Button btn = Load(exState);
                ReplaceButton(btn, exState);
            }
            finally
            {
                refresh?.Invoke();
            }


        }

    }
}

using AddInContract;
using Calculator_.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public delegate void RefreshCurrentValue();
    public delegate void ReplaceExtensionButton(Button btn, ExtensionState state);

    public partial class MainWindow : Window
    {
        private ExtensionsInnerDescription extensions = new ExtensionsInnerDescription()
        {
            Items = new List<ExtensionState>()
        };
        private List<Button> extensionPanelButtons = new List<Button>();
        private List<ExtensionState> extensionStates = new List<ExtensionState>();
        public event RefreshCurrentValue RefreshCurrentValue;
        public event ReplaceExtensionButton ReplaceButton;
        private MathExecutor executor = new MathExecutor();
        private int i = 0;
        private int j = 0;
        int extensionId = 0;
        private string pluginsPath = System.IO.Path.Combine(Environment.CurrentDirectory, "plugins");

        public MainWindow()
        {
            InitializeComponent();
            RefreshCurrentValue += RefreshCurrentNumberString;
            RefreshCurrentValue += SetCleanCurrentStringFirst;
            ReplaceButton += ReplaceExtensionButton;
            RefreshCurrentNumberString();
            LoadPlugins();
        }

        public void LoadPlugins()
        {
            
            ExtensionState currentState = new ExtensionState()
            {
                FullPath = String.Empty
            };
            
            try
            {
                var files = Directory.GetFiles(pluginsPath, "*.dll");
                
                for(int k =0; k< files.Length; k++)
                {
                    if (j > DefaultConstants.RowLimit & i > DefaultConstants.ColumnLimit)
                    {
                        break;
                        throw new ExtensionLimitExceeded();
                    }
                    currentState = new ExtensionState()
                    {
                        FullPath = files[k],
                        ExtensionId = extensionId,
                        Column = i,
                        Row = j
                    };
                    
                    LoadPlugin(currentState);
                }
            }
            catch(IOException ex)
            {
                Notificator.DisplayError(ex.Message);
            }
            catch (ExtensionLimitExceeded)
            {
                Notificator.DisplayError("Extension number is too large");
            }
            catch(PluginException ex)
            {
                Notificator.DisplayError(ex.Message);
                try
                {
                    LoadPlugin(currentState);
                }
                catch
                {

                }
            }
            catch (Exception ex)
            {
                Notificator.DisplayError(ex.Message);
            }

        }

        public void LoadPlugin(ExtensionState exState)
        {
            PlugInLoader loader = new PlugInLoader(RefreshCurrentValue, ReplaceButton);
            Assembly _Assembly = Assembly.LoadFile(exState.FullPath);
            var types = _Assembly.GetTypes()?.ToList();
            var type = types?.Find(a => typeof(IExtension).IsAssignableFrom(a));
            if(type != null)
            {
                exState.ExtensionId = extensionId++;
                var exType = (IExtension)Activator.CreateInstance(type);
                Button newBtn = loader.Load(exState);
                Grid.SetColumn(newBtn, exState.Column);
                Grid.SetRow(newBtn, exState.Row);
                ExtensionPanel.Children.Add(newBtn);
                extensionPanelButtons.Add(newBtn);
                extensionStates.Add(exState);
                i++;
                if (i > DefaultConstants.ColumnLimit)
                {
                    j++;
                    i = 0;
                }
            }
            
        }

        private void ReplaceExtensionButton(Button btn, ExtensionState state)
        {
            ExtensionPanel.Children.Clear();
            for(int i =0; i< extensionPanelButtons.Count; i++)
            {
                if (i != state.ExtensionId)
                {
                    Grid.SetColumn(extensionPanelButtons[i], extensionStates[i].Column);
                    Grid.SetRow(extensionPanelButtons[i], extensionStates[i].Row);
                    ExtensionPanel.Children.Add(extensionPanelButtons[i]);
                }
                else
                {
                    Button button = btn;
                    button.Background = Brushes.Red;
                    extensionPanelButtons[i] = button;
                    Grid.SetColumn(button, extensionStates[i].Column);
                    Grid.SetRow(button, extensionStates[i].Row);
                    ExtensionPanel.Children.Add(button);

                }
            }
        }

        private void SetCleanCurrentStringFirst()
        {
            executor.ShouldClear = true;
        }

        private void RefreshCommandString()
        {
            this.ExpressionString.Content = executor.GetCommandStringValue;
        }

        private void RefreshCurrentNumberString()
        {
            this.CurrentNumber.Content = MathExecutor.GetCurrentNumberString;
        }

        private void ClearString(object sender, RoutedEventArgs e)
        {
            MathExecutor.EmptyAllStringsAndSetInitialValue();
            RefreshCommandString();
            RefreshCurrentNumberString();
        }

        private void PushOneDigit(object sender, RoutedEventArgs e)
        {
            executor.PushDigit(1);
            RefreshCurrentNumberString();
        }

        private void PushTwoDigit(object sender, RoutedEventArgs e)
        {
            executor.PushDigit(2);
            RefreshCurrentNumberString();
        }

        private void PushThreeDigit(object sender, RoutedEventArgs e)
        {
            executor.PushDigit(3);
            RefreshCurrentNumberString();
        }

        private void RemoveLastChar(object sender, RoutedEventArgs e)
        {
            executor.RemoveLastChar();
            RefreshCurrentNumberString();
            RefreshCommandString();
        }

        private void PushFourDigit(object sender, RoutedEventArgs e)
        {
            executor.PushDigit(4);
            RefreshCurrentNumberString();
        }

        private void PushFiveDigit(object sender, RoutedEventArgs e)
        {
            executor.PushDigit(5);
            RefreshCurrentNumberString();
        }

        private void PushSixDigit(object sender, RoutedEventArgs e)
        {
            executor.PushDigit(6);
            RefreshCurrentNumberString();
        }

        private void PushSevenDigit(object sender, RoutedEventArgs e)
        {
            executor.PushDigit(7);
            RefreshCurrentNumberString();
        }

        private void PushEightDigit(object sender, RoutedEventArgs e)
        {
            executor.PushDigit(8);
            RefreshCurrentNumberString();
        }

        private void PushNineDigit(object sender, RoutedEventArgs e)
        {
            executor.PushDigit(9);
            RefreshCurrentNumberString();
        }

        private void PushZeroDigit(object sender, RoutedEventArgs e)
        {
            if (executor.ShouldClear)
            {
                RefreshCurrentNumberString();
                RefreshCommandString();
            }
            executor.PushDigit(0);
            RefreshCurrentNumberString();
        }

        private void PushPlus(object sender, RoutedEventArgs e)
        {
            executor.PushMathOperator(DefaultMathOperators.Plus);
            RefreshCommandString();
            RefreshCurrentNumberString();
        }

        private void PushMinus(object sender, RoutedEventArgs e)
        {
            executor.PushMathOperator(DefaultMathOperators.Minus);
            RefreshCommandString();
            RefreshCurrentNumberString();
        }

        private void PushMiltiply(object sender, RoutedEventArgs e)
        {
            executor.PushMathOperator(DefaultMathOperators.Multiply);
            RefreshCommandString();
            RefreshCurrentNumberString();
        }

        private void PushDivider(object sender, RoutedEventArgs e)
        {
            executor.PushMathOperator(DefaultMathOperators.Divider);
            RefreshCommandString();
            RefreshCurrentNumberString();
        }

        private void ExaluateExpression(object sender, RoutedEventArgs e)
        {
            executor.EvaluateExpression();
            RefreshCommandString();
            RefreshCurrentNumberString();
        }

        private void PushDot(object sender, RoutedEventArgs e)
        {
            executor.PushDot();
            RefreshCurrentNumberString();
        }

        
    }
}

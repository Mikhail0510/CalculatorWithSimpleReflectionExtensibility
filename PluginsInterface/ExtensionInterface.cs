
namespace AddInContract
{
    public interface IExtension
    {
        double Calculate(double number);
        double Calculate(int number);
        ExtensionInfo GetInfo();
    }

    public class ExtensionInfo
    {
        public string ButtonText { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
    }
}

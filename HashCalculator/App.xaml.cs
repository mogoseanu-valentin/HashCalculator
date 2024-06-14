using System.Configuration;
using System.Data;
using System.Windows;

namespace HashCalculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Data Data { get; private set; } = new Data();
    }
}

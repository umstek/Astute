using System.Windows;
using Astute.Configuration;

namespace UX
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            FileIO.SaveConfiguration(Configuration.Config);
        }
    }
}
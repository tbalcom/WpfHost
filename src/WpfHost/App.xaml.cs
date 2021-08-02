namespace WpfHost
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public App()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName))
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();
            ConfigureServices(services);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            ServiceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Configure Dependency Injection...
        /// </summary>
        /// <param name="serviceCollection">Collection of services.</param>
        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(MainWindow));
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            ServiceProvider.GetRequiredService<MainWindow>()
                .Show();
        }
    }
}

using Domein.Controllers;
using Domein.IRepositories;
using Domein.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Gentse_Feesten
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_StartUp(object sender, StartupEventArgs e)
        {
            IEvenementRepository evenementRepository = new EvenementRepository();
            IPlannerRepository plannerRepository = new PlannerRepository();

            DomeinController dc = new DomeinController(evenementRepository, plannerRepository);

            MainWindow wnd = new MainWindow(dc);

            wnd.Title= "Evenementen overzicht";
            wnd.Show();
        }
    }
}

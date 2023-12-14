using Cui;
using Domein.Controllers;
using Domein.IRepositories;
using Domein.Repositories;
using System;
using System.DirectoryServices.ActiveDirectory;

namespace Startup
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            (new StartUp()).Run();
        }
        private void Run()
        {
            IEvenementRepository evenementRepository = new EvenementRepository();
            IPlannerRepository plannerRepository = new PlannerRepository();

            var app = new GentseFeestenDBApp(new DomeinController(evenementRepository, plannerRepository));
            app.BewerkDB();
        }
    }
}

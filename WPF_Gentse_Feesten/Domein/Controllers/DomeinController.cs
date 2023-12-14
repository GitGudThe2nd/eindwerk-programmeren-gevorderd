using Domein.IRepositories;
using Domein.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Controllers
{
    public class DomeinController
    {
        private GentseFeesten _gentseFeesten;

        public DomeinController(IEvenementRepository evenementRepository, IPlannerRepository plannerRepository)
        {
            _gentseFeesten = new GentseFeesten(evenementRepository, plannerRepository); 
        }

        public void EnsurePopulated()
        {
            _gentseFeesten.EnsurePopulated();
        }

        public List<Evenement> GeefEvenementen()
        {
            return _gentseFeesten.GeefEvenementen();
        }

        public bool VoegEvenementToeAanPlanner(Evenement evenement)
        {
            return _gentseFeesten.VoegEvenementToeAanPlanner(evenement);
        }

        public void VerwijderEvenementVanPlanner(Evenement evenement)
        {
            _gentseFeesten.VerwijderEvenementVanPlanner(evenement);
        }

        public List<Evenement> GeefPlannerEvenementen()
        {
            return _gentseFeesten.GeefPlannerEvenementen().OrderBy(x => x.Startdatum).ToList();
        }
    }
}

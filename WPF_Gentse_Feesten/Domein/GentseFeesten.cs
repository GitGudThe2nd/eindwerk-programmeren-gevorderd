using Domein.IRepositories;
using Domein.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein
{
    public class GentseFeesten
    {
        private IEvenementRepository _evenementRepository;
        private IPlannerRepository _plannerRepository;

        public GentseFeesten(IEvenementRepository evenementRepository, IPlannerRepository plannerRepository)
        {
            _evenementRepository = evenementRepository;
            _plannerRepository = plannerRepository;
        }

        public void EnsurePopulated()
        {
            _evenementRepository.EnsurePopulated();
        }

        public List<Evenement> GeefEvenementen()
        {
            return _evenementRepository.GeefEvenementen();
        }

        private bool ControleerEvenementDatum(Evenement evenement)
        {
            var planner = _plannerRepository.GeefPlannerEvenementen();
            return !planner.Any(e => (evenement.Startdatum >= e.Startdatum && evenement.Startdatum <= e.Einddatum) || (evenement.Einddatum >= e.Startdatum && evenement.Einddatum <= e.Einddatum)
                        || (evenement.Startdatum <= e.Startdatum && evenement.Einddatum >= e.Einddatum));
        }

        public bool VoegEvenementToeAanPlanner(Evenement evenement)
        {
            if (ControleerEvenementDatum(evenement))
            {
                _plannerRepository.InsertEvenement(evenement);
                return true;
            }
            return false;
        }

        public void VerwijderEvenementVanPlanner(Evenement evenement)
        {
            _plannerRepository.VerwijderEvenement(evenement.Id);
        }

        public List<Evenement> GeefPlannerEvenementen()
        {
            return _plannerRepository.GeefPlannerEvenementen();
        }
    }
}

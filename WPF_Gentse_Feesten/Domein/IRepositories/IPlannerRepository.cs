using Domein.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.IRepositories
{
    public interface IPlannerRepository
    {
        void InsertEvenement(Evenement evenement);
        void VerwijderEvenement(Guid Id);
        List<Evenement> GeefPlannerEvenementen();
    }
}

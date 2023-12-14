using Domein.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.IRepositories
{
    public interface IEvenementRepository
    {
        void EnsurePopulated();

        List<Evenement> GeefEvenementen();
    }
}

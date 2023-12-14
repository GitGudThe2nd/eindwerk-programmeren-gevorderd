using Domein.Controllers;
using System;

namespace Cui
{
    public class GentseFeestenDBApp
    {
        private readonly DomeinController _dc;

        public GentseFeestenDBApp(DomeinController dc)
        {
            _dc = dc;
        }

        public void BewerkDB()
        {
            try
            {
                _dc.EnsurePopulated();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}

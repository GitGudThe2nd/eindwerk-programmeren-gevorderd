using System;
using System.Collections.Generic;

namespace Domein.Models
{
    public class Evenement
    {
        public Guid Id { get; set; }
        public DateTime? Einddatum { get; set; }
        public DateTime? Startdatum { get; set; }
        public List<Evenement> KindEvenementen { get; set; } = new List<Evenement>();
        public Guid? GuidParentEvenement { get; set; }

        public string Beschrijving { get; set; }
        public string Naam { get; set; }
        public double? Prijs { get; set; }

        public override string ToString()
        {
            return $"{Naam} | {Prijs} €";
        }
    }
}

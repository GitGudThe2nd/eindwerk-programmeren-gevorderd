using Domein.IRepositories;
using Domein.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Repositories
{
    public class EvenementRepository : IEvenementRepository
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Gentse_Feesten;Integrated Security=True;TrustServerCertificate=True";

        public void EnsurePopulated()
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM dbo.Evenementen", conn))
                    {
                        int count = (int)command.ExecuteScalar();
                        if (count > 0)
                        {
                            Console.WriteLine("The table has rows.");
                        }
                        else
                        {
                            string insertSql = "INSERT INTO Evenementen (Id, Einddatum, Startdatum, GuidParentEvenement, Beschrijving, " +
                            "Naam, Prijs) VALUES ( @Id, @Einddatum, @Startdatum, @GuidParentEvenement, @Beschrijving, @Naam, @Prijs);";
                            string filename = "../../../../Domein/gentse-feesten-evenementen-2022.csv";
                            using (StreamReader sr = new StreamReader(filename))
                            {
                                while (!sr.EndOfStream)
                                {
                                    var gesplits = sr.ReadLine().Split(';');
                                    if (gesplits.Length != 8)
                                        continue;

                                    SqlCommand insertCommand = new(insertSql, conn);
                                    insertCommand.Parameters.AddWithValue("@Id", Guid.Parse(gesplits[0]));
                                    insertCommand.Parameters.AddWithValue("@Einddatum", gesplits[1] != string.Empty ? DateTime.Parse(gesplits[1]) : DBNull.Value);
                                    insertCommand.Parameters.AddWithValue("@Startdatum", gesplits[2] != string.Empty ? DateTime.Parse(gesplits[2]) : DBNull.Value);
                                    if (Guid.TryParse(gesplits[4], out Guid parentEvent))
                                    {
                                        insertCommand.Parameters.AddWithValue("@GuidParentEvenement", parentEvent);
                                    }
                                    else
                                    {
                                        insertCommand.Parameters.AddWithValue("@GuidParentEvenement", DBNull.Value);
                                    }
                                    insertCommand.Parameters.AddWithValue("@Beschrijving", gesplits[5]);
                                    insertCommand.Parameters.AddWithValue("@Naam", gesplits[6]);
                                    insertCommand.Parameters.AddWithValue("@Prijs", gesplits[7] != string.Empty ? double.Parse(gesplits[7]) : DBNull.Value);

                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public List<Evenement> GeefEvenementen()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Evenementen", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var evenements = new List<Evenement>();

                        while (reader.Read())
                        {
                            var evenement = new Evenement
                            {
                                Id = reader.GetGuid(0),
                                Einddatum = reader.IsDBNull(1) ? null : reader.GetDateTime(1),
                                Startdatum = reader.IsDBNull(2) ? null : reader.GetDateTime(2),
                                GuidParentEvenement = reader.IsDBNull(3) ? null : reader.GetGuid(3),
                                Beschrijving = reader.GetString(4),
                                Naam = reader.GetString(5),
                                Prijs = reader.IsDBNull(6) ? null : Convert.ToDouble(reader.GetDecimal(6))

                            };
                            evenements.Add(evenement);
                        }

                        foreach (var evenement in evenements)
                        {
                            evenement.KindEvenementen = evenements.Where(x => x.GuidParentEvenement == evenement.Id).ToList();
                        }

                        foreach (var evenement in evenements)
                        {
                            GetDatesAndPrices(evenement);
                        }

                        return evenements;
                    }
                }
            }
        }
        private void GetDatesAndPrices(Evenement evenement)
        {
            if (!evenement.Prijs.HasValue)
            {
                evenement.Prijs = evenement.KindEvenementen.Sum(x => x.Prijs) ?? 0;
            }
            if (!evenement.Startdatum.HasValue)
            {
                evenement.Startdatum = evenement.KindEvenementen.Where(x => x.Startdatum.HasValue).Min(x => x.Startdatum);
            }
            if (!evenement.Einddatum.HasValue)
            {
                evenement.Einddatum = evenement.KindEvenementen.Where(x => x.Einddatum.HasValue).Max(x => x.Einddatum);
            }

            foreach (var kindEvenement in evenement.KindEvenementen)
            {
                GetDatesAndPrices(kindEvenement);
            }
        }


    }
}

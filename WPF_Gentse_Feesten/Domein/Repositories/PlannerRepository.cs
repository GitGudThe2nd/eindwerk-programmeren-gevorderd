using Domein.IRepositories;
using Domein.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domein.Repositories
{
    public class PlannerRepository : IPlannerRepository
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Gentse_Feesten;Integrated Security=True;TrustServerCertificate=True";

        public void InsertEvenement(Evenement evenement)
        {
            try
            {
                using (SqlConnection connection = new(connectionString))
                {
                    connection.Open();

                    string insertSql = "INSERT INTO Planner (Id, Einddatum, Startdatum, GuidParentEvenement, Beschrijving, " +
                            "Naam, Prijs) VALUES ( @Id, @Einddatum, @Startdatum, @GuidParentEvenement, @Beschrijving, @Naam, @Prijs);";

                    SqlCommand insertCommand = new(insertSql, connection);
                    insertCommand.Parameters.AddWithValue("@Id", evenement.Id);
                    insertCommand.Parameters.AddWithValue("@Einddatum", evenement.Einddatum != null ? evenement.Einddatum : DBNull.Value);
                    insertCommand.Parameters.AddWithValue("@Startdatum", evenement.Startdatum != null ? evenement.Startdatum : DBNull.Value);
                    insertCommand.Parameters.AddWithValue("@GuidParentEvenement", evenement.GuidParentEvenement != null ? evenement.GuidParentEvenement : DBNull.Value);
                    insertCommand.Parameters.AddWithValue("@Beschrijving", evenement.Beschrijving);
                    insertCommand.Parameters.AddWithValue("@Naam", evenement.Naam);
                    insertCommand.Parameters.AddWithValue("@Prijs", evenement.Prijs != null ? evenement.Prijs : DBNull.Value);
                    insertCommand.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void VerwijderEvenement(Guid Id)
        {
            try
            {
                using (SqlConnection connection = new(connectionString))
                {
                    connection.Open();

                    string deleteSQL = $"DELETE from Planner WHERE Id = @Id;";

                    SqlCommand deleteCommand = new(deleteSQL, connection);
                    deleteCommand.Parameters.AddWithValue("@Id", Id);
                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<Evenement> GeefPlannerEvenementen()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Planner", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var evenements = new List<Evenement>();

                        // hier pakken we alle evenementen zoals ze in de DB zijn.
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
                        return evenements;
                    }
                }
            }

        }
    }
}

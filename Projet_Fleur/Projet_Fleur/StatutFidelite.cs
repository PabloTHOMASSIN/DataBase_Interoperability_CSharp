using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Fleur
{
    internal class StatutFidelite
    {
        public int Id { get; set; }
        public string Statut { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int ClientId { get; set; }

        // Constructeur
        public StatutFidelite(int id, string statut, DateTime dateDebut, DateTime dateFin, int clientId)
        {
            Id = id;
            Statut = statut;
            DateDebut = dateDebut;
            DateFin = dateFin;
            ClientId = clientId;
        }
        // Méthode pour changer le statut de fidélité d'un client
        public static bool ChangerStatut(int clientId, string connectionString)
        {

            bool statutModifie = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Récupérer le nouveau statut de fidélité en fonction du nombre de commandes
                    MySqlCommand command = new MySqlCommand(@"
                UPDATE Statut_Fidelite
                SET statut = CASE
                    WHEN (
                        (SELECT COUNT(*) FROM Commande_Standard WHERE client_id = @clientId AND date_commande BETWEEN DATE_SUB(NOW(), INTERVAL 12 MONTH) AND NOW()) +
                        (SELECT COUNT(*) FROM Commande_Personnalisee WHERE client_id = @clientId AND date_commande BETWEEN DATE_SUB(NOW(), INTERVAL 12 MONTH) AND NOW())
                    ) >= 5 THEN 'OR'
                    WHEN (
                        (SELECT COUNT(*) FROM Commande_Standard WHERE client_id = @clientId AND date_commande BETWEEN DATE_SUB(NOW(), INTERVAL 12 MONTH) AND NOW()) +
                        (SELECT COUNT(*) FROM Commande_Personnalisee WHERE client_id = @clientId AND date_commande BETWEEN DATE_SUB(NOW(), INTERVAL 12 MONTH) AND NOW())
                    ) >= 1 THEN 'Bronze'
                    ELSE NULL
                END
                WHERE client_id = @clientId", connection);

                    command.Parameters.AddWithValue("@clientId", clientId);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Le statut de fidélité du client a été mis à jour.");
                        statutModifie = true;
                    }

                    
                    connection.Close();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            return statutModifie;
        }
        // Méthode pour obtenir le statut de fidelite d'un client  
        public static string GetStatutFidelite(int clientId, string connectionString)
        {
            string statut = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("SELECT statut FROM Statut_Fidelite WHERE client_id = @clientId", connection);
                    command.Parameters.AddWithValue("@clientId", clientId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            statut = reader.GetString("statut");
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            return statut;
        }

    }
}

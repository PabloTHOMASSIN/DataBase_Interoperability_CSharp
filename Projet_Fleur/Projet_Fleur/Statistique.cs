using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Fleur
{
    public static class Statistiques
    {
        //Méthode pour calculer le prix moyen d'une Commande et donc d'un bouquet 
        public static decimal CalculerPrixMoyenCommande(string connectionString)
        {
            decimal prixMoyen = 0;
            int nombreCommandes = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT SUM(montant_total) AS total, COUNT(*) AS count FROM (SELECT montant_total FROM commande_standard UNION ALL SELECT montant_total FROM commande_personnalisee) as t", connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        decimal total = (decimal)reader["total"];
                        nombreCommandes = Convert.ToInt32(reader["count"]);

                        if (nombreCommandes > 0)
                        {
                            prixMoyen = total / nombreCommandes;
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            return prixMoyen;
        }
        //Méthode pour trouver le meilleur client du mois
        public static int GetMeilleurClientDuMois(int mois, int annee, string connectionString)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT client_id
                            FROM (
                                SELECT client_id, SUM(montant_total) AS total_chiffre_affaires
                                FROM (
                                    SELECT id_commande_standard, client_id, montant_total
                                    FROM Commande_Standard
                                    WHERE YEAR(date_livraison) = @annee AND MONTH(date_livraison) = @mois
                                    UNION ALL
                                    SELECT id_commande_personnalisee, client_id, montant_total
                                    FROM Commande_Personnalisee
                                    WHERE YEAR(date_livraison) = @annee AND MONTH(date_livraison) = @mois
                                ) AS commandes
                                GROUP BY client_id
                                ORDER BY total_chiffre_affaires DESC
                            ) AS meilleurs_clients
                            LIMIT 1";
            command.Parameters.AddWithValue("@mois", mois);
            command.Parameters.AddWithValue("@annee", annee);
            object result = command.ExecuteScalar();
            if (result == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToInt32(result);
        }
        //Méthode pour trouver le nombre de commande menseulles
        public static int GetNombreCommandesMensuelles(int mois, int annee, string connectionString)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*) FROM (
                                SELECT id_commande_standard AS id_commande, 0 AS type_commande FROM Commande_Standard
                                WHERE YEAR(date_livraison) = @annee AND MONTH(date_livraison) = @mois
                                UNION
                                SELECT id_commande_personnalisee AS id_commande, 1 AS type_commande FROM Commande_Personnalisee
                                WHERE YEAR(date_livraison) = @annee AND MONTH(date_livraison) = @mois
                            ) AS commandes_mois";
                           
            command.Parameters.AddWithValue("@mois", mois);
            command.Parameters.AddWithValue("@annee", annee);
            return Convert.ToInt32(command.ExecuteScalar());
        }
        //Méthode pour trouver le nombre de commande annuelle
        public static int GetNombreCommandesAnnuelles(int annee, string connectionString)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*) FROM (
                            SELECT id_commande_standard AS id_commande, 0 AS type_commande FROM Commande_Standard
                            WHERE YEAR(date_livraison) = @annee
                            UNION
                            SELECT id_commande_personnalisee AS id_commande, 1 AS type_commande FROM Commande_Personnalisee
                            WHERE YEAR(date_livraison) = @annee 
                        ) AS commandes_annee";
            command.Parameters.AddWithValue("@annee", annee);
            return Convert.ToInt32(command.ExecuteScalar());
        }
        //Méthode pour trouver le CA mensuel
        public static decimal GetChiffreAffairesMensuel(int mois, int annee, string connectionString)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT SUM(montant_total) FROM (
                                SELECT montant_total FROM Commande_Standard
                                WHERE YEAR(date_livraison) = @annee AND MONTH(date_livraison) = @mois
                                UNION ALL
                                SELECT montant_total FROM Commande_Personnalisee
                                WHERE YEAR(date_livraison) = @annee AND MONTH(date_livraison) = @mois
                            ) AS chiffre_affaires_mois";
            command.Parameters.AddWithValue("@mois", mois);
            command.Parameters.AddWithValue("@annee", annee);
            object result = command.ExecuteScalar();
            if (result == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToDecimal(result);
        }
        //Méthode pour trouver le CA Annuel
        public static decimal GetChiffreAffairesAnnuel(int annee, string connectionString)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT SUM(montant_total) FROM (
                                SELECT montant_total FROM Commande_Standard
                                WHERE YEAR(date_livraison) = @annee
                                UNION ALL
                                SELECT montant_total FROM Commande_Personnalisee
                                WHERE YEAR(date_livraison) = @annee
                            ) AS chiffre_affaires_mois";
            command.Parameters.AddWithValue("@annee", annee);
            object result = command.ExecuteScalar();
            if (result == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToDecimal(result);
        }
        //Méthode pour trouver le meilleur produit du mois           
        public static string GetMeilleurProduitDuMois(int mois, int annee, string connectionString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT bs.nom AS NomProduit, COUNT(*) AS NombreDeCommandes " +
                               "FROM Commande_Personnalisee cs " +
                               "JOIN Produit bs ON cs.produit_id = bs.id_produit " +
                               "WHERE MONTH(cs.date_commande) = @mois AND YEAR(cs.date_commande) = @annee " +
                               "GROUP BY cs.produit_id " +
                               "ORDER BY NombreDeCommandes DESC " +
                               "LIMIT 1;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@mois", mois);
                command.Parameters.AddWithValue("@annee", annee);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string nomProduit = reader.GetString(0);
                    int nombreDeCommandes = reader.GetInt32(1);
                    return $"Le produit le plus populaire du mois est '{nomProduit}' avec {nombreDeCommandes} commandes.";
                }
                else
                {
                    return "Aucune commande standard n'a été trouvée pour le mois et l'année spécifiés.";
                }
            }
            
        }
        //Méthode pour trouver le meilleur bouquet standard du mois
        public static string GetMeilleurBouquetStandardDuMois(int mois, int annee, string connectionString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT bs.nom AS NomBouquetStandard, COUNT(*) AS NombreDeCommandes " +
                               "FROM Commande_Standard cs " +
                               "JOIN Bouquet_Standard bs ON cs.bouquet_standard_id = bs.id_bouquet_standard " +
                               "WHERE MONTH(cs.date_commande) = @mois AND YEAR(cs.date_commande) = @annee " +
                               "GROUP BY cs.bouquet_standard_id " +
                               "ORDER BY NombreDeCommandes DESC " +
                               "LIMIT 1;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@mois", mois);
                command.Parameters.AddWithValue("@annee", annee);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string nomBouquetStandard = reader.GetString(0);
                    int nombreDeCommandes = reader.GetInt32(1);
                    return $"Le bouquet standard le plus populaire du mois est '{nomBouquetStandard}' avec {nombreDeCommandes} commandes.";
                }
                else
                {
                    return "Aucune commande standard n'a été trouvée pour le mois et l'année spécifiés.";
                }
            }
        }
    }
}

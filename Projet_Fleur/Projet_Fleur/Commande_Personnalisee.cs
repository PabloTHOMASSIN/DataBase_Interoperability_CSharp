using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Fleur
{
        public class Commande_Personnalisee
        {
            //Attributs
            private int id;
            private int client_id;
            private DateTime date_commande;
            private DateTime date_livraison;
            private string adresse_livraison;
            private string message_accompagnement;
            private decimal montant_total;
            private string etat;
            private int magasin_id;
            private int produit_id;
            private int accessoire_id;
            //Propriétés
            public int Id { get => id; set => id = value; }
            public int ClientId { get => client_id; set => client_id = value; }
            public DateTime DateCommande { get => date_commande; set => date_commande = value; }
            public DateTime DateLivraison { get => date_livraison; set => date_livraison = value; }
            public string AdresseLivraison { get => adresse_livraison; set => adresse_livraison = value; }
            public string MessageAccompagnement { get => message_accompagnement; set => message_accompagnement = value; }
            public decimal MontantTotal { get => montant_total; set => montant_total = value; }
            public string Etat { get => etat; set => etat = value; }
            public int MagasinId { get => magasin_id; set => magasin_id = value; }
            public int Accessoire_Id { get => accessoire_id; set => magasin_id = value; }
            public int ProduitId { get => produit_id; set => produit_id = value; }
            //Constructeur vide
            public Commande_Personnalisee()
            {
                // Initialisation des propriétés
                Id = 0;
                DateCommande = DateTime.Now;
                DateLivraison = DateTime.Now;
                AdresseLivraison = string.Empty;
                MessageAccompagnement = string.Empty;
                MontantTotal = 0.0m;
                Etat = string.Empty;
                ClientId = 0;
                MagasinId = 0;
                ProduitId = 0;

            }
            //Constructeur
            public Commande_Personnalisee(int id, int clientId, DateTime dateCommande, DateTime dateLivraison, string adresseLivraison, string messageAccompagnement, decimal montantTotal, string etat, int magasinId, int produitID, int accessoire_id)
            {
                this.id = id;
                this.client_id = clientId;
                this.date_commande = dateCommande;
                this.date_livraison = dateLivraison;
                this.adresse_livraison = adresseLivraison;
                this.message_accompagnement = messageAccompagnement;
                this.montant_total = montantTotal;
                this.etat = etat;
                this.magasin_id = magasinId;
                this.produit_id = produitID;
                this.accessoire_id = accessoire_id;
            }
            //Méthode pour ajouter une commande personnalisée à la base de données
            public void Ajouter(string connectionString)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        MySqlCommand command = new MySqlCommand("INSERT INTO commande_personnalisee(id_commande_personnalisee, client_id, date_commande, date_livraison, adresse_livraison, message_accompagnement, montant_total, etat, magasin_id, produit_id, accessoire_id) VALUES(@id_commande_personnalisee, @clientId, @dateCommande, @dateLivraison, @adresseLivraison, @messageAccompagnement, @montantTotal, @etat, @magasinId, @produitID, @accessoireID)", connection);

                        command.Parameters.AddWithValue("@id_commande_personnalisee", id);
                        command.Parameters.AddWithValue("@clientId", client_id);
                        command.Parameters.AddWithValue("@dateCommande", date_commande);
                        command.Parameters.AddWithValue("@dateLivraison", date_livraison);
                        command.Parameters.AddWithValue("@adresseLivraison", adresse_livraison);
                        command.Parameters.AddWithValue("@messageAccompagnement", message_accompagnement);
                        command.Parameters.AddWithValue("@montantTotal", montant_total);
                        command.Parameters.AddWithValue("@etat", etat);
                        command.Parameters.AddWithValue("@magasinId", magasin_id);
                        command.Parameters.AddWithValue("@produitID", produit_id);
                        command.Parameters.AddWithValue("@accessoireID", accessoire_id);


                        command.ExecuteNonQuery();
                        Console.WriteLine("La commande personnalisée a été ajoutée à la base de données.");

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                }
            }
            //Méthode pour supprimer une commande personnalisée de la base de données
            public void Supprimer(string connectionString)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        MySqlCommand command = new MySqlCommand("DELETE FROM Commande_Personnalisee WHERE id_commande_personnalisee = @id", connection);
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();

                        Console.WriteLine("La commande personnalisée a été supprimée de la base de données.");

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                }
            }
            //Méthode pour afficher les détails d'une commande personnalisée
            public void AfficherDetails()
            {
                Console.WriteLine("ID : " + id);
                Console.WriteLine("Client ID : " + client_id);
                Console.WriteLine("Date de commande : " + date_commande);
                Console.WriteLine("Date de livraison : " + date_livraison);
                Console.WriteLine("Adresse de livraison : " + adresse_livraison);
                Console.WriteLine("Message d'accompagnement : " + message_accompagnement);
                Console.WriteLine("Montant total : " + montant_total);
                Console.WriteLine("État : " + etat);
                Console.WriteLine("Magasin ID : " + magasin_id);
                Console.WriteLine("Produit ID : " + produit_id);
                Console.WriteLine("Accessoire ID : " + accessoire_id);
                Console.WriteLine("------------------------");
            }
            //Méthode pour récupérer toutes les commandes personnalisées d'un client
            public static void GetCommandesPersonnaliseesParId(string connectionString, int commandeId)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        MySqlCommand command = new MySqlCommand("SELECT * FROM Commande_Personnalisee WHERE id_commande_personnalisee = @commandeId ", connection);
                        command.Parameters.AddWithValue("@commandeId", commandeId);

                        MySqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            int id = (int)reader["client_id"];
                            DateTime dateCommande = (DateTime)reader["date_commande"];
                            DateTime dateLivraison = (DateTime)reader["date_livraison"];
                            string adresseLivraison = (string)reader["adresse_livraison"];
                            string messageAccompagnement = (string)reader["message_accompagnement"];
                            decimal montantTotal = (decimal)reader["montant_total"];
                            string etat = (string)reader["etat"];
                            int magasinId = (int)reader["magasin_id"];
                            int produitId = (int)reader["produit_id"];
                            int accessoireID = (int)reader["accessoire_id"];

                            Console.WriteLine("Client ID : " + id);
                            Console.WriteLine("Date de commande : " + dateCommande);
                            Console.WriteLine("Date de livraison : " + dateLivraison);
                            Console.WriteLine("Adresse de livraison : " + adresseLivraison);
                            Console.WriteLine("Message d'accompagnement : " + messageAccompagnement);
                            Console.WriteLine("Montant total : " + montantTotal);
                            Console.WriteLine("État : " + etat);
                            Console.WriteLine("Magasin ID : " + magasinId);
                            Console.WriteLine("Produit ID : " + produitId);
                            Console.WriteLine("Accessoire ID : " + accessoireID);
                            Console.WriteLine("------------------------");
                        }

                        reader.Close();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                }
            }
            //Méthode pour afficher toutes les commandes personnalisées d'un client
            public static List<Commande_Personnalisee> GetCommandesLivreesParClient(string connectionString, int clientId)
            {
                List<Commande_Personnalisee> commandesLivrees = new List<Commande_Personnalisee>();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM Commande_Personnalisee WHERE etat = 'CL' AND client_id = {clientId}";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Commande_Personnalisee commande = new Commande_Personnalisee();

                            commande.Id = reader.GetInt32("id_commande_personnalisee");
                            commande.DateCommande = reader.GetDateTime("date_commande");
                            commande.DateLivraison = reader.GetDateTime("date_livraison");
                            commande.AdresseLivraison = reader.GetString("adresse_livraison");
                            commande.MessageAccompagnement = reader.GetString("message_accompagnement");
                            commande.MontantTotal = reader.GetDecimal("montant_total");
                            commande.Etat = reader.GetString("etat");
                            commande.ClientId = reader.GetInt32("client_id");
                            commande.MagasinId = reader.GetInt32("magasin_id");
                            commande.ProduitId = reader.GetInt32("produit_id");
                            commande.Accessoire_Id = reader.GetInt32("accessoire_id");

                            commandesLivrees.Add(commande);
                        }
                        reader.Close();
                    }
                    connection.Close();
                }

                return commandesLivrees;
            }
            //Méthode pour obtenir la liste de toute les commandes personnalisee
            public static List<Commande_Personnalisee> GetCommandesPersonnalisee(string connectionString)
            {
                List<Commande_Personnalisee> commandesPersonnalisee = new List<Commande_Personnalisee>();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM Commande_Personnalisee";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Commande_Personnalisee commande = new Commande_Personnalisee();

                            commande.Id = reader.GetInt32("id_commande_personnalisee");
                            commande.DateCommande = reader.GetDateTime("date_commande");
                            commande.DateLivraison = reader.GetDateTime("date_livraison");
                            commande.AdresseLivraison = reader.GetString("adresse_livraison");
                            commande.MessageAccompagnement = reader.IsDBNull("message_accompagnement") ? null : reader.GetString("message_accompagnement");
                            commande.MontantTotal = reader.GetDecimal("montant_total");
                            commande.Etat = reader.GetString("etat");
                            commande.ClientId = reader.GetInt32("client_id");
                            commande.MagasinId = reader.GetInt32("magasin_id");
                            commande.ProduitId = reader.GetInt32("produit_id");
                            commande.Accessoire_Id = reader.GetInt32("accessoire_id");

                            commandesPersonnalisee.Add(commande);
                        }

                        reader.Close();
                    }
                    connection.Close();
                }

                return commandesPersonnalisee;
            }
            //Méthode pour modifier l'etat d'une commande personnalisee
            public static void ModifierEtat(string connectionString, string etat, int commandeId)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"UPDATE Commande_Personnalisee SET etat='{etat}' WHERE id_commande_personnalisee={commandeId}";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }
    
}

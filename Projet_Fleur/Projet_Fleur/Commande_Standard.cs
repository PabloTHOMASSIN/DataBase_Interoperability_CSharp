using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_Fleur;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Utilities.Collections;
using System.Net.Sockets;

namespace Projet_Fleur
{
    public class Commande_Standard
    {
        //Attributs
        private int id;
        private int bouquetStandardId;
        private DateTime dateCommande;
        private DateTime dateLivraison;
        private string adresseLivraison;
        private string messageAccompagnement;
        private decimal montantTotal;
        private string etat;
        private int clientId;
        private int magasinId;
        //Propriétés
        public int Id { get => id; set => id = value; }
        public int BouquetStandardId { get => bouquetStandardId; set => bouquetStandardId = value; }
        public DateTime DateCommande { get => dateCommande; set => dateCommande = value; }
        public DateTime DateLivraison { get => dateLivraison; set => dateLivraison = value; }
        public string AdresseLivraison { get => adresseLivraison; set => adresseLivraison = value; }
        public string MessageAccompagnement { get => messageAccompagnement; set => messageAccompagnement = value; }
        public decimal MontantTotal { get => montantTotal; set => montantTotal = value; }
        public string Etat { get => etat; set => etat = value; }
        public int ClientId { get => clientId; set => clientId = value; }
        public int MagasinId { get => magasinId; set => magasinId = value; }
        //Constructeur à vide
        public Commande_Standard()
        {
            // Initialisation des propriétés
            Id = 0;
            BouquetStandardId = 0;
            DateCommande = DateTime.Now;
            DateLivraison = DateTime.Now;
            AdresseLivraison = string.Empty;
            MessageAccompagnement = string.Empty;
            MontantTotal = 0.0m;
            Etat = string.Empty;
            ClientId = 0;
            MagasinId = 0;
        }
        //Constructeur
        public Commande_Standard(int id, int bouquetStandardId, DateTime dateCommande, DateTime dateLivraison, string adresseLivraison, string messageAccompagnement, decimal montantTotal, string etat, int clientId, int magasinId)
        {
            this.id = id;
            this.bouquetStandardId = bouquetStandardId;
            this.dateCommande = dateCommande;
            this.dateLivraison = dateLivraison;
            this.adresseLivraison = adresseLivraison;
            this.messageAccompagnement = messageAccompagnement;
            this.montantTotal = montantTotal;
            this.etat = etat;
            this.clientId = clientId;
            this.magasinId = magasinId;
        }
        //Méthode pour ajouter une commande standard à la base de donnée
        public void Ajouter(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("INSERT INTO Commande_Standard (id_commande_standard, bouquet_standard_id, date_commande, date_livraison, adresse_livraison, message_accompagnement, montant_total, etat, client_id, magasin_id) VALUES (@id, @bouquetStandardId, @dateCommande, @dateLivraison, @adresseLivraison, @messageAccompagnement, @montantTotal, @etat, @clientId, @magasinId)", connection);

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@bouquetStandardId", bouquetStandardId);
                    command.Parameters.AddWithValue("@dateCommande", dateCommande);
                    command.Parameters.AddWithValue("@dateLivraison", dateLivraison);
                    command.Parameters.AddWithValue("@adresseLivraison", adresseLivraison);
                    command.Parameters.AddWithValue("@messageAccompagnement", messageAccompagnement);
                    command.Parameters.AddWithValue("@montantTotal", montantTotal);
                    command.Parameters.AddWithValue("@etat", etat);
                    command.Parameters.AddWithValue("@clientId", clientId);
                    command.Parameters.AddWithValue("@magasinId", magasinId);

                    command.ExecuteNonQuery();

                    Console.WriteLine("La commande standard a été ajoutée à la base de données.");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
        //Méthode pour supprimer une commande standard de la base de données
        public void Supprimer(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("DELETE FROM Commande_Standard WHERE id_commande_standard = @id", connection);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();

                    Console.WriteLine("La commande standard a été supprimée de la base de données.");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
        //Méthode pour afficher une commande standard sans requête sql
        public void Afficher()
        {
            Console.WriteLine($"ID : {id}");
            Console.WriteLine($"Bouquet standard ID : {bouquetStandardId}");
            Console.WriteLine($"Date commande : {dateCommande}");
            Console.WriteLine($"Date livraison : {dateLivraison}");
            Console.WriteLine($"Adresse livraison : {adresseLivraison}");
            Console.WriteLine($"Message accompagnement : {messageAccompagnement}");
            Console.WriteLine($"Montant total : {montantTotal}");
            Console.WriteLine($"État : {etat}");
            Console.WriteLine($"Client ID : {clientId}");
            Console.WriteLine($"Magasin ID : {magasinId}");
        }
        //Méthode pour afficher une commande standard en ayant son id
        public static void AfficherCommandesStandardId(string connectionString, int commandeId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM Commande_Standard WHERE id_commande_standard = @commandeId", connection);
                    command.Parameters.AddWithValue("@commandeId", commandeId);

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = (int)reader["client_id"];
                        int bouquetStandardId = (int)reader["bouquet_standard_id"];
                        DateTime dateCommande = (DateTime)reader["date_commande"];
                        DateTime dateLivraison = (DateTime)reader["date_livraison"];
                        string adresseLivraison = (string)reader["adresse_livraison"];
                        string messageAccompagnement = (string)reader["message_accompagnement"];
                        decimal montantTotal = (decimal)reader["montant_total"];
                        string etat = (string)reader["etat"];
                        int magasinId = (int)reader["magasin_id"];
  

                        Console.WriteLine($"Client ID : {id}");
                        Console.WriteLine($"Bouquet standard ID : {bouquetStandardId}");
                        Console.WriteLine($"Date commande : {dateCommande}");
                        Console.WriteLine($"Date livraison : {dateLivraison}");
                        Console.WriteLine($"Adresse livraison : {adresseLivraison}");
                        Console.WriteLine($"Message accompagnement : {messageAccompagnement}");
                        Console.WriteLine($"Montant total : {montantTotal}");
                        Console.WriteLine($"État : {etat}");
                        Console.WriteLine($"Magasin ID : {magasinId}");
                        Console.WriteLine("----------------------------------");
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
        //Méthode pour obtenir la liste des commandes standards livrées à un client
        public static List<Commande_Standard> GetCommandesLivreesParClient(string connectionString, int clientId)
        {
            List<Commande_Standard> commandesLivrees = new List<Commande_Standard>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM Commande_Standard WHERE etat = 'CL' AND client_id = {clientId}";

                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Commande_Standard commande = new Commande_Standard();

                        commande.Id = reader.GetInt32("id_commande_standard");
                        commande.BouquetStandardId = reader.GetInt32("bouquet_standard_id");
                        commande.DateCommande = reader.GetDateTime("date_commande");
                        commande.DateLivraison = reader.GetDateTime("date_livraison");
                        commande.AdresseLivraison = reader.GetString("adresse_livraison");
                        commande.MessageAccompagnement = reader.GetString("message_accompagnement");
                        commande.MontantTotal = reader.GetDecimal("montant_total");
                        commande.Etat = reader.GetString("etat");
                        commande.ClientId = reader.GetInt32("client_id");
                        commande.MagasinId = reader.GetInt32("magasin_id");

                        commandesLivrees.Add(commande);
                    }
                    
                    reader.Close();
                }
                connection.Close();
            }

            return commandesLivrees;
        }
        //Méthode pour obtenir toutes les commandes standards
        public static List<Commande_Standard> GetCommandesStandard(string connectionString)
        {
            List<Commande_Standard> commandesStandards = new List<Commande_Standard>();
           
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM Commande_Standard";

                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Commande_Standard commande = new Commande_Standard();

                        commande.Id = reader.GetInt32("id_commande_standard");
                        commande.BouquetStandardId = reader.GetInt32("bouquet_standard_id");
                        commande.DateCommande = reader.GetDateTime("date_commande");
                        commande.DateLivraison = reader.GetDateTime("date_livraison");
                        commande.AdresseLivraison = reader.GetString("adresse_livraison");

                        // Vérifiez d'abord si la valeur est NULL
                        if (!reader.IsDBNull(reader.GetOrdinal("message_accompagnement")))
                        {
                            commande.MessageAccompagnement = reader.GetString("message_accompagnement");
                        }

                        commande.MontantTotal = reader.GetDecimal("montant_total");
                        commande.Etat = reader.GetString("etat");
                        commande.ClientId = reader.GetInt32("client_id");
                        commande.MagasinId = reader.GetInt32("magasin_id");

                        commandesStandards.Add(commande);
                    }

                    reader.Close();
                }
                connection.Close();
            }

            return commandesStandards;
        }
        //Méthode pour modifier l'état d'une commande standard 
        public static void ModifierEtat(string connectionString, string etat, int commandeId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = $"UPDATE Commande_Standard SET etat='{etat}' WHERE id_commande_standard={commandeId}";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

    }
}



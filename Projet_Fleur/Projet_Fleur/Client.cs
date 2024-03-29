using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Projet_Fleur
{
    public class Client
    {
        //Attributs
        private int id;
        private string nom;
        private string prenom;
        private string telephone;
        private string courriel;
        private string motDePasse;
        private string adresseFacturation;
        private string carteCredit;
        //Propriétés
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Courriel { get => courriel; set => courriel = value; }
        public string MotDePasse { get => motDePasse; set => motDePasse = value; }
        public string AdresseFacturation { get => adresseFacturation; set => adresseFacturation = value; }
        public string CarteCredit { get => carteCredit; set => carteCredit = value; }
        //Constructeur
        public Client(int id, string nom, string prenom, string telephone, string courriel, string motDePasse, string adresseFacturation, string carteCredit)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.telephone = telephone;
            this.courriel = courriel;
            this.motDePasse = motDePasse;
            this.adresseFacturation = adresseFacturation;
            this.carteCredit = carteCredit;

        }
        //Méthode pour ajouter un client à la base de données
        public void Ajouter(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("INSERT INTO Client (id_client, nom, prenom, telephone, courriel, mot_de_passe, adresse_facturation, carte_credit) VALUES (@id, @nom, @prenom, @telephone, @courriel, @motDePasse, @adresseFacturation, @carteCredit)", connection);

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@nom", nom);
                    command.Parameters.AddWithValue("@prenom", prenom);
                    command.Parameters.AddWithValue("@telephone", telephone);
                    command.Parameters.AddWithValue("@courriel", courriel);
                    command.Parameters.AddWithValue("@motDePasse", motDePasse);
                    command.Parameters.AddWithValue("@adresseFacturation", adresseFacturation);
                    command.Parameters.AddWithValue("@carteCredit", carteCredit);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Le client a été ajouté à la base de donnée.");
                    }
                    else
                    {
                        Console.WriteLine("Aucun trouvé avec cet Id ou vous n'avez pas la permission.");
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
        //Méthode pour supprimer un client de la base de données
        public void Supprimer(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Désactiver temporairement la contrainte de clé étrangère pour supprimer les commandes associées au client
                    MySqlCommand disableFKCommand = new MySqlCommand("SET FOREIGN_KEY_CHECKS=0;", connection);
                    disableFKCommand.ExecuteNonQuery();

                    // Supprimer les commandes personnalisées associées au client
                    MySqlCommand deleteCommandeCommand = new MySqlCommand("DELETE FROM Commande_Personnalisee WHERE client_id = @id", connection);
                    deleteCommandeCommand.Parameters.AddWithValue("@id", id);
                    deleteCommandeCommand.ExecuteNonQuery();

                    // Supprimer le client
                    MySqlCommand deleteClientCommand = new MySqlCommand("DELETE FROM Client WHERE id_client = @id", connection);
                    deleteClientCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = deleteClientCommand.ExecuteNonQuery();

                    // Réactiver la contrainte de clé étrangère
                    MySqlCommand enableFKCommand = new MySqlCommand("SET FOREIGN_KEY_CHECKS=1;", connection);
                    enableFKCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Le client a été supprimé de la base de données.");
                    }
                    else
                    {
                        Console.WriteLine("Aucun client correspondant à l'ID spécifié n'a été trouvé.");
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
        //Méthode pour mettre à jour un client à notre base de données
        public void MettreAJour(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("UPDATE Client SET nom = @nom, prenom = @prenom, telephone = @telephone, courriel = @courriel, mot_de_passe = @motDePasse, adresse_facturation = @adresseFacturation, carte_credit = @carteCredit WHERE id_client = @id", connection);

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@nom", nom);
                    command.Parameters.AddWithValue("@prenom", prenom);
                    command.Parameters.AddWithValue("@telephone", telephone);
                    command.Parameters.AddWithValue("@courriel", courriel);
                    command.Parameters.AddWithValue("@motDePasse", motDePasse);
                    command.Parameters.AddWithValue("@adresseFacturation", adresseFacturation);
                    command.Parameters.AddWithValue("@carteCredit", carteCredit);

                    command.ExecuteNonQuery();

                    Console.WriteLine("Les informations du client ont été mises à jour.");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
        //Méthode pour récupérer tous les clients de la base de données
        public static List<Client> GetClients(string connectionString)
        {
            List<Client> clients = new List<Client>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("SELECT * FROM Client", connection);

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = (int)reader["id_client"];
                        string nom = (string)reader["nom"];
                        string prenom = (string)reader["prenom"];
                        string telephone = (string)reader["telephone"];
                        string courriel = (string)reader["courriel"];
                        string motDePasse = (string)reader["mot_de_passe"];
                        string adresseFacturation = (string)reader["adresse_facturation"];
                        string carteCredit = (string)reader["carte_credit"];

                        Client client = new Client(id, nom, prenom, telephone, courriel, motDePasse, adresseFacturation, carteCredit);
                        clients.Add(client);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            return clients;
        }
        //Méthode pour récupérer un client selon son id dans la base de donnée
        public static Client GetClientById(int clientId, string connectionString)
        {
            Client client = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("SELECT * FROM Client WHERE id_client = @id", connection);
                    command.Parameters.AddWithValue("@id", clientId);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string nom = (string)reader["nom"];
                        string prenom = (string)reader["prenom"];
                        string telephone = (string)reader["telephone"];
                        string courriel = (string)reader["courriel"];
                        string motDePasse = (string)reader["mot_de_passe"];
                        string adresseFacturation = (string)reader["adresse_facturation"];
                        string carteCredit = (string)reader["carte_credit"];

                        client = new Client(clientId, nom, prenom, telephone, courriel, motDePasse, adresseFacturation, carteCredit);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            return client;
        }

    }
}



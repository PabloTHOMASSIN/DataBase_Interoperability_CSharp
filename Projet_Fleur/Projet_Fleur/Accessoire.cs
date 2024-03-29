using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Fleur
{
    internal class Accessoire
    {
        //Attributs
        private int id_accessoire;
        private string nom;
        private string description_accessoire;
        private int quantite;
        private decimal prix;
        //Propriétés:
        public int Id_Accessoire { get => id_accessoire; set => id_accessoire = value; }
        public string Name { get => nom; set => nom = value; }
        public string Description_Accessoire { get => description_accessoire; set => description_accessoire = value; }
        public int Quantite { get => quantite; set => quantite = value; }
        public decimal Prix { get => prix; set => prix = value; }
        //Constructeur
        public Accessoire(int id_accessoire, string nom, string description_accessoire, int quantite, decimal prix)
        {
            this.id_accessoire = id_accessoire;
            this.nom = nom;
            this.description_accessoire = description_accessoire;
            this.quantite = quantite;
            this.prix = prix;


        }
        //Méthode pour obtenire la liste de tous les accessoires
        public static List<Accessoire> GetAccessoire(string connectionString)
        {
            List<Accessoire> accessoires = new List<Accessoire>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("SELECT * FROM Accessoire", connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id_accessoire");
                            string nom = reader.GetString("nom");
                            string description = reader.GetString("description_accessoire");
                            decimal prix = reader.GetDecimal("prix");
                            int quantite = reader.GetInt32("quantité");


                            Accessoire bouquet = new Accessoire(id, nom, description, quantite, prix);
                            accessoires.Add(bouquet);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            return accessoires;
        }
        //Méthode pour ajouter un accessoire 
        public void Ajouter(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Accessoire (id_accessoire, nom, description_accessoire, quantité, prix) VALUES (@id_accessoire, @nom, @description, @quantite, @prix)";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@id_accessoire", id_accessoire);
                    command.Parameters.AddWithValue("@nom", this.nom);
                    command.Parameters.AddWithValue("@description", this.description_accessoire);
                    command.Parameters.AddWithValue("@quantite", this.quantite);
                    command.Parameters.AddWithValue("@prix", this.prix);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
        //Méthode pour supprimer un accessoire
        public static void Supprimer(string connectionString, int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"DELETE FROM Accessoire WHERE id_accessoire = {id}";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
        //Méthode pour mettre à jour un accessoire
        public void MettreAJour(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("UPDATE Accessoire SET nom = @nom, description_accessoire = @description, prix = @prix, quantité = @qttdisponible WHERE id_accessoire = @id", connection);

                    command.Parameters.AddWithValue("@id", id_accessoire);
                    command.Parameters.AddWithValue("@nom", nom);
                    command.Parameters.AddWithValue("@description", description_accessoire);
                    command.Parameters.AddWithValue("@prix", prix);
                    command.Parameters.AddWithValue("@qttdisponible", quantite);



                    command.ExecuteNonQuery();

                    Console.WriteLine("Les informations de l'accessoire ont été mises à jour.");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);

            }
        }
    }

}
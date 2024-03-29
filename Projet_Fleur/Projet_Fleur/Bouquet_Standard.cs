using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Fleur
{


    internal class Bouquet_Standard
    {
        //Attributs
        private int id_bouquet_standard;
        private string nom;
        private string description;
        private decimal prix;
        private string categorie;
        private int produit_id;
        //Propriétés
        public int Id_Bouquet_Standard { get => id_bouquet_standard; set => id_bouquet_standard = value; }
        public string Name { get => nom; set => nom = value; }
        public string Description { get => description; set => description = value; }
        public decimal Prix { get => prix; set => prix = value; }
        public string Categorie { get => categorie; set => categorie = value; }
        public int  Produit_ID { get => produit_id; set => produit_id = value; }
        //Constructeur
        public Bouquet_Standard(int id_bouquet_standard, string nom, string descritpion, decimal prix, string categorie, int produit_id)
        {
            this.id_bouquet_standard = id_bouquet_standard;
            this.nom = nom;
            this.description = descritpion;
            this.prix = prix;
            this.categorie = categorie;
            this.produit_id = produit_id;

        }
        //Méthode pour obtenir la liste de tous les bouquets standards
        public static List<Bouquet_Standard> GetBouquetStandards(string connectionString)
        {
            List<Bouquet_Standard> bouquetStandards = new List<Bouquet_Standard>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("SELECT * FROM Bouquet_Standard", connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id_bouquet_standard = reader.GetInt32("id_bouquet_standard");
                            string nom = reader.GetString("nom");
                            string description = reader.GetString("description");
                            decimal prix = reader.GetDecimal("prix");
                            string categorie = reader.GetString("catégorie");
                            int produit_id = reader.GetInt32("produit_id");

                            Bouquet_Standard bouquet = new Bouquet_Standard(id_bouquet_standard, nom, description, prix, categorie, produit_id);
                            bouquetStandards.Add(bouquet);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            return bouquetStandards;
        }
        //Méthode pour ajouter un bouquet standard 
        public void Ajouter(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Bouquet_Standard (id_bouquet_standard, nom, description, prix, catégorie, produit_id) VALUES (@id_bouquet_standard, @nom, @description, @prix, @catégorie, @produit_id)";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@id_bouquet_standard", id_bouquet_standard);
                    command.Parameters.AddWithValue("@nom", this.nom);
                    command.Parameters.AddWithValue("@description", this.description);
                    command.Parameters.AddWithValue("@prix", this.prix);
                    command.Parameters.AddWithValue("@catégorie", categorie);
                    command.Parameters.AddWithValue("@produit_id", produit_id);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
        //Méthode pour supprimer un bouquet standard 
        public static void Supprimer(string connectionString, int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"DELETE FROM Bouquet_standard WHERE id_bouquet_standard = {id}";

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
        //Méthode pour mettre à jour un bouquet standard 
        public void MettreAJour(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("UPDATE Bouquet_Standard SET nom = @nom, description = @description, prix = @prix, catégorie = @categorie, produit_id = @produit_id WHERE id_bouquet_standard = @id", connection);

                    command.Parameters.AddWithValue("@id", id_bouquet_standard);
                    command.Parameters.AddWithValue("@nom", nom);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@prix", prix);
                    command.Parameters.AddWithValue("@categorie", categorie);
                    command.Parameters.AddWithValue("@produit_id", produit_id);



                    command.ExecuteNonQuery();

                    Console.WriteLine("Les informations du bouquet standard ont été mises à jour.");
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

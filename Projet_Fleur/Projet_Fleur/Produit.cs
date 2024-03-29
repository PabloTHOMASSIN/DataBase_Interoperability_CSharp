using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_Fleur;
using System.ComponentModel.Design;
using System.Data;

namespace Projet_Fleur
{
    public class Produit
    {
        //Attributs
        private int id_produit;
        private string nom;
        private string description;
        private decimal prix;
        private int qttdisponible;
        private string disponibilite;
        //Propriétés
        public int Id_Produit { get => id_produit; set => id_produit = value; }
        public string Name { get => nom; set => nom = value; }
        public string Description { get => description; set => description = value; }
        public decimal Prix { get => prix; set => prix = value; }
        public int QttDisponible { get => qttdisponible; set => qttdisponible = value; }
        public string Disponibilite { get => disponibilite; set => disponibilite = value; }
        //Constructeur
        public Produit(int id_produit, string nom, string description, decimal prix, int qttdisponible, string disponibilite)
        {


            this.id_produit = id_produit;
            this.nom = nom;
            this.description = description;
            this.prix = prix;
            this.qttdisponible = qttdisponible;
            this.disponibilite = disponibilite;

        }
        //Méthode pour obtenir la liste de tous les produits
        public static List<Produit> GetProduits(string connectionString)
        {
            List<Produit> produits = new List<Produit>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("SELECT * FROM Produit", connection);

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = (int)reader["id_produit"];
                        string nom = (string)reader["nom"];
                        string description = (string)reader["description"];
                        decimal prix = (decimal)reader["prix"];
                        int qttdisponible = (int)reader["quantitéDisponible"];
                        string disponibilite = (string)reader["disponibilité"];

                        Produit produit = new Produit(id, nom, description, prix, qttdisponible, disponibilite);
                        produits.Add(produit);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            return produits;
        }
        //Méthode pour obtenir un produit selon un id fournit
        public static Produit GetProduitById(int produit_id, string connectionString)
        {
            Produit produit = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("SELECT * FROM Produit WHERE id_produit = @id_produit", connection);
                    command.Parameters.AddWithValue("@id_produit", produit_id);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string nom = (string)reader["nom"];
                        string description = (string)reader["description"];
                        decimal prix = (decimal)reader["prix"];
                        int qttdisponible = (int)reader["quantitéDisponible"];
                        string disponibilite = (string)reader["disponibilité"];

                        produit = new Produit(produit_id, nom, description, prix, qttdisponible, disponibilite); ;
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            return produit;
        }
        //Méthode pour mettre à jour un produit dans la base de donnée
        public void MettreAJour(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("UPDATE Produit SET nom = @nom, description = @description, prix = @prix, quantitéDisponible = @qttdisponible, disponibilité = @disponibilite WHERE id_produit = @id", connection);

                    command.Parameters.AddWithValue("@id", id_produit);
                    command.Parameters.AddWithValue("@nom", nom);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@prix", prix);
                    command.Parameters.AddWithValue("@qttdisponible", qttdisponible);
                    command.Parameters.AddWithValue("@disponibilite", disponibilite);


                    command.ExecuteNonQuery();

                    Console.WriteLine("Les informations du produit ont été mises à jour.");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
        //Méthode pour ajouter un produit dans la base de donnée
        public void Ajouter(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("INSERT INTO Produit (id_produit, nom, description, prix, quantitéDisponible, disponibilité) VALUES (@id_produit, @nom, @description, @prix, @qttdisponible, @disponibilite)", connection);

                    command.Parameters.AddWithValue("@id_produit", id_produit);
                    command.Parameters.AddWithValue("@nom", nom);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@prix", prix);
                    command.Parameters.AddWithValue("@qttdisponible", qttdisponible);
                    command.Parameters.AddWithValue("@disponibilite", disponibilite);


                    command.ExecuteNonQuery();

                  



                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
        //Méthode pour supprimer un produit dans la base de donnée
        public void Supprimer(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Désactiver temporairement la contrainte de clé étrangère
                    MySqlCommand disableFKCommand = new MySqlCommand("SET foreign_key_checks = 0;", connection);
                    disableFKCommand.ExecuteNonQuery();

                    // Supprimer les produits associé au commande personnalisee
                    MySqlCommand deleteCommandeCommand = new MySqlCommand("DELETE FROM Commande_Personnalisee WHERE produit_id = @id", connection);
                    deleteCommandeCommand.Parameters.AddWithValue("@id", id_produit);
                    deleteCommandeCommand.ExecuteNonQuery();

                    // Supprimer les produits associé au magasin
                    MySqlCommand deleteCommandeCommand1 = new MySqlCommand("DELETE FROM Magasin WHERE produit_id= @id", connection);
                    deleteCommandeCommand1.Parameters.AddWithValue("@id", id_produit);
                    deleteCommandeCommand1.ExecuteNonQuery();

                    // Supprimer les produits associé au Details de commande
                    MySqlCommand deleteCommandeCommand2 = new MySqlCommand("DELETE FROM Details_Commande WHERE produit_id= @id", connection);
                    deleteCommandeCommand2.Parameters.AddWithValue("@id", id_produit);
                    deleteCommandeCommand2.ExecuteNonQuery();

                    // Supprimer les produits associé au bouquet standard
                    MySqlCommand deleteCommandeCommand3 = new MySqlCommand("DELETE FROM Bouquet_Standard WHERE produit_id= @id", connection);
                    deleteCommandeCommand3.Parameters.AddWithValue("@id", id_produit);
                    deleteCommandeCommand3.ExecuteNonQuery();

                    // Supprimer l'enregistrement parent dans produit
                    MySqlCommand deleteParentRecordCommand = new MySqlCommand("DELETE FROM Produit WHERE id_produit = @produitId", connection);
                    deleteParentRecordCommand.Parameters.AddWithValue("@produitId", id_produit);
                    int parentRowsAffected = deleteParentRecordCommand.ExecuteNonQuery();
                    

                    if (parentRowsAffected > 0)
                    {
                        Console.WriteLine("Le produit a été supprimé de la base de données.");
                    }
                    else
                    {
                        Console.WriteLine("Aucun produit correspondant à l'ID spécifié n'a été trouvé.");
                    }

                    // Réactiver la contrainte de clé étrangère
                    MySqlCommand enableForeignKeyCommand = new MySqlCommand("SET foreign_key_checks = 1;", connection);
                    enableForeignKeyCommand.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
        //Méthode pour obtenir le seuil d'un produit
        public static int Seuil(string connectionString, int idProduit)
        {
            int seuil = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT SUM(dc.quantite) AS quantite_totale
                                    FROM Details_Commande dc
                                    INNER JOIN Commande_Standard cs ON dc.commande_type = 'STANDARD' AND dc.commande_id = cs.id_commande_standard
                                    WHERE cs.etat IN ('VINV', 'CPAV')
                                    UNION ALL
                                    SELECT SUM(dc.quantite) AS quantite_totale
                                    FROM Details_Commande dc
                                    INNER JOIN Commande_Personnalisee cp ON dc.commande_type = 'PERSONNALISÉE' AND dc.commande_id = cp.id_commande_personnalisee
                                    WHERE cp.etat IN ('VINV', 'CPAV')";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idProduit", idProduit);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int.TryParse(reader["quantite_totale"].ToString(), out seuil);
                        }
                    }
                }
            }

            return seuil;
        }
    }
}


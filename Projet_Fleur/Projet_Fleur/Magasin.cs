using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_Fleur
{
    //On pourrait ici rajouter des méthodes classique vue dans tout le programme, d'ajout, suppression ... On considère pour simplifier le rendu que si Mr Belle Fleur doit ajouter un magasin il le fera à la main car cela est ponctuel, et pas quotidien / hebdomadaire ... ( #EasterEggs : sauf si il devenait le BigBoss de la fleur mais on en doute pour le moment vue que de pauvre étudiant développe son appli)
    public class Magasin
    {
        //Attributs
        private int id_magasin;
        private string name;
        private string adresse;
        private int bouquet_standard_id;
        private int produit_id;
        //Propriétés
        public int Id_Magasin { get => id_magasin; set => id_magasin = value; }
        public string Name { get => name; set => name = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public int Bouquet_Standard_Id { get => bouquet_standard_id; set => bouquet_standard_id = value; }
        public int Produit_Id { get => produit_id; set => produit_id = value; }
        //Constructeur
        public Magasin(int id_magasin, string name, string adresse, int bouquet_standard_id, int produit_id)
        {


            this.id_magasin = id_magasin;
            this.name = name;
            this.adresse = adresse;
            this.bouquet_standard_id = bouquet_standard_id;
            this.produit_id = produit_id;

        }
        //Méthode pour obtenir la liste des magasins
        public static List<Magasin> GetMagasins(string connectionString)
        {
            List<Magasin> magasins = new List<Magasin>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("SELECT * FROM Magasin", connection);

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int id_magasin = (int)reader["id_magasin"];
                        string name = (string)reader["nom"];
                        string adresse = (string)reader["adresse"];
                        int bouquet_standard_id = (int)reader["bouquet_standard_id"];
                        int produit_id = (int)reader["produit_id"];


                        Magasin magasin = new Magasin(id_magasin, name, adresse, bouquet_standard_id, produit_id);
                        magasins.Add(magasin);
                    }

                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            return magasins;
        }

    }
}

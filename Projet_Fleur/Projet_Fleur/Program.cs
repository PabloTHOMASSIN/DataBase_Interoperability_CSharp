using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Xml;
using Projet_Fleur;
using System.Drawing.Printing;
using MySqlX.XDevAPI;
using System.Diagnostics;
using Org.BouncyCastle.Tls;

internal class Program
{


    static void Main(string[] args)
    {
        string connectionString = " ";
        // Début phase de login
        do
        {
            Console.WriteLine("===BASE DE DONNEE : FLEUR ====");
            Console.WriteLine("Veuillez vous identifier : ");
            Console.Write("Identifiant : ");
            string id = Console.ReadLine();
            Console.Write("Mot de passe : ");
            string mdp = Console.ReadLine();


            if (id == "root" && mdp == "root")
            {
                connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleur;UID=root;PASSWORD=root";
            }
            else if (id == "bozo" && mdp == "bozo")
            {
                connectionString = "SERVER=localhost;PORT=3306;DATABASE=fleur;UID=bozo;PASSWORD=bozo";
            }
            else
            {
                Console.WriteLine("Erreur d'identification veuillez réessayer");
            }

        } while (connectionString == " ");
        // Fin phase de login
        bool quit = false;
        //Début Menu Principal
        while (!quit)
        {
            Console.WriteLine("=== Menu Principal ===");
            Console.WriteLine("1. Module Client");
            Console.WriteLine("2. Module Produit");
            Console.WriteLine("3. Module Commande");
            Console.WriteLine("4. Module Statistiques");
            Console.WriteLine("5. Export en XML (requête synchronisée)");
            Console.WriteLine("6. Export en JSON");
            Console.WriteLine("7. Quitter");

            Console.Write("Choix : ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GestionModuleClient(connectionString);
                    break;
                case "2":
                    GestionModuleProduit(connectionString);
                    break;
                case "3":
                    GestionModuleCommande(connectionString);
                    break;
                case "4":
                    GestionModuleStatistiques(connectionString);
                    break;
                case "5":
                    ExporterEnXML(connectionString);
                    break;
                case "6":
                    ExporterEnJSON(connectionString);
                    break;
                case "7":
                    quit = true;
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }

            Console.WriteLine();

        }
        //Fin Menu Principal
        Console.WriteLine("Application terminée.");
    }

    //Début Terminal de Commande
    static void GestionModuleClient(string connectionString)
    {
        bool back = false;

        while (!back)
        {
            Console.WriteLine("=== Module Client ===");
            Console.WriteLine("1. Afficher tous les clients");
            Console.WriteLine("2. Afficher les caractéristiques d'un client");
            Console.WriteLine("3. Ajouter un client");
            Console.WriteLine("4. Afficher les commandes livrée à un client");
            Console.WriteLine("5. Supprimer un client");
            Console.WriteLine("6. Modifier les coordonnées d'un client");
            Console.WriteLine("7. Afficher le statuts de fidélité des clients");
            Console.WriteLine("8. Modifier le statut de fidélité d'un client");
            Console.WriteLine("9. Afficher tous les clients avec une même addresse de facturation (requête auto-jointe)");
            Console.WriteLine("10. Revenir en arrière");

            Console.Write("Choix : ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AfficherTousLesClients(connectionString);
                    break;
                case "2":
                    AfficherCaracteristiquesClient(connectionString);
                    break;
                case "3":
                    AjouterUnClient(connectionString);
                    break;
                case "4":
                    AfficherCommandeLivreeClient(connectionString);
                    break;
                case "5":
                    SupprimerUnClient(connectionString);
                    break;
                case "6":
                    ModifierUnClient(connectionString);
                    break;
                case "7":
                    AfficherStatutFidelite(connectionString);
                    break;
                case "8":
                    ModifierStatutFidelite(connectionString);
                    break;
                case "9":
                    ClientMemeAddresseFacturation(connectionString);
                    break;
                case "10":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }

            Console.WriteLine();
        }
    }
    static void GestionModuleProduit(string connectionString)
    {
        bool back = false;

        while (!back)
        {
            Console.WriteLine("=== Module Produit ===");
            Console.WriteLine("1. Afficher tous les produits");
            Console.WriteLine("2. Afficher les caractéristiques d'un produit");
            Console.WriteLine("3. Vérifier le stock de tous les produits");
            Console.WriteLine("4. Afficher le stock des produits et bouquet standard par magasin");
            Console.WriteLine("5. Mettre à jour un produit");
            Console.WriteLine("6. Ajouter un produit");
            Console.WriteLine("7. Supprimer un produit");
            Console.WriteLine("8. Afficher tous les bouquets standards");
            Console.WriteLine("9. Afficher tous les accessoires");
            Console.WriteLine("10. Ajouter un accessoire");
            Console.WriteLine("11. Supprimer un accessoire");
            Console.WriteLine("12. Modifier un accessoire");
            Console.WriteLine("13. Ajouter un bouquet standard");
            Console.WriteLine("14. Supprimer un bouquet standard");
            Console.WriteLine("15. modifier un bouquet standard");
            Console.WriteLine("16. Revenir en arrière");

            Console.Write("Choix : ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AfficherTousLesProduits(connectionString);
                    break;
                case "2":
                    AfficherCaracteristiquesProduit(connectionString);
                    break;
                case "3":
                    VerifierStockTousLesProduits(connectionString);
                    break;
                case "4":
                    AfficherLeStockDesMagasins(connectionString);
                    break;
                case "5":
                    MettreAJourUnProduit(connectionString);
                    break;
                case "6":
                    AjouterUnProduit(connectionString);
                    break;
                case "7":
                    SupprimerUnProduit(connectionString);
                    break;
                case "8":
                    AfficherTousLesBouquetsStandard(connectionString);
                    break;
                case "9":
                    AfficherTousLesAccessoires(connectionString);
                    break;
                case "10":
                    AjouterAccessoire(connectionString);
                    break;
                case "11":
                    SupprimerAccessoire(connectionString);
                    break;
                case "12":
                    ModifierAccessoire(connectionString);
                    break;
                case "13":
                    AjouterBouquetStandard(connectionString);
                    break;
                case "14":
                    SupprimerBouquetstandard(connectionString);
                    break;
                case "15":
                    ModifierBouquetStandard(connectionString);
                    break;
                case "16":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }

            Console.WriteLine();

        }
    }
    static void GestionModuleCommande(string connectionString)
    {
        bool back = false;

        while (!back)
        {
            Console.WriteLine("=== Module Commande ===");
            Console.WriteLine("1. Afficher toutes les commandes");
            Console.WriteLine("2. Afficher les détails d'une commande");
            Console.WriteLine("3. Afficher les commandes selon leurs états");
            Console.WriteLine("4. Afficher l'état des commandes par jour");
            Console.WriteLine("5. Ajouter une commande");
            Console.WriteLine("6. Supprimer une commande");
            Console.WriteLine("7. Modifier l'état d'une commande");
            Console.WriteLine("8. Revenir en arrière");

            Console.Write("Choix : ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AfficherToutesLesCommandes(connectionString);
                    break;
                case "2":
                    AfficherDetailleCommande(connectionString);
                    break;
                case "3":
                    AfficherDetailsCommandeSelonEtat(connectionString);
                    break;
                case "4":
                    AfficherEtatCommandeParJour(connectionString);
                    break;
                case "5":
                    AjouterUneCommande(connectionString);
                    break;
                case "6":
                    SupprimerUneCommande(connectionString);
                    break;
                case "7":
                    ModifierEtatCommande(connectionString); //On estime que si un client veut modifier toutes sa commande, on la supprime et repasse commande, plus simple pour éviter de surcharger en doublon, puis on cherche donc juste à modifier l'état.
                    break;
                case "8":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }

            Console.WriteLine();
        }
    }
    static void GestionModuleStatistiques(string connectionString)
    {
        bool back = false;

        while (!back)
        {
            Console.WriteLine("=== Module Statistiques ===");
            Console.WriteLine("1. Calcul du prix moyen d'une commande");
            Console.WriteLine("2. Meilleur client du mois");
            Console.WriteLine("3. Bouquet standard le plus populaire du mois");
            Console.WriteLine("4. Le Meilleur produit du mois");
            Console.WriteLine("5. Nombre de commande mensuel");
            Console.WriteLine("6. Nombre de commande annuel");
            Console.WriteLine("7. Chiffre d'affaire mensuel (requête avec union)");
            Console.WriteLine("8. Chiffre d'affaire annuel");
            Console.WriteLine("9. Revenir en arrière");

            Console.Write("Choix : ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CalculerPrixMoyenCommande(connectionString);
                    break;
                case "2":
                    MeilleurClientDuMois(connectionString);
                    break;
                case "3":
                    BouquetStandardLePlusPopulaire(connectionString);
                    break;
                case "4":
                    MeilleurProduitDuMois(connectionString);
                    break;
                case "5":
                    NombreCommandeMensuel(connectionString);
                    break;
                case "6":
                    NombreCommandeAnnuel(connectionString);
                    break;
                case "7":
                    ChiffreAffaireMensuel(connectionString);
                    break;
                case "8":
                    ChiffreAffaireAnnuel(connectionString);
                    break;
                case "9":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }

            Console.WriteLine();

        }
    }
    //Fin Terminal de Commande

    //Les exports pour différent format selon le cahier des charges (pour la table client avec condition) :
    private static readonly object syncLock = new object();
    static void ExporterEnXML(string connectionString)
    {
        try
        {
            lock (syncLock)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    DateTime lastMonthDate = DateTime.Now.AddMonths(-1);

                    string query = $@"SELECT c.*
                  FROM Client c
                  JOIN (
                      SELECT client_id
                      FROM Commande_Standard
                      WHERE date_commande >= '{lastMonthDate.ToString("yyyy-MM-dd")}'
                      GROUP BY client_id
                      HAVING COUNT(id_commande_standard) > 1
                      UNION
                      SELECT client_id
                      FROM Commande_Personnalisee
                      WHERE date_commande >= '{lastMonthDate.ToString("yyyy-MM-dd")}'
                      GROUP BY client_id
                      HAVING COUNT(id_commande_personnalisee) > 1
                  ) AS t ON c.id_client = t.client_id";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable("Clients");
                    dataAdapter.Fill(dataTable);

                    // Créer un objet XmlDocument pour stocker les données au format XML
                    XmlDocument xmlDocument = new XmlDocument();

                    // Convertir la DataTable en XML
                    using (XmlWriter xmlWriter = xmlDocument.CreateNavigator().AppendChild())
                    {
                        dataTable.TableName = "Client"; // Définir le nom de la DataTable
                        dataTable.WriteXml(xmlWriter);
                    }

                    // Enregistrer le fichier XML
                    string fileName = "clients_multicommandes.xml";
                    xmlDocument.Save(fileName);

                    Console.WriteLine($"Export XML des clients ayant commandé plusieurs fois au cours du dernier mois effectué avec succès. Fichier : {fileName}");

                    connection.Close();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Une erreur s'est produite lors de l'export XML : " + ex.Message);
        }
    }
    static void ExporterEnJSON(string connectionString)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Récupérer toutes les tables de la base de données
                DataTable tablesSchema = connection.GetSchema("Tables");

                foreach (DataRow table in tablesSchema.Rows)
                {
                    string tableName = table["TABLE_NAME"].ToString();

                    // Vérifier si la table contient des données clients
                    if (tableName.ToLower() == "client")
                    {
                        // Récupérer les données des clients n'ayant pas commandé depuis plus de 6 mois
                        MySqlCommand command = new MySqlCommand(@"SELECT * FROM Client WHERE id_client NOT IN (
                    SELECT DISTINCT client_id FROM Commande_Standard WHERE date_commande >= DATE_SUB(CURDATE(), INTERVAL 6 MONTH) + INTERVAL 1 DAY
                    UNION
                    SELECT DISTINCT client_id FROM Commande_Personnalisee WHERE date_commande >= DATE_SUB(CURDATE(), INTERVAL 6 MONTH) + INTERVAL 1 DAY
                )", connection);
                        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // Convertir la DataTable en JSON
                        string json = JsonConvert.SerializeObject(dataTable, Newtonsoft.Json.Formatting.Indented);

                        // Enregistrer le fichier JSON
                        string fileName = $"{tableName}.json";
                        System.IO.File.WriteAllText(fileName, json);

                        Console.WriteLine($"Export JSON des données clients n'ayant pas commandé depuis plus de 6 mois effectué avec succès. Fichier : {fileName}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Une erreur s'est produite lors de l'export JSON : " + ex.Message);
        }

    }
    //Fin des exports 



    //Méthodes pour les fonctionnalités spécifiques de chaque module : 


    //Début Module GestionClient
    static void AfficherTousLesClients(string connectionString)
    {
        // Code pour afficher tous les clients
        Console.WriteLine("Affichage de tous les clients");

        List<Projet_Fleur.Client> clients = Projet_Fleur.Client.GetClients(connectionString);

        Console.WriteLine("Liste de tous les clients :");

        foreach (Projet_Fleur.Client client in clients)
        {
            Console.WriteLine($"ID: {client.Id}");
            Console.WriteLine($"Nom: {client.Nom}");
            Console.WriteLine($"Prénom: {client.Prenom}");
            Console.WriteLine($"Téléphone: {client.Telephone}");
            Console.WriteLine($"Courriel: {client.Courriel}");
            Console.WriteLine($"Mot de passe: {client.MotDePasse}");
            Console.WriteLine($"Adresse de facturation: {client.AdresseFacturation}");
            Console.WriteLine($"Carte de crédit: {client.CarteCredit}");
            Console.WriteLine("------------------------");
        }

    }
    static void AfficherCaracteristiquesClient(string connectionString)
    {
        // Code pour afficher les caractéristiques d'un client
        Console.WriteLine("Affichage des caractéristiques d'un client");

        Console.Write("Entrez l'ID du client : ");
        int clientId = Convert.ToInt32(Console.ReadLine());

        Projet_Fleur.Client client = Projet_Fleur.Client.GetClientById(clientId, connectionString);

        if (client != null)
        {
            Console.WriteLine($"ID: {client.Id}");
            Console.WriteLine($"Nom: {client.Nom}");
            Console.WriteLine($"Prénom: {client.Prenom}");
            Console.WriteLine($"Téléphone: {client.Telephone}");
            Console.WriteLine($"Courriel: {client.Courriel}");
            Console.WriteLine($"Mot de passe: {client.MotDePasse}");
            Console.WriteLine($"Adresse de facturation: {client.AdresseFacturation}");
            Console.WriteLine($"Carte de crédit: {client.CarteCredit}");
        }
        else
        {
            Console.WriteLine("Aucun client trouvé avec cet ID.");
        }
    }
    static void AjouterUnClient(string connectionString)
    {
        // Code pour ajouter un nouveau client
        Console.WriteLine("Ajout d'un nouveau client");

        Console.Write("Nom : ");
        string nom = Console.ReadLine();

        Console.Write("Prénom : ");
        string prenom = Console.ReadLine();

        Console.Write("Téléphone : ");
        string telephone = Console.ReadLine();

        Console.Write("Courriel : ");
        string courriel = Console.ReadLine();

        Console.Write("Mot de passe : ");
        string motDePasse = Console.ReadLine();

        Console.Write("Adresse de facturation : ");
        string adresseFacturation = Console.ReadLine();

        Console.Write("Carte de crédit : ");
        string carteCredit = Console.ReadLine();

        // Récupérer le dernier ID de la table Client
        int dernierId = 0;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            MySqlCommand command = new MySqlCommand("SELECT MAX(id_client) FROM Client", connection);
            object result = command.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                dernierId = Convert.ToInt32(result);
            }
            connection.Close();
        }

        int nouvelId = dernierId + 1;

        Projet_Fleur.Client client = new Projet_Fleur.Client(nouvelId, nom, prenom, telephone, courriel, motDePasse, adresseFacturation, carteCredit);
        client.Ajouter(connectionString);


    }
    static void SupprimerUnClient(string connectionString)
    {
        // Code pour supprimer un client existant
        Console.WriteLine("Suppression d'un client");

        Console.Write("Entrez l'ID du client à supprimer : ");
        int clientId = Convert.ToInt32(Console.ReadLine());

        Projet_Fleur.Client client = Projet_Fleur.Client.GetClientById(clientId, connectionString);

        if (client != null)
        {

            client.Supprimer(connectionString);

        }
        else
        {
            Console.WriteLine("Aucun client trouvé avec cet ID.");
        }

    }
    static void ModifierUnClient(string connectionString)
    {
        // Code pour modifier les informations d'un client existant
        Console.WriteLine("Modification d'un client");

        Console.Write("Entrez l'ID du client à modifier : ");
        int clientId = Convert.ToInt32(Console.ReadLine());

        Projet_Fleur.Client client = Projet_Fleur.Client.GetClientById(clientId, connectionString);

        if (client != null)
        {
            Console.WriteLine("Entrez les nouvelles informations du client :");

            Console.Write("Nom : ");
            string nom = Console.ReadLine();

            Console.Write("Prénom : ");
            string prenom = Console.ReadLine();

            Console.Write("Téléphone : ");
            string telephone = Console.ReadLine();

            Console.Write("Courriel : ");
            string courriel = Console.ReadLine();

            Console.Write("Mot de passe : ");
            string motDePasse = Console.ReadLine();

            Console.Write("Adresse de facturation : ");
            string adresseFacturation = Console.ReadLine();

            Console.Write("Carte de crédit : ");
            string carteCredit = Console.ReadLine();

            client.Nom = nom;
            client.Prenom = prenom;
            client.Telephone = telephone;
            client.Courriel = courriel;
            client.MotDePasse = motDePasse;
            client.AdresseFacturation = adresseFacturation;
            client.CarteCredit = carteCredit;

            client.MettreAJour(connectionString);

            Console.WriteLine("Les informations du client ont été modifiées.");
        }
        else
        {
            Console.WriteLine("Aucun client trouvé avec cet ID.");
        }
    }
    static void AfficherCommandeLivreeClient(string connectionString)
    {
        // Code pour afficher les commandes livrées d'un client
        Console.WriteLine("Affichage des commandes livrées d'un client");

        Console.Write("Entrez l'ID du client : ");
        int clientId = Convert.ToInt32(Console.ReadLine());

        List<Commande_Standard> commandesStandard = Commande_Standard.GetCommandesLivreesParClient(connectionString, clientId);
        List<Commande_Personnalisee> commandesPersonnalisees = Commande_Personnalisee.GetCommandesLivreesParClient(connectionString, clientId);

        Console.WriteLine($"Commandes livrées du client {clientId} :");

        // Afficher les commandes standard livrées
        Console.WriteLine("Commandes standard :");
        foreach (Commande_Standard commande in commandesStandard)
        {
            if (commande.Etat == "CL")
            {
                Console.WriteLine($"ID: {commande.Id}");
                Console.WriteLine($"Date de commande: {commande.DateCommande}");
                Console.WriteLine($"Prix: {commande.MontantTotal}");
                Console.WriteLine("------------------------");
            }
        }

        // Afficher les commandes personnalisées livrées
        Console.WriteLine("Commandes personnalisées :");
        foreach (Commande_Personnalisee commande in commandesPersonnalisees)
        {
            if (commande.Etat == "CL")
            {
                Console.WriteLine($"ID: {commande.Id}");
                Console.WriteLine($"Date de commande: {commande.DateCommande}");
                Console.WriteLine($"Prix: {commande.MontantTotal}");
                Console.WriteLine("------------------------");
            }
        }
    }
    static void AfficherStatutFidelite(string connectionString)
    {

        List<Projet_Fleur.Client> clients = Projet_Fleur.Client.GetClients(connectionString);

        string statut = null;
        foreach (Projet_Fleur.Client client in clients)
        {
            statut = StatutFidelite.GetStatutFidelite(client.Id, connectionString);

            if (!string.IsNullOrEmpty(statut))
            {
                Console.WriteLine($"Client ID: {client.Id}, Nom: {client.Nom}, Prénom: {client.Prenom}");
                Console.WriteLine($"Statut de fidélité: {statut}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Le client ID: {client.Id} n'a pas de statut de fidélité enregistré.");
                Console.WriteLine();
            }
        }


    }
    static void ModifierStatutFidelite(string connectionString)
    {
        List<Projet_Fleur.Client> clients = Projet_Fleur.Client.GetClients(connectionString);
        List<Projet_Fleur.Client> clientsModifies = new List<Projet_Fleur.Client>();

        foreach (Projet_Fleur.Client client in clients)
        {
            // Vérifier et mettre à jour le statut de fidélité
            if (StatutFidelite.ChangerStatut(client.Id, connectionString))
            {
                clientsModifies.Add(client);
            }
        }

        Console.WriteLine("Clients dont le statut de fidélité a été modifié :");

        foreach (Projet_Fleur.Client clientModifie in clientsModifies)
        {
            // Récupérer le nouveau statut de fidélité du client
            string nouveauStatut = StatutFidelite.GetStatutFidelite(clientModifie.Id, connectionString);

            Console.WriteLine($"Client ID: {clientModifie.Id}, Nom: {clientModifie.Nom}, Prénom: {clientModifie.Prenom}, Nouveau Statut: {nouveauStatut}");
        }

        Console.WriteLine("Vérification et mise à jour des statuts de fidélité terminées.");

    }
    static void ClientMemeAddresseFacturation(string connectionString)
    { 
            string query = "SELECT DISTINCT c1.nom, c1.prenom, c1.adresse_facturation FROM Client c1 INNER JOIN Client c2 ON c1.adresse_facturation = c2.adresse_facturation AND c1.id_client <> c2.id_client";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nom = reader.GetString(0);
                        string prenom = reader.GetString(1);
                        string adresse = reader.GetString(2);

                        Console.WriteLine("{0} {1} - Adresse de facturation : {2}", prenom, nom, adresse);
                    }
                }
            }
        }
    
    //Fin du Module GestionClient

    //Début du module GestionProduit
    static void AfficherTousLesProduits(string connectionString)
    {
        Console.WriteLine("Affichage de tous les produits");

        List<Produit> produits = Produit.GetProduits(connectionString);


        if (produits.Count > 0)
        {
            foreach (Produit produit in produits)
            {
                Console.WriteLine($"ID : {produit.Id_Produit}");
                Console.WriteLine($"Nom : {produit.Name}");
                Console.WriteLine($"Description : {produit.Description}");
                Console.WriteLine($"Prix : {produit.Prix:C}");
                Console.WriteLine($"Quantite disponible : {produit.QttDisponible}");
                Console.WriteLine($"Disponibilité : {produit.Disponibilite}");


            }
        }
        else
        {
            Console.WriteLine("Aucun produit trouvé dans la base de données.");
        }
    }
    static void AfficherTousLesBouquetsStandard(string connectionString)
    {
        Console.WriteLine("Affichage de tous les Bouquets Standard");

        List<Bouquet_Standard> bouquet_standards = Bouquet_Standard.GetBouquetStandards(connectionString);


        if (bouquet_standards.Count > 0)
        {
            foreach (Bouquet_Standard bouquet_standard in bouquet_standards)
            {
                Console.WriteLine($"ID : {bouquet_standard.Id_Bouquet_Standard}");
                Console.WriteLine($"Nom : {bouquet_standard.Name}");
                Console.WriteLine($"Description : {bouquet_standard.Description}");
                Console.WriteLine($"Prix : {bouquet_standard.Prix:C}");
                Console.WriteLine($"Catégorie : {bouquet_standard.Categorie}");

            }
        }
        else
        {
            Console.WriteLine("Aucun bouquet standard trouvé dans la base de données.");
        }
    }
    static void AfficherTousLesAccessoires(string connectionString)
    {
        Console.WriteLine("Affichage de tous les Accessoires");

        List<Accessoire> accessoires = Accessoire.GetAccessoire(connectionString);


        if (accessoires.Count > 0)
        {
            foreach (Accessoire accessoire in accessoires)
            {
                Console.WriteLine($"ID : {accessoire.Id_Accessoire}");
                Console.WriteLine($"Nom : {accessoire.Name}");
                Console.WriteLine($"Description : {accessoire.Description_Accessoire}");
                Console.WriteLine($"Prix : {accessoire.Prix:C}");
                Console.WriteLine($"Quantité : {accessoire.Quantite}");

            }
        }
        else
        {
            Console.WriteLine("Aucun accessoire trouvé dans la base de données.");
        }
    }
    static void AfficherCaracteristiquesProduit(string connectionString)
    {
        Console.WriteLine("Affichage des caractéristiques d'un produit");
        Console.Write("Entrez l'ID du produit : ");
        int produitId = Convert.ToInt32(Console.ReadLine());

        Produit produit = Produit.GetProduitById(produitId, connectionString);

        if (produit != null)
        {
            Console.WriteLine($"Caractéristiques du produit (ID: {produit.Id_Produit}) :");
            Console.WriteLine($"Nom : {produit.Name}");
            Console.WriteLine($"Description : {produit.Description}");
            Console.WriteLine($"Prix : {produit.Prix:C}");
            Console.WriteLine($"Quantite disponible : {produit.QttDisponible}");
            Console.WriteLine($"Disponibilité : {produit.Disponibilite}");

        }
        else
        {
            Console.WriteLine("Aucun produit trouvé avec cet ID.");
        }
    }
    static void VerifierStockTousLesProduits(string connectionString)
    {
        Console.WriteLine("Vérification du stock");
        
        List<Produit> produits = Produit.GetProduits(connectionString);
        
        foreach (Produit produit in produits)
        {
            if (produit.QttDisponible <= Produit.Seuil(connectionString, produit.Id_Produit))
            {
                Console.WriteLine($"Le produit {produit.Name} n'est pas disponible en stock.");

            }
        }
    }
    static void MettreAJourUnProduit(string connectionString)
    {
        // Code pour modifier les informations d'un produit existant
        Console.WriteLine("Modification d'un produit");

        Console.Write("Entrez l'ID du produit à modifier : ");
        int produitID = Convert.ToInt32(Console.ReadLine());

        Produit produit = Produit.GetProduitById(produitID, connectionString);

        if (produit != null)
        {
            Console.WriteLine("Entrez les nouvelles informations du produit :");

            Console.Write("Nom : ");
            string nom = Console.ReadLine();

            Console.Write("Description : ");
            string description = Console.ReadLine();

            Console.Write("Prix: ");
            decimal prix = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Quantité Disponible : ");
            int qttdisponible = Convert.ToInt32(Console.ReadLine());

            Console.Write("Disponibilité : ");
            string disponibilite = Console.ReadLine();


            produit.Name = nom;
            produit.Description = description;
            produit.Prix = prix;
            produit.QttDisponible = qttdisponible;
            produit.Disponibilite = disponibilite;


            produit.MettreAJour(connectionString);

            Console.WriteLine("Les informations du produit ont été modifiées.");
        }
        else
        {
            Console.WriteLine("Aucun produit trouvé avec cet ID.");
        }
    }
    static void AfficherLeStockDesMagasins(string connectionString)
    {
        Console.WriteLine("Affichage du stock dans les magasins");
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            SELECT m.nom AS nom_magasin, 
                   GROUP_CONCAT(DISTINCT bs.nom SEPARATOR ', ') AS bouquet_standard_present, 
                   GROUP_CONCAT(DISTINCT CONCAT(p.nom, ' (', p.quantitéDisponible, ')') SEPARATOR ', ') AS produits_present,
                   GROUP_CONCAT(DISTINCT CONCAT(a.nom, ' (', a.quantité, ')') SEPARATOR ', ') AS accessoires_present
            FROM Magasin m
            LEFT JOIN Bouquet_Standard bs ON m.bouquet_standard_id = bs.id_bouquet_standard
            LEFT JOIN Produit p ON m.produit_id = p.id_produit
            LEFT JOIN Magasin m2 ON m.nom = m2.nom 
                                    AND (bs.id_bouquet_standard IS NULL OR bs.id_bouquet_standard = m2.bouquet_standard_id) 
                                    AND (p.id_produit IS NULL OR p.id_produit = m2.produit_id)
            LEFT JOIN Accessoire a ON m.accessoire_id = a.id_accessoire
            WHERE (p.quantitéDisponible > 0 OR a.quantité > 0) 
                  AND m2.id_magasin IS NOT NULL
            GROUP BY m.id_magasin, m.nom";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string nomMagasin = reader.GetString("nom_magasin");
                    string bouquetStandardPresent = reader.IsDBNull(reader.GetOrdinal("bouquet_standard_present")) ? "" : reader.GetString("bouquet_standard_present");
                    string produitsPresent = reader.IsDBNull(reader.GetOrdinal("produits_present")) ? "" : reader.GetString("produits_present");
                    string accessoiresPresent = reader.IsDBNull(reader.GetOrdinal("accessoires_present")) ? "" : reader.GetString("accessoires_present");

                    string magasinInfo = $"Magasin : {nomMagasin}\n" +
                                         $"Bouquet Standard présent : {bouquetStandardPresent}\n" +
                                         $"Produits présents : {produitsPresent}\n" +
                                         $"Accessoires présents : {accessoiresPresent}\n";

                    Console.WriteLine(magasinInfo);
                    Console.WriteLine();
                }

                reader.Close();
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Une erreur s'est produite lors de l'exécution de la requête : " + ex.Message);
        }


    }
    static void AjouterUnProduit(string connectionString)
    {
        Console.WriteLine("Ajout d'un nouveau produit");

        Console.Write("Nom : ");
        string nom = Console.ReadLine();

        Console.Write("Prix : ");
        decimal prix = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Description : ");
        string description = Console.ReadLine();

        Console.Write("Quantité: ");
        int quantité = Convert.ToInt32(Console.ReadLine());

        Console.Write("Disponibilité : ");
        string disponibilite = Console.ReadLine();

        // Récupérer le dernier ID de la table Produit
        int dernierId = 0;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            MySqlCommand command = new MySqlCommand("SELECT MAX(id_produit) FROM Produit", connection);
            object result = command.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                dernierId = Convert.ToInt32(result);
            }

            connection.Close();
        }

        int nouvelId = dernierId + 1;

        Produit produit = new Produit(nouvelId, nom, description, prix, quantité, disponibilite);
        produit.Ajouter(connectionString);


    }
    static void SupprimerUnProduit(string connectionString)
    {
        {

            Console.WriteLine("Suppression d'un produit");

            Console.Write("Entrez l'ID du produit à supprimer : ");
            int produitID = Convert.ToInt32(Console.ReadLine());

            Produit produit = Produit.GetProduitById(produitID, connectionString);

            if (produit != null)
            {
                produit.Supprimer(connectionString);
            }
            else
            {
                Console.WriteLine("Aucun produit trouvé avec cet ID.");
            }
        }
    }
    static void AjouterBouquetStandard(string connectionString) 
    {
        Console.WriteLine("Ajouter un nouveau bouquet standard :");

        Console.Write("Nom : ");
        string nom = Console.ReadLine();

        Console.Write("Description : ");
        string description = Console.ReadLine();

        Console.Write("Prix : ");
        decimal prix = decimal.Parse(Console.ReadLine());

        Console.Write("Catégorie: ");
        string catégorie = Console.ReadLine();

        Console.Write("Id du produit : ");
        int produitId = Convert.ToInt32(Console.ReadLine());    


        int dernierId = 0;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            MySqlCommand command = new MySqlCommand("SELECT MAX(id_bouquet_standard) FROM Bouquet_Standard", connection);
            object result = command.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                dernierId = Convert.ToInt32(result);
            }
            connection.Close();
        }

        int nouvelId = dernierId + 1;
        Bouquet_Standard bouquet = new Bouquet_Standard(nouvelId, nom, description, prix, catégorie, produitId);
        bouquet.Ajouter(connectionString);
    }
    static void SupprimerBouquetstandard(string connectionString) 
    {
        Console.Write("Entrez l'ID du bouquet standard à supprimer : ");
        int id = Convert.ToInt32(Console.ReadLine());

        // Appel de la méthode Supprimer avec l'ID en paramètre
        Bouquet_Standard.Supprimer(connectionString, id);
    }
    static void ModifierBouquetStandard(string connectionString) 
    {
        Console.WriteLine("Modification d'un bouquet standard");

        Console.Write("Entrez l'ID du bouquet standard à modifier : ");
        int bouquetId = Convert.ToInt32(Console.ReadLine());


        List<Bouquet_Standard> bouquets = Bouquet_Standard.GetBouquetStandards(connectionString);

        foreach (Bouquet_Standard bouquet in bouquets)
        {
            if (bouquet.Id_Bouquet_Standard == bouquetId)
            {
                Console.WriteLine("Entrez les nouvelles informations du produit :");


                Console.Write("Nom : ");
                string nom = Console.ReadLine();

                Console.Write("Description : ");
                string description = Console.ReadLine();

                Console.Write("Prix : ");
                decimal prix = decimal.Parse(Console.ReadLine());

                Console.Write("Catégorie: ");
                string catégorie = Console.ReadLine();

                Console.Write("Id du produit : ");
                int produitId = Convert.ToInt32(Console.ReadLine());

                bouquet.Name = nom;
                bouquet.Description = description;
                bouquet.Prix = prix;
                bouquet.Categorie = catégorie;
                bouquet.Produit_ID = produitId;


                bouquet.MettreAJour(connectionString);

                Console.WriteLine("Les informations du bouquet standard ont été modifiées.");
            }

        }
    }
    static void AjouterAccessoire(string connectionString) 
    {
        Console.WriteLine("Ajouter un nouvel accessoire :");

        Console.Write("Nom : ");
        string nom = Console.ReadLine();

        Console.Write("Description : ");
        string description = Console.ReadLine();

        Console.Write("Prix : ");
        decimal prix = decimal.Parse(Console.ReadLine());

        Console.Write("Quantité : ");
        int quantite = int.Parse(Console.ReadLine());
        int dernierId = 0;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            MySqlCommand command = new MySqlCommand("SELECT MAX(id_accessoire) FROM Accessoire", connection);
            object result = command.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                dernierId = Convert.ToInt32(result);
            }
            connection.Close();
        }

        int nouvelId = dernierId + 1;
        Accessoire accessoire = new Accessoire(nouvelId, nom, description, quantite, prix);
        accessoire.Ajouter(connectionString);

    }
    static void SupprimerAccessoire(string connectionString) 
    {
        Console.Write("Entrez l'ID de l'accessoire à supprimer : ");
        int id = Convert.ToInt32(Console.ReadLine());

        // Appel de la méthode Supprimer avec l'ID en paramètre
        Accessoire.Supprimer(connectionString, id);
    }
    static void ModifierAccessoire(string connectionString) 
    {
       
        Console.WriteLine("Modification d'un accessoire");

        Console.Write("Entrez l'ID de l'accessoire à modifier : ");
        int accessoireId = Convert.ToInt32(Console.ReadLine());


        List<Accessoire> accessoires = Accessoire.GetAccessoire(connectionString);  
        
        foreach(Accessoire accessoire in accessoires)
        {
            if(accessoire.Id_Accessoire == accessoireId)
            {
                Console.WriteLine("Entrez les nouvelles informations de l'accessoire :");

                Console.Write("Nom : ");
                string nom = Console.ReadLine();

                Console.Write("Description : ");
                string description = Console.ReadLine();

                Console.Write("Prix: ");
                decimal prix = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Quantité Disponible : ");
                int qttdisponible = Convert.ToInt32(Console.ReadLine());

                accessoire.Name = nom;
                accessoire.Description_Accessoire = description;
                accessoire.Prix = prix;
                accessoire.Quantite = qttdisponible;


                accessoire.MettreAJour(connectionString);

                Console.WriteLine("Les informations de l'accessoire ont été modifiées.");
            }
            
        }  
    }

    //Fin du module GestionProduit

    //Début du module GestionCommande
    static void AfficherToutesLesCommandes(string connectionString)
    {
        Console.WriteLine("Affichage de toutes les commandes");

        List<Commande_Standard> commandeStandard = Commande_Standard.GetCommandesStandard(connectionString);
        List<Commande_Personnalisee> commandePersonnalisee = Commande_Personnalisee.GetCommandesPersonnalisee(connectionString);

        if (commandeStandard.Count > 0)
        {
            Console.WriteLine("--- Commande Standard ---");

            foreach (Commande_Standard commande in commandeStandard)
            {

                commande.Afficher();

            }
        }
        else
        {
            Console.WriteLine("Aucune commande standard trouvé dans la base de données.");
        }

        if (commandePersonnalisee.Count > 0)
        {
            Console.WriteLine("--- Commande Personnalisée ---");
            foreach (Commande_Personnalisee commandes in commandePersonnalisee)
            {

                commandes.AfficherDetails();

            }
        }
        else
        {
            Console.WriteLine("Aucune commande personnalisée trouvé dans la base de données.");
        }

    }
    static void AfficherDetailleCommande(string connectionString)
    {
        Console.Write("Commande Standard (S) ou Commande Personnalisee (P)  : ");
        string statutcommande = Console.ReadLine();
        Console.WriteLine();

        Console.Write("Veuillez saisir l'id de la commande : ");
        int id = Convert.ToInt32(Console.ReadLine());


        if (statutcommande == "S")
        {
            Commande_Standard.AfficherCommandesStandardId(connectionString, id);
        }
        else if (statutcommande == "P")
        {
            Commande_Personnalisee.GetCommandesPersonnaliseesParId(connectionString, id);
        }
        else { Console.WriteLine("Vérifier la saisie"); }


    }
    static void AfficherDetailsCommandeSelonEtat(string connectionString)
    {
        List<Commande_Standard> commandeStandard = Commande_Standard.GetCommandesStandard(connectionString);
        List<Commande_Personnalisee> commandePersonnalisee = Commande_Personnalisee.GetCommandesPersonnalisee(connectionString);

        Console.WriteLine("Veuillez saisir le statut des commandes à afficher");
        Console.WriteLine("Commande nécessitant une vérification du stock (VINV)");
        Console.WriteLine("Commande complète (CC)");
        Console.WriteLine("Commande personnalisée à vérifier (CPAV)");
        Console.WriteLine("Commande à livrer (CAL)");
        Console.WriteLine("Commande livrée (CL)");
        Console.Write("Choix : ");
        string etat = Console.ReadLine();

        if (etat == "CPAV ")
        {
            foreach (Commande_Personnalisee commande in commandePersonnalisee)
            {
                if (commande.Etat == etat)
                {
                    commande.AfficherDetails();
                }
            }
        }
        else if (etat == "VINV" || etat == "CC" || etat == "CAL" || etat == "CL")
        {
            Console.WriteLine("--- Commandes Personnaliséees ---");
            foreach (Commande_Personnalisee commande in commandePersonnalisee)
            {
                if (commande.Etat == etat)
                {
                    commande.AfficherDetails();
                    Console.WriteLine("-------------------------");

                }
            }
            Console.WriteLine("--- Commande Standards ---");
            foreach (Commande_Standard commande in commandeStandard)
            {
                if (commande.Etat == etat)
                {
                    commande.Afficher();
                    Console.WriteLine("-------------------------");

                }
            }
        }
        else { Console.WriteLine("Veuillez vérifier la saisie"); }
    }
    static void AfficherEtatCommandeParJour(string connectionString)
    {

        List<Commande_Standard> commandeStandard = Commande_Standard.GetCommandesStandard(connectionString);
        List<Commande_Personnalisee> commandePersonnalisee = Commande_Personnalisee.GetCommandesPersonnalisee(connectionString);

        Console.WriteLine("Veuillez saisir la date : ");
        DateTime date = Convert.ToDateTime(Console.ReadLine());

        Console.WriteLine("--- Commande Personnalisée ---");

        foreach (Commande_Personnalisee commande in commandePersonnalisee)
        {
            if (commande.DateLivraison <= date)
            {
                Console.WriteLine($"ID : {commande.ClientId}");
                Console.WriteLine($"Adresse livraison : {commande.AdresseLivraison}");
                Console.WriteLine($"État : {commande.Etat}");
                Console.WriteLine("-------------------------");

            }


        }

        Console.WriteLine("--- Commande Standards ---");

        foreach (Commande_Standard commande in commandeStandard)
        {
            if (commande.DateLivraison <= date)
            {

                Console.WriteLine($"ID : {commande.ClientId}");
                Console.WriteLine($"Adresse livraison : {commande.AdresseLivraison}");
                Console.WriteLine($"État : {commande.Etat}");
                Console.WriteLine("-------------------------");

            }

        }
    }
    static void AjouterUneCommande(string connectionString)
    {
        Console.WriteLine("Veuillez choisir le type de commande souhaité : standard (S) ou personnalisée(P)");
        string type_commande = Console.ReadLine();

        if(type_commande == "S")
        {
            Console.WriteLine("Ajout d'une commande Standard");

            Console.Write("ID du bouquet standard : ");
            int bouquetStandardId = int.Parse(Console.ReadLine());

            Console.Write("Date de la commande (AAAA-MM-JJ) : ");
            DateTime dateCommande = DateTime.Parse(Console.ReadLine());

            Console.Write("Date de livraison (AAAA-MM-JJ) : ");
            DateTime dateLivraison = DateTime.Parse(Console.ReadLine());

            Console.Write("Adresse de livraison : ");
            string adresseLivraison = Console.ReadLine();

            Console.Write("Message d'accompagnement : ");
            string messageAccompagnement = Console.ReadLine();

            Console.Write("Montant total : ");
            decimal montantTotal = Convert.ToDecimal(Console.ReadLine());
            
            Console.Write("Etat (VINV, CC, CPAV, CAL, CL): ");
            string etat = Console.ReadLine();

            Console.Write("ID du client : ");
            int clientId = int.Parse(Console.ReadLine());

            Console.Write("ID du magasin : ");
            int magasinId = int.Parse(Console.ReadLine());

            int dernierId = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT MAX(id_commande_standard) FROM Commande_Standard", connection);
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    dernierId = Convert.ToInt32(result);
                }
                connection.Close();
            }

            int nouvelId = dernierId + 1;

            Commande_Standard commande_Standard = new Commande_Standard(nouvelId, bouquetStandardId, dateCommande, dateLivraison, adresseLivraison, messageAccompagnement, montantTotal, etat, clientId, magasinId);
            commande_Standard.Ajouter(connectionString);
        }
        else if (type_commande == "P")
        {
            Console.WriteLine("Ajout d'une commande Personnalisée");
            
            Console.WriteLine("Saisissez l'identifiant du client :");
            int clientId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Saisissez la date de commande (au format yyyy-MM-dd) :");
            DateTime dateCommande = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Saisissez la date de livraison (au format yyyy-MM-dd) :");
            DateTime dateLivraison = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Saisissez l'adresse de livraison :");
            string adresseLivraison = Console.ReadLine();

            Console.WriteLine("Saisissez le message d'accompagnement :");
            string messageAccompagnement = Console.ReadLine();

            Console.WriteLine("Saisissez le montant total :");
            decimal montantTotal = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Etat (CC, CPAV, CAL, CL): ");
            string etat = Console.ReadLine();

            Console.WriteLine("Saisissez l'identifiant du magasin :");
            int magasinId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Saisissez l'identifiant du produit :");
            int produitId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Saisissez l'identifiant de l'accessoire :");
            int accessoireId = Convert.ToInt32(Console.ReadLine());

            int dernierId = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT MAX(id_commande_personnalisee) FROM Commande_Personnalisee", connection);
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    dernierId = Convert.ToInt32(result);
                }
                connection.Close();
            }

            int nouvelId = dernierId + 1;

            Commande_Personnalisee commande = new Commande_Personnalisee(nouvelId, clientId, dateCommande, dateLivraison, adresseLivraison, messageAccompagnement, montantTotal, etat, magasinId, produitId, accessoireId);
            commande.Ajouter(connectionString);
        }
        else
        {
            Console.WriteLine("Veuillez vérifier la saisie");
        }
        
    }
    static void SupprimerUneCommande(string connectionString)
    {
        Console.WriteLine("Veuillez choisir le type de commande souhaité : standard (S) ou personnalisée(P)");
        string type_commande = Console.ReadLine();

        if(type_commande == "S")
        {
            Console.WriteLine("Suppression d'une commande standard : ");
            int commande_standard_id = Convert.ToInt32(Console.ReadLine());

            List<Commande_Standard> commandes = Commande_Standard.GetCommandesStandard(connectionString);
            //On pourrait implémenter la recherche par Id ici, cette fonctionnalité étant ajouté à la fin on laisse une liste ici
            foreach (Commande_Standard commande in commandes)
            {
                if(commande.Id == commande_standard_id)
                {
                    commande.Supprimer(connectionString); break;
                } 
            }
        }
        else if (type_commande == "P")
        {
            Console.WriteLine("Suppression d'une commande personnalisee");
            int commande_personnalisee_id = Convert.ToInt32(Console.ReadLine());

            List<Commande_Personnalisee> commandes = Commande_Personnalisee.GetCommandesPersonnalisee(connectionString);
            //Même commentaire que supra
            foreach(Commande_Personnalisee commande in commandes)
            {
                if(commande.Id == commande_personnalisee_id)
                {
                    commande.Supprimer(connectionString);
                }
            }   
        }
        else
        {
            Console.WriteLine("Veuillez vérifier la saisie");
        }
    }
    static void ModifierEtatCommande(string connectionString)
    {
        Console.WriteLine("Veuillez choisir le type de commande souhaité : standard (S) ou personnalisée(P)");
        string type_commande = Console.ReadLine();

        if (type_commande == "S")
        {
            Console.WriteLine("Veuillez saisir l'id de la  commande standard : ");
            int commande_standard_id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Veuillez saisir le nouvel etat (VINV, CAL, CC, CL)");
            string etat = Console.ReadLine();
                
           Commande_Standard.ModifierEtat(connectionString, etat, commande_standard_id);
               
             
        }
        else if (type_commande == "P")
        {
            Console.WriteLine("Modifier l'état d'une commande personnalisee");
            int commande_personnalisee_id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Veuillez saisir le nouvel etat (VINV, CAL, CC, CL)");
            string etat = Console.ReadLine();

         
           Commande_Personnalisee.ModifierEtat(connectionString, etat, commande_personnalisee_id);
               
        }
        else
        {
            Console.WriteLine("Vérifier la saisie");
        }
    }
    //Fin du module GestionCommande

    //Début du module  GestionStatistique
    static void CalculerPrixMoyenCommande(string connectionString)
    {
        Console.Write("Prix moyen d'une commande (standard ou personnalisée) : ");
        decimal prixMoyen = Statistiques.CalculerPrixMoyenCommande(connectionString);
        Console.WriteLine(prixMoyen.ToString() + '€');
    }
    static void MeilleurClientDuMois(string connectionString)
    {
        Console.WriteLine("Veuillez saisir le mois et l'année : ");
        Console.Write("Mois en chiffre : ");
        int mois = Convert.ToInt32(Console.ReadLine());
        Console.Write("L'année en chiffre : ");
        int annee = Convert.ToInt32(Console.ReadLine());

        int meilleurclient = Statistiques.GetMeilleurClientDuMois(mois, annee, connectionString);

        Projet_Fleur.Client client = Projet_Fleur.Client.GetClientById(meilleurclient, connectionString);

        if (client != null)
        {
            Console.WriteLine($"ID: {client.Id}");
            Console.WriteLine($"Nom: {client.Nom}");
            Console.WriteLine($"Prénom: {client.Prenom}");
        }
        else
        {
            Console.WriteLine("Aucun client trouvé avec cet ID.");
        }

    }
    static void BouquetStandardLePlusPopulaire(string connectionString)
    {
        Console.WriteLine("Veuillez saisir le mois et l'année : ");
        Console.Write("Mois en chiffre : ");
        int mois = Convert.ToInt32(Console.ReadLine());
        Console.Write("L'année en chiffre : ");
        int annee = Convert.ToInt32(Console.ReadLine());

        string meilleurbouquet = Statistiques.GetMeilleurBouquetStandardDuMois(mois, annee, connectionString);

        Console.WriteLine(meilleurbouquet);

    }
    static void MeilleurProduitDuMois(string connectionString)
    {
        Console.WriteLine("Veuillez saisir le mois et l'année : ");
        Console.Write("Mois en chiffre : ");
        int mois = Convert.ToInt32(Console.ReadLine());
        Console.Write("L'année en chiffre : ");
        int annee = Convert.ToInt32(Console.ReadLine());

        string meilleurproduit = Statistiques.GetMeilleurProduitDuMois(mois, annee, connectionString);

        Console.WriteLine(meilleurproduit);

    }
    static void NombreCommandeMensuel(string connectionString)
    {
        Console.WriteLine("Veuillez saisir le mois et l'année : ");
        Console.Write("Mois en chiffre : ");
        int mois = Convert.ToInt32(Console.ReadLine());
        Console.Write("L'année en chiffre : ");
        int annee = Convert.ToInt32(Console.ReadLine());

        int nbCommande = Statistiques.GetNombreCommandesMensuelles(mois, annee, connectionString);
        Console.Write("Pour le mois et l'année choisi il y a eu : ");
        Console.WriteLine(nbCommande + " commande(s)");

    }
    static void NombreCommandeAnnuel(string connectionString)
    {
        Console.Write("Veuillez saisir l'année en chiffre : ");
        int annee = Convert.ToInt32(Console.ReadLine());

        int nbCommande = Statistiques.GetNombreCommandesAnnuelles(annee, connectionString);

        Console.Write("Pour l'année choisi il y a eu : ");
        Console.WriteLine(nbCommande + " commande(s)");
    }
    static void ChiffreAffaireMensuel(string connectionString)
    {
        Console.WriteLine("Veuillez saisir le mois et l'année : ");
        Console.Write("Mois en chiffre : ");
        int mois = Convert.ToInt32(Console.ReadLine());
        Console.Write("L'année en chiffre : ");
        int annee = Convert.ToInt32(Console.ReadLine());

        decimal ca = Statistiques.GetChiffreAffairesMensuel(mois, annee, connectionString);

        Console.WriteLine("Pour le mois et l'année choisi il y a eu une recette de  : " + ca + "€");
    }
    static void ChiffreAffaireAnnuel(string connectionString)
    {
        Console.Write("Veuillez saisir l'année en chiffre : ");
        int annee = Convert.ToInt32(Console.ReadLine());

        decimal ca = Statistiques.GetChiffreAffairesAnnuel(annee, connectionString);
        Console.WriteLine("Pour l'année choisi il y a eu une recette de  : " + ca + "€");
    }
    //Fin du module GestionStatistique
}
            
        









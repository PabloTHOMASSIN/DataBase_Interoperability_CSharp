
DROP SCHEMA IF EXISTS fleur;
CREATE DATABASE fleur;

USE fleur;

-- Table Client
CREATE TABLE Client (
  id_client INT PRIMARY KEY,
  nom VARCHAR(255) NOT NULL,
  prenom VARCHAR(255) NOT NULL,
  telephone VARCHAR(20) NOT NULL,
  courriel VARCHAR(255) NOT NULL,
  mot_de_passe VARCHAR(255) NOT NULL,
  adresse_facturation VARCHAR(255) NOT NULL,
  carte_credit VARCHAR(16) NOT NULL
);

-- Table Statut_Fidelite
CREATE TABLE Statut_Fidelite ( 
	id_fidelite INT PRIMARY KEY,
    statut ENUM("Or", "Bronze"),
    datedebut DATE NOT NULL,
    datefin DATE NOT NULL,
    client_id INT NOT NULL,
    FOREIGN KEY (client_id) REFERENCES Client(id_client)
);
-- Table Produit
CREATE TABLE Produit (
  id_produit INT PRIMARY KEY,
  nom VARCHAR(255) NOT NULL,
  description TEXT,
  prix DECIMAL(10, 2) NOT NULL,
  quantitéDisponible INT NOT NULL,
  disponibilité VARCHAR(255) NOT NULL
);
-- Accessoire
CREATE TABLE Accessoire(
	id_accessoire INT PRIMARY KEY,
	nom VARCHAR(255) NOT NULL,
	description_accessoire TEXT,
    quantité INT NOT NULL,
    prix DECIMAL NOT NULL
);

-- Table Bouquet_Standard
CREATE TABLE Bouquet_Standard (
  id_bouquet_standard INT PRIMARY KEY,
  nom VARCHAR(255) NOT NULL,
  description TEXT,
  prix DECIMAL(10, 2) NOT NULL,
  catégorie TEXT,
  produit_id INT,
  FOREIGN KEY (produit_id) REFERENCES Produit(id_produit)
);

-- Table Magasin
CREATE TABLE Magasin (
  id_magasin INT PRIMARY KEY,
  nom VARCHAR(255) NOT NULL,
  adresse VARCHAR(255) NOT NULL,
  bouquet_standard_id INT NOT NULL,
  produit_id INT NOT NULL,
  accessoire_id INT NOT NULL,
  FOREIGN KEY (bouquet_standard_id) REFERENCES Bouquet_Standard(id_bouquet_standard),
  FOREIGN KEY (produit_id) REFERENCES Produit(id_produit),
  FOREIGN KEY(accessoire_id) REFERENCES Accessoire(id_accessoire)
);

-- Table Commande_Standard
CREATE TABLE Commande_Standard (
  id_commande_standard INT PRIMARY KEY,
  bouquet_standard_id INT NOT NULL,
  date_commande DATE NOT NULL,
  date_livraison DATE NOT NULL,
  adresse_livraison VARCHAR(255) NOT NULL,
  message_accompagnement VARCHAR(255),
  montant_total DECIMAL(10, 2) NOT NULL,
  etat ENUM('VINV', 'CC', 'CAL','CL') NOT NULL,
  client_id INT NOT NULL,
  magasin_id INT NOT NULL,
  FOREIGN KEY (client_id) REFERENCES Client(id_client),
  FOREIGN KEY (magasin_id) REFERENCES Magasin(id_magasin),
  FOREIGN KEY (bouquet_standard_id) REFERENCES Bouquet_Standard(id_bouquet_standard)
);

-- Table Commande_Personnalisee
CREATE TABLE Commande_Personnalisee (
  id_commande_personnalisee INT PRIMARY KEY,
  date_commande DATE NOT NULL,
  date_livraison DATE NOT NULL,
  adresse_livraison VARCHAR(255) NOT NULL,
  message_accompagnement VARCHAR(255),
  montant_total DECIMAL(10, 2) NOT NULL,
  etat ENUM('VINV', 'CC', 'CPAV','CAL','CL') NOT NULL,
  client_id INT NOT NULL,
  magasin_id INT NOT NULL,
  produit_id INT NOT NULL,
  accessoire_id INT NOT NULL,
  FOREIGN KEY (client_id) REFERENCES Client(id_client),
  FOREIGN KEY (magasin_id) REFERENCES Magasin(id_magasin),
  FOREIGN KEY (produit_id) REFERENCES Produit(id_produit),
  FOREIGN KEY (accessoire_id) REFERENCES Accessoire(id_accessoire)
);


-- Table Détails_Commande
CREATE TABLE Details_Commande (
id_details_commande INT PRIMARY KEY,
commande_type ENUM('STANDARD', 'PERSONNALISÉE') NOT NULL,
commande_id INT NOT NULL,
produit_id INT NOT NULL,
quantite INT NOT NULL,
FOREIGN KEY (commande_id) REFERENCES Commande_Standard(id_commande_standard) ON DELETE CASCADE,
FOREIGN KEY (commande_id) REFERENCES Commande_Personnalisee(id_commande_personnalisee) ON DELETE CASCADE,
FOREIGN KEY (produit_id) REFERENCES Produit(id_produit)
);


-- Insertion des tuples dans la table clients
INSERT INTO Client VALUES
(1, 'Dupont', 'Jean', '514-111-1111', 'jean.dupont@gmail.com', 'MotDePasse1', '123 rue Principale, Montréal, QC', '1111222233334444'),
(2, 'Tremblay', 'Sophie', '514-222-2222', 'sophie.tremblay@gmail.com', 'MotDePasse2', '456 rue Sainte-Catherine, Montréal, QC', '5555666677778888'),
(3, 'Gagné', 'Marc', '514-333-3333', 'marc.gagne@gmail.com', 'MotDePasse3', '789 rue Sherbrooke, Montréal, QC', '9999000011112222'),
(4, 'Lavoie', 'Catherine', '514-444-4444', 'catherine.lavoie@gmail.com', 'MotDePasse4', '1010 rue Saint-Denis, Montréal, QC', '3333444455556666'),
(5, 'Leblanc', 'Patrick', '514-555-5555', 'patrick.leblanc@gmail.com', 'MotDePasse5', '1212 avenue des Pins, Montréal, QC', '7777888899990000'),
(6, 'Nguyen', 'Vanessa', '514-666-6666', 'vanessa.nguyen@gmail.com', 'MotDePasse6', '1313 boulevard René-Lévesque, Montréal, QC', '4444555566667777'),
(7, 'Girard', 'Simon', '514-777-7777', 'simon.girard@gmail.com', 'MotDePasse7', '1414 rue Saint-Hubert, Montréal, QC', '8888999900001111'),
(8, 'Bouchard', 'Emilie', '514-888-8888', 'emilie.bouchard@gmail.com', 'MotDePasse8', '1515 boulevard de Maisonneuve, Montréal, QC', '2222333344445555'),
(9, 'Fournier', 'Guillaume', '514-999-9999', 'guillaume.fournier@gmail.com', 'MotDePasse9', '1616 rue Sainte-Catherine, Montréal, QC', '6666777788889999'),
(10, 'Gauthier', 'Sophie', '514-111-1111', 'sophie.gauthier@gmail.com', 'MotDePasse10', '1717 rue Saint-Denis, Montréal, QC', '3333444455556666'),
(11, 'Gauthier', 'Sophie', '514-111-1111', 'sophie.gauthier@gmail.com', 'MotDePasse10', '1717 rue Saint-Denis, Montréal, QC', '3333444455556666');

-- IInsertion des tuples dans la table produits
INSERT INTO Produit (id_produit, nom, description, prix, quantitéDisponible, disponibilité)
VALUES
  (1, 'Rose', 'Belle rose rouge', 10.99, 100, 'Disponible'),
  (2, 'Lys', 'Élégant lys blanc', 15.99, 50, 'Disponible'),
  (3, 'Tulipe', 'Colorée tulipe multicolore', 8.99, 75, 'Disponible'),
  (4, 'Orchidée', 'Magnifique orchidée violette', 19.99, 25, 'Disponible'),
  (5, 'Marguerite', 'Fleur de marguerite blanche', 6.99, 80, 'Disponible'),
  (6, 'Gerbera', 'Belle gerbera', 5.00, 250, 'à l année'),
  (7, 'Jonquille', 'Délicate jonquille jaune', 7.99, 60, 'Disponible'),
  (8, 'Iris', 'Élégant iris bleu', 12.99, 40, 'Disponible'),
  (9, 'Pivoine', 'Belle pivoine rose', 22.99, 30, 'Disponible'),
  (10, 'Œillet', 'Fleur d\'oeillet rouge', 9.99, 70, 'Disponible');

 
 -- Insertion des tuples dans la table accessoires
  INSERT INTO Accessoire (id_accessoire, nom, description_accessoire, quantité, prix)
VALUES
	(1, 'Pot', 'un joli pot', 20, 39.99),
	(2, 'ruban rouge', 'ruban rouge sang', 10, 79.99),
	(3, 'ruban doré', 'Un ruban qui pétille', 15, 29.99),
	(4, 'Pot en verre', 'pot en verre transparent', 25, 24.99),
	(5, 'Support éducatif', 'En apprendre plus sur les plantes', 30, 49.99),
	(6, 'Panier', 'Panier pour balcon', 5, 149.99),
	(7, 'Ruban vert', 'ruban vert académie', 8, 34.99),
	(8, 'ruban bleu', 'ruban bleu roi', 12, 19.99),
	(9, 'ruban en cuir', 'ruban en cuir tanné en italie', 18, 39.99),
	(10, 'pot en terre cuite', 'pot en terre cuite ancien', 22, 29.99);

-- Insertion des tuples dans la table bouquets standards
INSERT INTO Bouquet_Standard (id_bouquet_standard, nom, description, prix, catégorie, produit_id)
VALUES 
  (1, 'Bouquet Romantique', 'Un bouquet romantique composé de roses rouges', 39.99, 'Romantique', 1),	
  (2, 'Bouquet Printanier', 'Un bouquet frais et coloré pour célébrer le printemps', 29.99, 'Printemps', 3),
  (3, 'Bouquet Élégant', 'Un bouquet élégant avec des lys blancs', 49.99, 'Élégant', 2),
  (4, 'Bouquet Exotique', 'Un bouquet exotique avec des orchidées violettes', 59.99, 'Exotique', 4),
  (5, 'Bouquet Champêtre', 'Un bouquet champêtre avec des marguerites blanches', 19.99, 'Champêtre', 5),
  (6, 'Bouquet de Roses Blanches', 'Un bouquet de roses blanches classique', 34.99, 'Classique', 1),
  (7, 'Bouquet de Lys Roses', 'Un bouquet de lys roses romantique', 42.99, 'Romantique', 2),
  (8, 'Bouquet de Pivoines', 'Un bouquet de pivoines fraîches', 44.99, 'Printemps', 3),
  (9, 'Bouquet de Gerberas', 'Un bouquet de gerberas coloré', 29.99, 'Coloré', 5),
  (10, 'Bouquet de Succulentes', 'Un bouquet original de succulentes', 39.99, 'Original', 4);

-- Insertion des tuples dans la table "Magasin"
INSERT INTO Magasin (id_magasin, nom, adresse, bouquet_standard_id, produit_id, accessoire_id)
VALUES
    (1, 'Magasin A', 'Adresse A', 1, 1, 1),
    (2, 'Magasin B', 'Adresse B', 2, 2, 2),
    (3, 'Magasin C', 'Adresse C', 3, 3, 3),
    (4, 'Magasin A', 'Adresse A', 4, 4, 4),
    (5, 'Magasin E', 'Adresse E', 5, 5, 5),
    (6, 'Magasin F', 'Adresse F', 6, 6, 6),
    (7, 'Magasin G', 'Adresse G', 7, 7, 7),
    (8, 'Magasin H', 'Adresse H', 8, 8, 8),
    (9, 'Magasin I', 'Adresse I', 9, 9, 9),
    (10, 'Magasin J', 'Adresse J', 10, 10, 10);

--  Insertion des tuples dans la table Commande_Personnalisee
INSERT INTO Commande_Personnalisee (id_commande_personnalisee, date_commande, date_livraison, adresse_livraison, message_accompagnement, montant_total, etat, client_id, magasin_id, produit_id,accessoire_id) 
VALUES
  (1, '2023-05-01', '2023-05-05', 'Adresse A', 'Joyeux anniversaire !', 50.00, 'VINV', 1, 1, 1, 1),
  (2, '2023-05-02', '2023-05-06', 'Adresse B', NULL, 75.00, 'CC', 2, 2, 2, 2),
  (3, '2023-05-03', '2023-05-07', 'Adresse C', NULL, 100.00, 'CPAV', 3, 3, 3, 3),
  (4, '2023-05-04', '2023-05-08', 'Adresse D', 'Bonne fête maman !', 125.00, 'CAL', 4, 4, 4, 4),
  (5, '2023-05-05', '2023-05-09', 'Adresse E', NULL, 150.00, 'CL', 5, 5, 5, 5),
  (6, '2023-05-06', '2023-05-10', 'Adresse F', NULL, 175.00, 'VINV', 6, 1, 6, 6),
  (7, '2023-05-07', '2023-05-11', 'Adresse G', 'Bon rétablissement !', 200.00, 'CC', 7, 2, 7, 7),
  (8, '2023-05-08', '2023-05-12', 'Adresse H', NULL, 225.00, 'CPAV', 10, 3, 8, 8),
  (9, '2023-05-09', '2023-05-13', 'Adresse I', NULL, 250.00, 'CAL', 10, 4, 9, 9),
  (10, '2023-05-10', '2023-05-14', 'Adresse J', 'Bonne fête grand-maman !', 275.00, 'CL', 10, 5, 10, 10);


-- Insertion des tuples dans la table Commande_Standard
INSERT INTO Commande_Standard (id_commande_standard, bouquet_standard_id, date_commande, date_livraison, adresse_livraison, message_accompagnement, montant_total, etat, client_id, magasin_id)
VALUES
(1, 1, '2022-05-10', '2022-05-12', '12 rue des Fleurs, Paris', 'Joyeux Anniversaire!', 29.99, 'CC', 1, 1),
(2, 2, '2022-05-10', '2022-05-13', '5 avenue des Roses, Marseille', 'Bonne fête maman!', 34.50, 'VINV', 2, 2),
(3, 3, '2022-05-11', '2022-05-14', '15 rue des Lilas, Lille', 'Félicitations pour votre mariage!', 45.99, 'CAL', 3, 3),
(4, 4, '2022-05-12', '2022-05-15', '25 avenue des Hortensias, Nice', NULL, 25.00, 'CL', 4, 4),
(5, 1, '2023-05-13', '2023-05-16', '30 rue des Violettes, Toulouse', 'Bon rétablissement!', 29.99, 'CC', 5, 5),
(6, 2, '2023-05-13', '2023-05-17', '8 avenue des Pivoines, Bordeaux', 'Joyeuses Pâques!', 34.50, 'VINV', 6, 6),
(7, 3, '2023-05-14', '2023-05-18', '18 rue des Marguerites, Lyon', 'Bonne fête des mères!', 45.99, 'CAL', 7, 7),
(8, 4, '2023-05-15', '2023-05-19', '10 avenue des Orchidées, Nantes', 'Félicitations pour votre promotion!', 25.00, 'CL', 10, 8),
(9, 1, '2023-05-16', '2023-05-20', '5 rue des Pensées, Strasbourg', 'Bon anniversaire!', 29.99, 'CC', 10, 9),
(10, 2, '2023-05-16', '2023-05-21', '20 avenue des Lys, Montpellier', 'Bonne fête grand-mère!', 34.50, 'VINV', 10, 10);

-- Insertion des tuples dans la table Détails Commande 
INSERT INTO Details_Commande 
VALUES
(1, 'STANDARD', 1, 1, 2),
(2, 'STANDARD', 2, 2, 1),
(3, 'PERSONNALISÉE', 3, 3, 3),
(4, 'STANDARD', 4, 4, 4),
(5, 'PERSONNALISÉE', 5, 5, 1),
(6, 'STANDARD', 6, 6, 2),
(7, 'STANDARD', 7, 7, 1),
(8, 'PERSONNALISÉE', 8, 8, 2),
(9, 'STANDARD', 9, 9, 3),
(10, 'PERSONNALISÉE', 10, 10, 4);


DROP USER 'bozo'@'localhost';
CREATE USER 'bozo'@'localhost' IDENTIFIED BY 'bozo';
GRANT SELECT ON fleur.* TO 'bozo'@'localhost';
FLUSH PRIVILEGES;

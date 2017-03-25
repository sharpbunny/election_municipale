/* Suppression des enregistrements de toutes les tables */

DELETE FROM stats_election;

DELETE FROM election;

DELETE FROM calcul_sieges;

DELETE FROM AnneeElection;

DELETE FROM Commune;

DELETE FROM Departement;

DELETE FROM Candidat;
DBCC CHECKIDENT ('Candidat', RESEED, 0)

DELETE FROM Liste;
DBCC CHECKIDENT ('Liste', RESEED, 0)

DELETE FROM Parti;

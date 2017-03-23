delete from stats_election;
delete from election;
delete from calcul_sieges;
delete from AnneeElection;
delete from Commune;
delete from Departement;
delete from Candidat;
DBCC CHECKIDENT ('Candidat', RESEED, 0)
delete from Liste;
DBCC CHECKIDENT ('Liste', RESEED, 0)
delete from Parti;

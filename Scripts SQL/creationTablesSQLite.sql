#------------------------------------------------------------
#        Script SQLite  
#------------------------------------------------------------


#------------------------------------------------------------
# Table: Candidat
#------------------------------------------------------------
CREATE TABLE Candidat(
	idCandidat  INTEGER autoincrement NOT NULL ,
	nom         TEXT NOT NULL ,
	prenom      TEXT ,
	sexe        TEXT NOT NULL ,
	idListe     INTEGER NOT NULL ,
	PRIMARY KEY (idCandidat) ,
	
	FOREIGN KEY (idListe) REFERENCES Liste(idListe)
);


#------------------------------------------------------------
# Table: Parti
#------------------------------------------------------------
CREATE TABLE Parti(
	code_nuance  TEXT NOT NULL ,
	PRIMARY KEY (code_nuance)
);


#------------------------------------------------------------
# Table: Liste
#------------------------------------------------------------
CREATE TABLE Liste(
	idListe      INTEGER autoincrement NOT NULL ,
	nomListe     TEXT NOT NULL ,
	code_nuance  TEXT NOT NULL ,
	PRIMARY KEY (idListe) ,
	
	FOREIGN KEY (code_nuance) REFERENCES Parti(code_nuance)
);


#------------------------------------------------------------
# Table: Commune
#------------------------------------------------------------
CREATE TABLE Commune(
	insee                  TEXT NOT NULL ,
	code_de_la_commune     TEXT NOT NULL ,
	libelle_de_la_commune  TEXT NOT NULL ,
	geo_point_2d           TEXT ,
	geo_shape              TEXT ,
	code_du_departement    INTEGER NOT NULL ,
	PRIMARY KEY (insee) ,
	
	FOREIGN KEY (code_du_departement) REFERENCES Departement(code_du_departement)
);


#------------------------------------------------------------
# Table: AnneeElection
#------------------------------------------------------------
CREATE TABLE AnneeElection(
	annee  NUMERIC NOT NULL ,
	PRIMARY KEY (annee)
);


#------------------------------------------------------------
# Table: Departement
#------------------------------------------------------------
CREATE TABLE Departement(
	code_du_departement     INTEGER NOT NULL ,
	libelle_du_departement  TEXT NOT NULL ,
	PRIMARY KEY (code_du_departement)
);


#------------------------------------------------------------
# Table: election
#------------------------------------------------------------
CREATE TABLE election(
	voix        INTEGER ,
	annee       NUMERIC NOT NULL ,
	idCandidat  INTEGER NOT NULL ,
	insee       TEXT NOT NULL ,
	PRIMARY KEY (annee,idCandidat,insee) ,
	
	FOREIGN KEY (annee) REFERENCES AnneeElection(annee),
	FOREIGN KEY (idCandidat) REFERENCES Candidat(idCandidat),
	FOREIGN KEY (insee) REFERENCES Commune(insee)
);


#------------------------------------------------------------
# Table: stats_election
#------------------------------------------------------------
CREATE TABLE stats_election(
	inscrits        INTEGER ,
	abstentions     INTEGER ,
	blancs_et_nuls  INTEGER ,
	exprimes        INTEGER ,
	votants         INTEGER ,
	annee           NUMERIC NOT NULL ,
	insee           TEXT NOT NULL ,
	PRIMARY KEY (annee,insee) ,
	
	FOREIGN KEY (annee) REFERENCES AnneeElection(annee),
	FOREIGN KEY (insee) REFERENCES Commune(insee)
);


#------------------------------------------------------------
# Table: calcul_sieges
#------------------------------------------------------------
CREATE TABLE calcul_sieges(
	sieges_elus      INTEGER ,
	sieges_cc        INTEGER ,
	sieges_secteurs  INTEGER ,
	insee            TEXT NOT NULL ,
	annee            NUMERIC NOT NULL ,
	idListe          INTEGER NOT NULL ,
	PRIMARY KEY (insee,annee,idListe) ,
	
	FOREIGN KEY (insee) REFERENCES Commune(insee),
	FOREIGN KEY (annee) REFERENCES AnneeElection(annee),
	FOREIGN KEY (idListe) REFERENCES Liste(idListe)
);



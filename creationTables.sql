/*------------------------------------------------------------
*        Script SQLSERVER 
------------------------------------------------------------*/


/*------------------------------------------------------------
-- Table: Candidat
------------------------------------------------------------*/
CREATE TABLE Candidat(
	idCandidat INT IDENTITY (1,1) NOT NULL ,
	nom        VARCHAR (40) NOT NULL ,
	prenom     VARCHAR (40)  ,
	sexe       CHAR (1)  NOT NULL ,
	idListe    INT  NOT NULL ,
	CONSTRAINT prk_constraint_Candidat PRIMARY KEY NONCLUSTERED (idCandidat)
);


/*------------------------------------------------------------
-- Table: Parti
------------------------------------------------------------*/
CREATE TABLE Parti(
	code_nuance VARCHAR (5) NOT NULL ,
	CONSTRAINT prk_constraint_Parti PRIMARY KEY NONCLUSTERED (code_nuance)
);


/*------------------------------------------------------------
-- Table: Liste
------------------------------------------------------------*/
CREATE TABLE Liste(
	idListe     INT IDENTITY (1,1) NOT NULL ,
	nomListe    VARCHAR (250) NOT NULL ,
	code_nuance VARCHAR (5) NOT NULL ,
	CONSTRAINT prk_constraint_Liste PRIMARY KEY NONCLUSTERED (idListe)
);


/*------------------------------------------------------------
-- Table: Commune
------------------------------------------------------------*/
CREATE TABLE Commune(
	insee                 VARCHAR (5) NOT NULL ,
	code_de_la_commune    VARCHAR (4) NOT NULL ,
	libelle_de_la_commune VARCHAR (50) NOT NULL ,
	geo_point_2d          VARCHAR (50)  ,
	geo_shape             VARCHAR (2250)  ,
	code_du_departement   SMALLINT  NOT NULL ,
	CONSTRAINT prk_constraint_Commune PRIMARY KEY NONCLUSTERED (insee)
);


/*------------------------------------------------------------
-- Table: AnneeElection
------------------------------------------------------------*/
CREATE TABLE AnneeElection(
	annee INT  NOT NULL ,
	CONSTRAINT prk_constraint_AnneeElection PRIMARY KEY NONCLUSTERED (annee)
);


/*------------------------------------------------------------
-- Table: Departement
------------------------------------------------------------*/
CREATE TABLE Departement(
	code_du_departement    SMALLINT  NOT NULL ,
	libelle_du_departement VARCHAR (50) NOT NULL ,
	CONSTRAINT prk_constraint_Departement PRIMARY KEY NONCLUSTERED (code_du_departement)
);


/*------------------------------------------------------------
-- Table: election
------------------------------------------------------------*/
CREATE TABLE election(
	voix       INT   ,
	annee      INT  NOT NULL ,
	idCandidat INT  NOT NULL ,
	insee      VARCHAR (5) NOT NULL ,
	CONSTRAINT prk_constraint_election PRIMARY KEY NONCLUSTERED (annee,idCandidat,insee)
);


/*------------------------------------------------------------
-- Table: stats_election
------------------------------------------------------------*/
CREATE TABLE stats_election(
	inscrits       INT   ,
	abstentions    INT   ,
	blancs_et_nuls INT   ,
	exprimes       INT   ,
	votants        INT   ,
	annee          INT  NOT NULL ,
	insee          VARCHAR (5) NOT NULL ,
	CONSTRAINT prk_constraint_stats_election PRIMARY KEY NONCLUSTERED (annee,insee)
);


/*------------------------------------------------------------
-- Table: calcul_sieges
------------------------------------------------------------*/
CREATE TABLE calcul_sieges(
	sieges_elus     SMALLINT   ,
	sieges_cc       SMALLINT   ,
	sieges_secteurs SMALLINT   ,
	insee           VARCHAR (5) NOT NULL ,
	annee           INT  NOT NULL ,
	idListe         INT  NOT NULL ,
	CONSTRAINT prk_constraint_calcul_sieges PRIMARY KEY NONCLUSTERED (insee,annee,idListe)
);



ALTER TABLE Candidat ADD CONSTRAINT FK_Candidat_idListe FOREIGN KEY (idListe) REFERENCES Liste(idListe);
ALTER TABLE Liste ADD CONSTRAINT FK_Liste_code_nuance FOREIGN KEY (code_nuance) REFERENCES Parti(code_nuance);
ALTER TABLE Commune ADD CONSTRAINT FK_Commune_code_du_departement FOREIGN KEY (code_du_departement) REFERENCES Departement(code_du_departement);
ALTER TABLE election ADD CONSTRAINT FK_election_annee FOREIGN KEY (annee) REFERENCES AnneeElection(annee);
ALTER TABLE election ADD CONSTRAINT FK_election_idCandidat FOREIGN KEY (idCandidat) REFERENCES Candidat(idCandidat);
ALTER TABLE election ADD CONSTRAINT FK_election_insee FOREIGN KEY (insee) REFERENCES Commune(insee);
ALTER TABLE stats_election ADD CONSTRAINT FK_stats_election_annee FOREIGN KEY (annee) REFERENCES AnneeElection(annee);
ALTER TABLE stats_election ADD CONSTRAINT FK_stats_election_insee FOREIGN KEY (insee) REFERENCES Commune(insee);
ALTER TABLE calcul_sieges ADD CONSTRAINT FK_calcul_sieges_insee FOREIGN KEY (insee) REFERENCES Commune(insee);
ALTER TABLE calcul_sieges ADD CONSTRAINT FK_calcul_sieges_annee FOREIGN KEY (annee) REFERENCES AnneeElection(annee);
ALTER TABLE calcul_sieges ADD CONSTRAINT FK_calcul_sieges_idListe FOREIGN KEY (idListe) REFERENCES Liste(idListe);

 ---------------------------------------------------------------
 --        Script Oracle.  
 ---------------------------------------------------------------


------------------------------------------------------------
-- Table: Candidat
------------------------------------------------------------
CREATE TABLE Candidat(
	idCandidat  NUMBER NOT NULL ,
	nom         VARCHAR2 (40) NOT NULL  ,
	prenom      VARCHAR2 (40)  ,
	sexe        CHAR (1)  NOT NULL  ,
	idListe     NUMBER(10,0)  NOT NULL  ,
	CONSTRAINT Candidat_Pk PRIMARY KEY (idCandidat)
);

------------------------------------------------------------
-- Table: Parti
------------------------------------------------------------
CREATE TABLE Parti(
	code_nuance  VARCHAR2 (5) NOT NULL  ,
	CONSTRAINT Parti_Pk PRIMARY KEY (code_nuance)
);

------------------------------------------------------------
-- Table: Liste
------------------------------------------------------------
CREATE TABLE Liste(
	idListe      NUMBER NOT NULL ,
	nomListe     VARCHAR2 (250) NOT NULL  ,
	code_nuance  VARCHAR2 (5) NOT NULL  ,
	CONSTRAINT Liste_Pk PRIMARY KEY (idListe)
);

------------------------------------------------------------
-- Table: Commune
------------------------------------------------------------
CREATE TABLE Commune(
	insee                  VARCHAR2 (5) NOT NULL  ,
	code_de_la_commune     VARCHAR2 (4) NOT NULL  ,
	libelle_de_la_commune  VARCHAR2 (50) NOT NULL  ,
	geo_point_2d           VARCHAR2 (50)  ,
	geo_shape              VARCHAR2 (2250)  ,
	code_du_departement    NUMBER(5,0)  NOT NULL  ,
	CONSTRAINT Commune_Pk PRIMARY KEY (insee)
);

------------------------------------------------------------
-- Table: AnneeElection
------------------------------------------------------------
CREATE TABLE AnneeElection(
	annee  NUMBER(4,0)  NOT NULL  ,
	CONSTRAINT AnneeElection_Pk PRIMARY KEY (annee)
);

------------------------------------------------------------
-- Table: Departement
------------------------------------------------------------
CREATE TABLE Departement(
	code_du_departement     NUMBER(5,0)  NOT NULL  ,
	libelle_du_departement  VARCHAR2 (50) NOT NULL  ,
	CONSTRAINT Departement_Pk PRIMARY KEY (code_du_departement)
);

------------------------------------------------------------
-- Table: election
------------------------------------------------------------
CREATE TABLE election(
	voix        NUMBER(10,0)   ,
	annee       NUMBER(4,0)  NOT NULL  ,
	idCandidat  NUMBER(10,0)  NOT NULL  ,
	insee       VARCHAR2 (5) NOT NULL  ,
	CONSTRAINT election_Pk PRIMARY KEY (annee,idCandidat,insee)
);

------------------------------------------------------------
-- Table: stats_election
------------------------------------------------------------
CREATE TABLE stats_election(
	inscrits        NUMBER(10,0)   ,
	abstentions     NUMBER(10,0)   ,
	blancs_et_nuls  NUMBER(10,0)   ,
	exprimes        NUMBER(10,0)   ,
	votants         NUMBER(10,0)   ,
	annee           NUMBER(4,0)  NOT NULL  ,
	insee           VARCHAR2 (5) NOT NULL  ,
	CONSTRAINT stats_election_Pk PRIMARY KEY (annee,insee)
);

------------------------------------------------------------
-- Table: calcul_sieges
------------------------------------------------------------
CREATE TABLE calcul_sieges(
	sieges_elus      NUMBER(5,0)   ,
	sieges_cc        NUMBER(5,0)   ,
	sieges_secteurs  NUMBER(5,0)   ,
	insee            VARCHAR2 (5) NOT NULL  ,
	annee            NUMBER(4,0)  NOT NULL  ,
	idListe          NUMBER(10,0)  NOT NULL  ,
	CONSTRAINT calcul_sieges_Pk PRIMARY KEY (insee,annee,idListe)
);




ALTER TABLE Candidat ADD FOREIGN KEY (idListe) REFERENCES Liste(idListe);
ALTER TABLE Liste ADD FOREIGN KEY (code_nuance) REFERENCES Parti(code_nuance);
ALTER TABLE Commune ADD FOREIGN KEY (code_du_departement) REFERENCES Departement(code_du_departement);
ALTER TABLE election ADD FOREIGN KEY (annee) REFERENCES AnneeElection(annee);
ALTER TABLE election ADD FOREIGN KEY (idCandidat) REFERENCES Candidat(idCandidat);
ALTER TABLE election ADD FOREIGN KEY (insee) REFERENCES Commune(insee);
ALTER TABLE stats_election ADD FOREIGN KEY (annee) REFERENCES AnneeElection(annee);
ALTER TABLE stats_election ADD FOREIGN KEY (insee) REFERENCES Commune(insee);
ALTER TABLE calcul_sieges ADD FOREIGN KEY (insee) REFERENCES Commune(insee);
ALTER TABLE calcul_sieges ADD FOREIGN KEY (annee) REFERENCES AnneeElection(annee);
ALTER TABLE calcul_sieges ADD FOREIGN KEY (idListe) REFERENCES Liste(idListe);

CREATE SEQUENCE Seq_Candidat_idCandidat START WITH 1 INCREMENT BY 1 NOCYCLE;
CREATE SEQUENCE Seq_Liste_idListe START WITH 1 INCREMENT BY 1 NOCYCLE;


CREATE OR REPLACE TRIGGER Candidat_idCandidat
	BEFORE INSERT ON Candidat 
  FOR EACH ROW 
	WHEN (NEW.idCandidat IS NULL) 
	BEGIN
		 select Seq_Candidat_idCandidat.NEXTVAL INTO :NEW.idCandidat from DUAL; 
	END;
CREATE OR REPLACE TRIGGER Liste_idListe
	BEFORE INSERT ON Liste 
  FOR EACH ROW 
	WHEN (NEW.idListe IS NULL) 
	BEGIN
		 select Seq_Liste_idListe.NEXTVAL INTO :NEW.idListe from DUAL; 
	END;


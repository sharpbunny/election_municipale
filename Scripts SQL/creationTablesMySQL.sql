#------------------------------------------------------------
#        Script MySQL.
#------------------------------------------------------------


#------------------------------------------------------------
# Table: Candidat
#------------------------------------------------------------

CREATE TABLE Candidat(
        idCandidat int (11) Auto_increment  NOT NULL ,
        nom        Varchar (40) NOT NULL ,
        prenom     Varchar (40) ,
        sexe       Char (1) NOT NULL ,
        idListe    Int NOT NULL ,
        PRIMARY KEY (idCandidat )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Parti
#------------------------------------------------------------

CREATE TABLE Parti(
        code_nuance Varchar (5) NOT NULL ,
        PRIMARY KEY (code_nuance )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Liste
#------------------------------------------------------------

CREATE TABLE Liste(
        idListe     int (11) Auto_increment  NOT NULL ,
        nomListe    Varchar (250) NOT NULL ,
        code_nuance Varchar (5) NOT NULL ,
        PRIMARY KEY (idListe )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Commune
#------------------------------------------------------------

CREATE TABLE Commune(
        insee                 Varchar (5) NOT NULL ,
        code_de_la_commune    Varchar (4) NOT NULL ,
        libelle_de_la_commune Varchar (50) NOT NULL ,
        geo_point_2d          Varchar (50) ,
        geo_shape             Varchar (2250) ,
        code_du_departement   Smallint NOT NULL ,
        PRIMARY KEY (insee )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: AnneeElection
#------------------------------------------------------------

CREATE TABLE AnneeElection(
        annee Year NOT NULL ,
        PRIMARY KEY (annee )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Departement
#------------------------------------------------------------

CREATE TABLE Departement(
        code_du_departement    Smallint NOT NULL ,
        libelle_du_departement Varchar (50) NOT NULL ,
        PRIMARY KEY (code_du_departement )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: election
#------------------------------------------------------------

CREATE TABLE election(
        voix       Int ,
        annee      Year NOT NULL ,
        idCandidat Int NOT NULL ,
        insee      Varchar (5) NOT NULL ,
        PRIMARY KEY (annee ,idCandidat ,insee )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: stats_election
#------------------------------------------------------------

CREATE TABLE stats_election(
        inscrits       Int ,
        abstentions    Int ,
        blancs_et_nuls Int ,
        exprimes       Int ,
        votants        Int ,
        annee          Year NOT NULL ,
        insee          Varchar (5) NOT NULL ,
        PRIMARY KEY (annee ,insee )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: calcul_sieges
#------------------------------------------------------------

CREATE TABLE calcul_sieges(
        sieges_elus     Smallint ,
        sieges_cc       Smallint ,
        sieges_secteurs Smallint ,
        insee           Varchar (5) NOT NULL ,
        annee           Year NOT NULL ,
        idListe         Int NOT NULL ,
        PRIMARY KEY (insee ,annee ,idListe )
)ENGINE=InnoDB;

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

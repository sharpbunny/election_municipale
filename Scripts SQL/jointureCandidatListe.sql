/* Jointure entre la table Candidat et Liste */

SELECT Candidat.nom, Candidat.prenom, Candidat.sexe, Liste.nomListe FROM Candidat
LEFT JOIN Liste on Candidat.idListe = Liste.idListe
;
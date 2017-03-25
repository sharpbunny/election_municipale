/* Jointure entre la table Candidat, Liste et Parti */

SELECT Candidat.nom, Candidat.prenom, Candidat.sexe, Liste.nomListe, Parti.code_nuance
FROM Candidat
LEFT JOIN Liste on Candidat.idListe = Liste.idListe
LEFT JOIN Parti on Liste.code_nuance = Parti.code_nuance
;
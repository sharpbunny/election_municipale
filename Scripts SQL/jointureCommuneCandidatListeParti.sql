/* Jointure entre la table Candidat, Liste et Parti */

SELECT Candidat.nom, Candidat.prenom, Candidat.sexe, Commune.libelle_de_la_commune,
		Liste.nomListe, Parti.code_nuance
FROM Commune
LEFT JOIN election on election.insee = Commune.insee
LEFT JOIN Candidat on Candidat.idCandidat = election.idCandidat
LEFT JOIN Liste on Candidat.idListe = Liste.idListe
LEFT JOIN Parti on Liste.code_nuance = Parti.code_nuance
;
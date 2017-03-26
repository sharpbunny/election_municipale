SELECT AnneeElection.annee,Candidat.nom, Candidat.prenom, election.voix, Commune.libelle_de_la_commune
FROM election
LEFT JOIN Candidat ON election.idCandidat = Candidat.idCandidat
LEFT JOIN Commune ON election.insee = Commune.insee
LEFT JOIN AnneeElection ON election.annee = AnneeElection.annee;
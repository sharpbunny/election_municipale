SELECT Commune.libelle_de_la_commune, stats_election.inscrits, 
	stats_election.votants, stats_election.blancs_et_nuls,
	stats_election.abstentions, stats_election.exprimes
FROM stats_election
LEFT JOIN AnneeElection ON AnneeElection.annee = stats_election.annee
LEFT JOIN Commune ON stats_election.insee = Commune.insee
ORDER BY
Commune.libelle_de_la_commune;
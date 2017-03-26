SELECT AnneeElection.annee,
Departement.libelle_du_departement,
Commune.libelle_de_la_commune, 
calcul_sieges.sieges_secteurs, calcul_sieges.sieges_cc, calcul_sieges.sieges_secteurs,
Liste.nomListe
FROM calcul_sieges

LEFT JOIN AnneeElection on calcul_sieges.annee = AnneeElection.annee
LEFT JOIN Liste on Liste.idListe = calcul_sieges.idListe
LEFT JOIN Commune on Commune.insee = calcul_sieges.insee
LEFT JOIN Departement on Departement.code_du_departement = Commune.code_du_departement

ORDER BY 
AnneeElection.annee,
Departement.libelle_du_departement, 
Commune.libelle_de_la_commune, 
Liste.nomListe
;
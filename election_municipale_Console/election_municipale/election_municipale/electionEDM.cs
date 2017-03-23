namespace election_municipale
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class electionEDM : DbContext
	{
		public electionEDM()
			: base("name=electionEDM")
		{
		}

		public virtual DbSet<AnneeElection> AnneeElection { get; set; }
		public virtual DbSet<calcul_sieges> calcul_sieges { get; set; }
		public virtual DbSet<Candidat> Candidat { get; set; }
		public virtual DbSet<Commune> Commune { get; set; }
		public virtual DbSet<Departement> Departement { get; set; }
		public virtual DbSet<election> election { get; set; }
		public virtual DbSet<Liste> Liste { get; set; }
		public virtual DbSet<Parti> Parti { get; set; }
		public virtual DbSet<stats_election> stats_election { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AnneeElection>()
				.HasMany(e => e.calcul_sieges)
				.WithRequired(e => e.AnneeElection)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AnneeElection>()
				.HasMany(e => e.election)
				.WithRequired(e => e.AnneeElection)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AnneeElection>()
				.HasMany(e => e.stats_election)
				.WithRequired(e => e.AnneeElection)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<calcul_sieges>()
				.Property(e => e.insee)
				.IsUnicode(false);

			modelBuilder.Entity<Candidat>()
				.Property(e => e.nom)
				.IsUnicode(false);

			modelBuilder.Entity<Candidat>()
				.Property(e => e.prenom)
				.IsUnicode(false);

			modelBuilder.Entity<Candidat>()
				.Property(e => e.sexe)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<Candidat>()
				.HasMany(e => e.election)
				.WithRequired(e => e.Candidat)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Commune>()
				.Property(e => e.insee)
				.IsUnicode(false);

			modelBuilder.Entity<Commune>()
				.Property(e => e.code_de_la_commune)
				.IsUnicode(false);

			modelBuilder.Entity<Commune>()
				.Property(e => e.libelle_de_la_commune)
				.IsUnicode(false);

			modelBuilder.Entity<Commune>()
				.Property(e => e.geo_point_2d)
				.IsUnicode(false);

			modelBuilder.Entity<Commune>()
				.Property(e => e.geo_shape)
				.IsUnicode(false);

			modelBuilder.Entity<Commune>()
				.HasMany(e => e.calcul_sieges)
				.WithRequired(e => e.Commune)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Commune>()
				.HasMany(e => e.election)
				.WithRequired(e => e.Commune)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Commune>()
				.HasMany(e => e.stats_election)
				.WithRequired(e => e.Commune)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Departement>()
				.Property(e => e.libelle_du_departement)
				.IsUnicode(false);

			modelBuilder.Entity<Departement>()
				.HasMany(e => e.Commune)
				.WithRequired(e => e.Departement)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<election>()
				.Property(e => e.insee)
				.IsUnicode(false);

			modelBuilder.Entity<Liste>()
				.Property(e => e.nomListe)
				.IsUnicode(false);

			modelBuilder.Entity<Liste>()
				.Property(e => e.code_nuance)
				.IsUnicode(false);

			modelBuilder.Entity<Liste>()
				.HasMany(e => e.calcul_sieges)
				.WithRequired(e => e.Liste)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Liste>()
				.HasMany(e => e.Candidat)
				.WithRequired(e => e.Liste)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Parti>()
				.Property(e => e.code_nuance)
				.IsUnicode(false);

			modelBuilder.Entity<Parti>()
				.HasMany(e => e.Liste)
				.WithRequired(e => e.Parti)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<stats_election>()
				.Property(e => e.insee)
				.IsUnicode(false);
		}
	}
}

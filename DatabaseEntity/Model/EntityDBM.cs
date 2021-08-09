namespace DatabaseEntity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

  //  [DbConfigurationType(typeof(DBModelConfig))]
    public partial class EntityDBM : DbContext
    {
        public EntityDBM()
          //  : base("name=EntityDBM")
            : base(AppConfig.CreateConnectionString(), true) 
        {
        }

        //Use this instead of 'public class DatabaseEntity : DbConfiguration'. This will automatically solve the
        //'no entity framework provider found for the ado.net provider with invariant name 'system.data.sqlclient' error. If I include the DBconfiguration class it will
        //cause an error when trying to access this assembly via another class. 
        //This will enable to execute as either a standalone assembly or as part of multiple Entity Framework assemblies being called by an exe (for example)
        private void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        

        public virtual DbSet<ActAdminComment> ActAdminComments { get; set; }
        public virtual DbSet<ActAdministration> ActAdministrations { get; set; }
        public virtual DbSet<ControlHelp> ControlHelps { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentPortfolio> DepartmentPortfolios { get; set; }
        public virtual DbSet<GeneralOrderActCommentModel> GeneralOrderActCommentModels { get; set; }
        public virtual DbSet<GeneralOrderActModel> GeneralOrderActModels { get; set; }
        public virtual DbSet<GeneralOrderGroupAct> GeneralOrderGroupActs { get; set; }
        public virtual DbSet<GeneralOrderGroupActList> GeneralOrderGroupActLists { get; set; }
        public virtual DbSet<GeneralOrderImporFileAttrib> GeneralOrderImporFileAttribs { get; set; }
        public virtual DbSet<GeneralOrderImportHeader> GeneralOrderImportHeaders { get; set; }
        public virtual DbSet<GeneralOrderPortfolioModel> GeneralOrderPortfolioModels { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<ILD_CurrentDocuments> ILD_CurrentDocuments { get; set; }
        public virtual DbSet<Minister> Ministers { get; set; }
        public virtual DbSet<MinisterPortfolio> MinisterPortfolios { get; set; }
        public virtual DbSet<ParagraphModel> ParagraphModels { get; set; }
        public virtual DbSet<ParagraphModelType> ParagraphModelTypes { get; set; }
        public virtual DbSet<Parliament> Parliaments { get; set; }
        public virtual DbSet<ParliamentMember> ParliamentMembers { get; set; }
        public virtual DbSet<ParliamentMemberPartyHouse> ParliamentMemberPartyHouses { get; set; }
        public virtual DbSet<Party> Parties { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<ScreenHelp> ScreenHelps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActAdminComment>()
                .Property(e => e.BulletChar)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ActAdministration>()
                .HasMany(e => e.ActAdminComments)
                .WithRequired(e => e.ActAdministration)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.DepartmentPortfolios)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DepartmentPortfolio>()
                .HasMany(e => e.ActAdministrations)
                .WithRequired(e => e.DepartmentPortfolio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralOrderActModel>()
                .HasMany(e => e.GeneralOrderActCommentModels)
                .WithRequired(e => e.GeneralOrderActModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralOrderGroupAct>()
                .HasMany(e => e.GeneralOrderGroupActLists)
                .WithRequired(e => e.GeneralOrderGroupAct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralOrderImportHeader>()
                .HasMany(e => e.GeneralOrderPortfolioModels)
                .WithRequired(e => e.GeneralOrderImportHeader)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralOrderPortfolioModel>()
                .HasMany(e => e.GeneralOrderActModels)
                .WithRequired(e => e.GeneralOrderPortfolioModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<House>()
                .HasMany(e => e.ParliamentMemberPartyHouses)
                .WithRequired(e => e.House)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ILD_CurrentDocuments>()
                .HasMany(e => e.ActAdministrations)
                .WithRequired(e => e.ILD_CurrentDocuments)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Minister>()
                .HasMany(e => e.MinisterPortfolios)
                .WithRequired(e => e.Minister)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParagraphModel>()
                .Property(e => e.BulletChar)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ParagraphModel>()
                .HasMany(e => e.GeneralOrderActCommentModels)
                .WithRequired(e => e.ParagraphModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParagraphModel>()
                .HasMany(e => e.GeneralOrderActModels)
                .WithRequired(e => e.ParagraphModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParagraphModel>()
                .HasMany(e => e.GeneralOrderPortfolioModels)
                .WithRequired(e => e.ParagraphModel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParagraphModelType>()
                .Property(e => e.BulletChar)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ParagraphModelType>()
                .HasMany(e => e.ParagraphModels)
                .WithRequired(e => e.ParagraphModelType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Parliament>()
                .HasMany(e => e.ParliamentMemberPartyHouses)
                .WithRequired(e => e.Parliament)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParliamentMember>()
                .HasMany(e => e.ParliamentMemberPartyHouses)
                .WithRequired(e => e.ParliamentMember)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParliamentMemberPartyHouse>()
                .HasMany(e => e.Ministers)
                .WithRequired(e => e.ParliamentMemberPartyHouse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Party>()
                .HasMany(e => e.ParliamentMemberPartyHouses)
                .WithRequired(e => e.Party)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Portfolio>()
                .HasMany(e => e.DepartmentPortfolios)
                .WithRequired(e => e.Portfolio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Portfolio>()
                .HasMany(e => e.MinisterPortfolios)
                .WithRequired(e => e.Portfolio)
                .WillCascadeOnDelete(false);
        }
    }
}

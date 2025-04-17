using Microsoft.EntityFrameworkCore;
using Nekretnine.Models;
using Nekretnine.Services;

namespace Nekretnine.Data;

public partial class RealEstateDbContext : DbContext
{

    private readonly IConfigurationService _configurationService;
    public RealEstateDbContext()
    {
    }

    public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options, IConfigurationService configurationService)
    : base(options)
    {
        _configurationService = configurationService;
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Inquiry> Inquiries { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<Offertype> Offertypes { get; set; }

    public virtual DbSet<Realestate> Realestates { get; set; }

    public virtual DbSet<Realestatetype> Realestatetypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configurationService.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.IdAddress).HasName("PRIMARY");

            entity.ToTable("address");

            entity.Property(e => e.IdAddress).HasColumnName("idAddress");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Number).HasMaxLength(10);
            entity.Property(e => e.Street).HasMaxLength(200);
        });

        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.KorisnikIdKorisnik).HasName("PRIMARY");

            entity.ToTable("administrator");

            entity.Property(e => e.KorisnikIdKorisnik)
                .ValueGeneratedNever()
                .HasColumnName("Korisnik_idKorisnik");

            entity.HasOne(d => d.KorisnikIdKorisnikNavigation).WithOne(p => p.Administrator)
                .HasForeignKey<Administrator>(d => d.KorisnikIdKorisnik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Administrator_Korisnik1");
        });

        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.KorisnikIdKorisnik).HasName("PRIMARY");

            entity.ToTable("agent");

            entity.Property(e => e.KorisnikIdKorisnik)
                .ValueGeneratedNever()
                .HasColumnName("Korisnik_idKorisnik");
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);

            entity.HasOne(d => d.KorisnikIdKorisnikNavigation).WithOne(p => p.Agent)
                .HasForeignKey<Agent>(d => d.KorisnikIdKorisnik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Agent_Korisnik1");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.KorisnikIdKorisnik).HasName("PRIMARY");

            entity.ToTable("client");

            entity.Property(e => e.KorisnikIdKorisnik)
                .ValueGeneratedNever()
                .HasColumnName("Korisnik_idKorisnik");

            entity.HasOne(d => d.KorisnikIdKorisnikNavigation).WithOne(p => p.Client)
                .HasForeignKey<Client>(d => d.KorisnikIdKorisnik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Klijent_Korisnik");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.IdContract).HasName("PRIMARY");

            entity.ToTable("contract");

            entity.HasIndex(e => e.ClientKorisnikIdKorisnik, "fk_Contract_Client1_idx");

            entity.HasIndex(e => e.OfferIdOffer, "fk_Contract_Offer1_idx");

            entity.Property(e => e.IdContract).HasColumnName("idContract");
            entity.Property(e => e.ClientKorisnikIdKorisnik).HasColumnName("Client_Korisnik_idKorisnik");
            entity.Property(e => e.Content).HasColumnType("mediumtext");
            entity.Property(e => e.OfferIdOffer).HasColumnName("Offer_idOffer");

            entity.HasOne(d => d.ClientKorisnikIdKorisnikNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.ClientKorisnikIdKorisnik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Contract_Client1");

            entity.HasOne(d => d.OfferIdOfferNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.OfferIdOffer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Contract_Offer1");
        });

        modelBuilder.Entity<Inquiry>(entity =>
        {
            entity.HasKey(e => e.IdInquiry).HasName("PRIMARY");

            entity.ToTable("inquiry");

            entity.HasIndex(e => e.ClientKorisnikIdKorisnik, "fk_Inquiry_Client1_idx");

            entity.HasIndex(e => e.OfferIdOffer, "fk_Inquiry_Offer1_idx");

            entity.Property(e => e.IdInquiry).HasColumnName("idInquiry");
            entity.Property(e => e.ClientKorisnikIdKorisnik).HasColumnName("Client_Korisnik_idKorisnik");
            entity.Property(e => e.Message).HasColumnType("mediumtext");
            entity.Property(e => e.OfferIdOffer).HasColumnName("Offer_idOffer");

            entity.HasOne(d => d.ClientKorisnikIdKorisnikNavigation).WithMany(p => p.Inquiries)
                .HasForeignKey(d => d.ClientKorisnikIdKorisnik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Inquiry_Client1");

            entity.HasOne(d => d.OfferIdOfferNavigation).WithMany(p => p.Inquiries)
                .HasForeignKey(d => d.OfferIdOffer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Inquiry_Offer1");
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.IdOffer).HasName("PRIMARY");

            entity.ToTable("offer");

            entity.HasIndex(e => e.RealestateIdRealestate, "fk_Offer_Realestate1_idx");

            entity.HasIndex(e => e.AgentKorisnikIdKorisnik, "fk_Oglas_Agent1_idx");

            entity.HasIndex(e => e.TipIdTip, "fk_Oglas_Tip1_idx");

            entity.Property(e => e.IdOffer).HasColumnName("idOffer");
            entity.Property(e => e.Active).HasDefaultValueSql("'1'");
            entity.Property(e => e.AgentKorisnikIdKorisnik).HasColumnName("Agent_Korisnik_idKorisnik");
            entity.Property(e => e.Price).HasPrecision(10);
            entity.Property(e => e.RealestateIdRealestate).HasColumnName("Realestate_idRealestate");
            entity.Property(e => e.ShortDescription).HasColumnType("text");
            entity.Property(e => e.TipIdTip).HasColumnName("Tip_idTip");
            entity.Property(e => e.Title).HasColumnType("tinytext");

            entity.HasOne(d => d.AgentKorisnikIdKorisnikNavigation).WithMany(p => p.Offers)
                .HasForeignKey(d => d.AgentKorisnikIdKorisnik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Oglas_Agent1");

            entity.HasOne(d => d.RealestateIdRealestateNavigation).WithMany(p => p.Offers)
                .HasForeignKey(d => d.RealestateIdRealestate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Offer_Realestate1");

            entity.HasOne(d => d.TipIdTipNavigation).WithMany(p => p.Offers)
                .HasForeignKey(d => d.TipIdTip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Oglas_Tip1");
        });

        modelBuilder.Entity<Offertype>(entity =>
        {
            entity.HasKey(e => e.IdType).HasName("PRIMARY");

            entity.ToTable("offertype");

            entity.Property(e => e.IdType).HasColumnName("idType");
            entity.Property(e => e.OfferType1)
                .HasMaxLength(20)
                .HasColumnName("OfferType");
        });

        modelBuilder.Entity<Realestate>(entity =>
        {
            entity.HasKey(e => e.IdRealestate).HasName("PRIMARY");

            entity.ToTable("realestate");

            entity.HasIndex(e => e.AdresaIdAdresa, "fk_Nekretnina_Adresa1_idx");

            entity.HasIndex(e => e.TipNekretnineIdTipNekretnine, "fk_Nekretnina_TipNekretnine1_idx");

            entity.Property(e => e.IdRealestate).HasColumnName("idRealestate");
            entity.Property(e => e.AdresaIdAdresa).HasColumnName("Adresa_idAdresa");
            entity.Property(e => e.Description).HasColumnType("mediumtext");
            entity.Property(e => e.ImagePaths).HasColumnType("json");
            entity.Property(e => e.SquareFootage).HasMaxLength(45);
            entity.Property(e => e.TipNekretnineIdTipNekretnine).HasColumnName("TipNekretnine_idTipNekretnine");

            entity.HasOne(d => d.AdresaIdAdresaNavigation).WithMany(p => p.Realestates)
                .HasForeignKey(d => d.AdresaIdAdresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Nekretnina_Adresa1");

            entity.HasOne(d => d.TipNekretnineIdTipNekretnineNavigation).WithMany(p => p.Realestates)
                .HasForeignKey(d => d.TipNekretnineIdTipNekretnine)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Nekretnina_TipNekretnine1");
        });

        modelBuilder.Entity<Realestatetype>(entity =>
        {
            entity.HasKey(e => e.IdRealesateType).HasName("PRIMARY");

            entity.ToTable("realestatetype");

            entity.Property(e => e.IdRealesateType).HasColumnName("idRealesateType");
            entity.Property(e => e.RealestateType1)
                .HasMaxLength(20)
                .HasColumnName("RealestateType");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Username, "Username_UNIQUE").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(513);
            entity.Property(e => e.Salt).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

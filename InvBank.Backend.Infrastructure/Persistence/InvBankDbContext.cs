using InvBank.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvBank.Backend.Infrastructure.Persistence;

public partial class InvBankDbContext : DbContext
{

    public virtual DbSet<Account> Accounts { get; set; } = null!;

    public virtual DbSet<AccountInv> AccountInvs { get; set; } = null!;

    public virtual DbSet<ActivesDepositAccount> ActivesDepositAccounts { get; set; } = null!;

    public virtual DbSet<ActivesInvestmentFund> ActivesInvestmentFunds { get; set; } = null!;

    public virtual DbSet<ActivesProperty> ActivesProperties { get; set; } = null!;

    public virtual DbSet<AtiveStateDeposit> AtiveStateDeposits { get; set; } = null!;

    public virtual DbSet<AtiveStateInvestmentFund> AtiveStateInvestmentFunds { get; set; } = null!;

    public virtual DbSet<AtiveStateProperty> AtiveStateProperties { get; set; } = null!;

    public virtual DbSet<Auth> Auths { get; set; } = null!;

    public virtual DbSet<AuthAccount> AuthAccounts { get; set; } = null!;

    public virtual DbSet<Bank> Banks { get; set; } = null!;

    public virtual DbSet<Company> Companies { get; set; } = null!;

    public virtual DbSet<PaymentDeposit> PaymentDeposits { get; set; } = null!;

    public virtual DbSet<PaymentInvestFund> PaymentInvestFunds { get; set; } = null!;

    public virtual DbSet<PaymentProperty> PaymentProperties { get; set; } = null!;

    public virtual DbSet<Postalcode> Postalcodes { get; set; } = null!;

    public virtual DbSet<Profile> Profiles { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=es2;Port=5432;Database=invbank;Username=invbank;Password=invbank");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("postgis")
            .HasPostgresExtension("uuid-ossp")
            .HasPostgresExtension("topology", "postgis_topology");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Iban).HasName("account_pkey");

            entity.ToTable("account");

            entity.Property(e => e.Iban)
                .HasMaxLength(35)
                .HasColumnName("iban");
            entity.Property(e => e.Bank)
                .HasMaxLength(35)
                .HasColumnName("bank");
            entity.Property(e => e.AmountValue)
                .HasColumnName("amount_value");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.BankNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Bank)
                .HasConstraintName("account_bank_fkey");
        });

        modelBuilder.Entity<AccountInv>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Account }).HasName("account_inv_pkey");

            entity.ToTable("account_inv");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Account)
                .HasMaxLength(35)
                .HasColumnName("account");
            entity.Property(e => e.AccountValue)
                .HasPrecision(14, 2)
                .HasColumnName("account_value");
            entity.Property(e => e.JoinAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("join_at");

            entity.HasOne(d => d.AccountNavigation).WithMany(p => p.AccountInvs)
                .HasForeignKey(d => d.Account)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_inv_account_fkey");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.AccountInvs)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_inv_id_fkey");
        });

        modelBuilder.Entity<ActivesDepositAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("actives_deposit_account_pkey");

            entity.ToTable("actives_deposit_account");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Account)
                .HasMaxLength(35)
                .HasColumnName("account");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DepositName)
                .HasMaxLength(100)
                .HasColumnName("deposit_name");
            entity.Property(e => e.DepositValue)
                .HasPrecision(14, 2)
                .HasColumnName("deposit_value");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.InitialDate).HasColumnName("initial_date");
            entity.Property(e => e.TaxPercent)
                .HasPrecision(5, 2)
                .HasColumnName("tax_percent");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.YearlyTax)
                .HasPrecision(5, 2)
                .HasColumnName("yearly_tax");

            entity.HasOne(d => d.AccountNavigation).WithMany(p => p.ActivesDepositAccounts)
                .HasForeignKey(d => d.Account)
                .HasConstraintName("actives_deposit_account_account_fkey");
        });

        modelBuilder.Entity<ActivesInvestmentFund>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("actives_investment_fund_pkey");

            entity.ToTable("actives_investment_fund");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.InitialDate).HasColumnName("initial_date");
            entity.Property(e => e.InvestName)
                .HasMaxLength(100)
                .HasColumnName("invest_name");
            entity.Property(e => e.InvestValue)
                .HasPrecision(14, 2)
                .HasColumnName("invest_value");
            entity.Property(e => e.TaxPercent)
                .HasPrecision(5, 2)
                .HasColumnName("tax_percent");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ActivesProperty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("actives_property_pkey");

            entity.ToTable("actives_property");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Account)
                .HasMaxLength(35)
                .HasColumnName("account");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .HasColumnName("designation");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.InitialDate).HasColumnName("initial_date");
            entity.Property(e => e.MonthValue)
                .HasPrecision(9, 2)
                .HasColumnName("month_value");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(15)
                .HasColumnName("postal_code");
            entity.Property(e => e.PropertyName)
                .HasMaxLength(100)
                .HasColumnName("property_name");
            entity.Property(e => e.PropertyValue)
                .HasPrecision(14, 2)
                .HasColumnName("property_value");
            entity.Property(e => e.RentValue)
                .HasPrecision(9, 2)
                .HasColumnName("rent_value");
            entity.Property(e => e.TaxPercent)
                .HasPrecision(5, 2)
                .HasColumnName("tax_percent");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.YearlyValue)
                .HasPrecision(5, 2)
                .HasColumnName("yearly_value");

            entity.HasOne(d => d.AccountNavigation).WithMany(p => p.ActivesProperties)
                .HasForeignKey(d => d.Account)
                .HasConstraintName("actives_property_account_fkey");
        });

        modelBuilder.Entity<AtiveStateDeposit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ative_state_deposit_pkey");

            entity.ToTable("ative_state_deposit");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.AtiveId).HasColumnName("ative_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.InitDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("init_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");

            entity.HasOne(d => d.Ative).WithMany(p => p.AtiveStateDeposits)
                .HasForeignKey(d => d.AtiveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ative_state_deposit_ative_id_fkey");
        });

        modelBuilder.Entity<AtiveStateInvestmentFund>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ative_state_investment_fund_pkey");

            entity.ToTable("ative_state_investment_fund");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.AtiveId).HasColumnName("ative_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.InitDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("init_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");

            entity.HasOne(d => d.Ative).WithMany(p => p.AtiveStateInvestmentFunds)
                .HasForeignKey(d => d.AtiveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ative_state_investment_fund_ative_id_fkey");
        });

        modelBuilder.Entity<AtiveStateProperty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ative_state_property_pkey");

            entity.ToTable("ative_state_property");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.AtiveId).HasColumnName("ative_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.InitDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("init_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");

            entity.HasOne(d => d.Ative).WithMany(p => p.AtiveStateProperties)
                .HasForeignKey(d => d.AtiveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ative_state_property_ative_id_fkey");
        });

        modelBuilder.Entity<Auth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auth_pkey");

            entity.ToTable("auth");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.IsEnable).HasColumnName("is_enable");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(100)
                .HasColumnName("user_password");
            entity.Property(e => e.UserRole)
                .HasDefaultValueSql("0")
                .HasColumnName("user_role");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<AuthAccount>(entity =>
        {
            entity.HasKey(e => new { e.Auth, e.Account }).HasName("auth_account_pkey");

            entity.ToTable("auth_account");

            entity.Property(e => e.Auth).HasColumnName("auth");
            entity.Property(e => e.Account)
                .HasMaxLength(35)
                .HasColumnName("account");
            entity.Property(e => e.JoinAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("join_at");

            entity.HasOne(d => d.AccountNavigation).WithMany(p => p.AuthAccounts)
                .HasForeignKey(d => d.Account)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auth_account_account_fkey");

            entity.HasOne(d => d.AuthNavigation).WithMany(p => p.AuthAccounts)
                .HasForeignKey(d => d.Auth)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auth_account_auth_fkey");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Iban).HasName("bank_pkey");

            entity.ToTable("bank");

            entity.Property(e => e.Iban)
                .HasMaxLength(35)
                .HasColumnName("iban");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(15)
                .HasColumnName("postal_code");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("company_pkey");

            entity.ToTable("company");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .HasColumnName("company_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Nif)
                .HasMaxLength(9)
                .HasColumnName("nif");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(15)
                .HasColumnName("postal_code");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Company)
                .HasForeignKey<Company>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("company_id_fkey");
        });

        modelBuilder.Entity<PaymentDeposit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_deposit_pkey");

            entity.ToTable("payment_deposit");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("amount");
            entity.Property(e => e.AtiveId).HasColumnName("ative_id");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("payment_date");

            entity.HasOne(d => d.Ative).WithMany(p => p.PaymentDeposits)
                .HasForeignKey(d => d.AtiveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_deposit_ative_id_fkey");
        });

        modelBuilder.Entity<PaymentInvestFund>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_invest_fund_pkey");

            entity.ToTable("payment_invest_fund");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("amount");
            entity.Property(e => e.AtiveId).HasColumnName("ative_id");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("payment_date");

            entity.HasOne(d => d.Ative).WithMany(p => p.PaymentInvestFunds)
                .HasForeignKey(d => d.AtiveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_invest_fund_ative_id_fkey");
        });

        modelBuilder.Entity<PaymentProperty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_property_pkey");

            entity.ToTable("payment_property");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("amount");
            entity.Property(e => e.AtiveId).HasColumnName("ative_id");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("payment_date");

            entity.HasOne(d => d.Ative).WithMany(p => p.PaymentProperties)
                .HasForeignKey(d => d.AtiveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_property_ative_id_fkey");
        });

        modelBuilder.Entity<Postalcode>(entity =>
        {
            entity.HasKey(e => e.PostalCode1).HasName("postalcode_pkey");

            entity.ToTable("postalcode");

            entity.Property(e => e.PostalCode1)
                .HasMaxLength(15)
                .HasColumnName("postal_code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Locality)
                .HasMaxLength(50)
                .HasColumnName("locality");
            entity.Property(e => e.Port).HasColumnName("port");
            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .HasColumnName("street");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("profile_pkey");

            entity.ToTable("profile");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Cc)
                .HasMaxLength(8)
                .HasColumnName("cc");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Nif)
                .HasMaxLength(9)
                .HasColumnName("nif");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(15)
                .HasColumnName("postal_code");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Profile)
                .HasForeignKey<Profile>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profile_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

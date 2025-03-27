using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace api_ecres.Model;

public partial class EcresMreContext : DbContext
{
    public EcresMreContext()
    {
    }

    public EcresMreContext(DbContextOptions<EcresMreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Sysdiagram> Sysdiagrams { get; set; }

    public virtual DbSet<TblCompany> TblCompanies { get; set; }

    public virtual DbSet<TblCompany0> TblCompany0s { get; set; }

    public virtual DbSet<TblCompany1> TblCompany1s { get; set; }

    public virtual DbSet<TblCompany2> TblCompany2s { get; set; }

    public virtual DbSet<TblContract> TblContracts { get; set; }

    public virtual DbSet<TblContract1> TblContract1s { get; set; }

    public virtual DbSet<TblContractType> TblContractTypes { get; set; }

    public virtual DbSet<TblMonth> TblMonths { get; set; }

    public virtual DbSet<TblPriceEquivalent> TblPriceEquivalents { get; set; }

    public virtual DbSet<TblRubberType> TblRubberTypes { get; set; }

    public virtual DbSet<TblShipment> TblShipments { get; set; }

    public virtual DbSet<TblShipmentTerm> TblShipmentTerms { get; set; }

    public virtual DbSet<TblState> TblStates { get; set; }

    public virtual DbSet<TblStatus> TblStatuses { get; set; }

    public virtual DbSet<TblUploadUserManual> TblUploadUserManuals { get; set; }

    public virtual DbSet<TblUserMre> TblUserMres { get; set; }

    public virtual DbSet<TblYear> TblYears { get; set; }

    public virtual DbSet<UvwContractReport> UvwContractReports { get; set; }

    public virtual DbSet<UvwEmail> UvwEmails { get; set; }

    public virtual DbSet<UvwExcelMre> UvwExcelMres { get; set; }

    public virtual DbSet<UvwPowerBiSmr> UvwPowerBiSmrs { get; set; }

    public virtual DbSet<UvwPowerBiSmr1> UvwPowerBiSmr1s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=pdbsql1601;Initial Catalog=ecres_MRE;User=sa; Password=s@2016; Trusted_Connection=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sysdiagram>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sysdiagrams");

            entity.Property(e => e.Definition).HasColumnName("definition");
            entity.Property(e => e.DiagramId).HasColumnName("diagram_id");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name");
            entity.Property(e => e.PrincipalId).HasColumnName("principal_id");
            entity.Property(e => e.Version).HasColumnName("version");
        });

        modelBuilder.Entity<TblCompany>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK_tblCompanyNew");

            entity.ToTable("tblCompany");

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Add1)
                .HasMaxLength(255)
                .HasColumnName("add1");
            entity.Property(e => e.Add2)
                .HasMaxLength(255)
                .HasColumnName("add2");
            entity.Property(e => e.Add3)
                .HasMaxLength(255)
                .HasColumnName("add3");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .HasColumnName("company_name");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(255)
                .HasColumnName("contact_number");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Email1)
                .HasMaxLength(255)
                .HasColumnName("email1");
            entity.Property(e => e.FaxNo)
                .HasMaxLength(255)
                .HasColumnName("fax_no");
            entity.Property(e => e.LicenseNo).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Pic)
                .HasMaxLength(255)
                .HasColumnName("PIC");
            entity.Property(e => e.Postcode)
                .HasMaxLength(255)
                .HasColumnName("postcode");
            entity.Property(e => e.ResetToken).HasMaxLength(50);
            entity.Property(e => e.ResetTokenExpiration).HasColumnType("datetime");
            entity.Property(e => e.RubberGrade)
                .HasMaxLength(255)
                .HasColumnName("rubber_grade");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("state");
            entity.Property(e => e.TelNo)
                .HasMaxLength(255)
                .HasColumnName("tel_no");
            entity.Property(e => e.Town)
                .HasMaxLength(255)
                .HasColumnName("town");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        modelBuilder.Entity<TblCompany0>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK_tblCompany");

            entity.ToTable("tblCompany0");

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Add1)
                .HasMaxLength(255)
                .HasColumnName("add1");
            entity.Property(e => e.Add2)
                .HasMaxLength(255)
                .HasColumnName("add2");
            entity.Property(e => e.Add3)
                .HasMaxLength(255)
                .HasColumnName("add3");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .HasColumnName("company_name");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(255)
                .HasColumnName("contact_number");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Email1)
                .HasMaxLength(255)
                .HasColumnName("email1");
            entity.Property(e => e.FaxNo)
                .HasMaxLength(255)
                .HasColumnName("fax_no");
            entity.Property(e => e.LicenseNo).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Pic)
                .HasMaxLength(255)
                .HasColumnName("PIC");
            entity.Property(e => e.Postcode)
                .HasMaxLength(255)
                .HasColumnName("postcode");
            entity.Property(e => e.ResetToken).HasMaxLength(50);
            entity.Property(e => e.ResetTokenExpiration).HasColumnType("datetime");
            entity.Property(e => e.RubberGrade)
                .HasMaxLength(255)
                .HasColumnName("rubber_grade");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("state");
            entity.Property(e => e.TelNo)
                .HasMaxLength(255)
                .HasColumnName("tel_no");
            entity.Property(e => e.Town)
                .HasMaxLength(255)
                .HasColumnName("town");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        modelBuilder.Entity<TblCompany1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblCompany1");

            entity.Property(e => e.Add1)
                .HasMaxLength(255)
                .HasColumnName("add1");
            entity.Property(e => e.Add2)
                .HasMaxLength(255)
                .HasColumnName("add2");
            entity.Property(e => e.Add3)
                .HasMaxLength(255)
                .HasColumnName("add3");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .HasColumnName("company_name");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(255)
                .HasColumnName("contact_number");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Email1)
                .HasMaxLength(255)
                .HasColumnName("email1");
            entity.Property(e => e.FaxNo)
                .HasMaxLength(255)
                .HasColumnName("fax_no");
            entity.Property(e => e.LicenseNo).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Pic)
                .HasMaxLength(255)
                .HasColumnName("PIC");
            entity.Property(e => e.Postcode)
                .HasMaxLength(255)
                .HasColumnName("postcode");
            entity.Property(e => e.RubberGrade)
                .HasMaxLength(255)
                .HasColumnName("rubber_grade");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("state");
            entity.Property(e => e.TelNo)
                .HasMaxLength(255)
                .HasColumnName("tel_no");
            entity.Property(e => e.Town)
                .HasMaxLength(255)
                .HasColumnName("town");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("type");
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        modelBuilder.Entity<TblCompany2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblCompany2");

            entity.Property(e => e.Add1)
                .HasMaxLength(255)
                .HasColumnName("add1");
            entity.Property(e => e.Add2)
                .HasMaxLength(255)
                .HasColumnName("add2");
            entity.Property(e => e.Add3)
                .HasMaxLength(255)
                .HasColumnName("add3");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .HasColumnName("company_name");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(255)
                .HasColumnName("contact_number");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Email1)
                .HasMaxLength(255)
                .HasColumnName("email1");
            entity.Property(e => e.FaxNo)
                .HasMaxLength(255)
                .HasColumnName("fax_no");
            entity.Property(e => e.LicenseNo).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Pic)
                .HasMaxLength(255)
                .HasColumnName("PIC");
            entity.Property(e => e.Postcode)
                .HasMaxLength(10)
                .HasColumnName("postcode");
            entity.Property(e => e.RubberGrade)
                .HasMaxLength(255)
                .HasColumnName("rubber_grade");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("state");
            entity.Property(e => e.TelNo)
                .HasMaxLength(255)
                .HasColumnName("tel_no");
            entity.Property(e => e.Town)
                .HasMaxLength(50)
                .HasColumnName("town");
            entity.Property(e => e.Type)
                .HasMaxLength(3)
                .HasColumnName("type");
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        modelBuilder.Entity<TblContract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK_tbl_contract00_copy");

            entity.ToTable("tbl_contract");

            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.BuyerSeller).HasColumnName("buyer_seller");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.ContractDate)
                .HasMaxLength(50)
                .HasColumnName("contract_date");
            entity.Property(e => e.ContractNo)
                .HasMaxLength(50)
                .HasColumnName("contract_no");
            entity.Property(e => e.ContractType)
                .HasMaxLength(50)
                .HasColumnName("contract_type");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Currency)
                .HasMaxLength(50)
                .HasColumnName("currency");
            entity.Property(e => e.DeletedStatus).HasColumnName("deletedStatus");
            entity.Property(e => e.Destination)
                .HasMaxLength(255)
                .HasColumnName("destination");
            entity.Property(e => e.IsDraft).HasColumnName("isDraft");
            entity.Property(e => e.Month1)
                .HasMaxLength(50)
                .HasColumnName("month1");
            entity.Property(e => e.Month2)
                .HasMaxLength(50)
                .HasColumnName("month2");
            entity.Property(e => e.OtherTerm)
                .HasMaxLength(50)
                .HasColumnName("other_term");
            entity.Property(e => e.PlaceFactoryPort)
                .HasMaxLength(255)
                .HasColumnName("place_factory_port");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price");
            entity.Property(e => e.PriceEquivalent)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price_equivalent");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("quantity");
            entity.Property(e => e.QuantityActual)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("quantityActual");
            entity.Property(e => e.RemarksCentrifugedLatex)
                .HasMaxLength(50)
                .HasColumnName("remarks_centrifugedLatex");
            entity.Property(e => e.ResubmitDate)
                .HasColumnType("datetime")
                .HasColumnName("resubmit_date");
            entity.Property(e => e.RubberId)
                .HasMaxLength(50)
                .HasColumnName("rubber_id");
            entity.Property(e => e.ShipmentId).HasColumnName("shipment_id");
            entity.Property(e => e.ShipmentTermId).HasColumnName("shipment_term_id");
            entity.Property(e => e.StatusId).HasColumnName("statusId");
            entity.Property(e => e.Trade)
                .HasMaxLength(50)
                .HasColumnName("trade");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .HasColumnName("unit");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<TblContract1>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK_tbl_contract0");

            entity.ToTable("tbl_contract1");

            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.BuyerSeller).HasColumnName("buyer_seller");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.ContractDate)
                .HasMaxLength(50)
                .HasColumnName("contract_date");
            entity.Property(e => e.ContractNo)
                .HasMaxLength(50)
                .HasColumnName("contract_no");
            entity.Property(e => e.ContractType)
                .HasMaxLength(50)
                .HasColumnName("contract_type");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Currency)
                .HasMaxLength(50)
                .HasColumnName("currency");
            entity.Property(e => e.DeletedStatus).HasColumnName("deletedStatus");
            entity.Property(e => e.Destination)
                .HasMaxLength(255)
                .HasColumnName("destination");
            entity.Property(e => e.IsDraft).HasColumnName("isDraft");
            entity.Property(e => e.Month1)
                .HasMaxLength(50)
                .HasColumnName("month1");
            entity.Property(e => e.Month2)
                .HasMaxLength(50)
                .HasColumnName("month2");
            entity.Property(e => e.OtherTerm)
                .HasMaxLength(50)
                .HasColumnName("other_term");
            entity.Property(e => e.PlaceFactoryPort)
                .HasMaxLength(255)
                .HasColumnName("place_factory_port");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price");
            entity.Property(e => e.PriceEquivalent)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price_equivalent");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("quantity");
            entity.Property(e => e.QuantityActual)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("quantityActual");
            entity.Property(e => e.RemarksCentrifugedLatex)
                .HasMaxLength(50)
                .HasColumnName("remarks_centrifugedLatex");
            entity.Property(e => e.ResubmitDate)
                .HasColumnType("datetime")
                .HasColumnName("resubmit_date");
            entity.Property(e => e.RubberId)
                .HasMaxLength(50)
                .HasColumnName("rubber_id");
            entity.Property(e => e.ShipmentId).HasColumnName("shipment_id");
            entity.Property(e => e.ShipmentTermId).HasColumnName("shipment_term_id");
            entity.Property(e => e.StatusId).HasColumnName("statusId");
            entity.Property(e => e.Trade)
                .HasMaxLength(50)
                .HasColumnName("trade");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .HasColumnName("unit");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<TblContractType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_contractType");

            entity.ToTable("tbl_contractType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContractType)
                .HasMaxLength(50)
                .HasColumnName("contractType");
        });

        modelBuilder.Entity<TblMonth>(entity =>
        {
            entity.ToTable("tbl_month");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Month)
                .HasMaxLength(50)
                .HasColumnName("month");
        });

        modelBuilder.Entity<TblPriceEquivalent>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_price_equivalent");

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.ForexRate)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("forexRate");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PriceEquivalent)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("priceEquivalent");
            entity.Property(e => e.RubberId).HasColumnName("rubber_id");
            entity.Property(e => e.ShipmentTermId).HasColumnName("shipment_term_id");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<TblRubberType>(entity =>
        {
            entity.HasKey(e => e.RubberId);

            entity.ToTable("tbl_rubberType");

            entity.Property(e => e.RubberId)
                .ValueGeneratedNever()
                .HasColumnName("rubber_id");
            entity.Property(e => e.RubberType)
                .HasMaxLength(50)
                .HasColumnName("rubber_type");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<TblShipment>(entity =>
        {
            entity.HasKey(e => e.ShipmentId);

            entity.ToTable("tbl_shipment");

            entity.Property(e => e.ShipmentId).HasColumnName("shipment_id");
            entity.Property(e => e.ShipmentType)
                .HasMaxLength(50)
                .HasColumnName("shipment_type");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<TblShipmentTerm>(entity =>
        {
            entity.HasKey(e => e.ShipmentTermId);

            entity.ToTable("tbl_shipmentTerm");

            entity.Property(e => e.ShipmentTermId).HasColumnName("shipment_term_id");
            entity.Property(e => e.ShipmentTerm)
                .HasMaxLength(50)
                .HasColumnName("shipment_term");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<TblState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_state");

            entity.ToTable("tbl_state");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.StateCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("state_code");
            entity.Property(e => e.StateCodeSmp)
                .HasMaxLength(50)
                .HasColumnName("state_code_SMP");
            entity.Property(e => e.StateName)
                .HasMaxLength(50)
                .HasColumnName("state_name");
        });

        modelBuilder.Entity<TblStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK_status");

            entity.ToTable("tbl_status");

            entity.Property(e => e.StatusId).HasColumnName("statusId");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<TblUploadUserManual>(entity =>
        {
            entity.ToTable("Tbl_uploadUserManual");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserManual).HasColumnName("userManual");
        });

        modelBuilder.Entity<TblUserMre>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("tbl_userMRE");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("username");
        });

        modelBuilder.Entity<TblYear>(entity =>
        {
            entity.ToTable("tbl_year");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<UvwContractReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("uvwContractReport");

            entity.Property(e => e.BuyerSeller).HasColumnName("buyer_seller");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .HasColumnName("company_name");
            entity.Property(e => e.ContractDate)
                .HasColumnType("date")
                .HasColumnName("contract_date");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.ContractNo)
                .HasMaxLength(50)
                .HasColumnName("contract_no");
            entity.Property(e => e.ContractType)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("contract_type");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("currency");
            entity.Property(e => e.Destination)
                .HasMaxLength(255)
                .HasColumnName("destination");
            entity.Property(e => e.Month1)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("month1");
            entity.Property(e => e.Month2)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("month2");
            entity.Property(e => e.OtherTerm)
                .HasMaxLength(50)
                .HasColumnName("other_term");
            entity.Property(e => e.PlaceFactoryPort)
                .HasMaxLength(255)
                .HasColumnName("place_factory_port");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price");
            entity.Property(e => e.PriceEquivalent)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price_equivalent");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("quantity");
            entity.Property(e => e.RemarksCentrifugedLatex)
                .HasMaxLength(50)
                .HasColumnName("remarks_centrifugedLatex");
            entity.Property(e => e.RubberType)
                .HasMaxLength(50)
                .HasColumnName("rubber_type");
            entity.Property(e => e.ShipmentTerm)
                .HasMaxLength(50)
                .HasColumnName("shipment_term");
            entity.Property(e => e.ShipmentType)
                .HasMaxLength(50)
                .HasColumnName("shipment_type");
        });

        modelBuilder.Entity<UvwEmail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("uvw_email");

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .HasColumnName("company_name");
            entity.Property(e => e.Email)
                .HasMaxLength(4000)
                .HasColumnName("email");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        modelBuilder.Entity<UvwExcelMre>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("uvw_excel_mre");

            entity.Property(e => e.BuyerSeller).HasColumnName("buyer_seller");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .HasColumnName("company_name");
            entity.Property(e => e.ContractDate)
                .HasColumnType("date")
                .HasColumnName("contract_date");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.ContractNo)
                .HasMaxLength(50)
                .HasColumnName("contract_no");
            entity.Property(e => e.ContractType)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("contract_type");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("currency");
            entity.Property(e => e.Destination)
                .HasMaxLength(255)
                .HasColumnName("destination");
            entity.Property(e => e.Month1)
                .HasMaxLength(10)
                .HasColumnName("month1");
            entity.Property(e => e.Month2)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("month2");
            entity.Property(e => e.OtherTerm)
                .HasMaxLength(50)
                .HasColumnName("other_term");
            entity.Property(e => e.PlaceFactoryPort)
                .HasMaxLength(255)
                .HasColumnName("place_factory_port");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price");
            entity.Property(e => e.PriceEquivalent)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price_equivalent");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("quantity");
            entity.Property(e => e.RemarksCentrifugedLatex)
                .HasMaxLength(50)
                .HasColumnName("remarks_centrifugedLatex");
            entity.Property(e => e.RubberType)
                .HasMaxLength(50)
                .HasColumnName("rubber_type");
            entity.Property(e => e.ShipmentTerm)
                .HasMaxLength(50)
                .HasColumnName("shipment_term");
            entity.Property(e => e.ShipmentType)
                .HasMaxLength(50)
                .HasColumnName("shipment_type");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
        });

        modelBuilder.Entity<UvwPowerBiSmr>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("uvw_Power_BI_SMR");

            entity.Property(e => e.BuyerSeller).HasColumnName("buyer_seller");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .HasColumnName("company_name");
            entity.Property(e => e.ContractDate)
                .HasColumnType("date")
                .HasColumnName("contract_date");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.ContractNo)
                .HasMaxLength(50)
                .HasColumnName("contract_no");
            entity.Property(e => e.ContractType)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("contract_type");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("currency");
            entity.Property(e => e.Destination)
                .HasMaxLength(255)
                .HasColumnName("destination");
            entity.Property(e => e.Expr1).HasMaxLength(101);
            entity.Property(e => e.Month1)
                .HasMaxLength(10)
                .HasColumnName("month1");
            entity.Property(e => e.Month2)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("month2");
            entity.Property(e => e.NewContractDate).HasColumnType("date");
            entity.Property(e => e.OtherTerm)
                .HasMaxLength(50)
                .HasColumnName("other_term");
            entity.Property(e => e.PlaceFactoryPort)
                .HasMaxLength(255)
                .HasColumnName("place_factory_port");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price");
            entity.Property(e => e.PriceEquivalent)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price_equivalent");
            entity.Property(e => e.PriceEquivalent1)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("priceEquivalent");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("quantity");
            entity.Property(e => e.RemarksCentrifugedLatex)
                .HasMaxLength(50)
                .HasColumnName("remarks_centrifugedLatex");
            entity.Property(e => e.RubberType)
                .HasMaxLength(50)
                .HasColumnName("rubber_type");
            entity.Property(e => e.ShipmentTerm)
                .HasMaxLength(50)
                .HasColumnName("shipment_term");
            entity.Property(e => e.ShipmentType)
                .HasMaxLength(50)
                .HasColumnName("shipment_type");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
        });

        modelBuilder.Entity<UvwPowerBiSmr1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("uvw_Power_BI_SMR_1");

            entity.Property(e => e.BuyerSeller).HasColumnName("buyer_seller");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .HasColumnName("company_name");
            entity.Property(e => e.ContractDate)
                .HasColumnType("date")
                .HasColumnName("contract_date");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.ContractNo)
                .HasMaxLength(50)
                .HasColumnName("contract_no");
            entity.Property(e => e.ContractType)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("contract_type");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("currency");
            entity.Property(e => e.Destination)
                .HasMaxLength(255)
                .HasColumnName("destination");
            entity.Property(e => e.Expr2).HasMaxLength(101);
            entity.Property(e => e.Month).HasMaxLength(10);
            entity.Property(e => e.Month1)
                .HasMaxLength(10)
                .HasColumnName("month1");
            entity.Property(e => e.NewContractDate).HasColumnType("datetime");
            entity.Property(e => e.OtherTerm)
                .HasMaxLength(50)
                .HasColumnName("other_term");
            entity.Property(e => e.PlaceFactoryPort)
                .HasMaxLength(255)
                .HasColumnName("place_factory_port");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price");
            entity.Property(e => e.PriceEquivalent)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("price_equivalent");
            entity.Property(e => e.PriceEquivalent1)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("priceEquivalent");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("quantity");
            entity.Property(e => e.RemarksCentrifugedLatex)
                .HasMaxLength(50)
                .HasColumnName("remarks_centrifugedLatex");
            entity.Property(e => e.RubberType)
                .HasMaxLength(50)
                .HasColumnName("rubber_type");
            entity.Property(e => e.ShipmentTerm)
                .HasMaxLength(50)
                .HasColumnName("shipment_term");
            entity.Property(e => e.ShipmentType)
                .HasMaxLength(50)
                .HasColumnName("shipment_type");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

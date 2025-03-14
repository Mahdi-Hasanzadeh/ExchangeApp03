﻿// <auto-generated />
using System;
using API.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250305182805_index added4")]
    partial class indexadded4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Shared.Models.BuyAndSellTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("BuyTransactionId")
                        .HasColumnType("int");

                    b.Property<decimal>("ConvertedAmount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrencyExchangeAccountId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,4)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("SellTransactionId")
                        .HasColumnType("int");

                    b.Property<int>("SourceCurrencyId")
                        .HasColumnType("int");

                    b.Property<int>("TargetCurrencyId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuyTransactionId");

                    b.HasIndex("CurrencyExchangeAccountId");

                    b.HasIndex("SellTransactionId");

                    b.HasIndex("SourceCurrencyId");

                    b.HasIndex("TargetCurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("BuyAndSellTransactions");
                });

            modelBuilder.Entity("Shared.Models.Currency.CurrencyEntity", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CurrencyId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Symbol")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CurrencyId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId", "CurrencyId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("Shared.Models.Currency.CurrencyExchangeRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseCurrencyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Buy")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Sell")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TargetCurrencyId")
                        .HasColumnType("int");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BaseCurrencyId");

                    b.HasIndex("EffectiveDate");

                    b.HasIndex("IsActive");

                    b.HasIndex("TargetCurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("CurrencyExchangeRates");
                });

            modelBuilder.Entity("Shared.Models.CustomerAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<int?>("AccountType")
                        .HasColumnType("int");

                    b.Property<decimal>("BorrowAmount")
                        .HasPrecision(18, 4)
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IDCardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Lastname")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PassportNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId", "AccountNumber")
                        .IsUnique();

                    b.HasIndex("UserId", "AccountType");

                    b.ToTable("CustomerAccounts");
                });

            modelBuilder.Entity("Shared.Models.CustomerBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId", "CustomerId", "CurrencyId");

                    SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("UserId", "CustomerId", "CurrencyId"), new[] { "Balance" });

                    b.ToTable("CustomerBalances");
                });

            modelBuilder.Entity("Shared.Models.CustomerTransactionHistory", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 4)
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<byte>("DealType")
                        .HasColumnType("tinyint");

                    b.Property<string>("DepositOrWithdrawBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DocumentNumber")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<byte>("TransactionType")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId");

                    b.HasIndex("CustomerId", "TransactionType");

                    b.ToTable("CustomerTransactionHistories");
                });

            modelBuilder.Entity("Shared.Models.Helpers.UserPreference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BaseCurrencyId")
                        .HasColumnType("int");

                    b.Property<int>("LastUsedAccountNumber")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BaseCurrencyId")
                        .IsUnique()
                        .HasFilter("[BaseCurrencyId] IS NOT NULL");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserPreferences");
                });

            modelBuilder.Entity("Shared.Models.TransferBetweenAccountHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CommisionAccountId")
                        .HasColumnType("int");

                    b.Property<int?>("CommisionCurrencyId")
                        .HasColumnType("int");

                    b.Property<int>("CommisionType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RecievedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RecievedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecieverDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecieverId")
                        .HasColumnType("int");

                    b.Property<int>("RecieverTransactionId")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("SendBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SendedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SenderDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<int>("SenderTransactionId")
                        .HasColumnType("int");

                    b.Property<int?>("TransactionFeeAccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("TransactionFeeAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TransactionFeeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionFeeRecievedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommisionAccountId");

                    b.HasIndex("CommisionCurrencyId");

                    b.HasIndex("CurrencyId")
                        .HasDatabaseName("IX_Transfers_CurrencyId");

                    b.HasIndex("RecieverId")
                        .HasDatabaseName("IX_Transfers_RecieverId");

                    b.HasIndex("RecieverTransactionId");

                    b.HasIndex("SenderId")
                        .HasDatabaseName("IX_Transfers_SenderId");

                    b.HasIndex("SenderTransactionId");

                    b.HasIndex("TransactionFeeAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("TransferBetweenAccountHistories");
                });

            modelBuilder.Entity("Shared.Models.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ParentUserId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentUserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Shared.Models.BuyAndSellTransaction", b =>
                {
                    b.HasOne("Shared.Models.CustomerTransactionHistory", "BuyTransaction")
                        .WithMany()
                        .HasForeignKey("BuyTransactionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shared.Models.CustomerAccount", "CurrencyExchangeAccount")
                        .WithMany()
                        .HasForeignKey("CurrencyExchangeAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Models.CustomerTransactionHistory", "SellTransaction")
                        .WithMany()
                        .HasForeignKey("SellTransactionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shared.Models.Currency.CurrencyEntity", "SourceCurrencyEntity")
                        .WithMany()
                        .HasForeignKey("SourceCurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shared.Models.Currency.CurrencyEntity", "TargetCurrencyEntity")
                        .WithMany()
                        .HasForeignKey("TargetCurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shared.Models.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BuyTransaction");

                    b.Navigation("CurrencyExchangeAccount");

                    b.Navigation("SellTransaction");

                    b.Navigation("SourceCurrencyEntity");

                    b.Navigation("TargetCurrencyEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("Shared.Models.Currency.CurrencyEntity", b =>
                {
                    b.HasOne("Shared.Models.UserEntity", "UserEntity")
                        .WithMany("Currency")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("Shared.Models.Currency.CurrencyExchangeRate", b =>
                {
                    b.HasOne("Shared.Models.Currency.CurrencyEntity", "BaseCurrency")
                        .WithMany("BaseCurrencyRates")
                        .HasForeignKey("BaseCurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shared.Models.Currency.CurrencyEntity", "TargetCurrency")
                        .WithMany("TargetCurrencyRates")
                        .HasForeignKey("TargetCurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shared.Models.UserEntity", "UserEntity")
                        .WithMany("CurrencyExchangeRates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaseCurrency");

                    b.Navigation("TargetCurrency");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("Shared.Models.CustomerAccount", b =>
                {
                    b.HasOne("Shared.Models.UserEntity", "User")
                        .WithMany("CustomerAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shared.Models.CustomerBalance", b =>
                {
                    b.HasOne("Shared.Models.Currency.CurrencyEntity", "CurrencyEntity")
                        .WithMany("CustomerBalances")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shared.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("CustomerBalances")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Models.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CurrencyEntity");

                    b.Navigation("CustomerAccount");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("Shared.Models.CustomerTransactionHistory", b =>
                {
                    b.HasOne("Shared.Models.Currency.CurrencyEntity", "CurrencyEntity")
                        .WithMany("CustomerTransactionHistories")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shared.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("CustomerTransactionHistories")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Models.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CurrencyEntity");

                    b.Navigation("CustomerAccount");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("Shared.Models.Helpers.UserPreference", b =>
                {
                    b.HasOne("Shared.Models.Currency.CurrencyEntity", "CurrencyEntity")
                        .WithOne("UserPreference")
                        .HasForeignKey("Shared.Models.Helpers.UserPreference", "BaseCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Shared.Models.UserEntity", "UserEntity")
                        .WithOne("UserPreference")
                        .HasForeignKey("Shared.Models.Helpers.UserPreference", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrencyEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("Shared.Models.TransferBetweenAccountHistory", b =>
                {
                    b.HasOne("Shared.Models.CustomerAccount", "CommisionAccount")
                        .WithMany()
                        .HasForeignKey("CommisionAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Shared.Models.Currency.CurrencyEntity", "CommisionCurrencyEntity")
                        .WithMany()
                        .HasForeignKey("CommisionCurrencyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Shared.Models.Currency.CurrencyEntity", "CurrencyEntity")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shared.Models.CustomerAccount", "RecieverAccount")
                        .WithMany()
                        .HasForeignKey("RecieverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shared.Models.CustomerTransactionHistory", "RecieverTransactionHistory")
                        .WithMany()
                        .HasForeignKey("RecieverTransactionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shared.Models.CustomerAccount", "SenederAccount")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shared.Models.CustomerTransactionHistory", "SenderTransactionHistory")
                        .WithMany()
                        .HasForeignKey("SenderTransactionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shared.Models.CustomerTransactionHistory", "TransactionFeeAccount")
                        .WithMany()
                        .HasForeignKey("TransactionFeeAccountId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Shared.Models.UserEntity", "UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CommisionAccount");

                    b.Navigation("CommisionCurrencyEntity");

                    b.Navigation("CurrencyEntity");

                    b.Navigation("RecieverAccount");

                    b.Navigation("RecieverTransactionHistory");

                    b.Navigation("SenderTransactionHistory");

                    b.Navigation("SenederAccount");

                    b.Navigation("TransactionFeeAccount");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("Shared.Models.UserEntity", b =>
                {
                    b.HasOne("Shared.Models.UserEntity", "ParentUser")
                        .WithMany("SubUsers")
                        .HasForeignKey("ParentUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentUser");
                });

            modelBuilder.Entity("Shared.Models.Currency.CurrencyEntity", b =>
                {
                    b.Navigation("BaseCurrencyRates");

                    b.Navigation("CustomerBalances");

                    b.Navigation("CustomerTransactionHistories");

                    b.Navigation("TargetCurrencyRates");

                    b.Navigation("UserPreference")
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Models.CustomerAccount", b =>
                {
                    b.Navigation("CustomerBalances");

                    b.Navigation("CustomerTransactionHistories");
                });

            modelBuilder.Entity("Shared.Models.UserEntity", b =>
                {
                    b.Navigation("Currency");

                    b.Navigation("CurrencyExchangeRates");

                    b.Navigation("CustomerAccounts");

                    b.Navigation("SubUsers");

                    b.Navigation("UserPreference")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

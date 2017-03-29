using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SimpleInvoices;

namespace InvoicingSystem.Migrations
{
    [DbContext(typeof(InvoiceContext))]
    partial class InvoiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("SimpleInvoices.Billers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("address");

                    b.Property<string>("city");

                    b.Property<string>("contact");

                    b.Property<string>("email");

                    b.Property<bool>("enable");

                    b.Property<string>("name");

                    b.Property<string>("password");

                    b.HasKey("Id");

                    b.ToTable("biller");
                });

            modelBuilder.Entity("SimpleInvoices.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("address");

                    b.Property<string>("ankle");

                    b.Property<string>("armHole");

                    b.Property<string>("backNeckDepth");

                    b.Property<string>("backNeckWidth");

                    b.Property<string>("bicep");

                    b.Property<string>("calf");

                    b.Property<string>("chaak");

                    b.Property<string>("chest");

                    b.Property<string>("contact");

                    b.Property<string>("daaman");

                    b.Property<string>("email");

                    b.Property<bool>("enable");

                    b.Property<string>("forearm");

                    b.Property<string>("frontNeckDepth");

                    b.Property<string>("frontNeckWidth");

                    b.Property<string>("fullSleveLength");

                    b.Property<string>("hips");

                    b.Property<string>("imagepath");

                    b.Property<string>("kneeCap");

                    b.Property<string>("longShirtLength");

                    b.Property<string>("lowerWaist");

                    b.Property<string>("measurementType");

                    b.Property<string>("name");

                    b.Property<string>("pantLength");

                    b.Property<string>("sholder");

                    b.Property<string>("shortShirtLength");

                    b.Property<string>("sleeveLength");

                    b.Property<string>("thigh");

                    b.Property<string>("upperWaist");

                    b.Property<string>("waist");

                    b.Property<string>("wrist");

                    b.HasKey("Id");

                    b.ToTable("customer");
                });

            modelBuilder.Entity("SimpleInvoices.CustomFields", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("enable");

                    b.Property<string>("fieldName");

                    b.Property<int?>("productId");

                    b.Property<string>("tableName");

                    b.HasKey("Id");

                    b.HasIndex("productId");

                    b.ToTable("customFields");
                });

            modelBuilder.Entity("SimpleInvoices.Design", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("LedgerDetailsledgersId");

                    b.Property<int?>("LedgerDetailsproductId");

                    b.Property<string>("color");

                    b.Property<string>("cut");

                    b.Property<bool>("enable");

                    b.Property<string>("fabric");

                    b.Property<string>("name");

                    b.Property<string>("note");

                    b.HasKey("Id");

                    b.HasIndex("LedgerDetailsledgersId", "LedgerDetailsproductId");

                    b.ToTable("design");
                });

            modelBuilder.Entity("SimpleInvoices.FieldValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("billersId");

                    b.Property<int?>("customerId");

                    b.Property<int?>("customfieldsId");

                    b.Property<bool>("enable");

                    b.Property<int?>("productId");

                    b.Property<string>("value");

                    b.HasKey("Id");

                    b.HasIndex("billersId");

                    b.HasIndex("customerId");

                    b.HasIndex("customfieldsId");

                    b.HasIndex("productId");

                    b.ToTable("FieldValues");
                });

            modelBuilder.Entity("SimpleInvoices.LedgerDetails", b =>
                {
                    b.Property<int>("ledgersId");

                    b.Property<int>("productId");

                    b.Property<double>("quantity");

                    b.Property<int?>("taxId");

                    b.HasKey("ledgersId", "productId");

                    b.HasIndex("productId");

                    b.HasIndex("taxId");

                    b.ToTable("ledgerDetails");
                });

            modelBuilder.Entity("SimpleInvoices.Ledgers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("amount");

                    b.Property<double>("balance");

                    b.Property<int?>("billerId");

                    b.Property<DateTime>("createdDate");

                    b.Property<int?>("customerId");

                    b.Property<DateTime>("deliveryDate");

                    b.Property<DateTime>("dueDate");

                    b.Property<bool>("enable");

                    b.Property<string>("invoiceName");

                    b.Property<string>("note");

                    b.HasKey("Id");

                    b.HasIndex("billerId");

                    b.HasIndex("customerId");

                    b.ToTable("ledgers");
                });

            modelBuilder.Entity("SimpleInvoices.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("LedgersId");

                    b.Property<double>("amount");

                    b.Property<DateTime>("createdDate");

                    b.Property<bool>("enable");

                    b.Property<int?>("paymentTypesId");

                    b.HasKey("Id");

                    b.HasIndex("LedgersId");

                    b.HasIndex("paymentTypesId");

                    b.ToTable("payment");
                });

            modelBuilder.Entity("SimpleInvoices.PaymentTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("enable");

                    b.Property<string>("name");

                    b.HasKey("Id");

                    b.ToTable("paymentTypes");
                });

            modelBuilder.Entity("SimpleInvoices.product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("color");

                    b.Property<DateTime>("createdOn");

                    b.Property<string>("description");

                    b.Property<bool>("enable");

                    b.Property<string>("name");

                    b.Property<string>("note");

                    b.Property<double>("price");

                    b.Property<double>("unitPrice");

                    b.HasKey("Id");

                    b.ToTable("products");
                });

            modelBuilder.Entity("SimpleInvoices.Taxes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("enable");

                    b.Property<string>("name");

                    b.Property<double>("percent");

                    b.HasKey("Id");

                    b.ToTable("taxes");
                });

            modelBuilder.Entity("SimpleInvoices.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("enable");

                    b.Property<string>("name");

                    b.Property<string>("password");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("SimpleInvoices.CustomFields", b =>
                {
                    b.HasOne("SimpleInvoices.product")
                        .WithMany("customFields")
                        .HasForeignKey("productId");
                });

            modelBuilder.Entity("SimpleInvoices.Design", b =>
                {
                    b.HasOne("SimpleInvoices.LedgerDetails")
                        .WithMany("designs")
                        .HasForeignKey("LedgerDetailsledgersId", "LedgerDetailsproductId");
                });

            modelBuilder.Entity("SimpleInvoices.FieldValue", b =>
                {
                    b.HasOne("SimpleInvoices.Billers", "billers")
                        .WithMany()
                        .HasForeignKey("billersId");

                    b.HasOne("SimpleInvoices.Customer", "customer")
                        .WithMany()
                        .HasForeignKey("customerId");

                    b.HasOne("SimpleInvoices.CustomFields", "customfields")
                        .WithMany("FieldValues")
                        .HasForeignKey("customfieldsId");

                    b.HasOne("SimpleInvoices.product", "product")
                        .WithMany()
                        .HasForeignKey("productId");
                });

            modelBuilder.Entity("SimpleInvoices.LedgerDetails", b =>
                {
                    b.HasOne("SimpleInvoices.Ledgers", "ledgers")
                        .WithMany("ledgerDetails")
                        .HasForeignKey("ledgersId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleInvoices.product", "product")
                        .WithMany("ledgerDetails")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleInvoices.Taxes", "tax")
                        .WithMany()
                        .HasForeignKey("taxId");
                });

            modelBuilder.Entity("SimpleInvoices.Ledgers", b =>
                {
                    b.HasOne("SimpleInvoices.Billers", "biller")
                        .WithMany()
                        .HasForeignKey("billerId");

                    b.HasOne("SimpleInvoices.Customer", "customer")
                        .WithMany()
                        .HasForeignKey("customerId");
                });

            modelBuilder.Entity("SimpleInvoices.Payment", b =>
                {
                    b.HasOne("SimpleInvoices.Ledgers")
                        .WithMany("payment")
                        .HasForeignKey("LedgersId");

                    b.HasOne("SimpleInvoices.PaymentTypes", "paymentTypes")
                        .WithMany()
                        .HasForeignKey("paymentTypesId");
                });
        }
    }
}

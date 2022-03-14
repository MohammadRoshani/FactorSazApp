﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WaybillApp.Migrations
{
    using WaybillApp.Data;

    [DbContext(typeof(DatabaseContext))]
    [Migration("13980321044524_test")]
    partial class Test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("WaybillApp.Model.Customer", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("FullName");

                    b.Property<string>("NationalId");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Code");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("WaybillApp.Model.InboundInvoice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BillWayCode");

                    b.Property<string>("Count");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("DriverCode");

                    b.Property<string>("DriverName");

                    b.Property<string>("Fare");

                    b.Property<bool>("IsChecked");

                    b.Property<string>("Name");

                    b.Property<string>("OriginCityCode");

                    b.Property<string>("OriginCityName");

                    b.Property<string>("OwnerName");

                    b.Property<string>("OwnerPhone");

                    b.Property<DateTime>("Time");

                    b.Property<string>("WareName");

                    b.HasKey("Id");

                    b.ToTable("InboundInvoices");
                });

            modelBuilder.Entity("WaybillApp.Model.Location", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Discharge");

                    b.HasKey("Code");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("WaybillApp.Model.OutboundInvoice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AfterFare");

                    b.Property<int>("BeforeFare");

                    b.Property<int>("BillWayCode");

                    b.Property<DateTime>("Date");

                    b.Property<string>("DestinationCity");

                    b.Property<string>("DestinationDischarge");

                    b.Property<string>("ReceiverFullName");

                    b.Property<string>("ReceiverPhoneNumber");

                    b.Property<string>("SenderFullName");

                    b.Property<string>("SenderNationalId");

                    b.Property<string>("SenderPhoneNumber");

                    b.Property<DateTime>("Time");

                    b.Property<int>("UrbanFare");

                    b.Property<int>("Wage");

                    b.Property<string>("Wares");

                    b.HasKey("Id");

                    b.ToTable("OutboundInvoices");
                });

            modelBuilder.Entity("WaybillApp.Model.User", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime>("ExpiredDate");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Token");

                    b.HasKey("Guid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WaybillApp.Model.Ware", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Code");

                    b.ToTable("Wares");
                });
#pragma warning restore 612, 618
        }
    }
}
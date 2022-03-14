namespace WaybillApp.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Test : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Customers");

            migrationBuilder.DropTable("InboundInvoices");

            migrationBuilder.DropTable("Locations");

            migrationBuilder.DropTable("OutboundInvoices");

            migrationBuilder.DropTable("Users");

            migrationBuilder.DropTable("Wares");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Customers",
                table => new
                             {
                                 Code = table.Column<string>(),
                                 FullName = table.Column<string>(nullable: true),
                                 PhoneNumber = table.Column<string>(nullable: true),
                                 Address = table.Column<string>(nullable: true),
                                 NationalId = table.Column<string>(nullable: true)
                             },
                constraints: table => { table.PrimaryKey("PK_Customers", x => x.Code); });

            migrationBuilder.CreateTable(
                "InboundInvoices",
                table => new
                             {
                                 Id = table.Column<long>().Annotation("Sqlite:Autoincrement", true),
                                 Name = table.Column<string>(nullable: true),
                                 BillWayCode = table.Column<int>(),
                                 Date = table.Column<DateTime>(),
                                 Time = table.Column<DateTime>(),
                                 DriverCode = table.Column<string>(nullable: true),
                                 DriverName = table.Column<string>(nullable: true),
                                 OriginCityCode = table.Column<string>(nullable: true),
                                 OriginCityName = table.Column<string>(nullable: true),
                                 OwnerName = table.Column<string>(nullable: true),
                                 WareName = table.Column<string>(nullable: true),
                                 OwnerPhone = table.Column<string>(nullable: true),
                                 Count = table.Column<string>(nullable: true),
                                 Fare = table.Column<string>(nullable: true),
                                 Description = table.Column<string>(nullable: true),
                                 IsChecked = table.Column<bool>()
                             },
                constraints: table => { table.PrimaryKey("PK_InboundInvoices", x => x.Id); });

            migrationBuilder.CreateTable(
                "Locations",
                table => new
                             {
                                 Code = table.Column<string>(),
                                 City = table.Column<string>(nullable: true),
                                 Discharge = table.Column<string>(nullable: true)
                             },
                constraints: table => { table.PrimaryKey("PK_Locations", x => x.Code); });

            migrationBuilder.CreateTable(
                "OutboundInvoices",
                table => new
                             {
                                 Id = table.Column<long>().Annotation("Sqlite:Autoincrement", true),
                                 SenderFullName = table.Column<string>(nullable: true),
                                 SenderPhoneNumber = table.Column<string>(nullable: true),
                                 SenderNationalId = table.Column<string>(nullable: true),
                                 ReceiverFullName = table.Column<string>(nullable: true),
                                 ReceiverPhoneNumber = table.Column<string>(nullable: true),
                                 Wares = table.Column<string>(nullable: true),
                                 UrbanFare = table.Column<int>(),
                                 BeforeFare = table.Column<int>(),
                                 AfterFare = table.Column<int>(),
                                 Wage = table.Column<int>(),
                                 Date = table.Column<DateTime>(),
                                 Time = table.Column<DateTime>(),
                                 BillWayCode = table.Column<int>(),
                                 DestinationCity = table.Column<string>(nullable: true),
                                 DestinationDischarge = table.Column<string>(nullable: true)
                             },
                constraints: table => { table.PrimaryKey("PK_OutboundInvoices", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                             {
                                 Guid = table.Column<Guid>(),
                                 Name = table.Column<string>(nullable: true),
                                 Password = table.Column<string>(nullable: true),
                                 Token = table.Column<string>(nullable: true),
                                 CreateDate = table.Column<DateTime>(),
                                 ExpiredDate = table.Column<DateTime>()
                             },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Guid); });

            migrationBuilder.CreateTable(
                "Wares",
                table => new { Code = table.Column<string>(), Name = table.Column<string>(nullable: true) },
                constraints: table => { table.PrimaryKey("PK_Wares", x => x.Code); });
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerAssetTracker.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedDat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "IsForeign", "Name" },
                values: new object[,]
                {
                    { 1, "123 Úzká, Praha 4", false, "ABC Company" },
                    { 2, "456 International Blvd, New York", true, "Global Corp" },
                    { 3, "Hlavní 4, Pardubice", false, "KovoRobo" },
                    { 4, "Dolní 2, Brno", false, "Metal2U s.r.o." }
                });

            migrationBuilder.InsertData(
                table: "Licenses",
                columns: new[] { "Id", "CustomerId", "MachineId", "MaintenanceContract", "Software", "Type", "Vendor", "Version" },
                values: new object[] { 5, 2, null, null, "IMK UP!", 0, "Topmes", "3.4.3" });

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "Id", "CustomerId", "MachineType", "Manufacturer", "Name", "PurchaseDate", "SerialNumber" },
                values: new object[,]
                {
                    { 1, 1, 0, "Hexagon", "CMM XYZ-1000", new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "CMM-SN-001" },
                    { 2, 1, 1, "FARO", "Arm R-7", new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ARM-SN-002" },
                    { 3, 2, 3, "Leica", "Laser Tracker LT-500", new DateTime(2021, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "LT-SN-003" },
                    { 4, 3, 2, "Scantech", "NimbleTrack", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "SK1230054" }
                });

            migrationBuilder.InsertData(
                table: "Licenses",
                columns: new[] { "Id", "CustomerId", "MachineId", "MaintenanceContract", "Software", "Type", "Vendor", "Version" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "PC-DMIS", 1, "Hexagon", "2023.1" },
                    { 2, 1, 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PolyWorks", 2, "InnovMetric", "2024" },
                    { 3, 2, 3, null, "SpatialAnalyzer", 0, "New River Kinematics", "2023" },
                    { 4, 3, 4, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PolyWorks", 1, "InnovMetric", "2024" }
                });

            migrationBuilder.InsertData(
                table: "ServiceRecords",
                columns: new[] { "Id", "Date", "MachineId", "Technician", "Text" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Jan Novák", "Pravidelná kalibrace a údržba. Vyměněny filtry." },
                    { 2, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Petra Svobodová", "Oprava kloubu ramene. Testováno a funkční." },
                    { 3, new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Jan Novák", "Periodická 1 roční kalibrace dle ISO 10360" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Licenses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Licenses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Licenses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Licenses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Licenses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ServiceRecords",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ServiceRecords",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ServiceRecords",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

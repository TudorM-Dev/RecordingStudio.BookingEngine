using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecordingStudio.BookingEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedReferenceData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Microphone" },
                    { 2, "Mixing Console" },
                    { 3, "Vocal Booth" },
                    { 4, "Grand Piano" }
                });

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Standard recording session", "Recording Session" },
                    { 2, "Mixing an existing recording", "Mixing" },
                    { 3, "Recording session with grand piano", "Piano Recording" }
                });

            migrationBuilder.InsertData(
                table: "Studios",
                columns: new[] { "Id", "Name", "Sector" },
                values: new object[,]
                {
                    { 1, "Studio A", "Downtown" },
                    { 2, "Studio B", "Uptown" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { 1, "client@example.com", "Test Client" });

            migrationBuilder.InsertData(
                table: "ServiceTypeRequiredFacilities",
                columns: new[] { "FacilityId", "ServiceTypeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "StudioFacilities",
                columns: new[] { "FacilityId", "StudioId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "StudioServiceExclusions",
                columns: new[] { "ServiceTypeId", "StudioId" },
                values: new object[] { 2, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ServiceTypeRequiredFacilities",
                keyColumns: new[] { "FacilityId", "ServiceTypeId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ServiceTypeRequiredFacilities",
                keyColumns: new[] { "FacilityId", "ServiceTypeId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ServiceTypeRequiredFacilities",
                keyColumns: new[] { "FacilityId", "ServiceTypeId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ServiceTypeRequiredFacilities",
                keyColumns: new[] { "FacilityId", "ServiceTypeId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ServiceTypeRequiredFacilities",
                keyColumns: new[] { "FacilityId", "ServiceTypeId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ServiceTypeRequiredFacilities",
                keyColumns: new[] { "FacilityId", "ServiceTypeId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "StudioFacilities",
                keyColumns: new[] { "FacilityId", "StudioId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "StudioFacilities",
                keyColumns: new[] { "FacilityId", "StudioId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "StudioFacilities",
                keyColumns: new[] { "FacilityId", "StudioId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "StudioFacilities",
                keyColumns: new[] { "FacilityId", "StudioId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "StudioFacilities",
                keyColumns: new[] { "FacilityId", "StudioId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "StudioFacilities",
                keyColumns: new[] { "FacilityId", "StudioId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "StudioServiceExclusions",
                keyColumns: new[] { "ServiceTypeId", "StudioId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ServiceTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Studios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Studios",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AddedDataToTestoviTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Testovi",
                columns: new[] { "TestId", "KursId", "Nivo" },
                values: new object[,]
                {
                    { 1, 1, "I" },
                    { 2, 1, "II" },
                    { 3, 1, "III" },
                    { 4, 2, "I" },
                    { 5, 2, "II" },
                    { 6, 2, "III" },
                    { 7, 3, "I" },
                    { 8, 3, "II" },
                    { 9, 3, "III" },
                    { 10, 4, "I" },
                    { 11, 4, "II" },
                    { 12, 4, "III" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Testovi",
                keyColumns: new[] { "TestId", "KursId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Testovi",
                keyColumns: new[] { "TestId", "KursId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Testovi",
                keyColumns: new[] { "TestId", "KursId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Testovi",
                keyColumns: new[] { "TestId", "KursId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "Testovi",
                keyColumns: new[] { "TestId", "KursId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "Testovi",
                keyColumns: new[] { "TestId", "KursId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "Testovi",
                keyColumns: new[] { "TestId", "KursId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "Testovi",
                keyColumns: new[] { "TestId", "KursId" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "Testovi",
                keyColumns: new[] { "TestId", "KursId" },
                keyValues: new object[] { 9, 3 });

            migrationBuilder.DeleteData(
                table: "Testovi",
                keyColumns: new[] { "TestId", "KursId" },
                keyValues: new object[] { 10, 4 });

            migrationBuilder.DeleteData(
                table: "Testovi",
                keyColumns: new[] { "TestId", "KursId" },
                keyValues: new object[] { 11, 4 });

            migrationBuilder.DeleteData(
                table: "Testovi",
                keyColumns: new[] { "TestId", "KursId" },
                keyValues: new object[] { 12, 4 });
        }
    }
}

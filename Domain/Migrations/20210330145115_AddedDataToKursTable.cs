using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AddedDataToKursTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Kursevi",
                columns: new[] { "KursId", "NazivKursa" },
                values: new object[,]
                {
                    { 1, "Napredne .NET tehnologije" },
                    { 2, "Napredna Java" },
                    { 3, "Projektovanje informacionih sistema" },
                    { 4, "Osnove teorije igara" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Kursevi",
                keyColumn: "KursId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kursevi",
                keyColumn: "KursId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Kursevi",
                keyColumn: "KursId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Kursevi",
                keyColumn: "KursId",
                keyValue: 4);
        }
    }
}

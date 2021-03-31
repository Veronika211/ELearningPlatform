using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AddedDataToLekcijaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lekcija",
                columns: new[] { "KursId", "LekcijaId", "Naziv", "Sadrzaj" },
                values: new object[,]
                {
                    { 1, 1, "Tipovi objekata", null },
                    { 1, 2, "Konstruktori", null },
                    { 2, 3, "Osnove OOP", null },
                    { 3, 4, "Tronivojska arhitektura", null },
                    { 3, 5, "Implementacija korisnickog interfejsa", null },
                    { 4, 6, "Mesovite igre", null },
                    { 4, 7, "Dilema zatvorenika", null },
                    { 4, 8, "Igre koordinacije", null },
                    { 4, 9, "Igre antikoordinacije", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lekcija",
                keyColumns: new[] { "KursId", "LekcijaId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Lekcija",
                keyColumns: new[] { "KursId", "LekcijaId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Lekcija",
                keyColumns: new[] { "KursId", "LekcijaId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Lekcija",
                keyColumns: new[] { "KursId", "LekcijaId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "Lekcija",
                keyColumns: new[] { "KursId", "LekcijaId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "Lekcija",
                keyColumns: new[] { "KursId", "LekcijaId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "Lekcija",
                keyColumns: new[] { "KursId", "LekcijaId" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.DeleteData(
                table: "Lekcija",
                keyColumns: new[] { "KursId", "LekcijaId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "Lekcija",
                keyColumns: new[] { "KursId", "LekcijaId" },
                keyValues: new object[] { 4, 9 });
        }
    }
}

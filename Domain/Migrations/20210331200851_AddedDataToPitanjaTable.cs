using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AddedDataToPitanjaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pitanja",
                columns: new[] { "PitanjeId", "Discriminator", "Naziv", "TestId", "TestKursId" },
                values: new object[] { 1, "Checkbox", "Koja su cetiri osnovna principa OOP?", 4, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pitanja",
                keyColumn: "PitanjeId",
                keyValue: 1);
        }
    }
}

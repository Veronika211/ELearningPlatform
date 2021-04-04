using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AddedTableAdministratori : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administratori",
                columns: table => new
                {
                    AdministratorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administratori", x => x.AdministratorId);
                });

            migrationBuilder.InsertData(
                table: "Administratori",
                columns: new[] { "AdministratorId", "Ime", "Password", "Prezime", "Username" },
                values: new object[] { 1, "Tatjana", "ts", "Stojanovic", "ts" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administratori");
        }
    }
}

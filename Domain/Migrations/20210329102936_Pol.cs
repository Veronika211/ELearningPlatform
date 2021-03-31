using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class Pol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Polaganje_Testovi_TestId1_TestKursId",
                table: "Polaganje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Polaganje",
                table: "Polaganje");

            migrationBuilder.DropIndex(
                name: "IX_Polaganje_TestId1_TestKursId",
                table: "Polaganje");

            migrationBuilder.DropColumn(
                name: "TestId1",
                table: "Polaganje");

            migrationBuilder.AlterColumn<int>(
                name: "TestKursId",
                table: "Polaganje",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TestId",
                table: "Polaganje",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PolaganjeId",
                table: "Polaganje",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Polaganje",
                table: "Polaganje",
                column: "PolaganjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Polaganje_KorisnikId",
                table: "Polaganje",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Polaganje_TestId_TestKursId",
                table: "Polaganje",
                columns: new[] { "TestId", "TestKursId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Polaganje_Testovi_TestId_TestKursId",
                table: "Polaganje",
                columns: new[] { "TestId", "TestKursId" },
                principalTable: "Testovi",
                principalColumns: new[] { "TestId", "KursId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Polaganje_Testovi_TestId_TestKursId",
                table: "Polaganje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Polaganje",
                table: "Polaganje");

            migrationBuilder.DropIndex(
                name: "IX_Polaganje_KorisnikId",
                table: "Polaganje");

            migrationBuilder.DropIndex(
                name: "IX_Polaganje_TestId_TestKursId",
                table: "Polaganje");

            migrationBuilder.DropColumn(
                name: "PolaganjeId",
                table: "Polaganje");

            migrationBuilder.AlterColumn<int>(
                name: "TestKursId",
                table: "Polaganje",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TestId",
                table: "Polaganje",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestId1",
                table: "Polaganje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Polaganje",
                table: "Polaganje",
                columns: new[] { "KorisnikId", "TestId" });

            migrationBuilder.CreateIndex(
                name: "IX_Polaganje_TestId1_TestKursId",
                table: "Polaganje",
                columns: new[] { "TestId1", "TestKursId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Polaganje_Testovi_TestId1_TestKursId",
                table: "Polaganje",
                columns: new[] { "TestId1", "TestKursId" },
                principalTable: "Testovi",
                principalColumns: new[] { "TestId", "KursId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}

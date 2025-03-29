using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class addcampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorTitularId",
                table: "Sustituciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdDoctorTitular",
                table: "Sustituciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Provincias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sustituciones_DoctorTitularId",
                table: "Sustituciones",
                column: "DoctorTitularId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sustituciones_Personas_DoctorTitularId",
                table: "Sustituciones",
                column: "DoctorTitularId",
                principalTable: "Personas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sustituciones_Personas_DoctorTitularId",
                table: "Sustituciones");

            migrationBuilder.DropIndex(
                name: "IX_Sustituciones_DoctorTitularId",
                table: "Sustituciones");

            migrationBuilder.DropColumn(
                name: "DoctorTitularId",
                table: "Sustituciones");

            migrationBuilder.DropColumn(
                name: "IdDoctorTitular",
                table: "Sustituciones");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Provincias");
        }
    }
}

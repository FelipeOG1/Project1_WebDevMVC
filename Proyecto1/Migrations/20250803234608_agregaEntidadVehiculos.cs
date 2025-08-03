using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1.Migrations
{
    /// <inheritdoc />
    public partial class agregaEntidadVehiculos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Placa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Traccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimaFechaAtencion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TratamientoEspecialNano = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Placa);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehiculos");
        }
    }
}

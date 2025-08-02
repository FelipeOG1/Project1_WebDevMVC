using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1.Migrations
{
    /// <inheritdoc />
    public partial class AgregaEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Cedula = table.Column<int>(type: "int", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalarioPorDia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiasVacacionesAcumulados = table.Column<int>(type: "int", nullable: false),
                    FechaRetiro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontoLiquidacion = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Cedula);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleados");
        }
    }
}

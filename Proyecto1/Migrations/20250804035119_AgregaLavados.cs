using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1.Migrations
{
    /// <inheritdoc />
    public partial class AgregaLavados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lavados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehiculoPlaca = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClienteIdentificacion = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    nombreTipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    precio_con_iva = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lavados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lavados_Clientes_ClienteIdentificacion",
                        column: x => x.ClienteIdentificacion,
                        principalTable: "Clientes",
                        principalColumn: "Identificacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lavados_Vehiculos_VehiculoPlaca",
                        column: x => x.VehiculoPlaca,
                        principalTable: "Vehiculos",
                        principalColumn: "Placa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoLavado",
                columns: table => new
                {
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prestaciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lavados_ClienteIdentificacion",
                table: "Lavados",
                column: "ClienteIdentificacion");

            migrationBuilder.CreateIndex(
                name: "IX_Lavados_VehiculoPlaca",
                table: "Lavados",
                column: "VehiculoPlaca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lavados");

            migrationBuilder.DropTable(
                name: "TipoLavado");
        }
    }
}

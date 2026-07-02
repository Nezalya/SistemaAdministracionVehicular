using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAV.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caracteristicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Categoria = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caracteristicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Nif = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Concesionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Nif = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concesionarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaisOrigen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidosFabrica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcesionarioId = table.Column<int>(type: "int", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEstimadaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TotalPedido = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosFabrica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidosFabrica_Concesionarios_ConcesionarioId",
                        column: x => x.ConcesionarioId,
                        principalTable: "Concesionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiciosOficiales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcesionarioId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Nif = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiciosOficiales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiciosOficiales_Concesionarios_ConcesionarioId",
                        column: x => x.ConcesionarioId,
                        principalTable: "Concesionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcesionarioId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Nif = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendedores_Concesionarios_ConcesionarioId",
                        column: x => x.ConcesionarioId,
                        principalTable: "Concesionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarcaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrecioBase = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    PotenciaFiscal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Cilindrada = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modelos_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    VendedorId = table.Column<int>(type: "int", nullable: true),
                    ConcesionarioId = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Concesionarios_ConcesionarioId",
                        column: x => x.ConcesionarioId,
                        principalTable: "Concesionarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuarios_Vendedores_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArticulosPedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    TipoArticulo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ModeloId = table.Column<int>(type: "int", nullable: true),
                    CaracteristicaId = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticulosPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticulosPedido_Caracteristicas_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristicas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArticulosPedido_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArticulosPedido_PedidosFabrica_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "PedidosFabrica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Automoviles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModeloId = table.Column<int>(type: "int", nullable: false),
                    NumBastidor = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AnoFabricacion = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ConcesionarioPropietarioId = table.Column<int>(type: "int", nullable: false),
                    ConcesionarioUbicacionId = table.Column<int>(type: "int", nullable: true),
                    ServicioOficialUbicacionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automoviles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Automoviles_Concesionarios_ConcesionarioPropietarioId",
                        column: x => x.ConcesionarioPropietarioId,
                        principalTable: "Concesionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Automoviles_Concesionarios_ConcesionarioUbicacionId",
                        column: x => x.ConcesionarioUbicacionId,
                        principalTable: "Concesionarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Automoviles_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Automoviles_ServiciosOficiales_ServicioOficialUbicacionId",
                        column: x => x.ServicioOficialUbicacionId,
                        principalTable: "ServiciosOficiales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModelosExtra",
                columns: table => new
                {
                    ModeloId = table.Column<int>(type: "int", nullable: false),
                    CaracteristicaId = table.Column<int>(type: "int", nullable: false),
                    PrecioExtra = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelosExtra", x => new { x.ModeloId, x.CaracteristicaId });
                    table.ForeignKey(
                        name: "FK_ModelosExtra_Caracteristicas_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModelosExtra_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelosSerie",
                columns: table => new
                {
                    ModeloId = table.Column<int>(type: "int", nullable: false),
                    CaracteristicaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelosSerie", x => new { x.ModeloId, x.CaracteristicaId });
                    table.ForeignKey(
                        name: "FK_ModelosSerie_Caracteristicas_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModelosSerie_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutomovilId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    VendedorId = table.Column<int>(type: "int", nullable: true),
                    ServicioOficialId = table.Column<int>(type: "int", nullable: true),
                    PedidoFabricaId = table.Column<int>(type: "int", nullable: true),
                    PrecioCobrado = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    FormaPago = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Financiera = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaVenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Matricula = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Origen = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_Automoviles_AutomovilId",
                        column: x => x.AutomovilId,
                        principalTable: "Automoviles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventas_PedidosFabrica_PedidoFabricaId",
                        column: x => x.PedidoFabricaId,
                        principalTable: "PedidosFabrica",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ventas_ServiciosOficiales_ServicioOficialId",
                        column: x => x.ServicioOficialId,
                        principalTable: "ServiciosOficiales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ventas_Vendedores_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VentasExtra",
                columns: table => new
                {
                    VentaId = table.Column<int>(type: "int", nullable: false),
                    CaracteristicaId = table.Column<int>(type: "int", nullable: false),
                    PrecioCobrado = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasExtra", x => new { x.VentaId, x.CaracteristicaId });
                    table.ForeignKey(
                        name: "FK_VentasExtra_Caracteristicas_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VentasExtra_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticulosPedido_CaracteristicaId",
                table: "ArticulosPedido",
                column: "CaracteristicaId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticulosPedido_ModeloId",
                table: "ArticulosPedido",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticulosPedido_PedidoId",
                table: "ArticulosPedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Automoviles_ConcesionarioPropietarioId",
                table: "Automoviles",
                column: "ConcesionarioPropietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Automoviles_ConcesionarioUbicacionId",
                table: "Automoviles",
                column: "ConcesionarioUbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Automoviles_ModeloId",
                table: "Automoviles",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Automoviles_NumBastidor",
                table: "Automoviles",
                column: "NumBastidor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Automoviles_ServicioOficialUbicacionId",
                table: "Automoviles",
                column: "ServicioOficialUbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Nif",
                table: "Clientes",
                column: "Nif",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Concesionarios_Nif",
                table: "Concesionarios",
                column: "Nif",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_Nombre",
                table: "Marcas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_MarcaId",
                table: "Modelos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelosExtra_CaracteristicaId",
                table: "ModelosExtra",
                column: "CaracteristicaId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelosSerie_CaracteristicaId",
                table: "ModelosSerie",
                column: "CaracteristicaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosFabrica_ConcesionarioId",
                table: "PedidosFabrica",
                column: "ConcesionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosOficiales_ConcesionarioId",
                table: "ServiciosOficiales",
                column: "ConcesionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosOficiales_Nif",
                table: "ServiciosOficiales",
                column: "Nif",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ConcesionarioId",
                table: "Usuarios",
                column: "ConcesionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Username",
                table: "Usuarios",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_VendedorId",
                table: "Usuarios",
                column: "VendedorId",
                unique: true,
                filter: "[VendedorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_ConcesionarioId",
                table: "Vendedores",
                column: "ConcesionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_Nif",
                table: "Vendedores",
                column: "Nif",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_AutomovilId",
                table: "Ventas",
                column: "AutomovilId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ClienteId",
                table: "Ventas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_PedidoFabricaId",
                table: "Ventas",
                column: "PedidoFabricaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ServicioOficialId",
                table: "Ventas",
                column: "ServicioOficialId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_VendedorId",
                table: "Ventas",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_VentasExtra_CaracteristicaId",
                table: "VentasExtra",
                column: "CaracteristicaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticulosPedido");

            migrationBuilder.DropTable(
                name: "ModelosExtra");

            migrationBuilder.DropTable(
                name: "ModelosSerie");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "VentasExtra");

            migrationBuilder.DropTable(
                name: "Caracteristicas");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Automoviles");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "PedidosFabrica");

            migrationBuilder.DropTable(
                name: "Vendedores");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "ServiciosOficiales");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Concesionarios");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alimentacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alimentacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaLaboral",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Sueldo = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaLaboral", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ecosistema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecosistema", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zona",
                columns: table => new
                {
                    ZonaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EcosistemaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zona", x => x.ZonaId);
                    table.ForeignKey(
                        name: "FK_Zona_Ecosistema",
                        column: x => x.EcosistemaId,
                        principalTable: "Ecosistema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Alimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    StockMinimo = table.Column<int>(type: "int", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Calorias = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alimento_Proveedor",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Jaula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jaula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jaula_Zona",
                        column: x => x.ZonaId,
                        principalTable: "Zona",
                        principalColumn: "ZonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Especie = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NombrePopular = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    EcosistemaId = table.Column<int>(type: "int", nullable: true),
                    UrlImagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    JaulaId = table.Column<int>(type: "int", nullable: true),
                    AlimentacionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animal_Alimentacion",
                        column: x => x.AlimentacionId,
                        principalTable: "Alimentacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Animal_Ecosistema",
                        column: x => x.EcosistemaId,
                        principalTable: "Ecosistema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Animal_Jaula",
                        column: x => x.JaulaId,
                        principalTable: "Jaula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apodo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Cp = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CategoriasId = table.Column<int>(type: "int", nullable: true),
                    JaulaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleado_CategoriaLaboral",
                        column: x => x.CategoriasId,
                        principalTable: "CategoriaLaboral",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleado_Jaula",
                        column: x => x.JaulaId,
                        principalTable: "Jaula",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Dosis",
                columns: table => new
                {
                    DosisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: true),
                    AlimentoId = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dosis", x => x.DosisId);
                    table.ForeignKey(
                        name: "FK_Dosis_Alimento",
                        column: x => x.AlimentoId,
                        principalTable: "Alimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dosis_Animal",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alimentacion_Nombre",
                table: "Alimentacion",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alimento_Nombre",
                table: "Alimento",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alimento_ProveedorId",
                table: "Alimento",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_AlimentacionId",
                table: "Animal",
                column: "AlimentacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_EcosistemaId",
                table: "Animal",
                column: "EcosistemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_JaulaId",
                table: "Animal",
                column: "JaulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_NombrePopular",
                table: "Animal",
                column: "NombrePopular");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaLaboral_Descripcion",
                table: "CategoriaLaboral",
                column: "Descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dosis_AlimentoId",
                table: "Dosis",
                column: "AlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Dosis_AnimalId",
                table: "Dosis",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Ecosistema_Descripcion",
                table: "Ecosistema",
                column: "Descripcion");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_CategoriasId",
                table: "Empleado",
                column: "CategoriasId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_JaulaId",
                table: "Empleado",
                column: "JaulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Jaula_Codigo",
                table: "Jaula",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jaula_ZonaId",
                table: "Jaula",
                column: "ZonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_Nombre",
                table: "Proveedor",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Zona_EcosistemaId",
                table: "Zona",
                column: "EcosistemaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dosis");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Alimento");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "CategoriaLaboral");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "Alimentacion");

            migrationBuilder.DropTable(
                name: "Jaula");

            migrationBuilder.DropTable(
                name: "Zona");

            migrationBuilder.DropTable(
                name: "Ecosistema");
        }
    }
}

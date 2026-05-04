using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GestionLegalP.Migrations
{
    /// <inheritdoc />
    public partial class IncialGestionLegal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CasoLegal",
                columns: table => new
                {
                    Id_CasoLegal = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    TipoCaso = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    FechaApertura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstadoCaso = table.Column<string>(type: "text", nullable: false),
                    Prioridad = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasoLegal", x => x.Id_CasoLegal);
                });

            migrationBuilder.CreateTable(
                name: "Consentimiento",
                columns: table => new
                {
                    Id_Consentimiento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    TipoTratamiento = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Firmado = table.Column<bool>(type: "boolean", nullable: false),
                    NombreFirmante = table.Column<string>(type: "text", nullable: false),
                    ParentescoFirmante = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consentimiento", x => x.Id_Consentimiento);
                });

            migrationBuilder.CreateTable(
                name: "DocumentoLegal",
                columns: table => new
                {
                    Id_DocumentoLegal = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Formato = table.Column<string>(type: "text", nullable: false),
                    ArchivoUrl = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentoLegal", x => x.Id_DocumentoLegal);
                });

            migrationBuilder.CreateTable(
                name: "Regla",
                columns: table => new
                {
                    Id_Regla = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    TipoRegla = table.Column<string>(type: "text", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regla", x => x.Id_Regla);
                });

            migrationBuilder.CreateTable(
                name: "Solicitud",
                columns: table => new
                {
                    Id_Solicitud = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    TipoSolicitud = table.Column<string>(type: "text", nullable: false),
                    Motivo = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitud", x => x.Id_Solicitud);
                });

            migrationBuilder.CreateTable(
                name: "CasoInvolucrado",
                columns: table => new
                {
                    Id_CasoInvolucrado = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Id_CasoLegal = table.Column<int>(type: "integer", nullable: false),
                    RolInvolucrado = table.Column<string>(type: "text", nullable: false),
                    DescripcionParticipacion = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasoInvolucrado", x => x.Id_CasoInvolucrado);
                    table.ForeignKey(
                        name: "FK_CasoInvolucrado_CasoLegal_Id_CasoLegal",
                        column: x => x.Id_CasoLegal,
                        principalTable: "CasoLegal",
                        principalColumn: "Id_CasoLegal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CasoDocumento",
                columns: table => new
                {
                    Id_CasoDocumento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Id_CasoLegal = table.Column<int>(type: "integer", nullable: false),
                    Id_DocumentoLegal = table.Column<int>(type: "integer", nullable: false),
                    TipoRelacion = table.Column<string>(type: "text", nullable: false),
                    FechaAdjunta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasoDocumento", x => x.Id_CasoDocumento);
                    table.ForeignKey(
                        name: "FK_CasoDocumento_CasoLegal_Id_CasoLegal",
                        column: x => x.Id_CasoLegal,
                        principalTable: "CasoLegal",
                        principalColumn: "Id_CasoLegal",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CasoDocumento_DocumentoLegal_Id_DocumentoLegal",
                        column: x => x.Id_DocumentoLegal,
                        principalTable: "DocumentoLegal",
                        principalColumn: "Id_DocumentoLegal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsentimientoDocumento",
                columns: table => new
                {
                    Id_ConsentimientoDocumento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Id_Consentimiento = table.Column<int>(type: "integer", nullable: false),
                    Id_DocumentoLegal = table.Column<int>(type: "integer", nullable: false),
                    FechaAsociacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsentimientoDocumento", x => x.Id_ConsentimientoDocumento);
                    table.ForeignKey(
                        name: "FK_ConsentimientoDocumento_Consentimiento_Id_Consentimiento",
                        column: x => x.Id_Consentimiento,
                        principalTable: "Consentimiento",
                        principalColumn: "Id_Consentimiento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsentimientoDocumento_DocumentoLegal_Id_DocumentoLegal",
                        column: x => x.Id_DocumentoLegal,
                        principalTable: "DocumentoLegal",
                        principalColumn: "Id_DocumentoLegal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentoDecision",
                columns: table => new
                {
                    Id_DocumentoDecision = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Id_DocumentoLegal = table.Column<int>(type: "integer", nullable: false),
                    TipoDecision = table.Column<string>(type: "text", nullable: false),
                    FechaDecision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Observacion = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentoDecision", x => x.Id_DocumentoDecision);
                    table.ForeignKey(
                        name: "FK_DocumentoDecision_DocumentoLegal_Id_DocumentoLegal",
                        column: x => x.Id_DocumentoLegal,
                        principalTable: "DocumentoLegal",
                        principalColumn: "Id_DocumentoLegal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudRevision",
                columns: table => new
                {
                    Id_SolicitudRevision = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Id_Solicitud = table.Column<int>(type: "integer", nullable: false),
                    FechaRevision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Resultado = table.Column<string>(type: "text", nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudRevision", x => x.Id_SolicitudRevision);
                    table.ForeignKey(
                        name: "FK_SolicitudRevision_Solicitud_Id_Solicitud",
                        column: x => x.Id_Solicitud,
                        principalTable: "Solicitud",
                        principalColumn: "Id_Solicitud",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CasoDocumento_Id_CasoLegal",
                table: "CasoDocumento",
                column: "Id_CasoLegal");

            migrationBuilder.CreateIndex(
                name: "IX_CasoDocumento_Id_DocumentoLegal",
                table: "CasoDocumento",
                column: "Id_DocumentoLegal");

            migrationBuilder.CreateIndex(
                name: "IX_CasoInvolucrado_Id_CasoLegal",
                table: "CasoInvolucrado",
                column: "Id_CasoLegal");

            migrationBuilder.CreateIndex(
                name: "IX_ConsentimientoDocumento_Id_Consentimiento",
                table: "ConsentimientoDocumento",
                column: "Id_Consentimiento");

            migrationBuilder.CreateIndex(
                name: "IX_ConsentimientoDocumento_Id_DocumentoLegal",
                table: "ConsentimientoDocumento",
                column: "Id_DocumentoLegal");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoDecision_Id_DocumentoLegal",
                table: "DocumentoDecision",
                column: "Id_DocumentoLegal");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudRevision_Id_Solicitud",
                table: "SolicitudRevision",
                column: "Id_Solicitud");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CasoDocumento");

            migrationBuilder.DropTable(
                name: "CasoInvolucrado");

            migrationBuilder.DropTable(
                name: "ConsentimientoDocumento");

            migrationBuilder.DropTable(
                name: "DocumentoDecision");

            migrationBuilder.DropTable(
                name: "Regla");

            migrationBuilder.DropTable(
                name: "SolicitudRevision");

            migrationBuilder.DropTable(
                name: "CasoLegal");

            migrationBuilder.DropTable(
                name: "Consentimiento");

            migrationBuilder.DropTable(
                name: "DocumentoLegal");

            migrationBuilder.DropTable(
                name: "Solicitud");
        }
    }
}

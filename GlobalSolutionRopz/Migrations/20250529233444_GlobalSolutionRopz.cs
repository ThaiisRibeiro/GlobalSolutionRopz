using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalSolutionRopz.Migrations
{
    /// <inheritdoc />
    public partial class GlobalSolutionRopz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Api_Global_Dotnet_Alerta",
                columns: table => new
                {
                    id_alerta = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    tipo_mensagem = table.Column<string>(name: "tipo_mensagem  ", type: "NVARCHAR2(2000)", nullable: false),
                    estado = table.Column<string>(name: "estado ", type: "NVARCHAR2(2000)", nullable: false),
                    temperatura = table.Column<int>(name: "temperatura ", type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Api_Global_Dotnet_Alerta", x => x.id_alerta);
                });

            migrationBuilder.CreateTable(
                name: "Api_Global_Dotnet_Mensagem",
                columns: table => new
                {
                    tipo_mensagem = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Api_Global_Dotnet_Mensagem", x => x.tipo_mensagem);
                });

            migrationBuilder.CreateTable(
                name: "Api_Global_Dotnet_Usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    estado = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    cep = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    telefone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Api_Global_Dotnet_Usuario", x => x.id_usuario);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Api_Global_Dotnet_Alerta");

            migrationBuilder.DropTable(
                name: "Api_Global_Dotnet_Mensagem");

            migrationBuilder.DropTable(
                name: "Api_Global_Dotnet_Usuario");
        }
    }
}

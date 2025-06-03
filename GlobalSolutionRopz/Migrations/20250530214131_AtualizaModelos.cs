using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalSolutionRopz.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaModelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "tipo_mensagem  ",
                table: "Api_Global_Dotnet_Alerta",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tipo_mensagem  ",
                table: "Api_Global_Dotnet_Alerta",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");
        }
    }
}

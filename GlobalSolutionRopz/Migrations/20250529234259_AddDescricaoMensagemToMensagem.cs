using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalSolutionRopz.Migrations
{
    /// <inheritdoc />
    public partial class AddDescricaoMensagemToMensagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "descricao_mensagem",
                table: "Api_Global_Dotnet_Mensagem",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descricao_mensagem",
                table: "Api_Global_Dotnet_Mensagem");
        }
    }
}

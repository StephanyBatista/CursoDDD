using Microsoft.EntityFrameworkCore.Migrations;

namespace Manutencao.Solicitacao.Infra.BancoDeDados.Migrations
{
    public partial class AdicionaAprovador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Aprovador_Identificador",
                table: "SolicitacaoDeManutecao",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Aprovador_Nome",
                table: "SolicitacaoDeManutecao",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aprovador_Identificador",
                table: "SolicitacaoDeManutecao");

            migrationBuilder.DropColumn(
                name: "Aprovador_Nome",
                table: "SolicitacaoDeManutecao");
        }
    }
}

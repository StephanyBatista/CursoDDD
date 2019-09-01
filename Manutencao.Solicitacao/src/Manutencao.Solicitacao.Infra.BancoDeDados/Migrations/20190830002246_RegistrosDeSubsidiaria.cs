using Manutencao.Solicitacao.Infra.BancoDeDados.Contexto;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Manutencao.Solicitacao.Infra.BancoDeDados.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190830002246_RegistrosDeSubsidiaria")]
    public class RegistrosDeSubsidiaria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Subsidiarias VALUES (NEWID(), 'Brasil');");
            migrationBuilder.Sql("INSERT INTO Subsidiarias VALUES (NEWID(), 'Argentina');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitacaoDeManutecao");

            migrationBuilder.DropTable(
                name: "Subsidiarias");
        }
    }
}

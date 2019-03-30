using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Manutencao.Solicitacao.Infra.BancoDeDados.Migrations
{
    public partial class AddSolicitacaoDeManutencao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitacaoDeManutecao",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Solicitante_Id = table.Column<int>(nullable: false),
                    Solicitante_Nome = table.Column<string>(nullable: true),
                    TipoDeSolicitacaoDeManutencao = table.Column<int>(nullable: false),
                    Justificativa = table.Column<string>(nullable: true),
                    Contrato_Numero = table.Column<string>(nullable: true),
                    Contrato_NomeDaEmpresa = table.Column<string>(nullable: true),
                    Contrato_CnpjDaEmpresa = table.Column<string>(nullable: true),
                    Contrato_DataFinalDaVigencia = table.Column<DateTime>(nullable: false),
                    InicioDesejadoParaManutencao = table.Column<DateTime>(nullable: false),
                    DataDaSolicitacao = table.Column<DateTime>(nullable: false),
                    StatusDaSolicitacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoDeManutecao", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitacaoDeManutecao");
        }
    }
}

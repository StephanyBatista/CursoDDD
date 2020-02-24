using System;

namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public class SolicitacaoDeManutencao : Entidade
    {
        public Autor Solicitante { get; private set; }
        public Autor Aprovador { get; private set; }
        public string IdentificadorDaSubsidiaria { get; private set; }
        public TipoDeSolicitacaoDeManutencao TipoDeSolicitacaoDeManutencao { get; }
        public string Justificativa { get; private set; }
        public Contrato Contrato { get; private set; }  
        public DateTime InicioDesejadoParaManutencao { get; private set; }
        public DateTime DataDaSolicitacao { get; private set; }
        public StatusDaSolicitacao StatusDaSolicitacao { get; private set; }

        private SolicitacaoDeManutencao() { }

        public SolicitacaoDeManutencao(string identificadorDaSubsidiaria, 
            int identificadorDoSolicitante, string nomeDoSolicitante,
            TipoDeSolicitacaoDeManutencao tipoDeSolicitacaoDeManutencao,
            string justificativa, string numeroDoContrato, string nomeDaTerceirizada, string cnpjDaTerceirizada, 
            string gestorDoContrato, DateTime dataFinalDaVigência,
            DateTime inicioDesejadoParaManutencao)
        {
            ExcecaoDeDominio.LancarQuando(string.IsNullOrEmpty(identificadorDaSubsidiaria), "Subsidiária é inválida");
            ExcecaoDeDominio.LancarQuando(string.IsNullOrEmpty(justificativa), "Justificativa inválida");
            ExcecaoDeDominio.LancarQuando(inicioDesejadoParaManutencao.Date < DateTime.Now.Date, "Data do inicio desejado não pode ser inferior a data de hoje");

            Solicitante = new Autor(identificadorDoSolicitante, nomeDoSolicitante);
            IdentificadorDaSubsidiaria = identificadorDaSubsidiaria;
            TipoDeSolicitacaoDeManutencao = tipoDeSolicitacaoDeManutencao;
            Justificativa = justificativa;
            Contrato = new Contrato(numeroDoContrato, nomeDaTerceirizada, cnpjDaTerceirizada, gestorDoContrato, dataFinalDaVigência);
            InicioDesejadoParaManutencao = inicioDesejadoParaManutencao;
            DataDaSolicitacao = DateTime.Now;
            StatusDaSolicitacao = StatusDaSolicitacao.Pendente;
            //TODO: Por motivos do EF Core, sempre um owned deve ser atribuido :(.
            Aprovador = new Autor(0, "Sem aprovador");
        }

        public void Cancelar()
        {
            StatusDaSolicitacao = StatusDaSolicitacao.Cancelada;
        }

        public void Reprovar(Autor aprovador)
        {
            Aprovador = aprovador;
            StatusDaSolicitacao = StatusDaSolicitacao.Reprovada;
        }

        public void Aprovar(Autor aprovador)
        {
            Aprovador = aprovador;
            StatusDaSolicitacao = StatusDaSolicitacao.Aprovada;
        }
    }
}
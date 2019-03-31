using System;
using Manutencao.Solicitacao.Dominio;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public class SolicitadorDeManutencao
    {
        private readonly ISolicitacaoDeManutencaoRepositorio _solicitacaoDeManutencaoRepositorio;
        private readonly IBuscadorDeContrato _buscadorDeContrato;
        private readonly ICanceladorDeSolicitacoesDeManutencaoPendentes _canceladorDeSolicitacoesDeManutencaoPendentes;

        public SolicitadorDeManutencao(ISolicitacaoDeManutencaoRepositorio solicitacaoDeManutencaoRepositorio,
            IBuscadorDeContrato buscadorDeContrato,
            ICanceladorDeSolicitacoesDeManutencaoPendentes canceladorDeSolicitacoesDeManutencaoPendentes)
        {
            _solicitacaoDeManutencaoRepositorio = solicitacaoDeManutencaoRepositorio;
            _buscadorDeContrato = buscadorDeContrato;
            _canceladorDeSolicitacoesDeManutencaoPendentes = canceladorDeSolicitacoesDeManutencaoPendentes;
        }

        public void Solicitar(SolicitacaoDeManutencaoDto dto)
        {
            var contratoDto = _buscadorDeContrato.Buscar(dto.NumeroDoContrato).Result;
            ExcecaoDeDominio.LancarQuando(contratoDto == null, "Contrato não encontrado no ERP");

            var tipoDeSolicitacaoDeManutencao =
                Enum.Parse<TipoDeSolicitacaoDeManutencao>(dto.TipoDeSolicitacaoDeManutencao.ToString());
            var solicitacaoDeManutencao = 
                new SolicitacaoDeManutencao(
                    dto.SolicitanteId, 
                    dto.NomeDoSolicitante,
                    tipoDeSolicitacaoDeManutencao, 
                    dto.Justificativa,
                    contratoDto.Numero, 
                    contratoDto.NomeDaEmpresa, 
                    contratoDto.CnpjDaEmpresa, 
                    contratoDto.DataFinalDaVigencia,
                    dto.InicioDesejadoParaManutencao);

            var solicitacoesDeManutencaoPendentes =
                _solicitacaoDeManutencaoRepositorio.ObterPendentesDoTipo(tipoDeSolicitacaoDeManutencao);
            _canceladorDeSolicitacoesDeManutencaoPendentes.Cancelar(solicitacoesDeManutencaoPendentes);

            _solicitacaoDeManutencaoRepositorio.Adicionar(solicitacaoDeManutencao);
        }
    }
}
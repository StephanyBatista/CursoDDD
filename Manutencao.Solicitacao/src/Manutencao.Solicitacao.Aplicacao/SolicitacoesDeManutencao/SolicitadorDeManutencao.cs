using System;
using Manutencao.Solicitacao.Aplicacao.Subsidiarias;
using Manutencao.Solicitacao.Dominio;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public class SolicitadorDeManutencao
    {
        private readonly ISolicitacaoDeManutencaoRepositorio _solicitacaoDeManutencaoRepositorio;
        private readonly ISubsidiariaRepositorio _subsidiariaRepositorio;
        private readonly IBuscadorDeContrato _buscadorDeContrato;
        private readonly ICanceladorDeSolicitacoesDeManutencaoPendentes _canceladorDeSolicitacoesDeManutencaoPendentes;

        public SolicitadorDeManutencao(ISolicitacaoDeManutencaoRepositorio solicitacaoDeManutencaoRepositorio,
            ISubsidiariaRepositorio subsidiariaRepositorio,
            IBuscadorDeContrato buscadorDeContrato,
            ICanceladorDeSolicitacoesDeManutencaoPendentes canceladorDeSolicitacoesDeManutencaoPendentes)
        {
            _solicitacaoDeManutencaoRepositorio = solicitacaoDeManutencaoRepositorio;
            _subsidiariaRepositorio = subsidiariaRepositorio;
            _buscadorDeContrato = buscadorDeContrato;
            _canceladorDeSolicitacoesDeManutencaoPendentes = canceladorDeSolicitacoesDeManutencaoPendentes;
        }

        public void Solicitar(SolicitacaoDeManutencaoDto dto)
        {
            var solicitacao = _subsidiariaRepositorio.ObterPorId(dto.SubsidiariaId);

            var contratoDto = _buscadorDeContrato.Buscar(dto.NumeroDoContrato).Result;
            ExcecaoDeDominio.LancarQuando(contratoDto == null, "Contrato não encontrado no ERP");

            var tipoDeSolicitacaoDeManutencao =
                Enum.Parse<TipoDeSolicitacaoDeManutencao>(dto.TipoDeSolicitacaoDeManutencao.ToString());
            var solicitacaoDeManutencao = 
                new SolicitacaoDeManutencao(
                    solicitacao,
                    dto.SolicitanteId, 
                    dto.NomeDoSolicitante,
                    tipoDeSolicitacaoDeManutencao, 
                    dto.Justificativa,
                    contratoDto.Numero, 
                    contratoDto.NomeDaTerceirizada, 
                    contratoDto.CnpjDaTerceirizada,
                    contratoDto.GestorDoContrato,
                    contratoDto.DataFinalDaVigencia,
                    dto.InicioDesejadoParaManutencao);

            var solicitacoesDeManutencaoPendentes =
                _solicitacaoDeManutencaoRepositorio.ObterPendentesDoTipo(tipoDeSolicitacaoDeManutencao);
            _canceladorDeSolicitacoesDeManutencaoPendentes.Cancelar(solicitacoesDeManutencaoPendentes);

            _solicitacaoDeManutencaoRepositorio.Adicionar(solicitacaoDeManutencao);
        }
    }
}
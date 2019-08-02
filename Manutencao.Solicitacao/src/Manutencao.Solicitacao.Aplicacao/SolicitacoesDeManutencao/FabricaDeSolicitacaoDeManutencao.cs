using System;
using Manutencao.Solicitacao.Aplicacao.Subsidiarias;
using Manutencao.Solicitacao.Dominio;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public class FabricaDeSolicitacaoDeManutencao
    {
        private readonly ISubsidiariaRepositorio _subsidiariaRepositorio;
        private readonly IBuscadorDeContrato _buscadorDeContrato;

        protected FabricaDeSolicitacaoDeManutencao() {}

        public FabricaDeSolicitacaoDeManutencao(ISubsidiariaRepositorio subsidiariaRepositorio, IBuscadorDeContrato buscadorDeContrato)
        {
            _subsidiariaRepositorio = subsidiariaRepositorio;
            _buscadorDeContrato = buscadorDeContrato;
        }

        public virtual SolicitacaoDeManutencao Fabricar(SolicitacaoDeManutencaoDto dto)
        {
            var subsidiaria = _subsidiariaRepositorio.ObterPorId(dto.SubsidiariaId);
            ExcecaoDeDominio.LancarQuando(subsidiaria == null, "Subsidiária não encontrada"); 

            var contratoDto = _buscadorDeContrato.Buscar(dto.NumeroDoContrato).Result;
            ExcecaoDeDominio.LancarQuando(contratoDto == null, "Contrato não encontrado no ERP");

            var tipoDeSolicitacaoDeManutencao =
                Enum.Parse<TipoDeSolicitacaoDeManutencao>(dto.TipoDeSolicitacaoDeManutencao.ToString());

            return new SolicitacaoDeManutencao(
                    subsidiaria.Id,
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
        }
    }
}

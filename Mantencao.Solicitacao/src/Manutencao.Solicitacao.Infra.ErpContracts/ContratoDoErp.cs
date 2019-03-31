using System.Runtime.Serialization;

namespace Manutencao.Solicitacao.Infra.ErpContracts
{
    public class ContratoDoErp
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "company")]
        public string Company { get; set; }
        [DataMember(Name = "companyid")]
        public string CompanyId { get; set; }
        [DataMember(Name = "finaldate")]
        public string FinalDate { get; set; }
    }
}

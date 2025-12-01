
namespace RadarRendaFixa.Api.Domain;

public enum TipoProduto
{
    Cdb,
    Lci,
    Lca,
    TesouroPrefixado,
    TesouroSelic,
    TesouroIpca,
    Debenture
}

public enum Indexador
{
    Cdi,
    Ipca,
    Prefixado
}

public enum PerfilRisco
{
    Conservador,
    Moderado,
    Turbo
}

public enum NivelRisco
{
    Baixo,
    Medio,
    Alto
}

public class OfertaRendaFixa
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Fonte { get; set; } = string.Empty;
    public string Corretora { get; set; } = string.Empty;
    public string Emissor { get; set; } = string.Empty;
    public string NomeProduto { get; set; } = string.Empty;
    public TipoProduto Tipo { get; set; }
    public bool GarantiaFgc { get; set; }
    public Indexador Indexador { get; set; }

    /// <summary>
    /// Exemplo: CDI 120% -> TaxaPrincipal = 1.20m
    /// Prefixado 11% -> TaxaPrincipal = 0.11m
    /// </summary>
    public decimal TaxaPrincipal { get; set; }

    /// <summary>
    /// Ex.: IPCA + 6 -> TaxaAdicional = 0.06m
    /// </summary>
    public decimal? TaxaAdicional { get; set; }

    public int PrazoEmDias { get; set; }
    public DateOnly DataVencimento { get; set; }
    public bool LiquidezDiaria { get; set; }
    public decimal Minimo { get; set; }
    public string UrlInvestir { get; set; } = string.Empty;
    public DateTime UltimaAtualizacao { get; set; } = DateTime.UtcNow;
}

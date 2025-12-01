
using RadarRendaFixa.Api.Domain;

namespace RadarRendaFixa.Api.Repositories;

public interface IOfertaRepository
{
    IEnumerable<OfertaRendaFixa> ListarOfertasAtivas();
}

public class InMemoryOfertaRepository : IOfertaRepository
{
    private readonly List<OfertaRendaFixa> _ofertas = new();

    public InMemoryOfertaRepository()
    {
        _ofertas.Add(new OfertaRendaFixa
        {
            Fonte = "Mock",
            Corretora = "Corretora A",
            Emissor = "Banco A",
            NomeProduto = "CDB Banco A 120% CDI",
            Tipo = TipoProduto.Cdb,
            GarantiaFgc = true,
            Indexador = Indexador.Cdi,
            TaxaPrincipal = 1.20m,
            PrazoEmDias = 720,
            DataVencimento = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(720)),
            LiquidezDiaria = false,
            Minimo = 1000,
            UrlInvestir = "https://corretoraa.com/cdb120"
        });

        _ofertas.Add(new OfertaRendaFixa
        {
            Fonte = "Mock",
            Corretora = "Corretora B",
            Emissor = "Banco B",
            NomeProduto = "LCI Banco B 95% CDI",
            Tipo = TipoProduto.Lci,
            GarantiaFgc = true,
            Indexador = Indexador.Cdi,
            TaxaPrincipal = 0.95m,
            PrazoEmDias = 365,
            DataVencimento = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(365)),
            LiquidezDiaria = false,
            Minimo = 5000,
            UrlInvestir = "https://corretorab.com/lci95"
        });

        _ofertas.Add(new OfertaRendaFixa
        {
            Fonte = "Mock",
            Corretora = "Corretora C",
            Emissor = "Tesouro Nacional",
            NomeProduto = "Tesouro IPCA+ 2035",
            Tipo = TipoProduto.TesouroIpca,
            GarantiaFgc = false,
            Indexador = Indexador.Ipca,
            TaxaPrincipal = 0.00m,
            TaxaAdicional = 0.06m,
            PrazoEmDias = 365 * 10,
            DataVencimento = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(10)),
            LiquidezDiaria = true,
            Minimo = 100,
            UrlInvestir = "https://tesourodireto.com.br/ipca2035"
        });
    }

    public IEnumerable<OfertaRendaFixa> ListarOfertasAtivas()
        => _ofertas;
}

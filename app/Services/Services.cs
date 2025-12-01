
using RadarRendaFixa.Api.Contracts;
using RadarRendaFixa.Api.Domain;
using RadarRendaFixa.Api.Repositories;

namespace RadarRendaFixa.Api.Services;

public interface ITabelaIrService
{
    decimal ObterAliquotaIr(int prazoEmDias);
}

public class TabelaIrService : ITabelaIrService
{
    public decimal ObterAliquotaIr(int prazoEmDias)
    {
        if (prazoEmDias <= 180) return 0.225m;
        if (prazoEmDias <= 360) return 0.20m;
        if (prazoEmDias <= 720) return 0.175m;
        return 0.15m;
    }
}

public interface ISimuladorRendaFixaService
{
    (decimal valorBruto, decimal valorLiquido, decimal rentabilidadeLiquidaAnual)
        Simular(OfertaRendaFixa oferta, decimal valor, int prazoEmDias);
}

public class SimuladorRendaFixaService : ISimuladorRendaFixaService
{
    private readonly ITabelaIrService _tabelaIrService;

    public SimuladorRendaFixaService(ITabelaIrService tabelaIrService)
    {
        _tabelaIrService = tabelaIrService;
    }

    // Valores simplificados para o MVP
    private const decimal CdiAnual = 0.13m;
    private const decimal IpcaAnual = 0.04m;

    public (decimal valorBruto, decimal valorLiquido, decimal rentabilidadeLiquidaAnual)
        Simular(OfertaRendaFixa oferta, decimal valor, int prazoEmDias)
    {
        var taxaBrutaAnual = CalcularTaxaBrutaAnual(oferta);
        var taxaBrutaAoDia = (decimal)Math.Pow(
            (double)(1 + taxaBrutaAnual),
            1.0 / 252.0
        ) - 1;

        var valorBruto = valor * (decimal)Math.Pow(
            (double)(1 + taxaBrutaAoDia),
            prazoEmDias
        );

        var ganhoBruto = valorBruto - valor;

        var aliquota = _tabelaIrService.ObterAliquotaIr(prazoEmDias);
        var imposto = ganhoBruto * aliquota;

        var valorLiquido = valorBruto - imposto;

        var rentabilidadeBrutaTotal = (valorLiquido / valor) - 1;
        var rentabilidadeLiquidaAnual = (decimal)Math.Pow(
            (double)(1 + rentabilidadeBrutaTotal),
            252.0 / prazoEmDias
        ) - 1;

        return (decimal.Round(valorBruto, 2),
                decimal.Round(valorLiquido, 2),
                decimal.Round(rentabilidadeLiquidaAnual, 4));
    }

    private decimal CalcularTaxaBrutaAnual(OfertaRendaFixa oferta)
    {
        return oferta.Indexador switch
        {
            Indexador.Cdi => CdiAnual * oferta.TaxaPrincipal,
            Indexador.Ipca => IpcaAnual + (oferta.TaxaAdicional ?? 0),
            Indexador.Prefixado => oferta.TaxaPrincipal,
            _ => oferta.TaxaPrincipal
        };
    }
}

public interface IRankingService
{
    RankingResponse ObterRanking(RankingRequest request);
}

public class RankingService : IRankingService
{
    private readonly IOfertaRepository _ofertaRepository;
    private readonly ISimuladorRendaFixaService _simulador;

    public RankingService(IOfertaRepository ofertaRepository, ISimuladorRendaFixaService simulador)
    {
        _ofertaRepository = ofertaRepository;
        _simulador = simulador;
    }

    public RankingResponse ObterRanking(RankingRequest request)
    {
        var ofertas = _ofertaRepository.ListarOfertasAtivas();

        ofertas = FiltrarPorPerfil(ofertas, request);

        var respostas = new List<OfertaSimuladaResponse>();

        foreach (var oferta in ofertas)
        {
            var (bruto, liquido, rentAnual) = _simulador.Simular(
                oferta,
                request.Valor,
                request.PrazoEmDias
            );

            var nivelRisco = CalcularNivelRisco(oferta);
            var score = CalcularScore(oferta, rentAnual, nivelRisco, request.PrazoEmDias, request.Perfil);

            respostas.Add(new OfertaSimuladaResponse(
                oferta.Id,
                oferta.NomeProduto,
                oferta.Corretora,
                oferta.Emissor,
                oferta.Tipo,
                oferta.Indexador,
                oferta.GarantiaFgc,
                oferta.LiquidezDiaria,
                oferta.TaxaPrincipal,
                request.Valor,
                bruto,
                liquido,
                rentAnual * 100,
                nivelRisco,
                score,
                oferta.UrlInvestir
            ));
        }

        var top = respostas
            .OrderByDescending(r => r.Score)
            .Take(10)
            .ToList();

        return new RankingResponse(top);
    }

    private IEnumerable<OfertaRendaFixa> FiltrarPorPerfil(IEnumerable<OfertaRendaFixa> ofertas, RankingRequest request)
    {
        return request.Perfil switch
        {
            PerfilRisco.Conservador =>
                ofertas.Where(o => o.GarantiaFgc && o.PrazoEmDias <= request.PrazoEmDias + 180),
            PerfilRisco.Moderado =>
                ofertas.Where(o => o.PrazoEmDias <= request.PrazoEmDias + 365),
            PerfilRisco.Turbo =>
                ofertas,
            _ => ofertas
        };
    }

    private NivelRisco CalcularNivelRisco(OfertaRendaFixa oferta)
    {
        if (oferta.GarantiaFgc && oferta.PrazoEmDias <= 720) return NivelRisco.Baixo;
        if (oferta.GarantiaFgc) return NivelRisco.Medio;
        return NivelRisco.Alto;
    }

    private decimal CalcularScore(
        OfertaRendaFixa oferta,
        decimal rentAnual,
        NivelRisco risco,
        int prazoSolicitado,
        PerfilRisco perfil)
    {
        var pesoRent = perfil == PerfilRisco.Turbo ? 0.8m : 0.6m;
        var pesoPrazo = perfil == PerfilRisco.Conservador ? 0.3m : 0.2m;
        var pesoRisco = 0.2m;

        var prazoAnos = oferta.PrazoEmDias / 365m;
        var fatorRisco = risco switch
        {
            NivelRisco.Baixo => 0,
            NivelRisco.Medio => 1,
            NivelRisco.Alto => 2,
            _ => 1
        };

        var diffPrazoAnos = Math.Abs(oferta.PrazoEmDias - prazoSolicitado) / 365m;

        var score = rentAnual * pesoRent
                    - diffPrazoAnos * pesoPrazo
                    - fatorRisco * pesoRisco;

        if (oferta.LiquidezDiaria && perfil != PerfilRisco.Turbo)
        {
            score += 0.02m;
        }

        return score;
    }
}

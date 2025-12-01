
using RadarRendaFixa.Api.Domain;

namespace RadarRendaFixa.Api.Contracts;

public record RankingRequest(
    decimal Valor,
    int PrazoEmDias,
    PerfilRisco Perfil
);

public record OfertaSimuladaResponse(
    Guid OfertaId,
    string NomeProduto,
    string Corretora,
    string Emissor,
    TipoProduto Tipo,
    Indexador Indexador,
    bool GarantiaFgc,
    bool LiquidezDiaria,
    decimal TaxaBruta,
    decimal ValorAplicado,
    decimal ValorBruto,
    decimal ValorLiquido,
    decimal RentabilidadeLiquidaPercentAnual,
    NivelRisco NivelRisco,
    decimal Score,
    string UrlInvestir
);

public record RankingResponse(
    IReadOnlyCollection<OfertaSimuladaResponse> Ofertas
);

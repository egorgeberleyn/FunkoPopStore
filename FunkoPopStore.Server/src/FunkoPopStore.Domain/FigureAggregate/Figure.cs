using FunkoPopStore.Domain.Common.Primitives;
using FunkoPopStore.Domain.FigureAggregate.Enums;
using FunkoPopStore.Domain.SeriesAggregate;

namespace FunkoPopStore.Domain.FigureAggregate;

public sealed class Figure : AggregateRoot
{
    public string Name { get; private set; }
        
    public Guid? SeriesId { get; private set; }

    public decimal Price { get; private set; }
        
    public Rarity Rarity { get; private set; }

    public DateTime ProductionYear { get; init; }

    public Series Series { get; set; } = null!;

    private Figure(Guid id, string name, decimal price, Rarity rarity, DateTime productionYear, Guid? seriesId = null)
        : base(id)
    {
        Name = name;
        Price = price;
        Rarity = rarity;
        ProductionYear = productionYear;
        SeriesId = seriesId;
    }

    public static Figure Create(string name, decimal price, Rarity rarity, 
        DateTime productionYear, Guid? seriesId = null) =>
        new(Guid.NewGuid(), name, price, rarity, productionYear, seriesId);

    public Figure Update(string name, decimal price, Rarity rarity, Guid? seriesId = null)
    {
        Name = name;
        Price = price;
        Rarity = rarity;
        SeriesId = seriesId;

        return this;
    }
}
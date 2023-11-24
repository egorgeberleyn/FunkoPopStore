namespace FunkoPopStore.Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class Figure
    {
        public static readonly Guid Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
        public static readonly Guid SeriesId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        public const string Name = "Funko Pop";
        public const string Rarity = "Common";
        public const decimal Price = 50;
        public static readonly DateTime ProductionOfYear = DateTime.Parse("05.07.2009");
        
        public static readonly Guid IncorrectId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
    }
}
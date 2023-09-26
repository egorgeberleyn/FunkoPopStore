namespace KittyStore.Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class Cat
    {
        public static readonly Guid Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
        public const string Name = "Bars";
        public const string Color = "blue";
        public const int Age = 12;
        public const string Breed = "kun'lun";
        public const string Gender = "Male";
        public const decimal Price = 50;

        public static readonly Guid IncorrectId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
    }
}
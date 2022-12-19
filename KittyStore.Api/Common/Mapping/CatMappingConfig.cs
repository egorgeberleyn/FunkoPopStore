using KittyStore.Application.Cats.Commands.CreateCat;
using KittyStore.Application.Cats.Commands.UpdateCat;
using KittyStore.Contracts.Admin.Cats;
using KittyStore.Domain.CatAggregate;
using Mapster;

namespace KittyStore.Api.Common.Mapping
{
    public class CatMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<List<Cat>, List<CatResponse>>();
        
            config.NewConfig<CreateCatRequest, CreateCatCommand>();

            config.NewConfig<UpdateCatRequest, UpdateCatCommand>();

            config.NewConfig<Cat, CatResponse>()
                .Map(dist => dist.Id, src => src.Id.Value);
        }
    }
}
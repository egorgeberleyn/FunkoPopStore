using FunkoPopStore.Application.Figures.Commands.CreateFigure;
using FunkoPopStore.Application.Figures.Commands.UpdateFigure;
using FunkoPopStore.Contracts.Admin.Figures;
using FunkoPopStore.Domain.FigureAggregate;
using Mapster;

namespace FunkoPopStore.Api.Common.Mapping;

public class FigureMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateFigureRequest, CreateFigureCommand>();

        config.NewConfig<UpdateFigureRequest, UpdateFigureCommand>();

        config.NewConfig<Figure, FigureResponse>();
    }
}
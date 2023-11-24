using ErrorOr;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using MediatR.Pipeline;

namespace FunkoPopStore.Application.Common.SaveChangesPostProcessor;

public class SaveChangesCommandPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    where TRequest : ICommand
    where TResponse : IErrorOr
{
    private readonly IAppDbContext _dbContext;

    public SaveChangesCommandPostProcessor(IAppDbContext context)
    {
        _dbContext = context;
    }

    public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken) =>
        await _dbContext.SaveChangesAsync(cancellationToken);
}
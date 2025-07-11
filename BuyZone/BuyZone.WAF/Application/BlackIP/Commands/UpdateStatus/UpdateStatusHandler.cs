using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.WAF.Application.BlackIP.Commands.UpdateStatus;

public class UpdateStatusHandler:IRequestHandler<UpdateStatusCommand.Request>
{
    private readonly IRepository _repository;

    public UpdateStatusHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateStatusCommand.Request request, CancellationToken cancellationToken)
    {
        var blackIp = await _repository.TrackingQuery<Domain.Entities.BlockIP>()
            .FirstOrDefaultAsync(b => b.Id == request.BlackIPId, cancellationToken);
        if(blackIp == null)
            throw new Exception("Black IP not found");
        blackIp.Status = request.Status;
        await _repository.SaveChangesAsync();
    }
}
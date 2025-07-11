using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.WAF.Application.BlackIP.Commands.Add;

public class AddBlockIPHandler:IRequestHandler<AddBlockIPCommand.Request>
{
    private readonly IRepository _repository;

    public AddBlockIPHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(AddBlockIPCommand.Request request, CancellationToken cancellationToken)
    {
        var blackIp=await _repository.TrackingQuery<Domain.Entities.BlockIP>()
            .FirstOrDefaultAsync(b => b.IpAddress == request.IpAddress, cancellationToken);
        if (blackIp is not null)
        {
            blackIp.Status = request.Status;
            await _repository.SaveChangesAsync();
            return;
        }
        blackIp = new Domain.Entities.BlockIP(request.IpAddress, request.Status);
        await _repository.AddAsync(blackIp);
        await _repository.SaveChangesAsync();
    }
}
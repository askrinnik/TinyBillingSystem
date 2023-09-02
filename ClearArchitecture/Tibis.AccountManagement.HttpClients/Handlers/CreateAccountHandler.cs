using MediatR;
using Tibis.AccountManagement.CQRS.Requests;

namespace Tibis.AccountManagement.HttpClients.Handlers;

internal class CreateAccountHandler : IRequestHandler<CreateAccountRequest, CQRS.Models.AccountDto>
{
    private readonly IAccountManagementClient _client;

    public CreateAccountHandler(IAccountManagementClient client) => _client = client;

    public async Task<CQRS.Models.AccountDto> Handle(CreateAccountRequest request, CancellationToken cancellationToken) => 
        await _client.CreateAsync(request, cancellationToken);
}
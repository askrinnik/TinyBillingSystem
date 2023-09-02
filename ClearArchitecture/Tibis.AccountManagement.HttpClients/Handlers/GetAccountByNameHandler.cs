using MediatR;
using Tibis.AccountManagement.CQRS.Models;
using Tibis.AccountManagement.CQRS.Requests;

namespace Tibis.AccountManagement.HttpClients.Handlers;

internal class GetAccountByNameHandler: IRequestHandler<GetAccountByNameRequest, AccountDto>
{
    private readonly IAccountManagementClient _client;

    public GetAccountByNameHandler(IAccountManagementClient client) => _client = client;

    public async Task<AccountDto> Handle(GetAccountByNameRequest request, CancellationToken cancellationToken) =>
        await _client.GetAccountAsync(request.Name, cancellationToken);
}
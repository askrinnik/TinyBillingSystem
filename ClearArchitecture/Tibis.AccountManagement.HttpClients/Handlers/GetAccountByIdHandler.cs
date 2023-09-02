using MediatR;
using Tibis.AccountManagement.CQRS.Models;
using Tibis.AccountManagement.CQRS.Requests;

namespace Tibis.AccountManagement.HttpClients.Handlers;

internal class GetAccountByIdHandler: IRequestHandler<GetAccountByIdRequest, AccountDto>
{
    private readonly IAccountManagementClient _client;

    public GetAccountByIdHandler(IAccountManagementClient client) => _client = client;

    public async Task<AccountDto> Handle(GetAccountByIdRequest request, CancellationToken cancellationToken) => 
        await _client.GetAccountAsync(request.Id, cancellationToken);
}
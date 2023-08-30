using MediatR;
using Tibis.Application.AccountManagement.Models;
using Tibis.Application.AccountManagement.Queries;
using Tibis.Domain.Interfaces;
using Tibis.Domain.AccountManagement;

namespace Tibis.Application.AccountManagement.Handlers;

public class GetAccountByNameHandler: IRequestHandler<GetAccountByNameRequest, AccountDto>
{
    private readonly IRetrieve<string, Account> _repository;

    public GetAccountByNameHandler(IRetrieve<string, Account> repository) => 
        _repository = repository;

    public async Task<AccountDto> Handle(GetAccountByNameRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.TryRetrieveAsync(request.Name);
        return item == null ? throw new AccountNotFoundException(request.Name) : AccountDto.From(item);
    }
}
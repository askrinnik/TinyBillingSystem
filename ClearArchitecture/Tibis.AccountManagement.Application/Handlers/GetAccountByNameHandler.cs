using MediatR;
using Tibis.AccountManagement.CQRS.Models;
using Tibis.AccountManagement.CQRS.Requests;
using Tibis.AccountManagement.Domain;
using Tibis.AccountManagement.Domain.Exceptions;
using Tibis.Contracts.Interfaces;

namespace Tibis.AccountManagement.Application.Handlers;

internal class GetAccountByNameHandler: IRequestHandler<GetAccountByNameRequest, AccountDto>
{
    private readonly IRetrieve<string, Account> _repository;

    public GetAccountByNameHandler(IRetrieve<string, Account> repository) => 
        _repository = repository;

    public async Task<AccountDto> Handle(GetAccountByNameRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.TryRetrieveAsync(request.Name);
        return item == null ? throw new AccountNotFoundException(request.Name) : item.ToDto();
    }
}
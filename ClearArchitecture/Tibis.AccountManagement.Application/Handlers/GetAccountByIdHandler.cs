using MediatR;
using Tibis.AccountManagement.CQRS.Models;
using Tibis.AccountManagement.CQRS.Requests;
using Tibis.AccountManagement.Domain;
using Tibis.AccountManagement.Domain.Exceptions;
using Tibis.Contracts.Interfaces;

namespace Tibis.AccountManagement.Application.Handlers;

internal class GetAccountByIdHandler: IRequestHandler<GetAccountByIdRequest, AccountDto>
{
    private readonly IRetrieve<Guid, Account> _repository;

    public GetAccountByIdHandler(IRetrieve<Guid, Account> repository) => 
        _repository = repository;

    public async Task<AccountDto> Handle(GetAccountByIdRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.TryRetrieveAsync(request.Id);
        return item == null ? throw new AccountNotFoundException(request.Id) : item.ToDto();
    }
}
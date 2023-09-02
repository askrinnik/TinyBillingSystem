using MediatR;
using Tibis.AccountManagement.CQRS.Models;
using Tibis.AccountManagement.CQRS.Requests;
using Tibis.AccountManagement.Domain;
using Tibis.Contracts.Interfaces;

namespace Tibis.AccountManagement.Application.Handlers;

internal class CreateAccountHandler : IRequestHandler<CreateAccountRequest, AccountDto>
{
    private readonly ICreate<Account> _repository;

    public CreateAccountHandler(ICreate<Account> repository) =>
        _repository = repository;

    public async Task<AccountDto> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.CreateAsync(new(request.Name));
        return item.ToDto();
    }
}
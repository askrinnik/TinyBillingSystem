using MediatR;
using Tibis.Application.AccountManagement.Models;
using Tibis.Application.AccountManagement.Queries;
using Tibis.Domain.AccountManagement;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.AccountManagement.Handlers;

public class CreateAccountHandler : IRequestHandler<CreateAccountRequest, AccountDto>
{
    private readonly ICreate<Account> _repository;

    public CreateAccountHandler(ICreate<Account> repository) =>
        _repository = repository;

    public async Task<AccountDto> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.CreateAsync(new(request.Name));
        return AccountDto.From(item);
    }
}
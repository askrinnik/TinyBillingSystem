using MediatR;
using Tibis.Application.AccountManagement.Models;
using Tibis.Application.AccountManagement.Queries;
using Tibis.Domain.AccountManagement;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.AccountManagement.Handlers;

public class CreateAccountHandler : IRequestHandler<CreateAccountRequest, AccountDto>
{
    private readonly ICreate<Account> _accountRepository;

    public CreateAccountHandler(ICreate<Account> accountRepository) =>
        _accountRepository = accountRepository;

    public async Task<AccountDto> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.CreateAsync(new(request.Name));
        return AccountDto.FromAccount(account);
    }
}
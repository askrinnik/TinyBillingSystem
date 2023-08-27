using MediatR;
using Tibis.Application.AccountManagement.Models;
using Tibis.Application.AccountManagement.Queries;
using Tibis.Domain;
using Tibis.Domain.AccountManagement;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.AccountManagement.Handlers
{
    public class GetAccountByIdHandler: IRequestHandler<GetAccountByIdRequest, AccountDto>
    {
        private readonly IRetrieve<Guid, Account> _accountRepository;

        public GetAccountByIdHandler(IRetrieve<Guid, Account> accountRepository) => 
            _accountRepository = accountRepository;

        public async Task<AccountDto> Handle(GetAccountByIdRequest request, CancellationToken cancellationToken)
        {
            var item = await _accountRepository.TryRetrieveAsync(request.Id);
            return item == null ? throw new AccountNotFoundException(request.Id) : AccountDto.FromAccount(item);
        }
    }
}

using MediatR;
using Tibis.Application.AccountManagement.Models;
using Tibis.Application.AccountManagement.Queries;
using Tibis.Domain.AccountManagement;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.AccountManagement.Handlers
{
    public class GetAccountByIdHandler: IRequestHandler<GetAccountByIdRequest, AccountDto>
    {
        private readonly IRetrieve<Guid, Account> _repository;

        public GetAccountByIdHandler(IRetrieve<Guid, Account> repository) => 
            _repository = repository;

        public async Task<AccountDto> Handle(GetAccountByIdRequest request, CancellationToken cancellationToken)
        {
            var item = await _repository.TryRetrieveAsync(request.Id);
            return item == null ? throw new AccountNotFoundException(request.Id) : AccountDto.From(item);
        }
    }
}

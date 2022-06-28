using BankService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OperationService.DataAccess
{
    public interface IBankRepository
    {
        Task<Response<List<Bank>>> GetExistingBanks();
    }
}
using BankService.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace BankMicroservice_Tests.Infracstructure
{
    public static class BankResponse
    {
        public static StringContent OkResponse => BuildOkResponse();
        public static StringContent InternalErrorResponse => BuildInternalErrorResponse();

        private static StringContent BuildOkResponse()
        {
            var response = new ApiResponse<List<Bank>>
            {
                Result = new List<Bank>
                {
                    new Bank{ BankName = "Wema", BankCode = "78665"},
                     new Bank{ BankName = "Gtb", BankCode = "65757"}
                }
            };
            var json = JsonSerializer.Serialize(response);
            return new StringContent(json);
        }

        private static StringContent BuildInternalErrorResponse()
        {
            var json = JsonSerializer.Serialize(new { StatusCode = 500, IsSuccessStatusCode = false, Message = "Internal Error." });
            return new StringContent(json);
        }
    }
}

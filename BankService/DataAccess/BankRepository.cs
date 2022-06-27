using BankService.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OperationService.DataAccess
{
    public class BankRepository : IBankRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BankRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Response<IEnumerable<Bank>>> GetExistingBanks()
        {
            try
            {
                using var client = _httpClientFactory.CreateClient("banks");
                var response = await client.GetAsync("https://wema-alatdev-apimgt.azure-api.net/alat-test/api/Shared/GetAllBanks");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var banks = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Bank>>>(data);
                    return new Response<IEnumerable<Bank>>()
                    {
                        Data = banks.Result,
                        Message = "List of existing banks.",
                        StatusCode = (int)response.StatusCode,
                        Succeeded = response.IsSuccessStatusCode
                    };
                }
                return new Response<IEnumerable<Bank>>()
                {
                    Data = null,
                    Message = response.ReasonPhrase,
                    StatusCode = (int)response.StatusCode,
                    Succeeded = response.IsSuccessStatusCode
                };
            }
            catch (Exception exc)
            {
                return new Response<IEnumerable<Bank>>()
                {
                    Data = null,
                    Message = exc.Message == "No such host is known." ? "Network error!" : exc.Message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Succeeded = false
                };
            }
        }
    }
}

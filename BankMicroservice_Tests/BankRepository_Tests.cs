using BankMicroservice_Tests.Infracstructure;
using BankService.Models;
using OperationService.DataAccess;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BankMicroservice_Tests
{
    public class BankRepository_Tests
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BankRepository_Tests()
        {
            _httpClientFactory = HttpClientBuilder.BankClientFactory(BankResponse.OkResponse);
        }

        [Fact]
        public async Task GetExistingBanks_Should_Return_A_List_Of_Bank()
        {
            //Arrange
            var sut = new BankRepository(_httpClientFactory);

            //Act
            var result = await sut.GetExistingBanks();

            //Assert
            Assert.IsType<Response<List<Bank>>>(result);
        }

        [Fact]
        public async Task GetExistingBanks_Should_Return_Expected_Values_From_the_Api()
        {
            //Arrange
            var sut = new BankRepository(_httpClientFactory);

            //Act
            var result = await sut.GetExistingBanks();

            //Assert
            Assert.Equal(2, result.Data.Count);
            Assert.NotNull(result.Data);
        }
    }
}

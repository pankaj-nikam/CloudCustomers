using System.Net.Http.Json;
using CloudCustomers.Models;
using CloudCustomers.Models.Configurations;
using CloudCustomers.Services.Abstract;
using Microsoft.Extensions.Options;

namespace CloudCustomers.Services.Concrete
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _client;
        private readonly UsersApiOptions _apiConfig;

        public UsersService(HttpClient client, IOptions<UsersApiOptions> apiConfig)
        {
            _client = client;
            _apiConfig = apiConfig.Value;
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var usersResponse = await _client.GetAsync(_apiConfig.Endpoint);
                if (usersResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<User>();
                }
                var responseContent = usersResponse.Content;
                var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
                return allUsers;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
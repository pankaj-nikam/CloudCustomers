using CloudCustomers.Models;

namespace CloudCustomers.Services.Abstract
{
    public interface IUsersService
    {
        Task<List<User>> GetAllUsers();
    }
}
using System.Threading.Tasks;
using BuySellApp.API.Models;

namespace BuySellApp.API.Interfaces {
    public interface IAuthRepository {
        Task<User> Register (User user, string password);

        Task<User> Login (string username, string password);

        Task<bool> UserExists (string username);

    }
}
using Models;
using System.Threading.Tasks;

namespace Utilities.TokenGeneration
{
    public interface ITokenGenerator
    {
        Task<string> GenerateTokenAsync(User user);
    }
}

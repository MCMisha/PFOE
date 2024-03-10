using System.Security.Cryptography;
using System.Text;

namespace WebApi.Services;

public class HashService
{
    private static SHA256 sHA256 = SHA256.Create();

    public string GetHash(string input)
    {
        byte[] inputBytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(inputBytes).Replace("-", string.Empty).ToLower();
    }
}
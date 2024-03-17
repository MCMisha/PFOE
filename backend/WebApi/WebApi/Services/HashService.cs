using System.Security.Cryptography;
using System.Text;

namespace WebApi.Services;

public class HashService
{
    private readonly SHA256 _sha256Hash = SHA256.Create();

    public string GetSha256Hash(string? input)
    {
        byte[] data = _sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input ?? throw new ArgumentNullException(nameof(input))));

        var sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; ++i)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();
    }
}
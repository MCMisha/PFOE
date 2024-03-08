using System.Security.Cryptography;
using System.Text;

namespace WebApi.Services;

public class HashService
{
    private readonly SHA256 _sha256Hash;

    public HashService()
    {
        _sha256Hash = SHA256.Create();
    }

    public string GetHash(string input)
    {
        byte[] data = _sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        var sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; ++i)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();
    }
}
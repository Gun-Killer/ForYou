using System;
using System.Security.Cryptography;
using System.Text;
using ForMemory.Domain.Interfaces.Services.Sign;

namespace ForMemory.Service.Sign
{
    public class SHA512SignService : ISignService
    {
        /// <inheritdoc />
        public string SignType { get; }

        public SHA512SignService()
        {
            SignType = "SHA512";
        }

        /// <inheritdoc />
        public bool Next(string signType)
        {
            return !SignType.Equals(signType, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <inheritdoc />
        public string Sign(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            
            using var cryptoServiceProvider = new SHA512CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(value);
            var hash = cryptoServiceProvider.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
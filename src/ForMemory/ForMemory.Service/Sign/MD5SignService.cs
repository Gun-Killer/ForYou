using System;
using System.Security.Cryptography;
using System.Text;
using ForMemory.Domain.Interfaces.Services.Sign;

namespace ForMemory.Service.Sign
{
    public class MD5SignService : ISignService
    {
        /// <inheritdoc />
        public string SignType { get; }

        public MD5SignService()
        {
            SignType = "MD5";
        }
        /// <inheritdoc />
        public bool Next(string signType)
        {
            return SignType.Equals(signType, StringComparison.InvariantCultureIgnoreCase) == false;
        }

        /// <inheritdoc />
        public string Sign(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            var sb = new StringBuilder(32);

            using (var md5 = MD5.Create())
            {
                var res=md5.ComputeHash(Encoding.UTF8.GetBytes(value));
                foreach (var re in res)
                {
                    sb.Append($"{re:x2}");
                }
            }

            return sb.ToString();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using ForMemory.Domain.Interfaces.Services.Sign;

namespace ForMemory.Service.Sign
{
    public class SignExecutive : ISignExecutive
    {
        private readonly IEnumerable<ISignService> _services;

        public SignExecutive(IEnumerable<ISignService> services)
        {
            _services = services;
        }

        /// <inheritdoc />
        public string Sign(string type, string value)
        {
            return _services.FirstOrDefault(t => t.Next(type) == false)?.Sign(value);
        }
    }
}
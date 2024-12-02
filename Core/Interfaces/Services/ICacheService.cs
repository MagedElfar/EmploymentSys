using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ICacheService
    {
        Task<T> GetData<T>(string key);

        Task<bool> SetData<T>(string key, T value, TimeSpan? expirationTime = null);

        Task<bool> RemoveData(string key);

    }
}

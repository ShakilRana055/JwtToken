using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.Interface
{
    public interface IJwtAuthenticaionRepository
    {
        string Authenticate(string userName, string password);
    }
}

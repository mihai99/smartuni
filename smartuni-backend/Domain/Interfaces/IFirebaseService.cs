using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;

namespace Domain.Interfaces
{
    public interface IFirebaseService
    {
        public Task<UserRecord> CreateFirebaseAccount(string email, string password, IReadOnlyDictionary<string, object> claims);
    }
}

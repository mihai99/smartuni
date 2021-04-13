using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using FirebaseAdmin.Auth;

namespace Services
{
    public class FirebaseService : IFirebaseService
    {
        public async Task<UserRecord> CreateFirebaseAccount(string email, string password, IReadOnlyDictionary<string, object> claims)
        {

            UserRecordArgs newAccount = new UserRecordArgs()
            {
                Email = email,
                EmailVerified = false,
                Password = password,
                Disabled = false,
            };
            UserRecord newAccountResult = await FirebaseAuth.DefaultInstance.CreateUserAsync(newAccount);
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(newAccountResult.Uid, claims);
            return newAccountResult;
        }
    }
}

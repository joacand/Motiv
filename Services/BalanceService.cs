using Motiv.Interfaces;
using Motiv.Models;
using System;
using System.Threading.Tasks;

namespace Motiv.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IUserDataDatastore userDataDatastore;

        private UserData UserData { get; set; }

        public BalanceService(IUserDataDatastore userDataDatastore)
        {
            this.userDataDatastore = userDataDatastore ?? throw new ArgumentNullException(nameof(userDataDatastore));
        }

        public async Task Init()
        {
            if (UserData is null)
            {
                UserData = await userDataDatastore.Load();
            }
        }

        public void SubtractBalance(int points)
        {
            UserData.Balance -= points;
        }

        public void AddBalance(int points)
        {
            UserData.Balance += points;
        }

        public async Task Save()
        {
            await userDataDatastore.Save(UserData);
        }

        public int Balance => UserData?.Balance ?? 0;
    }
}

using FiveOhFirstDataCore.Data.Account;
using FiveOhFirstDataCore.Data.Structures;

using Microsoft.AspNetCore.Identity;

using System.Security.Claims;

namespace FiveOhFirstDataCore.Data.Services
{
    public class InitalAccountPopulationService
    {
        private readonly UserManager<Trooper> _manager;

        public InitalAccountPopulationService(UserManager<Trooper> manager)
        {
            _manager = manager;
        }

        public async Task InitializeAsync()
        {
#if DEBUG
            var data = TestData;
            foreach (var set in data)
            {
                var trooper = await EnsureUser(set.Item1, "foo");
                await _manager.AddClaimsAsync(trooper, set.Item2);
                await _manager.AddToRolesAsync(trooper, set.Item3);
            }
#else
            var access = Guid.NewGuid().ToString();
            var t = new Trooper()
            {
                Id = -1,
                UserName = "Admin",
                NickName = "Admin",
                AccessCode = access,
                Rank = TrooperRank.Trooper,
                Slot = Slot.Archived,
                DiscordId = "abcdefghijklmnopqrstuvwxyz",
                SteamLink = "abcdefghijklmnopqrstuvwxyz"
            };

            var trooper = await EnsureUser(t, access);
            await _manager.AddToRolesAsync(trooper, new string[] { "Admin" });
#endif
        }

        private async Task<Trooper> EnsureUser(Trooper trooper, string password)
        {
            var user = await _manager.FindByIdAsync(trooper.Id.ToString());
            if (user is null)
            {
                user = trooper;
                await _manager.CreateAsync(user, password);
            }

            if (user is null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user;
        }

        private static List<(Trooper, List<Claim>, List<string>)> TestData
        {
            get
            {
                return new()
                {
                    (new Trooper()
                    {
                        Id = 56910,
                        BirthNumber = 56910,
                        UserName = "Atom",
                        NickName = "Atom",
                        Rank = TrooperRank.Trooper,
                        RTORank = RTORank.Cadet,
                        MedicRank = null,
                        WardenRank = null,
                        WarrantRank = null,
                        Flight = null,
                        PilotRank = null,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Role = Role.RTO,
                        Slot = Slot.AvalancheThreeThree | Slot.AvalancheThreeTwo,
                        Team = null,
                        StartOfService = DateTime.Now,
                        LastPromotion = DateTime.Now,
                        DiscordId = "133735496479145984",
                        SteamLink = "b"
                    },
                    new()
                    {
                        new("Slotted", "SomeDate"),
                        new("Instructor", "RTO"),
                        new("Display", "56910 Atom")
                    },
                    new()
                    {
                        "Trooper",
                        "RTO",
                        "Admin"
                    })
                };
            }
        }
    }
}

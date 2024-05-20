using RHP.Entities.Models;

namespace RHP.Data
{
    public static class DbInitialiser
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Create mock users
            var users = new List<User>();

            for (int i = 1; i <= 25; i++)
            {
                users.Add(new User { Id = $"#User{i}{new Random().Next(1000, 9999)}", Email = $"User{i}@user.com", Password = "1234", active = true, Status = UserStatus.Online });
            }

            foreach (var user in users)
            {
                context.User.Add(user);
            }

            context.SaveChanges();

            // Create players for each user
            var players = new List<Player>();

            for (int i = 0; i < users.Count; i++)
            {
                var player = new Player { Name = $"Player{i + 1}", User = users[i] };
                players.Add(player);
                context.Player.Add(player);
            }

            context.SaveChanges();

            // Create halls and add players
            var halls = new List<Hall>();

            for (int i = 0; i < 10; i++) // Adjust the number of halls as needed
            {
                var hall = new Hall { Title = $"Hall{i + 1}", GameMaster = players[i] };
                hall.Players.Add(players[(i + 1) % 25]); // Add a player to the hall
                hall.Players.Add(players[(i + 2) % 25]); // Add another player to the hall

                // Ensure the GameMaster is also a Player in the Players list
                if (!hall.Players.Contains(hall.GameMaster))
                {
                    hall.Players.Add(hall.GameMaster);
                }

                halls.Add(hall);
                context.Hall.Add(hall);
            }

            context.SaveChanges();
        }
    }
}

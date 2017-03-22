using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xonorpChatBot.data.ObjectModels;

namespace xonorpChatBot.data
{
    public class XonorpChatBotDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public static void SetInitializer()
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<XonorpChatBotDB, Migrations.Configuration>());
        }
    }
}

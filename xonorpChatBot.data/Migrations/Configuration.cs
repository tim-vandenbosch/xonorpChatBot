using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xonorpChatBot.data.Migrations
{
    class Configuration : DbMigrationsConfiguration<xonorpChatBot.data.XonorpChatBotDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(XonorpChatBotDB context)
        {
            // seed data here
        }
    }
}

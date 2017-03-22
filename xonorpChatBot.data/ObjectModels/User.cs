using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xonorpChatBot.data.ObjectModels
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int TotalPost { get; set; }
        public DateTime FirstSeen { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}

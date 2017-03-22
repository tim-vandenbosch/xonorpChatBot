using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xonorpChatBot.data.ObjectModels
{
    public class Post
    {
        public int PostId { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xonorpChatBot.data.ObjectModels;

namespace xonorpChatBot.data
{
    interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Add(User user);
    }
}

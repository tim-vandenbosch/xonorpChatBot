using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using xonorpChatBot.data;
using xonorpChatBot.data.ObjectModels;

namespace xonorpChatBot.data
{
    public class UserDbRepository : IUserRepository
    {
        // init
        private readonly XonorpChatBotDB _context;

        // default constructor
        public UserDbRepository(XonorpChatBotDB context)
        {
            _context = context;
        }

        #region UserDbInteraction

        // getting all users
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        // inserting a new user + RETURN THIS USER TO SHOW THE CREATED USER INSTANTLY
        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        #endregion


        public Post Add(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }
        
        // I don't get this function
        //public void addPostToUserInDB(string _name)
        //{
        //    using (var db = new XonorpChatBotDB())
        //    {
        //        var userUpdate =
        //            from u in db.Users
        //            where u.Name == _name
        //            select u;

        //        foreach (User u in userUpdate)
        //        {
        //            u.TotalPost++;
        //        }
        //        db.SaveChanges();
        //    }
        public void AddPointsTo(User user, int points)
        {
            if (CheckUser(user))
                _context.Users.Find(user).Points += points;
        }

        private bool CheckUser(User user)
        {
            if (_context.Users.Equals(user)) return true;
            return false;
        }

        //public void UpdateUserPoints(string _name, int _poins)
        //{
        //    using (var db = new XonorpChatBotDB())
        //    {

        //        var pointsUpdate =
        //            from u in db.Users
        //            where u.Name == _name
        //            select u;
        //        foreach (User u in pointsUpdate)
        //        {
        //            u.Points += _poins;
        //        }

        //public bool CheckIfUserExistInDB(string _name)
        //{
        //    List<string> userList = new List<string>();
        //    bool yesNo;
        //    using (var db = new XonorpChatBotDB())
        //    {
        //        var users = from u in db.Users
        //                    orderby u.Name
        //                    select u;

        //        foreach (User user in users)
        //        {
        //            userList.Add(user.Name);
        //        }

        //        if (userList.Contains(_name))
        //        {
        //            yesNo = true;
        //        }
        //        else yesNo = false;

        //    }

        //    return yesNo;
        //}

    }
}
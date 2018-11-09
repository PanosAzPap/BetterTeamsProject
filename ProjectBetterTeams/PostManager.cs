using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ProjectBetterTeams
{
    class PostManager
    {
        public void GetPosts()
        {
            List<Posts> Posts;
            using (var db = new TeamsContext())
            {
                Posts = db.Posts.Select(post => post).ToList();
            }
            foreach (Posts post in Posts)
            {
                Console.WriteLine($">>>{post.PostID}, {post.UsernameSender} at {post.DateTime}:\n {post.Post}");
            }
        }

        public void CreatePost(string Sender, string Post)
        {
            Posts post = new Posts()
            {
                UsernameSender = Sender,
                DateTime = DateTime.Now,
                Post = Post
            };

            using (var db = new TeamsContext())
            {
                db.Posts.Add(post);
                db.SaveChanges();
            }
        }

        public void DeletePost()
        {
            bool Invalid;
            int ID = 0;
            Posts post;
            GetPosts();
            do
            {
                Console.Write("Select Post ID to delete: ");
                Invalid = false;
                try
                {
                    ID = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Input!");
                    Invalid = true;
                }
            } while (Invalid);
            post = FindPost(ID);
            using (var db = new TeamsContext())
            {
                post = db.Posts.Find(ID);
                db.Posts.Remove(post);
                db.SaveChanges();
            }
        }

        public Posts FindPost(int ID)
        {
            Posts post;
            using (var db = new TeamsContext())
            {
                post = db.Posts.Find(ID);
            }
            return post;
        }
    }
}

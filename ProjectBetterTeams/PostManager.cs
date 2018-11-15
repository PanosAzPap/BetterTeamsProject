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

            try
            {
                using (var db = new TeamsContext())
                {
                    db.Posts.Add(post);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Post Is Empty!!!");
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
                db.Entry(post).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public void DeleteUserPosts(string Username)
        {
            List<Posts> Posts;
            using (var db = new TeamsContext())
            {
                Console.WriteLine("Deleting Posts...");
                Posts = db.Posts.Where(p => p.UsernameSender == Username).Select(p => p).ToList();

                foreach (var item in Posts)
                {
                    db.Entry(item).State = EntityState.Deleted;
                    db.SaveChanges();
                }
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

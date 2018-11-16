using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Threading;
using System.Reflection;

namespace ProjectBetterTeams
{
    class UserManager
    {
        //Find User
        public Users FindUser(string Username)
        {
            Users user = new Users();
            using (var db = new TeamsContext())
            {
                return user = db.Users.Find(Username);
            }
        }

        //Create New User
        public void CreateNewUser(UserDTO user)
        {
            Users NewUser = new Users();

            NewUser.Username = user.username;
            NewUser.Password = user.password;
            NewUser.FirstName = user.firstname;
            NewUser.LastName = user.lastname;
            NewUser.DateOFBirth = user.dateofbirth.Date;
            NewUser.UserType = user.usertype;

            using (var db = new TeamsContext())
            {
                db.Users.Add(NewUser);
                db.SaveChanges();
            }

            Console.WriteLine($"{NewUser.FirstName} wellcome to BetterTeams!");
        }

        //Get List Of Users(except self)
        public List<string> GetUsernames(string Username)
        {
            List<string> Usernames = new List<string>();

            using (var db = new TeamsContext())
            {
                Usernames = db.Users.Where(u => u.Username != Username).Select(u => u.Username).ToList();
            }
            return Usernames;
        }

        //Show Users Table
        public void GetAllUsers()
        {
            List<Users> UsersList;
            using (var db = new TeamsContext())
            {
                UsersList = db.Users.Select(user => user).ToList();
            }

            foreach (Users user in UsersList)
            {
                Console.WriteLine($"|Username: {user.Username} | Firstname: {user.FirstName} | Lastname: {user.LastName} | DateOfBirth: {user.DateOFBirth} | UserType: {user.UserType} |");
            }
        }

        //SuperAdmin Modification Access
        public void ModifyUser(Users CurrentUser)
        {
            bool Invalid;
            foreach (string name in GetUsernames(CurrentUser.Username))
            {
                Console.WriteLine($"--> {name}");
            }
            do
            {
                Invalid = false;
                Console.Write("Insert Username: ");
                string Username = Console.ReadLine();

                if (FindUser(Username) == null)
                {
                    Console.WriteLine("Username Not Found!");
                    Invalid = true;
                }
                else
                {
                    Users user = FindUser(Username);
                    Console.WriteLine("Select what you would like to edit:");
                    Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-");
                    Console.WriteLine($"1. Username: {user.Username}\n2. Firstname: {user.FirstName}\n3. Lastname: {user.LastName}\n4. DateOfBirth: {user.DateOFBirth}\n5. UserType: {user.UserType}\n6. Delete User\n0. Exit");

                    char Choice = Console.ReadKey(true).KeyChar;
                    switch (Choice)
                    {
                        case '1':
                            string NewUsername;
                            do
                            {
                                Invalid = false;
                                Console.Write("New Username: ");
                                NewUsername = Console.ReadLine();
                                if (FindUser(NewUsername) != null)
                                    Invalid = true;
                            } while (Invalid);

                            using (var db = new TeamsContext())
                            {
                                user.Username = NewUsername;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete!");
                            }
                            break;
                        case '2':
                            //FirstName
                            Console.Write("New FirstName: ");
                            string NewFirstname = Console.ReadLine();

                            using (var db = new TeamsContext())
                            {
                                user.FirstName = NewFirstname;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete!");
                            }
                            break;
                        case '3':
                            //LastName
                            Console.Write("New LastName: ");
                            string NewLastname = Console.ReadLine();

                            using (var db = new TeamsContext())
                            {
                                user.LastName = NewLastname;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete!");
                            }
                            break;
                        case '4':
                            //Date Of Birth
                            DateTime NewDateOfBirth = new DateTime();
                            do
                            {
                                Invalid = false;
                                Console.Write("New Date Of Birth(yyyy/mm/dd): ");
                                try
                                {
                                    NewDateOfBirth = DateTime.Parse(Console.ReadLine());
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Invalid Format!");
                                    Invalid = true;
                                }
                            } while (Invalid);

                            using (var db = new TeamsContext())
                            {
                                user.DateOFBirth = NewDateOfBirth;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete!");
                            }
                            break;
                        case '5':
                            //User Type
                            char NewUserType;
                            do
                            {
                                Console.Clear();
                                Invalid = false;
                                Console.WriteLine("Choose New User Type:\n1. Student\n2. Teacher\n3. Admin");
                                NewUserType = Console.ReadKey(true).KeyChar;
                                switch (NewUserType)
                                {
                                    case '1':
                                        user.UserType = "Student";
                                        break;
                                    case '2':
                                        user.UserType = "Teacher";
                                        break;
                                    case '3':
                                        user.UserType = "Admin";
                                        break;
                                    default:
                                        Console.WriteLine("Wrong User Type!");
                                        Invalid = true;
                                        break;
                                }
                            } while (Invalid);

                            using (var db = new TeamsContext())
                            {
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete!");
                            }
                            break;
                        case '6':
                            Console.WriteLine("Are you sure (y/n)?");
                            char IsSure = Console.ReadKey(true).KeyChar;
                            if (IsSure == 'y')
                                DeleteUser(user);
                            break;
                        case '0':
                            Invalid = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Input!");
                            Invalid = true;
                            break;
                    }

                }
            } while (Invalid);
        }

        //Admin Modifications Access
        public void AdminModifyUser(Users CurrentUser)
        {
            bool Invalid;
            foreach (string name in GetUsernames(CurrentUser.Username))
            {
                Console.WriteLine($"--> {name}");
            }
            do
            {
                Invalid = false;
                Console.Write("Insert Username: ");
                string Username = Console.ReadLine();

                if (FindUser(Username) == null)
                {
                    Console.WriteLine("Username Not Found!");
                    Invalid = true;
                }
                else
                {
                    Users user = FindUser(Username);
                    Console.WriteLine("Select what you would like to edit:");
                    Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-");
                    Console.WriteLine($"1. Username: {user.Username}\n2. Firstname: {user.FirstName}\n3. Lastname: {user.LastName}\n4. DateOfBirth: {user.DateOFBirth}\n5. UserType: {user.UserType}\n6. Delete User\n0. Exit");

                    char Choice = Console.ReadKey(true).KeyChar;
                    switch (Choice)
                    {
                        case '1':
                            string NewUsername;
                            do
                            {
                                Invalid = false;
                                Console.Write("New Username: ");
                                NewUsername = Console.ReadLine();
                                if (FindUser(NewUsername) != null)
                                    Invalid = true;
                            } while (Invalid);

                            using (var db = new TeamsContext())
                            {
                                user.Username = NewUsername;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete!");
                            }
                            break;
                        case '2':
                            //FirstName
                            Console.Write("New FirstName: ");
                            string NewFirstname = Console.ReadLine();

                            using (var db = new TeamsContext())
                            {
                                user.FirstName = NewFirstname;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete!");
                            }
                            break;
                        case '3':
                            //LastName
                            Console.Write("New LastName: ");
                            string NewLastname = Console.ReadLine();

                            using (var db = new TeamsContext())
                            {
                                user.LastName = NewLastname;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete!");
                            }
                            break;
                        case '4':
                            //Date Of Birth
                            DateTime NewDateOfBirth = new DateTime();
                            do
                            {
                                Invalid = false;
                                Console.Write("New Date Of Birth(yyyy/mm/dd): ");
                                try
                                {
                                    NewDateOfBirth = DateTime.Parse(Console.ReadLine());
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Invalid Format!");
                                    Invalid = true;
                                }
                            } while (Invalid);

                            using (var db = new TeamsContext())
                            {
                                user.DateOFBirth = NewDateOfBirth;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete!");
                            }
                            break;
                        case '0':
                            Invalid = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Input!");
                            Invalid = true;
                            break;
                    }

                }
            } while (Invalid);
        }

        //Student Modifications Access
        public void StudentModifyUser(Users User)
        {
            char TryAgain = '0';
            Console.WriteLine("What would you like tou edit?");
            Console.WriteLine($"1. Username: {User.Username}\n2. Password \n3. Firstname: {User.FirstName}\n4. Lastname: {User.LastName}\n5. DateOfBirth: {User.DateOFBirth}");
            ConsoleKeyInfo editChoice = Console.ReadKey(true);
            switch (editChoice.KeyChar)
            {
                case '1':
                    Console.Write("***NOTE***\nApplication will Restart after changes!!!\nNew Username:");
                    string NewUserName = Console.ReadLine();
                    if (FindUser(NewUserName) == null)
                    {
                        using (var db = new TeamsContext())
                        {
                            User.Username = Console.ReadLine();
                            db.Entry(User).State = System.Data.Entity.EntityState.Modified;
                            Console.WriteLine("Saving, please wait...");
                            db.SaveChanges();
                        }
                        Console.WriteLine("Complete!\nLoging Out...");
                        Thread.Sleep(3000);
                        string fileName = Assembly.GetExecutingAssembly().Location;
                        System.Diagnostics.Process.Start(fileName);
                        Environment.Exit(0);
                    }
                    break;
                case '2':
                    Console.Write("***NOTE***\nApplication will Restart after changes!!!\nNew Username:");                    
                    UserSignUp EncryptPass = new UserSignUp();
                    do
                    {
                        Console.Write("Current Password: ");
                        string CurrentPassword = EncryptPass.EncryptPassword(Console.ReadLine());
                        if (CurrentPassword == User.Password)
                        {
                            using (var db = new TeamsContext())
                            {
                                Console.Write("New Password: ");
                                User.Password = EncryptPass.EncryptPassword(Console.ReadLine());
                                db.Entry(User).State = System.Data.Entity.EntityState.Modified;
                                Console.WriteLine("Saving, please wait...");
                                db.SaveChanges();
                            }
                            Console.WriteLine("Complete!\nLoging Out...");
                            Thread.Sleep(3000);
                            string fileName = Assembly.GetExecutingAssembly().Location;
                            System.Diagnostics.Process.Start(fileName);
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Password");
                            Console.WriteLine("Press 1 to Try Again");
                            TryAgain = Console.ReadKey(true).KeyChar;
                        }
                    } while (TryAgain == '1');
                    break;
                case '3':
                    Console.Write("New FirstName: ");
                    string NewFirstname = Console.ReadLine();
                    using (var db = new TeamsContext())
                    {
                        User.FirstName = NewFirstname;
                        db.Entry(User).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    break;
                case '4':
                    Console.Write("New LastName: ");
                    string NewLastname = Console.ReadLine();
                    using (var db = new TeamsContext())
                    {
                        User.LastName = NewLastname;
                        db.Entry(User).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    break;
                case '5':
                    do
                    {
                        Console.Write("New Date Of Birth(yyyy/mm/dd: ");
                        try
                        {
                            DateTime NewDateofbirth = DateTime.Parse(Console.ReadLine());
                            using (var db = new TeamsContext())
                            {
                                User.DateOFBirth = NewDateofbirth;
                                db.Entry(User).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Wrong Input!\nPress 1 to Try Again");
                            TryAgain = Console.ReadKey(true).KeyChar;
                        }
                    } while (TryAgain == '1');
                    break;
                default:
                    break;
            }
        }

        //Teacher Modification Access
        public void TeacherModifyUser(Users CurrentUser)
        {
            bool Invalid;
            GetUsernames(CurrentUser.Username);
            do
            {
                Invalid = false;
                Console.Write("Insert Username: ");
                string Username = Console.ReadLine();

                if (FindUser(Username) == null)
                {
                    Console.WriteLine("Username Not Found!");
                    Invalid = true;
                }
                else
                {
                    char Choice;
                    Users user = FindUser(Username);
                    Console.WriteLine("Select what you would like to edit:");
                    Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-");
                    Console.WriteLine($"Username: {user.Username}\n1. Firstname: {user.FirstName}\n2. Lastname: {user.LastName}\n3. DateOfBirth: {user.DateOFBirth} ");
                    Choice = Console.ReadKey(true).KeyChar;
                    switch (Choice)
                    {
                        case '1':
                            //FirstName
                            Console.Write("New FirstName: ");
                            string NewFirstname = Console.ReadLine();

                            using (var db = new TeamsContext())
                            {
                                user.FirstName = NewFirstname;
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete!");
                            }
                            break;
                        case '2':
                            //LastName
                            Console.Write("New LastName: ");
                            string NewLastname = Console.ReadLine();

                            using (var db = new TeamsContext())
                            {
                                user.LastName = NewLastname;
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete!");
                            }
                            break;
                        case '3':
                            //Date Of Birth
                            DateTime NewDateOfBirth = new DateTime();
                            do
                            {
                                Invalid = false;
                                Console.Write("New Date Of Birth(yyyy/mm/dd): ");
                                try
                                {
                                    NewDateOfBirth = DateTime.Parse(Console.ReadLine());
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Invalid Format!");
                                    Invalid = true;
                                }
                            } while (Invalid);

                            using (var db = new TeamsContext())
                            {
                                user.DateOFBirth = NewDateOfBirth;
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete Successful!");
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid Input!");
                            Invalid = true;
                            break;
                    }

                }
            } while (Invalid);
        }

        //Delete User
        public void DeleteUser(Users user)
        {
            MessageManager messages = new MessageManager();
            PostManager posts = new PostManager();

            Console.WriteLine("Please wait...\nRemoving Users data...");
            posts.DeleteUserPosts(user.Username);
            messages.DeleteUserMessages(user.Username);
            using (var db = new TeamsContext())
            {
                Console.WriteLine("Finishing...");
                db.Entry(user).State = System.Data.Entity.EntityState.Deleted;              
                db.SaveChanges();
            }
            Console.WriteLine("Remove Complete!");
        }

        //Valid User
        public bool ConfirmUser(string Username, string Password)
        {
            UserSignUp Pass = new UserSignUp();
            Users user = FindUser(Username);
            if ((user == null) || (user.Password != Pass.EncryptPassword(Password)))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }

}

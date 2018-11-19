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
        /// <summary>
        /// Finds User 
        /// </summary>
        /// <param name="Username"></param>
        /// <returns>Object Users. If there is not user with this Username returns null</returns>
        public Users FindUser(string Username)
        {
            Users user = new Users();
            using (var db = new TeamsContext())
            {
                return user = db.Users.Find(Username);
            }
        }

        /// <summary>
        /// Creates A new User
        /// </summary>
        /// <param name="user"></param>
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

        /// <summary>
        /// Creates a list of all Usernames in database. 
        /// </summary>
        /// <param name="Username"></param>
        /// <returns>A List<String></Sting></returns>
        public List<string> GetUsernames(string Username)
        {
            List<string> Usernames = new List<string>();

            using (var db = new TeamsContext())
            {
                Usernames = db.Users.Where(u => u.Username != Username).Select(u => u.Username).ToList();
            }
            return Usernames;
        }

        /// <summary>
        /// Projects all Users in table Users.
        /// </summary>
        public void GetAllUsers()
        {
            List<Users> UsersList;
            using (var db = new TeamsContext())
            {
                UsersList = db.Users.Select(user => user).ToList();
            }

            foreach (Users user in UsersList)
            {
                Console.WriteLine($"|Username: {user.Username} | Firstname: {user.FirstName} | Lastname: {user.LastName} | DateOfBirth: {user.DateOFBirth.Date} | UserType: {user.UserType} |");
            }
        }

        /// <summary>
        /// SuperAdmin Modification Access
        /// </summary>
        /// <param name="CurrentUser"></param>
        public void ModifyUser(Users CurrentUser)
        {
            bool Invalid;
            Console.WriteLine($"--> {CurrentUser.Username}");
            foreach (string name in GetUsernames(CurrentUser.Username))
            {
                Console.WriteLine($"--> {name}");
            }
            do
            {
                Invalid = false;
                Console.Write("Insert Username or 'exit' to exit editor: ");
                string Username = Console.ReadLine();

                if (Username == "exit")
                    return;

                //Check If User Exists
                if (FindUser(Username) == null)
                {
                    Console.WriteLine("Username Not Found!");
                    Invalid = true;
                }
                else
                {
                    UIProcedures UI = new UIProcedures();
                    Users user = FindUser(Username);
                    UI.UIShowAccess(user);
                    char Choice = Console.ReadKey(true).KeyChar;
                    switch (Choice)
                    {
                        case '1':
                            Console.WriteLine("You cant change Username!");
                            Thread.Sleep(1500);
                            break;
                        case '2':
                            UserSignUp PassEncrypt = new UserSignUp();
                            string NewPassword = "";

                            if (user.Username != CurrentUser.Username)
                            {
                                Console.WriteLine("You cant change Password of other users!!");
                                Thread.Sleep(1500);
                                break;
                            }

                            do
                            {
                                Console.Write("Current Password: ");
                                string CurrentPassword = PassEncrypt.EncryptPassword(Console.ReadLine());
                                Invalid = false;
                                if (CurrentUser.Password == CurrentPassword)
                                {
                                    Console.WriteLine("***NOTE***\nApplication will Restart after changes!!!");
                                    Console.Write("New Password: ");
                                    NewPassword = PassEncrypt.EncryptPassword(Console.ReadLine());
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Password");
                                    Invalid = true;
                                }

                            } while (Invalid);

                            using (var db = new TeamsContext())
                            {
                                user.Password = NewPassword;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                Console.WriteLine("Saving, please wait...");
                                db.SaveChanges();
                            }
                            Console.WriteLine("Complete!\nLoging Out...");
                            Thread.Sleep(3000);
                            string fileName = Assembly.GetExecutingAssembly().Location;
                            System.Diagnostics.Process.Start(fileName);
                            Environment.Exit(0);
                            break;
                        case '3':
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
                        case '4':
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
                        case '5':
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
                        case '6':
                            //User Type
                            if (user.UserType != "SuperAdmin")
                            {
                                Console.WriteLine("You cant Change your Type!");
                                Thread.Sleep(2000);
                                break;
                            }

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
                                Console.WriteLine("Complete!");
                            }
                            break;
                        case '7':
                            if (user.UserType == "SuperAdmin")
                            {
                                Console.WriteLine("You cant delete Yourself!\nYour Are The Super Admin!");
                                Thread.Sleep(2000);
                                break;
                            }
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

        /// <summary>
        /// Admins Modification Access
        /// </summary>
        /// <param name="CurrentUser"></param>
        public void AdminModifyUser(Users CurrentUser)
        {
            bool Invalid;
            Console.WriteLine($"--> {CurrentUser.Username}");
            foreach (string name in GetUsernames(CurrentUser.Username))
            {
                Console.WriteLine($"--> {name}");
            }

            do
            {
                Invalid = false;
                Console.Write("Insert Username or 'exit' to exit editor: ");
                string Username = Console.ReadLine();
                Users user = FindUser(Username);
                if (Username == "exit")
                    return;

                if (user == null)
                {
                    Console.WriteLine("Username Not Found!");
                    Invalid = true;
                }
                else if (user.Username == "Admin")
                {
                    Console.WriteLine("You cant Edit Admin!\nYou dont want to...Believe me!");
                }
                else
                {
                    UIProcedures UI = new UIProcedures();
                    UI.UIShowAccess(user);
                    char Choice = Console.ReadKey(true).KeyChar;
                    switch (Choice)
                    {
                        case '1':
                            Console.WriteLine("You cant change Username!");
                            Thread.Sleep(1500);
                            break;
                        case '2':
                            UserSignUp PassEncrypt = new UserSignUp();
                            string NewPassword = "";

                            if (user.Username != CurrentUser.Username)
                            {
                                Console.WriteLine("You cant change Password of other users!!");
                                Thread.Sleep(1500);
                                break;
                            }

                            do
                            {
                                Console.Write("Current Password: ");
                                string CurrentPassword = PassEncrypt.EncryptPassword(Console.ReadLine());
                                Invalid = false;
                                if (CurrentUser.Password == CurrentPassword)
                                {
                                    Console.WriteLine("***NOTE***\nApplication will Restart after changes!!!");
                                    Console.Write("New Password: ");
                                    NewPassword = PassEncrypt.EncryptPassword(Console.ReadLine());
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Password");
                                    Invalid = true;
                                }

                            } while (Invalid);

                            using (var db = new TeamsContext())
                            {
                                user.Password = NewPassword;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                Console.WriteLine("Saving, please wait...");
                                db.SaveChanges();
                            }
                            Console.WriteLine("Complete!\nLoging Out...");
                            Thread.Sleep(3000);
                            string fileName = Assembly.GetExecutingAssembly().Location;
                            System.Diagnostics.Process.Start(fileName);
                            Environment.Exit(0);
                            break;
                        case '3':
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
                        case '4':
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
                        case '5':
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
                        case '6':
                            Console.WriteLine("You are not authorized to delete users!");
                            break;
                        case '0':
                            return;
                        default:
                            Console.WriteLine("Invalid Input!");
                            Invalid = true;
                            break;
                    }
                }
            } while (Invalid);
        }

        /// <summary>
        /// Students Modification Access
        /// </summary>
        /// <param name="CurrentUser"></param>
        public void StudentModifyUser(Users User)
        {
            UIProcedures UI = new UIProcedures();
            char TryAgain = '0';
            UI.ShowProfile(User);
            ConsoleKeyInfo editChoice = Console.ReadKey(true);
            switch (editChoice.KeyChar)
            {
                case '1':
                    Console.WriteLine("You cant change Username!");
                    Thread.Sleep(1500);
                    break;
                case '2':
                    Console.WriteLine("***NOTE***\nApplication will Restart after changes!!!");
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
                        Console.Write("New Date Of Birth(yyyy/mm/dd): ");
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
                case '0':
                    return;
                default:
                    break;
            }
        }

        /// <summary>
        /// Techers Modification Access
        /// </summary>
        /// <param name="CurrentUser"></param>
        public void TeacherModifyUser(Users CurrentUser)
        {
            bool Invalid;
            Console.WriteLine($"--> {CurrentUser.Username}");
            foreach (string name in GetUsernames(CurrentUser.Username))
            {
                Console.WriteLine($"--> {name}");
            }

            do
            {
                Invalid = false;
                Console.Write("Insert Username or 'exit' to exit editor: ");
                string Username = Console.ReadLine();
                Users user = FindUser(Username);

                if (Username == "exit")
                    return;


                if (user == null)
                {
                    Console.WriteLine("Username Not Found!");
                    Invalid = true;
                }
                else if (user.Username == "Admin")
                {
                    Console.WriteLine("You cant Edit Admin!\nYou dont want to...Believe me!");
                }
                else
                {
                    UIProcedures UI = new UIProcedures();
                    UI.UIShowAccess(user);
                    char Choice = Console.ReadKey(true).KeyChar;

                    switch (Choice)
                    {
                        case '1':
                            Console.WriteLine("You cant change Username!");
                            Thread.Sleep(1500);
                            break;
                        case '2':
                            UserSignUp PassEncrypt = new UserSignUp();
                            string NewPassword = "";

                            if (user.Username != CurrentUser.Username)
                            {
                                Console.WriteLine("You cant change Password of other users!!");
                                Thread.Sleep(1500);
                                break;
                            }

                            do
                            {
                                Console.WriteLine("***NOTE***\nApplication will Restart after changes!!!");
                                Console.Write("Current Password: ");
                                string CurrentPassword = PassEncrypt.EncryptPassword(Console.ReadLine());
                                Invalid = false;
                                if (CurrentUser.Password == CurrentPassword)
                                {
                                    Console.Write("New Password: ");
                                    NewPassword = PassEncrypt.EncryptPassword(Console.ReadLine());
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Password");
                                    Invalid = true;
                                }

                            } while (Invalid);

                            using (var db = new TeamsContext())
                            {
                                user.Password = NewPassword;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                Console.WriteLine("Saving, please wait...");
                                db.SaveChanges();
                            }

                            Console.WriteLine("Complete!\nLoging Out...");
                            Thread.Sleep(3000);
                            string fileName = Assembly.GetExecutingAssembly().Location;
                            System.Diagnostics.Process.Start(fileName);
                            Environment.Exit(0);
                            break;
                        case '3':
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
                        case '4':
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
                        case '5':
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
                        case '6':
                            Console.WriteLine("You are not authorized to delete users!");
                            break;
                        case '0':
                            return;
                        default:
                            Console.WriteLine("Invalid Input!");
                            Invalid = true;
                            break;
                    }

                }
            } while (Invalid);
        }

        /// <summary>
        /// Deletes Posts, Messages and the User.
        /// </summary>
        /// <param name="user"></param>
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

        /// <summary>
        /// Login Confirmation
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns>True if username matches password</returns>
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

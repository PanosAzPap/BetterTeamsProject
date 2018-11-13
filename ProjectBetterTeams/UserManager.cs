using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Threading;
using System.Reflection;

//Replace Delete section with Entity.State
//Replace Edit section with Entity.State

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
            NewUser.DateOFBirth = user.dateofbirth;
            NewUser.UserType = user.usertype;

            using (var db = new TeamsContext())
            {
                db.Users.Add(NewUser);
                db.SaveChanges();
            }

            Console.WriteLine($"{NewUser.FirstName} wellcome to BetterTeams!");
        }

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

        //User Modification
        public void ModifyUser()
        {
            bool Invalid;
            GetAllUsers();
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
                    Int16 Choice = 0;
                    Users user = FindUser(Username);
                    Console.WriteLine("Select what you would like to edit:");
                    Console.WriteLine($"1. Username: {user.Username}\n2. Firstname: {user.FirstName}\n3. Lastname: {user.LastName}\n4. DateOfBirth: {user.DateOFBirth}\n5. UserType: {user.UserType}");

                    try
                    {
                        Choice = Int16.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid Input!");
                        Invalid = true;
                    }

                    switch (Choice)
                    {
                        case 1:
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
                        case 2:
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
                        case 3:
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
                        case 4:
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
                        case 5:
                            //User Type
                            string NewUserType;
                            do
                            {
                                Invalid = false;
                                Console.WriteLine("Choose New User Type:\nStudent\nTeacher\nAdmin");
                                NewUserType = Console.ReadLine();
                                if (NewUserType != "Student" || NewUserType != "Teacher" || NewUserType != "Admin")
                                {
                                    Console.WriteLine("Wrong User Type!");
                                    Invalid = true;
                                }
                            } while (Invalid);

                            using (var db = new TeamsContext())
                            {
                                user.UserType = NewUserType;
                                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete!");
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

        public void StudentModifyUser(Users User)
        {            
            Console.WriteLine("What would you like tou edit?");
            Console.WriteLine($"1. Username: {User.Username}\n2. Firstname: {User.FirstName}\n3. Lastname: {User.LastName}\n4. DateOfBirth: {User.DateOFBirth}");
            ConsoleKeyInfo editChoice = Console.ReadKey(true);
            switch (editChoice.KeyChar)
            {
                case '1':
                    Console.Write("***NOTE***\nUser will log out after changes!!!\nNew Username:");
                    string NewUserName = Console.ReadLine();
                    if (FindUser(NewUserName) == null)
                    {
                        using (var db = new TeamsContext())
                        {
                            User.Username = Console.ReadLine();
                            Console.WriteLine("Saving, please wait...");
                            db.SaveChanges();
                        }
                        Console.WriteLine("Complete!\nLoging Out...");
                        Thread.Sleep(3000);
                        MainProcedure procedure = new MainProcedure();
                        string fileName = Assembly.GetExecutingAssembly().Location;
                        System.Diagnostics.Process.Start(fileName);
                        Environment.Exit(0);
                    }
                    break;
                default:
                    break;
            }
        }

        public void TeacherModifyUser()
        {
            bool Invalid;
            GetAllUsers();
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
                    Int16 Choice = 0;
                    Users user = FindUser(Username);
                    Console.WriteLine("Select what you would like to edit:");
                    Console.WriteLine($"1. Username: {user.Username}\n2. Firstname: {user.FirstName}\n3. Lastname: {user.LastName}\n4. DateOfBirth: {user.DateOFBirth} ");

                    try
                    {
                        Choice = Int16.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid Input!");
                        Invalid = true;
                    }

                    switch (Choice)
                    {
                        case 1:
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
                                db.SaveChanges();
                                Console.WriteLine("Modify Complete Successful!");
                            }
                            break;
                        case 2:
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
                        case 3:
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
                        case 4:
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
        public void DeleteUser()
        {
            Users user;
            bool Invalid;
            GetAllUsers();
            do
            {
                Invalid = false;
                Console.Write("Choose User(Username): ");
                string Username = Console.ReadLine();
                user = FindUser(Username);
                if (user == null)
                {
                    Console.WriteLine("User Not Found!");
                    Invalid = true;
                }
            } while (Invalid);

            using (var db = new TeamsContext())
            {
                db.Users.Remove(user);
                db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            Console.WriteLine("Remove Complete Successful");
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

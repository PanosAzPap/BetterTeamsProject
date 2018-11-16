using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ProjectBetterTeams
{
    class UserSignUp
    {
        UserManager userManager = new UserManager();
        UserDTO User;

        //Sign Up User
        public void SignUp()
        {
            bool Invalid;
            //Gather User Information
            Console.Clear();
            User = new UserDTO();

            string firstname;
            string lastname;
            do
            {
                Invalid = false;
                Console.Write("First Name: ");
                firstname = Console.ReadLine();                
                if (firstname == "")
                {
                    Console.WriteLine("First Name cant be empty!");
                    Invalid = true;
                }                
            } while (Invalid);
            User.firstname = DoUpper(firstname);
            do
            {
                Invalid = false;
                Console.Write("Last Name: ");
                lastname = Console.ReadLine();            
                if (lastname == "")
                {
                    Console.WriteLine("Last Name cant be empty!");
                    Invalid = true;
                }
            } while (Invalid);
            User.lastname = DoUpper(lastname);

            do
            {
                Invalid = false;
                Console.Write("Date Of Birth (yyyy/mm/dd): ");
                try
                {
                    User.dateofbirth = DateTime.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Date! The valid format is yyyy/mm/dd");
                    Invalid = true;
                }
            } while (Invalid);

            bool InvalidUserName;
            //Checking for valid Username
            do
            {
                InvalidUserName = false;
                Console.Write("Username: ");
                User.username = Console.ReadLine();
                if (User.username == "")
                {
                    Console.WriteLine("Username cant be empty!");
                    InvalidUserName = true;
                    continue;
                }
                try
                {
                    if (User.username == userManager.FindUser(User.username).Username)
                    {
                        Console.WriteLine($"Username {User.username} is already taken!");
                        InvalidUserName = true;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            } while (InvalidUserName);

            //Check Valid Password
            bool InvalidPassword = false;
            string Password;
            do
            {
                InvalidPassword = false;
                Console.Write("Password: ");
                Password = Console.ReadLine();
                if (Password == "")
                {
                    InvalidPassword = true;
                    Console.WriteLine("Password cant be empty");
                }
            } while (InvalidPassword);
            User.password = EncryptPassword(Password);

            //Send to Database
            userManager.CreateNewUser(User);

        }

        public string EncryptPassword(string Password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(Password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        public string DoUpper(string Name)
        {
            if (Name == null || Name == "")
                return "Invalid";

            if (Name.Length > 1)
            {
                Name = char.ToUpper(Name[0]) + Name.Substring(1);
            }
            else
            {
                Name = Name.ToUpper();
            }
            return Name;
        }
    }
}

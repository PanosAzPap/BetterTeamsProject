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

            do
            {
                Invalid = false;
                Console.Write("First Name: ");
                User.firstname = DoUpper(Console.ReadLine());
                if (User.firstname == "Invalid")
                    Invalid = true;
            } while (Invalid);

            do
            {
                Invalid = false;
                Console.Write("Last Name: ");
                User.lastname = DoUpper(Console.ReadLine());
                if (User.lastname == "Invalid")
                    Invalid = true;
            } while (Invalid);

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

            Console.Write("Password: ");
            string Password = Console.ReadLine();
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

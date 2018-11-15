using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ProjectBetterTeams
{
    public class UIProcedures
    {

        UserManager userManager = new UserManager();
        UserSignUp NewUser = new UserSignUp();

        public UIProcedures()
        {
            Console.BackgroundColor = ConsoleColor.Blue;            
            Console.SetWindowSize(106, 30);            
            Console.CursorVisible = false;
            Console.Title = "B e t t e r - T e a m s";
        }
        //Wellcome Message
        public void Wellcome()
        {
            Console.WriteLine("                                   ===================================");
            Console.WriteLine("***********************************|    Welcome To Better-Teams!     |***********************************");
            Console.WriteLine("                                   ===================================");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

        }


        //Main Menu UI
        public void UIMainMenu(Users User)
        {
            User = userManager.FindUser(User.Username);
            Console.Clear();
            Console.WriteLine("========== M A I N   M E N U ==========");
            Console.WriteLine("\n");
            Console.WriteLine($"Wellcome, {User.FirstName}!");
            Console.WriteLine("\n");
            Console.WriteLine("1. Chat     2. Rooms     3. View Profile    4. Edit User    0. Log Out");
        }

        public void UIRedirectSignUp()
        {
            Console.WriteLine("Invalid Username or Password");
            Console.WriteLine("1. Create Account  |  2. Try Again");

            char input = Console.ReadKey(true).KeyChar;

            if (input == '1')
                NewUser.SignUp();
        }

        public void ViewProfile(string Username)
        {
            Users user = userManager.FindUser(Username);
            Console.WriteLine($"Username: {user.Username}\nFirst Name: {user.FirstName}\nLast Name: {user.LastName}\nDate Of Birth: {user.DateOFBirth}\nUser Type: {user.UserType}");
            Console.WriteLine("Press any key to go Back to Menu");
            Console.ReadKey();
            Console.Clear();
        }

        public string ShowUsernames(Users user)
        {
            foreach (string Username in userManager.GetUsernames(user.Username))
            {
                Console.WriteLine(Username);
            }
            Console.Write("Select User to Chat: ");
            string User = Console.ReadLine();
            return User;
        }
    }

}

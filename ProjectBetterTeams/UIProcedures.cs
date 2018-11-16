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
            Console.Title = "B e t t e r - T e a m s";
        }

        /// <summary>
        /// Wellcome Message
        /// </summary>
        public void Wellcome()
        {
            Console.WriteLine("                                   ===================================");
            Console.WriteLine("***********************************|    Welcome To Better-Teams!     |***********************************");
            Console.WriteLine("                                   ===================================");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

        }

        /// <summary>
        /// Projects Main Menu
        /// </summary>
        /// <param name="User"></param>
        public void UIMainMenu(Users User)
        {
            User = userManager.FindUser(User.Username);
            Console.Clear();
            Console.WriteLine("========== M A I N   M E N U ==========");
            Console.WriteLine("\n");
            Console.WriteLine($"Wellcome, {User.FirstName}!");
            Console.WriteLine("\n");
            Console.WriteLine("1. Chat  |  2. Posts  |  3. View Profile  |  4. Edit  |  0. Log Out");
        }

        /// <summary>
        /// Redirects to Signup page
        /// </summary>
        public void UIRedirectSignUp()
        {
            Console.WriteLine("");
            Console.WriteLine("Invalid Username or Password");
            Console.WriteLine("1. Create Account  |  2. Try Again");

            char input = Console.ReadKey(true).KeyChar;

            if (input == '1')
                NewUser.SignUp();
        }

        /// <summary>
        /// Shows Users Profile
        /// </summary>
        /// <param name="Username"></param>
        public void ViewProfile(string Username)
        {
            Users user = userManager.FindUser(Username);
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Here Is Your Profile!\nIf you want to Edit it go to Edit User Section!");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine($"Username: {user.Username}\nFirst Name: {user.FirstName}\nLast Name: {user.LastName}\nDate Of Birth: {user.DateOFBirth.Day}/{user.DateOFBirth.Month}/{user.DateOFBirth.Year}\nUser Type: {user.UserType}");
            Console.WriteLine("Press any key to go Back to Menu");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Shows Edit Menu
        /// </summary>
        /// <param name="user"></param>
        public void UIEditMenu(Users user)
        {
            if (user.UserType == "SuperAdmin")
            {
                Console.WriteLine("");
                Console.WriteLine(" ----------------------------------------------------------------");
                Console.WriteLine(" | What do you want to edit?                                    |");
                Console.WriteLine(" | 1. Users | 2. Messages | 3. Posts | 4. Delete Logs | 0. Back |");
                Console.WriteLine(" ----------------------------------------------------------------");
            }
            else if (user.UserType == "Admin")
            {
                Console.WriteLine("");
                Console.WriteLine(" -----------------------------------------------");
                Console.WriteLine(" | What do you want to edit?                   |");
                Console.WriteLine(" | 1. Users | 2. Messages | 3. Posts | 0. Back |");
                Console.WriteLine(" -----------------------------------------------");
            }
            else if (user.UserType == "Teacher")
            {
                Console.WriteLine("");
                Console.WriteLine(" ---------------------------------");
                Console.WriteLine(" | What do you want to edit?     |");
                Console.WriteLine(" | 1. Users | 2. Posts | 0. Back |");
                Console.WriteLine(" ---------------------------------");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine(" ----------------------------");
                Console.WriteLine(" | What do you want to edit?|");
                Console.WriteLine(" |   1. Users  |   0. Back  |");
                Console.WriteLine(" ----------------------------");
            }
        }

        /// <summary>
        /// Shows properties for edit
        /// </summary>
        /// <param name="user"></param>
        public void UIShowAccess(Users user)
        {

                Console.WriteLine("------------------------------------");
                Console.WriteLine($"Choose what do you want to edit:");
                Console.WriteLine("------------------------------------");
                Console.WriteLine($"1. Username: {user.Username}\n2. Password\n3. Firstname: {user.FirstName}\n4. Lastname: {user.LastName}\n5. DateOfBirth: {user.DateOFBirth.Day}/{user.DateOFBirth.Month}/{user.DateOFBirth.Year}\n6. UserType: {user.UserType}\n7. Delete User\n0. Exit");
        }

        /// <summary>
        /// Shows properties of student for edit
        /// </summary>
        /// <param name="user"></param>
        public void ShowProfile(Users user)
        {
            Console.WriteLine("");
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"User: {user.Username}");
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"1. Username: {user.Username}\n2. Password\n3. Firstname: {user.FirstName}\n4. Lastname: {user.LastName}\n5. DateOfBirth: {user.DateOFBirth.Day}/{user.DateOFBirth.Month}/{user.DateOFBirth.Year}\n6. UserType: {user.UserType}\n0. Exit");
        }

        /// <summary>
        /// Projects Usernames in Database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Return a string Username</returns>
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

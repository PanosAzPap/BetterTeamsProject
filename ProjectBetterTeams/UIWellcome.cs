using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProjectBetterTeams
{
    public class UIWelcome
    {
        UserSignUp NewUser = new UserSignUp();
        UserManager userManager = new UserManager();
        PostManager postManager = new PostManager();
        MessageManager messageManager = new MessageManager();

        //Wellcome Message
        public void Wellcome()
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Welcome To ProjectBetterTeams!");
        }

        //Start Menu
        public Int16 StartMenu()
        {
            bool IsValid;
            Int16 MenuChoice = 0;
            do
            {
                IsValid = true;
                Console.WriteLine("1. Log In\n2. Sign Up");

                try
                {
                    MenuChoice = Convert.ToInt16(Console.ReadLine());
                }
                catch (Exception)
                {
                    MenuChoice = 0;
                }

                if ((MenuChoice != 1) && (MenuChoice != 2))
                {
                    Console.WriteLine("Invalid Input! Try Again!");
                    IsValid = false;
                }
            } while (!IsValid);

            return MenuChoice;
        }



        public bool MainMenu(string Username, string Password, out char Choice, out Users User)
        {

            UserManager userManager = new UserManager();

            do
            {
                string input;
                

                if (userManager.ConfirmUser(Username, Password))
                {
                    User = userManager.FindUser(Username);
                    char choice;
                    Console.Clear();
                    Console.WriteLine("========== M A I N   M E N U ==========");
                    Console.WriteLine("\n");
                    Console.WriteLine($"Wellcome, {User.FirstName}!");
                    Console.WriteLine("\n");
                    Console.WriteLine("1. Chat     2. Rooms     3. View Profile");
                    try
                    {
                        choice = char.Parse(Console.ReadLine());
                        Choice = choice;
                        return true;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Username or Password");
                    Console.WriteLine("1. Create Account\n2. Try Again");
                    input = Console.ReadLine();
                    if (input == "1")
                        NewUser.SignUp();
                    Console.Write("Username: ");
                    Username = Console.ReadLine();
                    Console.Write("Password: ");
                    Password = Console.ReadLine();
                }
                
            } while (true);

        }

        public void ViewProfile(string Username)
        {
            Users user = userManager.FindUser(Username);
            Console.WriteLine($"Username: {user.Username}\nFirst Name: {user.FirstName}\nLast Name: {user.LastName}\nDate Of Birth: {user.DateOFBirth}\nUser Type: {user.UserType}");
            Console.WriteLine("Press any key to go Back to Menu");
            Console.ReadKey();
            Console.Clear();
        }

        public void MenuOptions(Users User, char Choice, out bool Stay)
        {
            switch (Choice)
            {
                case '1':
                    ChatMenu(User);
                    Stay = true;
                    break;
                case '2':
                    //Posts
                    Stay = true;
                    break;
                case '3':
                    ViewProfile(User.Username);
                    Stay = false;
                    break;
                case '4':
                    if ((User.UserType == "Teacher") || (User.UserType == "SuperAdmin"))
                        EditMenu(User);
                    Stay = true;

                    break;
                case '0':
                    Stay = false;
                    break;
                default:
                    Console.WriteLine("Invalid Ipnut");
                    Stay = true;
                    break;
            }
        }

        public void EditMenu(Users User)
        {
            if (User.UserType == "SuperAdmin")
            {
                string input;
                Console.WriteLine("What do you want to edit?\n1. Users\n2. Messages\n3. Posts");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        userManager.ModifyUser();
                        break;
                    case "2":
                        Console.WriteLine("Select Message to Delete");
                        messageManager.DeleteMessage();
                        break;
                    case "3":
                        Console.WriteLine("Select Post to Delete:");
                        postManager.DeletePost();
                        break;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Edit:\n1. Users\n2. Go back");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        userManager.ModifyUser();
                        break;
                    case "2":
                        return;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }
            }
        }

        public void ChatBox(Users user)
        {
            bool Exit = true;
            foreach (string Username in userManager.GetUsernames(user.Username))
            {
                Console.WriteLine(Username);
            }
            Console.Write("Select User to Chat: ");
            string User = Console.ReadLine();
            while (Exit)
            {
                Console.Clear();
                messageManager.GetMessages(user.Username, User);
                messageManager.SendMessage(user.Username, User);
                //https://docs.microsoft.com/en-us/dotnet/api/system.eventhandler?view=netframework-4.7.2
                Thread.Sleep(1000);
            }
        }

        public void ChatMenu(Users User)
        {
            Console.WriteLine("1. Chat\n2. Delete Message");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ChatBox(User);
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }
    }
}

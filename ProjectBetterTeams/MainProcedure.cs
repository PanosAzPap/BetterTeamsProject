using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProjectBetterTeams
{
    public class MainProcedure
    {
        UIProcedures UI = new UIProcedures();
        UserSignUp NewUser = new UserSignUp();
        UserManager userManager = new UserManager();
        PostManager postManager = new PostManager();
        MessageManager messageManager = new MessageManager();

        //Start Menu
        public ConsoleKeyInfo StartMenu()
        {
            do
            {
                Console.Clear();
                UI.Wellcome();
                Console.WriteLine("");
                Console.WriteLine("1. Log In  |  2. Sign Up");
                ConsoleKeyInfo MenuChoice = Console.ReadKey(true);

                if (MenuChoice.KeyChar == '1' || MenuChoice.KeyChar == '2')
                    return MenuChoice;

            } while (true);
        }


        public bool MainMenu(string Username, string Password, out char Choice, out Users User)
        {

            UserManager userManager = new UserManager();

            do
            {

                if (userManager.ConfirmUser(Username, Password))
                {
                    User = userManager.FindUser(Username);
                    Console.Clear();

                    UI.UIMainMenu(User);

                    ConsoleKeyInfo choice = Console.ReadKey(true);
                    Choice = choice.KeyChar;

                    return true;
                }
                else
                {
                    UI.UIRedirectSignUp();

                    Console.Write("Username: ");
                    Username = Console.ReadLine();
                    Console.Write("Password: ");
                    Password = Console.ReadLine();
                }
            } while (true);
        }



        public void MenuOptions(Users User, char Choice, out bool Stay)
        {
            do
            {
                switch (Choice)
                {
                    case '1':
                        ChatMenu(User);
                        Stay = true;
                        break;
                    case '2':
                        PostMenu(User);
                        Stay = true;
                        break;
                    case '3':
                        UI.ViewProfile(User.Username);
                        Stay = false;
                        break;
                    case '4':
                        EditMenu(User);
                        Stay = true;
                        break;
                    case '0':
                        Stay = false;
                        break;
                    default:
                        Console.WriteLine("");
                        Stay = true;
                        break;
                }
            } while (Stay);
        }

        public void EditMenu(Users User)
        {
            if (User.UserType == "SuperAdmin")
            {
                string input;
                Console.WriteLine("What do you want to edit?\n1. Users | 2. Messages | 3. Posts");
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
            else if (User.UserType == "Teacher")
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
            else
            {
                userManager.StudentModifyUser(User);
            }

        }

        public void ChatBox(Users user)
        {
            bool Exit = true;
            string User = UI.ShowUsernames(user);
            while (Exit)
            {
                Console.Clear();
                messageManager.GetMessages(user.Username, User);
                Console.WriteLine("1. Send Message | 2. Exit");

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.KeyChar)
                    {
                        case '1':
                            Console.Write("-->");
                            messageManager.SendMessage(user.Username, User);
                            break;
                        case '2':
                            Exit = false;
                            break;
                        default:

                            break;
                    }
                }
                Thread.Sleep(3000);
            }
        }

        public void ChatMenu(Users User)
        {
            Console.WriteLine("1. Select User\n2. Delete Message\n3. Back");
            ConsoleKeyInfo choice = Console.ReadKey(true);
            switch (choice.KeyChar)
            {
                case '1':
                    ChatBox(User);
                    Console.ReadKey();
                    break;
                case '2':
                    messageManager.DeleteMessage();
                    break;
                case '3':
                    UI.UIMainMenu(User);
                    break;
                default:
                    break;
            }
        }

        public void PostMenu(Users user)
        {
            Console.Clear();
            bool Stay = true;

            while (Stay)
            {
                Console.Clear();
                Console.WriteLine("Loading Posts...");
                postManager.GetPosts();
                Console.WriteLine("1. Create A Post\n2. Delete A Post\n5. Exit Posts");
                if (Console.KeyAvailable)
                {
                    Console.WriteLine("Please wait...");
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.KeyChar)
                    {
                        case '1':
                            Console.Write("-->");
                            string Post = Console.ReadLine();
                            postManager.CreatePost(user.Username, Post);
                            Stay = true;
                            break;
                        case '2':
                            Console.Clear();
                            postManager.DeletePost();
                            Stay = true;
                            break;
                        case '5':
                            char escape = key.KeyChar;
                            Stay = false;
                            break;
                        default:

                            break;
                    }
                }
                Thread.Sleep(2000);
            }
        }
    }
}

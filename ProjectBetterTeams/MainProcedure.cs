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
        LogFileAccess Log = new LogFileAccess();

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


        public string HidePassword()
        {
            string password = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        return password;
                    }
                }
            } while (true);
        }


        public bool MainMenu(string Username, string Password, out Users User)
        {

            UserManager userManager = new UserManager();

            do
            {

                if (userManager.ConfirmUser(Username, Password))
                {

                    User = userManager.FindUser(Username);
                    Log.LogUser(User.Username);
                    Console.Clear();

                    UI.UIMainMenu(User);                   

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



        public void MenuOptions(Users User, char Choice)
        {
            do
            {
                switch (Choice)
                {
                    case '1':
                        ChatMenu(User);
                        break;
                    case '2':
                        PostMenu(User);
                        break;
                    case '3':
                        UI.ViewProfile(User.Username);
                        break;
                    case '4':
                        EditMenu(User);
                        break;
                    case '0':                        
                        return;
                    default:
                        Console.WriteLine("");
                        break;                        
                }

                UI.UIMainMenu(User);
                Choice = Console.ReadKey(true).KeyChar;                
            } while (true);
        }

        public void EditMenu(Users User)
        {
            do
            {
                if (User.UserType == "SuperAdmin")
                {
                    char input;
                    Console.WriteLine("What do you want to edit?\n1. Users | 2. Messages | 3. Posts | 0. Back");
                    input = Console.ReadKey(true).KeyChar;

                    switch (input)
                    {
                        case '1':
                            userManager.ModifyUser(User);
                            break;
                        case '2':
                            Console.WriteLine("Select Message to Delete");
                            messageManager.DeleteMessage();
                            break;
                        case '3':
                            Console.WriteLine("Select Post to Delete:");
                            postManager.DeletePost();
                            break;
                        case '0':
                            return;                            
                        default:
                            Console.WriteLine("Invalid Input!");
                            break;
                    }
                }
                else if (User.UserType == "Teacher")
                {
                    Console.WriteLine("Edit:\n1. Users\n2. Go back");
                    char input = Console.ReadKey(true).KeyChar;

                    switch (input)
                    {
                        case '1':
                            userManager.TeacherModifyUser(User);
                            break;
                        case '2':
                            return;
                        default:
                            Console.WriteLine("Invalid Input!");
                            break;
                    }
                }
                else if (User.UserType == "Admin")
                {
                    userManager.AdminModifyUser(User);
                }
                else
                {
                    userManager.StudentModifyUser(User);
                }
            } while (true);
        }

        public void ChatBox(Users user)
        {
            Console.Clear();
            bool Exit = true;
            string User = UI.ShowUsernames(user);
            while (Exit)
            {
                Console.Clear();
                messageManager.GetMessages(user.Username, User);
                Console.WriteLine("1. Send Message | 2. Exit");

                if (Console.KeyAvailable)
                {

                    char key = Console.ReadKey(true).KeyChar;
                    switch (key)
                    {
                        case '1':
                            Console.Write("-->");
                            messageManager.SendMessage(user.Username, User);
                            break;
                        case '2':
                            Console.WriteLine("Exiting...");
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
                    break;
                case '2':
                    messageManager.DeleteMessage();
                    break;
                case '3':
                    return;
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

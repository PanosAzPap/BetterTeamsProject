using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBetterTeams
{
    class Program
    {
        static void Main(string[] args)
        {

            UserManager userManager = new UserManager();
            UIWelcome Wellcome = new UIWelcome();
            UserSignUp NewUser = new UserSignUp();
            do
            {
                Console.Clear();
                Wellcome.Wellcome();
                bool DontExit = true;

                int input = Wellcome.StartMenu();

                if (input == 2)
                {
                    NewUser.SignUp();
                }
                else
                {

                    do
                    {
                        Console.Clear();
                        Wellcome.Wellcome();
                        Console.Write("Username: ");
                        string Username = Console.ReadLine();
                        Console.Write("Password: ");
                        string Password = Console.ReadLine();
                        Users User;
                        char Choice;

                        if (Wellcome.MainMenu(Username, Password, out Choice, out User))
                        {
                            bool Stay;
                            do
                            {
                                Stay = false;
                                Wellcome.MenuOptions(User, Choice, out Stay);
                            } while (Stay);
                        }
                        Console.WriteLine("Press 0 to Log Out!");
                        string exit = Console.ReadLine();
                        if (exit == "0")
                            DontExit = false;
                    } while (DontExit);

                }



                Console.ReadKey();
            } while (true);
        }
    }
}


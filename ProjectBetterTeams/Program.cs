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
            // TODO:
            // 1. Create class LogFileAccess
            // 2. Fix GoBack Method
            // 3. Fix Admin Modification Access in MainProcedure


            UserManager userManager = new UserManager();
            MainProcedure procedure = new MainProcedure();
            UIProcedures UI = new UIProcedures();
            UserSignUp NewUser = new UserSignUp();

            do
            {
                Console.Clear();
                UI.Wellcome();
                bool Exit = true;

                if (procedure.StartMenu().KeyChar == '2')
                {
                    NewUser.SignUp();
                }
                else
                {

                    do
                    {
                        Console.Clear();
                        UI.Wellcome();
                        Console.Write("Username: ");
                        string Username = Console.ReadLine();
                        Console.Write("Password: ");
                        string Password = Console.ReadLine();
                        Users User;
                        char Choice;

                        if (procedure.MainMenu(Username, Password, out Choice, out User))
                        {
                            do
                            {                               
                                procedure.MenuOptions(User, Choice, out Exit);
                            } while (Exit);
                        }
                        Console.WriteLine("Press 0 to Exit!");
                        ConsoleKeyInfo UserExit = Console.ReadKey(true);
                        if (UserExit.KeyChar == '0')
                            Exit = false;
                    } while (Exit);
                }
            } while (true);
        }
    }
}


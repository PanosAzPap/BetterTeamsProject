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
            //Connection strings:
            //DESKTOP-VCB6LRK\SQLEXPRESS
            //PAPMAN-PC\SQLEXPRESS

            // TODO:
            // 2. Fix Admin Modification Access in MainProcedure

            //Initializers
            #region
            MainProcedure procedure = new MainProcedure();
            UIProcedures UI = new UIProcedures();
            UserSignUp NewUser = new UserSignUp();
            #endregion

            Console.Clear();
            UI.Wellcome();
            bool LogOut = false;


            do
            {
                if (procedure.StartMenu().KeyChar == '2')
                {
                    NewUser.SignUp();
                }
                else
                {
                    Console.Clear();
                    UI.Wellcome();
                    Console.Write("Username: ");
                    string Username = Console.ReadLine();
                    Console.Write("Password: ");
                    string Password = procedure.HidePassword();
                    Users User;
                    char Choice;

                    do
                    {
                        LogOut = true;
                        if (procedure.MainMenu(Username, Password, out User))
                        {
                            Choice = Console.ReadKey(true).KeyChar;
                            procedure.MenuOptions(User, Choice);
                        }
                        Console.WriteLine("Are you Sure?(y/n)");
                        char TrueLogOut = Console.ReadKey(true).KeyChar;
                        if (TrueLogOut == 'y')
                            LogOut = false;
                    } while (LogOut);
                }
            } while (true);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;




namespace ProjectBetterTeams
{
    class MessageManager
    {

        public void SendMessage(string Sender, string Receiver)
        {
            Messages message = new Messages();

            string TextMessage = Console.ReadLine();
            string FirstPartText = "";
            string SecondPartText = "";
            if (TextMessage == null || TextMessage == " ")
                TextMessage = "Empty Message";
            if (TextMessage.Length <= 250)
            {

                message.UsernameSender = Sender;
                message.Receiver = Receiver;
                message.DateTime = DateTime.Now;
                message.Message = TextMessage;
                using (var db = new TeamsContext())
                {
                    Console.WriteLine("Sending...");
                    db.Messages.Add(message);
                    db.SaveChanges();
                }
                Console.WriteLine("Μessage sent successfully!");
            }
            else
            {
                for (int i = 0; i < TextMessage.Length; i++)
                {
                    if (i < TextMessage.Length / 2)
                    {
                        FirstPartText = FirstPartText + TextMessage[i];
                    }
                    else
                    {
                        SecondPartText = SecondPartText + TextMessage[i];
                    }
                }
                message.UsernameSender = Sender;
                message.Receiver = Receiver;
                message.DateTime = DateTime.Now;
                message.Message = FirstPartText;
                using (var db = new TeamsContext())
                {
                    Console.WriteLine("Sending (1/2)...");
                    db.Messages.Add(message);
                    db.SaveChanges();
                }
                Console.WriteLine("1/2 Complete");
                message.UsernameSender = Sender;
                message.Receiver = Receiver;
                message.DateTime = DateTime.Now;
                message.Message = FirstPartText;
                using (var db = new TeamsContext())
                {
                    Console.WriteLine("Sending (2/2)...");
                    db.Messages.Add(message);
                    db.SaveChanges();
                }
                Console.WriteLine("Μessage sent!");
            }
        }

        public List<Messages> GetMessages(string Username, string otherUsername)
        {
            UserManager userManager = new UserManager();
            List<Messages> messages = new List<Messages>();

            if (userManager.FindUser(Username) != null)
            {
                using (var db = new TeamsContext())
                {
                    messages = db.Messages.Where(message => (message.UsernameSender == Username || message.Receiver == Username) && (message.UsernameSender == otherUsername || message.Receiver == otherUsername))
                        .Distinct().ToList();
                }

                foreach (var mess in messages)
                {
                    Console.WriteLine($"--From: {mess.UsernameSender} To: {mess.Receiver}, {mess.DateTime}:\n>>> {mess.Message}");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                }
            }
            return messages;
        }

        public void GetMessages()
        {
            List<Messages> messages;
            using (var db = new TeamsContext())
            {
                messages = db.Messages.Select(m => m).ToList();
            }

            foreach (var mess in messages)
            {
                Console.WriteLine($"--ID: {mess.MessageID} From: {mess.UsernameSender} To: {mess.Receiver}, {mess.DateTime}:\n>>> {mess.Message}");
                Console.WriteLine("-----------------------------------------------------------------------------------");
            }
        }

        public Messages FindMessage(int ID)
        {
            Messages message;
            using (var db = new TeamsContext())
            {
                message = db.Messages.Find(ID);
            }
            return message;
        }

        public void DeleteMessage()
        {
            int ID;
            Messages message = null;
            GetMessages();
            Console.Write("Select Message ID to Delete Or 0 to Go Back:");

            try
            {
                ID = int.Parse(Console.ReadLine());
                message = FindMessage(ID);
            }
            catch
            {
                ID = 0;
            }
            if (ID == 0)
                return;
            if (message != null)
            {
                using (var db = new TeamsContext())
                {

                    db.Entry(message).State = EntityState.Deleted;

                    db.SaveChanges();
                }
                Console.WriteLine("Removal Complete!");
            }
            else
            {
                Console.WriteLine("Message Not Found");
            }
        }

        public void DeleteUserMessages(string Username)
        {
            List<Messages> messages;
            using (var db = new TeamsContext())
            {
                Console.WriteLine("Deleting messages...");
                messages = db.Messages.Where(m => m.UsernameSender == Username || m.Receiver == Username).Select(m => m).ToList();

                foreach(var message in messages)
                {
                    db.Entry(message).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }

    }
}

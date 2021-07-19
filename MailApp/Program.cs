using System;
using MailKit.Net.Smtp;
using MimeKit;



namespace MailApp
{
    class Program
    {
        private static ConsoleColor originalBGColor;
        private static ConsoleColor originalFGColor;

        static void Main(string[] args)
        {
            //Create a new mime message object which we are going to use to fill the message data.
            MimeMessage message = new MimeMessage();
            //Add the sender info that will appear in the email message
            message.From.Add(new MailboxAddress("Alkis", "TestEmail@gmail.com"));

            //Add the rerceiver email address
            message.To.Add(MailboxAddress.Parse("idipus1@gmail.com"));

            //add the message subject
            message.Subject = "Keep Learning!";

            //Add the message body as plain text the 'plain'string passed to the TextPart indicates that
            //it's plain text and not HTML for example.
            message.Body = new TextPart("plain")
            {
                Text = @"Yes,
                    Keep Learning.
                    Dude."
            };

            //ask the user to enter the email
            Console.Write("Email: ");
            string emailAddress = Console.ReadLine();
            //Ask the password to the user
            Console.Write("Password: ");
            //for security reasons we need to mask the password,to do that we will turn the console's backround
            //Store original values of the console's backround and foreground color
            //Console.BackgroundColor = ConsoleColor.Green;
            //Console.ForegroundColor = ConsoleColor.Green;
            //Read the password
            string password = Console.ReadLine();
            //Set the console's backround and foreground colors back to the original values we stored earlier
            //Console.BackgroundColor = originalBGColor;
            //Console.ForegroundColor = originalFGColor;

            //Create smtp new client
            SmtpClient client = new SmtpClient();
            try
            {
                //connect to the gmail smtp server using port 465 with ssl enabled
                client.Connect("smtp.gmail.com", 465, true);
                //Note:only needed if the smtp server requires authentication,like gmail for example
                client.Authenticate(emailAddress, password);
                client.Send(message);

                //display message if no exception was thrown
                Console.WriteLine("Email sent!");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                //at any case always disconnect from the client
                client.Disconnect(true);
                //and dispode of the client object
                client.Dispose();
            }
        }
    }
}

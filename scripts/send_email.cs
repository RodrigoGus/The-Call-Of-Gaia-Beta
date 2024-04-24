// using Godot;
// using System;
// using System.Net;
// using System.Net.Mail;


// public partial class send_email : Node
// {
//     SendEmail("andersonsilvasouza714@gmail.com", "", "to@example.com", "Subject Here", "Body of the email here.");


// static void SendEmail(string fromEmail, string password, string toEmail, string subject, string body)
// {
//     try
//     {
//         SmtpClient smtp = new SmtpClient
//         {
//             Host = "smtp.gmail.com",
//             Port = 587,
//             EnableSsl = true,
//             DeliveryMethod = SmtpDeliveryMethod.Network,
//             UseDefaultCredentials = false,
//             Credentials = new NetworkCredential(fromEmail, password)
//         };

//         using (var message = new MailMessage(fromEmail, toEmail)
//         {
//             Subject = subject,
//             Body = body
//         })
//         {
//             smtp.Send(message);
//         }

//         Console.WriteLine("Email sent successfully!");
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine("Failed to send email. Error: " + ex.Message);
//     }
// }

// }
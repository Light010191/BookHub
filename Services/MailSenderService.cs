using MimeKit;

namespace BooksHub.Services
{
    public class MailSenderService
		{
			public async Task Send(string email, string subject, string content)
			{
				var emailMessage = new MimeMessage();
				emailMessage.From.Add(new MailboxAddress("EShop", "postmaster@sandboxe2a0e6699dd74fd99df559ca2398a76a.mailgun.org"));
				emailMessage.To.Add(new MailboxAddress("Client", email));
				emailMessage.Subject = subject;
				emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = content };
				using (var client = new MailKit.Net.Smtp.SmtpClient())
				{
					await client.ConnectAsync("smtp.mailgun.org", 587, false);
					await client.AuthenticateAsync("postmaster@sandboxe2a0e6699dd74fd99df559ca2398a76a.mailgun.org",
                        "bcb97a00f152ad2dc78825b809a796e4-1c7e8847-46088acd");
					await client.SendAsync(emailMessage);
					await client.DisconnectAsync(true);
				}
			}
		}
	}


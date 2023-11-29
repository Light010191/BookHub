using MimeKit;

namespace BooksHub.Services
{
    public class MailSenderService
		{
			public async Task Send(string email, string subject, string content)
			{
				var emailMessage = new MimeMessage();
				emailMessage.From.Add(new MailboxAddress("EShop", "postmaster@....mailgun.org"));
				emailMessage.To.Add(new MailboxAddress("Client", email));
				emailMessage.Subject = subject;
				emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = content };
				using (var client = new MailKit.Net.Smtp.SmtpClient())
				{
					await client.ConnectAsync("smtp.mailgun.org", 587, false);
					await client.AuthenticateAsync("postmaster@....mailgun.org",
                        "key");
					await client.SendAsync(emailMessage);
					await client.DisconnectAsync(true);
				}
			}
		}
	}


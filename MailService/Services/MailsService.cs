using MailKit.Net.Smtp;
using MimeKit;

namespace MailService;

public class MailsService
{
     private readonly string _email;
    private readonly string _password;
    private readonly IConfiguration _configuration;

    public MailsService(IConfiguration configuration)
    {
        _configuration=configuration;
        _email = _configuration.GetValue<string>("EmailSettings:Email");
        _password = _configuration.GetValue<string>("EmailSettings:Password");
    }
    public async Task SendEmail(ChatUserMessageDto chatUser, string Message){
         MimeMessage message1 = new MimeMessage();

            message1.From.Add(new MailboxAddress("ChatApp ", _email));

            message1.To.Add(new MailboxAddress(chatUser.Fullname, chatUser.Email));

            message1.Subject = "Welcome to our ChatApp";

            var body = new TextPart("html")
            {
                Text = Message.ToString()
            };

            message1.Body = body;
             ///

            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);

            client.Authenticate(_email, _password);

            await client.SendAsync(message1);

            await client.DisconnectAsync(true);
    }

}

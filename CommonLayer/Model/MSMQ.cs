using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQ
    {
        MessageQueue messageQ = new MessageQueue();
        public void sendData2Queue(string token)
        {
            messageQ.Path = @".\private$\Bills";
            if(!MessageQueue.Exists(messageQ.Path))
            {
                MessageQueue.Create(messageQ.Path);
            }
            
            messageQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQ.ReceiveCompleted += MessageQ_ReceiveCompleted;
            messageQ.Send(token);
            messageQ.BeginReceive();
            messageQ.Close();
            
        }

        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQ.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();
            string subject = "Fundoo Notes Reset Link";
            string body = token;
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("anantvikramsingh1224@gmail.com", "suhnircszsypazot"),
                EnableSsl = true
            };
            smtp.Send("anantvikramsingh1224@gmail.com", "anantvikramsingh1224@gmail.com", subject, body);
            messageQ.BeginReceive();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace BIDVSmartContent.Services.Contact
{
    public class ContactService
    {
        /// <summary>
        /// Hàm thực thi gửi email. 
        /// </summary>
        /// <param name="smtpUserName">Tên đăng nhập email gửi thư: vd:tuanitpro</param>
        /// <param name="smtpPassword">Mật khẩu của email gửi thư</param>
        /// <param name="smtpHost">Host email. vd smtp.gmail.com</param>
        /// <param name="smtpPort">Port vd: 465</param>
        /// <param name="toEmail">Email nhận vd: tuanitpro@gmail.com</param>
        /// <param name="subject">Chủ đề</param>
        /// <param name="body">Nội dung thư gửi</param>
        /// <returns>True-Thành công/False-Thất bại</returns>
        public bool Send(string smtpUserName, string smtpPassword, string smtpHost, int smtpPort,
            string toEmail, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Host = smtpHost;
                    smtpClient.Port = smtpPort;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(smtpUserName),
                        Body = body,
                        Priority = MailPriority.Normal,
                    };

                    msg.To.Add(toEmail);

                    smtpClient.Send(msg);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
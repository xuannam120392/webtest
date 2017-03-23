using BIDVSmartContent.Models.Contact;
using BIDVSmartContent.Models.Home;
using BIDVSmartContent.Models.Support;
using BIDVSmartContent.Repository.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace BIDVSmartContent.Services.Support
{
    public class SupportService
    {
        public IList<SupportModel> GetListSupport(string status)
        {
            try
            {
                var datas = new List<SupportModel>();
                var supportStore = new SupportStore();
                var dt = supportStore.GetListSupport(status);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new SupportModel()
                    {
                        ID = int.Parse( dt.Rows[i]["SUPPORT_ID"].ToString()),
                        TITLE = dt.Rows[i]["SUPPORT_HOTEN"].ToString(),
                        CONTENT = dt.Rows[i]["SUPPORT_CONTENT"].ToString(),
                        EMAIL = dt.Rows[i]["SUPPORT_EMAIL"].ToString(),
                        STATUS = dt.Rows[i]["SUPPORT_STATUS"].ToString().Trim() == "1",
                        FROM_EMAIL = dt.Rows[i]["FROM_EMAIL"].ToString(),
                        SDT = dt.Rows[i]["SUPPORT_SDT"].ToString()
                    };
                    datas.Add(model);
                }
                return datas;
            }

            catch (Exception)
            {
                return null;
            }
        }
        public bool CreateSupport(ContactModel model)
        {
            try
            {

                var supportStore = new SupportStore();
                var dt = supportStore.CreateSupport(model);
                if (dt)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
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
        public SupportModel GetSupportById(string id)
        {
            try
            {
                var supporttStore = new SupportStore();
                var dt = supporttStore.GetSupportById(id);
                var model = new SupportModel();
                if (dt.Rows.Count == 0) return null;
                model.ID = int.Parse(dt.Rows[0]["SUPPORT_ID"].ToString());
                model.TITLE = dt.Rows[0]["SUPPORT_HOTEN"].ToString();
                model.SDT = dt.Rows[0]["SUPPORT_SDT"].ToString();
                model.STATUS = dt.Rows[0]["SUPPORT_STATUS"].ToString().Trim() == "1";
                model.EMAIL = dt.Rows[0]["SUPPORT_EMAIL"].ToString();
                return model;
            }

            catch (Exception)
            {
                return null;
            }
        }
        public bool SupportChangeStatus(string id, string status)
        {
            try
            {
                var supporttStore = new SupportStore();
                var dt = supporttStore.SupportChangeStatus(id, status);
                if (dt)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SupportDelete(string id)
        {
            try
            {
                var supporttStore = new SupportStore();
                var dt = supporttStore.SupportDelete(id);
                if (dt)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
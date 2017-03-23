
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Xml;

namespace BIDVSmartContent.Helpers
{
    public static class CommonHelper
    {
        public static string GetProcedureForPkqSystem(string procedureName)
        {
            return string.Format("PKG_WEB_BACKEND_SYSTEM.{0}", procedureName);
        }

        public static string GetProcedureForPkqSystemSupport(string procedureName)
        {
            return string.Format("PKG_WEB_BAKEND_SYTEM_SUPPORT.{0}", procedureName);
        }

        public static string GetProcedurePkqOperation(string procedureName)
        {
            return string.Format("PKG_WEB_BACKEND_OPERATION.{0}", procedureName);
        }

        public static string GetProcedurePkqCatalog(string procedureName)
        {
            return string.Format("PKG_WEB_BACKEND_CATALOG.{0}", procedureName);
        }

        public static string GetProcedurePkgConfigSystem(string procedureName)
        {
            return string.Format("PKG_WEB_BACKEND_CONFIGSYSTEM.{0}", procedureName);
        }

        public static string GetProcedurePkgCustomer(string procedureName)
        {
            return string.Format("PKG_WEB_BACKEND_CUSTOMER.{0}", procedureName);
        }
        public static string GetProcedurePkgSupport(string procedureName)
        {
            return string.Format("pkg_web_backend_support.{0}", procedureName);
        }

        public static string GetProcedurePkgTransaction(string procedureName)
        {
            return string.Format("PKG_USSD.{0}", procedureName);
        }
        // replace special character ""\" to ""
        public static string ReplaceSpecialCharacter(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return str.Replace("\"", "").Trim();
        }

        public static string GetIp()
        {
            return string.Empty;
        }

        public static int? ToNullableInt32(string s)
        {
            int i;
            if (Int32.TryParse(s, out i)) return i;
            return null;
        }

        public static long? ToNullableInt64(string s)
        {
            long i;
            if (Int64.TryParse(s, out i)) return i;
            return null;
        }

        public static decimal? ToNullableDecimal(string s)
        {
            decimal i;
            if (Decimal.TryParse(s, out i)) return i;
            return null;
        }

        public static DateTime? ToDateTimeNullable(string s)
        {
            DateTime date;
            if (DateTime.TryParse(s, out date)) return date;
            return null;
        }

        public static DateTime ToDateTimeFormatDDMMYYYY(string s)
        {
            try
            {
                return DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("ToDateTimeFormatDDMMYYYY:{0}", ex));
                return DateTime.Now;
            }
        }
        public static string ToDateTimeString(string s)
        {
            DateTime date;
            if (DateTime.TryParse(s, out date)) return date.ToString("MM/dd/yyyy");
            return string.Empty;
        }
        public static string ConvertXmlStringToJson(string xml)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                return JsonConvert.SerializeXmlNode(doc);
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("ConvertXmlStringToJson: {0}", ex));
                return string.Empty;
            }
        }
        public static string ConvertXmlFileToJson(string file)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(file);
                return JsonConvert.SerializeXmlNode(doc);
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("ConvertXmlFileToJson: {0}", ex));
                return string.Empty;
            }
        }

        public static DynamicJsonConverter.DynamicJsonObject GetObject(string response)
        {
            try
            {
                response = response.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
                var json = CommonHelper.ConvertXmlStringToJson(response);
                var serialize = new JavaScriptSerializer();
                serialize.RegisterConverters(new[] { new DynamicJsonConverter() });
                dynamic obj = serialize.Deserialize(json, typeof(object));
                obj = obj as DynamicJsonConverter.DynamicJsonObject;
                return obj;
            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetObject: {0}", ex));
                return null;
            }
        }
    }
}
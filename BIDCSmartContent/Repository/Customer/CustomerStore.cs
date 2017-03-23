using BIDVSmartContent.Helpers;
using DBConnection.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIDCSmartContent.Repository.Customer
{
    public class CustomerStore
    {
        private DB db = new DB();

        public DataTable GetListCustomer(string name,string status)
        {
            try
            {
                var sql = "CUSTOMER_GetList";
                var sqlParams = new[]
                {
                    new SqlParameter("P_CUS_NAME", SqlDbType.VarChar),
                    new SqlParameter("p_CUS_STATUS", SqlDbType.Char)
                };
                sqlParams[0].Value = name;
                sqlParams[1].Value = status;
                var dt = db.ExecuteDataTable(CommandType.StoredProcedure, sql, sqlParams);
                return dt;

            }
            catch (Exception ex)
            {
                NLogHelper.Logger.Error(string.Format("GetListCustomer: {0}", ex.ToString()));
                return null;
            }
        }
    }
}
using BIDCSmartContent.Models.Customer;
using BIDCSmartContent.Repository.Customer;
using BIDVSmartContent.Models.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDCSmartContent.Services.Customer
{
    public class CustomerService
    {// doi t ty co ty viec o tiep di coppy het ah umh het roi

        public IList<CustomerModel> GetListCustomer(string name, string status)
        {
            try
            {
                var datas = new List<CustomerModel>();
                var CustomerStore = new CustomerStore();
                var dt = CustomerStore.GetListCustomer(name, status);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new CustomerModel()
                    {
                        Id = int.Parse(dt.Rows[i]["CUS_ID"].ToString()),
                        Name = dt.Rows[i]["CUS_NAME"].ToString(),
                        Type = dt.Rows[i]["CUS_TYPE"].ToString(),
                        Status = dt.Rows[i]["CUS_STATUS"].ToString()
                        // CREATED_DATE = DateTime.Parse(dt.Rows[i]["CREATED_DATE"].ToString())
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
    }
}
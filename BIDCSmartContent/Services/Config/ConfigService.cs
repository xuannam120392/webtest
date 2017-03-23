using BIDVSmartContent.Models.ConfigModels;
using BIDVSmartContent.Repository.Config;
using System;
using System.Collections.Generic;

namespace BIDVSmartContent.Services.Config
{
    public class ConfigService
    {
        public IList<ConfigModel> GetListConfig(string code, string status)
        {
            try
            {
                var datas = new List<ConfigModel>();
                var configStore = new ConfigStore();
                var dt = configStore.GetListConfig(code, status);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new ConfigModel()
                    {
                        Code = dt.Rows[i]["CONFIG_CODE"].ToString(),
                        Value = dt.Rows[i]["CONFIG_VALUE"].ToString(),
                        Desc = dt.Rows[i]["CONFIG_DESC"].ToString(),
                        Status = dt.Rows[i]["CONFIG_STATUS"].ToString().Trim() == "1"
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
        public bool CreateConfig(ConfigModel model)
        {
            try
            {

                var configStore = new ConfigStore();
                var dt = configStore.CreateConfig(model);
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
        public bool UpdateConfig(ConfigModel model)
        {
            try
            {

                var configStore = new ConfigStore();
                var dt = configStore.UpdateConfig(model);
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

        public ConfigModel GetConfigById(string code)
        {
            try
            {
                var configStore = new ConfigStore();
                var model = new ConfigModel();
                var dt = configStore.GetConfigById(code);
                if (dt.Rows.Count == 0) return null;
                model.Code = dt.Rows[0]["CONFIG_CODE"].ToString();
                model.Value = dt.Rows[0]["CONFIG_VALUE"].ToString();
                model.Desc = dt.Rows[0]["CONFIG_DESC"].ToString();
                model.Status = dt.Rows[0]["CONFIG_STATUS"].ToString() == "1";
                return model;
            }

            catch (Exception)
            {
                return null;
            }
        }
        public bool ConfigChangeStatus(string  code, string status)
        {
            try
            {

                var configStore = new ConfigStore();
                var dt = configStore.ConfigChangeStatus(code, status);
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

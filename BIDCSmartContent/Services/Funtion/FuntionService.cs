using BIDVSmartContent.Models.FunctionModel;
using BIDVSmartContent.Repository.Funtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Services.Funtion
{
    public class FuntionService
    {
        public IList<FunctionViewModel> GetFunctionsByUserId(decimal id, decimal? parentId, string funcCode, string funcDisplay)
        {
            var funStore = new FunStore();
            var dt = funStore.GetFunctionsByUserId(id, parentId, funcCode, funcDisplay);
            var temp = new Dictionary<decimal, FunctionViewModel>();
            var map = new Dictionary<decimal, decimal>();
            int min = 100;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var model = new FunctionViewModel()
                {
                    FuncId = int.Parse(dt.Rows[i]["FUNC_ID"].ToString()),
                    FuncCode = dt.Rows[i]["FUNC_CODE"].ToString(),
                    FuncName = dt.Rows[i]["FUNC_NAME"].ToString(),
                    FuncUrl = dt.Rows[i]["FUNC_URL"].ToString(),
                    FuncOrder = Int32.Parse(dt.Rows[i]["FUNC_ORDER"].ToString()),
                    FuncDisplay = dt.Rows[i]["FUNC_DISPLAY"].ToString(),
                    FuncLevel = Int32.Parse(dt.Rows[i]["FUNC_LEVEL"].ToString()),
                    FuncParentId = Int64.Parse(dt.Rows[i]["PARENT_ID"].ToString()),
                    FuncControl = dt.Rows[i]["FUNC_CONTROL"].ToString()
                };

                temp.Add(model.FuncId, model);
                if (model.FuncLevel > 0 && model.FuncLevel < min)
                {
                    min = model.FuncLevel;
                }
                if (model.FuncParentId != null)
                {
                    map.Add(model.FuncId, (decimal)model.FuncParentId);
                }

            }
            foreach (var key in map.Keys)
            {
                if (temp.ContainsKey(key) && temp.ContainsKey(map[key]))
                {
                    temp[map[key]].ChildFunctions.Add(temp[key]);
                }
            }
            return temp.Values.ToList();
        }

        public IList<FunctionViewModel> GetListFuntion(string code)
        {
            try
            {
                var datas = new List<FunctionViewModel>();
                var funStore = new FunStore();
                var dt = funStore.GetListFuntion(code);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new FunctionViewModel()
                    {
                        FuncId = int.Parse(dt.Rows[i]["FUNC_ID"].ToString()),
                        FuncCode = dt.Rows[i]["FUNC_CODE"].ToString(),
                        FuncName = dt.Rows[i]["FUNC_NAME"].ToString(),
                        FuncUrl = dt.Rows[i]["FUNC_URL"].ToString(),
                        FuncOrder = int.Parse(dt.Rows[i]["FUNC_ORDER"].ToString()),
                        FuncDisplay = dt.Rows[i]["FUNC_DISPLAY"].ToString(),
                        FuncLevel = int.Parse(dt.Rows[i]["FUNC_LEVEL"].ToString()),
                        FuncParentId = int.Parse(dt.Rows[i]["PARENT_ID"].ToString()),
                        FuncControl = dt.Rows[i]["FUNC_CONTROL"].ToString()
                    };
                    datas.Add(model);
                }
                return datas;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public bool CreateFuntion(FunctionViewModel model)
        {
            try
            {

                var funStore = new FunStore();
                var dt = funStore.CreateFuntion(model);
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
        public bool UpdateFuntion(FunctionViewModel model)
        {
            try
            {

                var funStore = new FunStore();
                var dt = funStore.UpdateFuntion(model);
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

        public FunctionViewModel GetFunById(string id)
        {
            try
            {
                var funStore = new FunStore();
                var model= new FunctionViewModel();
                var dt = funStore.GetFunById(id);               
                if (dt.Rows.Count == 0) return null;
                        model.FuncId = int.Parse(dt.Rows[0]["FUNC_ID"].ToString());
                        model.FuncCode = dt.Rows[0]["FUNC_CODE"].ToString();
                        model.FuncName = dt.Rows[0]["FUNC_NAME"].ToString();
                        model.FuncUrl = dt.Rows[0]["FUNC_URL"].ToString();
                        model.FuncOrder = int.Parse(dt.Rows[0]["FUNC_ORDER"].ToString());
                        model.FuncDisplay = dt.Rows[0]["FUNC_DISPLAY"].ToString();
                        model.FuncLevel = int.Parse(dt.Rows[0]["FUNC_LEVEL"].ToString());
                        model.FuncParentId = int.Parse(dt.Rows[0]["PARENT_ID"].ToString());
                        model.FuncControl = dt.Rows[0]["FUNC_CONTROL"].ToString();
                return model;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}
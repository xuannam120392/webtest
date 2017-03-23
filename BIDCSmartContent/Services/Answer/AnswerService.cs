using BIDVSmartContent.Models.AnswerModels;
using BIDVSmartContent.Repository.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace BIDVSmartContent.Services.Answer
{
    public class AnswerService
    {
        public IList<AnswerModel> GetListAnswer(string status)
        {
            try
            {
                var datas = new List<AnswerModel>();
                var answerStore = new AnswerStore();
                var dt = answerStore.GetListAnswer(status);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new AnswerModel()
                    {
                        AS_ID = int.Parse(dt.Rows[i]["AS_ID"].ToString()),
                        AS_CONTENT = HtmlRemoval.StripTagsRegex(dt.Rows[i]["AS_CONTENT"].ToString()),
                        QS_CONTENT = HtmlRemoval.StripTagsRegex(dt.Rows[i]["QS_CONTENT"].ToString()),
                        CREATED_USER = dt.Rows[i]["CREATED_USER"].ToString(),
                        AS_STATUS = dt.Rows[i]["AS_STATUS"].ToString().Trim()=="1"
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
        public bool CreateAnswer(AnswerModel model)
        {
            try
            {

                var answerStore = new AnswerStore();
                var dt = answerStore.CreateAnswer(model);
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
        public bool UpdateAnswer(AnswerModel model)
        {
            try
            {

                var answerStore = new AnswerStore();
                var dt = answerStore.UpdateAnswer(model);
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
        public bool AnswerDelete(string id)
        {
            try
            {

                var answerStore = new AnswerStore();
                var dt = answerStore.AnswerDelete(id);
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
        public AnswerModel GetAnswerById(string id)
        {
            try
            {
                var answerStore = new AnswerStore();
                var dt = answerStore.GetAnswerById(id);
                var model = new AnswerModel();
                if (dt.Rows.Count == 0) return null;
                model.AS_ID = int.Parse(dt.Rows[0]["AS_ID"].ToString());
                model.AS_CONTENT = dt.Rows[0]["AS_CONTENT"].ToString();
                model.QS_CONTENT = dt.Rows[0]["QS_CONTENT"].ToString();
                model.AS_STATUS = dt.Rows[0]["AS_STATUS"].ToString() == "1";
                return model;
            }

            catch (Exception)
            {
                return null;
            }
        }
        public bool AnswerChangeStatus(string id, string status)
        {
            try
            {

                var answerStore = new AnswerStore();
                var dt = answerStore.AnswerChangeStatus(id, status);
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
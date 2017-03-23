using BIDVSmartContent.Models.Block;
using BIDVSmartContent.Repository.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDVSmartContent.Services.Block
{
    public class BlockService
    {
        public IList<BlockModel> GetListBlock(string tab,string status)
        {
            try
            {
                var datas = new List<BlockModel>();
                var blockStore = new BlockStore();
                var dt = blockStore.GetListBlock(tab,status);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new BlockModel()
                    {
                        ID = int.Parse(dt.Rows[i]["BLOCK_ID"].ToString()),
                        TITLE = dt.Rows[i]["BLOCK_TITLE"].ToString(),
                        CONTENT = HtmlRemoval.StripTagsRegex(dt.Rows[i]["BLOCK_CONTENT"].ToString()),
                        TAB = dt.Rows[i]["BLOCK_TAB"].ToString(),
                        POSITION = dt.Rows[i]["BLOCK_POSITION"].ToString(),
                        STATUS = dt.Rows[i]["BLOCK_STATUS"].ToString().Trim() == "1",
                        SECTION = dt.Rows[i]["BLOCK_SECTION"].ToString(),
                        IMAGE = dt.Rows[i]["BLOCK_IMAGE"].ToString()
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
        public IList<BlockModel> GetListBlockFont(string section)
        {
            try
            {
                var datas = new List<BlockModel>();
                var blockStore = new BlockStore();
                var dt = blockStore.GetListBlockFont(section);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new BlockModel()
                    {
                        ID = int.Parse(dt.Rows[i]["BLOCK_ID"].ToString()),
                        TITLE = dt.Rows[i]["BLOCK_TITLE"].ToString(),
                        CONTENT = HtmlRemoval.StripTagsRegex(dt.Rows[i]["BLOCK_CONTENT"].ToString()),
                        TAB = dt.Rows[i]["BLOCK_TAB"].ToString(),
                        POSITION = dt.Rows[i]["BLOCK_POSITION"].ToString(),
                        STATUS = dt.Rows[i]["BLOCK_STATUS"].ToString().Trim() == "1",
                        SECTION = dt.Rows[i]["BLOCK_SECTION"].ToString(),
                        IMAGE = dt.Rows[i]["BLOCK_IMAGE"].ToString()
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
        public BlockModel GetBlockBySection(string section)
        {
            try
            {
                var datas = new List<FontBlockViewModel>();
                var blockStore = new BlockStore();
                var dt = blockStore.GetBlockBySection(section);
                var listFrontModelTab1 = new FontBlockViewModel();
                var model = new BlockModel();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    model = new BlockModel()
                    {
                        ID = int.Parse(dt.Rows[i]["BLOCK_ID"].ToString()),
                        TITLE = dt.Rows[i]["BLOCK_TITLE"].ToString(),
                        CONTENT = dt.Rows[i]["BLOCK_CONTENT"].ToString(),
                        TAB = dt.Rows[i]["BLOCK_TAB"].ToString(),
                        POSITION = dt.Rows[i]["BLOCK_POSITION"].ToString(),
                        STATUS = dt.Rows[i]["BLOCK_STATUS"].ToString().Trim() == "1"
                    };
                }
                return model;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public bool CreateBlock(BlockModel model)
        {
            try
            {

                var blockStore = new BlockStore();
                var dt = blockStore.CreateBlock(model);
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
        public bool UpdateBlock(BlockModel model)
        {
            try
            {

                var blockStore = new BlockStore();
                var dt = blockStore.UpdateBlock(model);
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

        public BlockModel GetBlockById(string id)
        {
            try
            {
                var blockStore = new BlockStore();
                var dt = blockStore.GetBlockById(id);
                var model = new BlockModel();
                if (dt.Rows.Count == 0) return null;
                model.ID = int.Parse(dt.Rows[0]["BLOCK_ID"].ToString());
                model.TITLE = dt.Rows[0]["BLOCK_TITLE"].ToString();
                model.CONTENT = dt.Rows[0]["BLOCK_CONTENT"].ToString();
                model.TAB = dt.Rows[0]["BLOCK_TAB"].ToString();
                model.POSITION = dt.Rows[0]["BLOCK_POSITION"].ToString();
                model.STATUS = dt.Rows[0]["BLOCK_STATUS"].ToString() == "1";
                model.SECTION = dt.Rows[0]["BLOCK_SECTION"].ToString();
                model.IMAGE = dt.Rows[0]["BLOCK_IMAGE"].ToString();
                return model;
            }

            catch (Exception)
            {
                return null;
            }
        }
        public bool BlockChangeStatus(string id, string status)
        {
            try
            {

                var blockStore = new BlockStore();
                bool dt = blockStore.BlockChangeStatus(id, status);
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
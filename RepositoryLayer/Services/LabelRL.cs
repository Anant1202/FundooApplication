using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public LabelEntity CreateLabel(LabelModel labelModel, long userid, long noteid)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();
                labelEntity.LabelName = labelModel.LabelName;
                labelEntity.NoteID = noteid;
                labelEntity.UserId = userid;
                fundooContext.LabelTable.Add(labelEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return labelEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<LabelEntity> GetAllLabel()
        {
            return fundooContext.LabelTable.ToList();
        }
        public LabelEntity UpdateLabel(LabelModel labelModel, long id)
        {
            LabelEntity data = fundooContext.LabelTable.SingleOrDefault(x => x.LabelID == id);
            if (data != null)
            {

                data.LabelName = labelModel.LabelName;
                fundooContext.LabelTable.Update(data);
                fundooContext.SaveChanges();
                return data;
            }
            else
            {
                return null;
            }
        }
        public LabelEntity DeleteLabel(long id)
        {
            LabelEntity data = fundooContext.LabelTable.SingleOrDefault(x => x.LabelID == id);
            if (data != null)
            {

                fundooContext.LabelTable.Remove(data);
                fundooContext.SaveChanges();
                return data;
            }
            else
            {
                return null;
            }
        }
    }
}

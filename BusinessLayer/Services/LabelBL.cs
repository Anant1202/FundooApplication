using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBL :ILabelBL
    {
        private readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }
        public LabelEntity CreateLabel(LabelModel labelModel, long userid, long noteid)
        {
            try
            {
                return labelRL.CreateLabel(labelModel, userid, noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<LabelEntity> GetAllLabel()
        {
            try
            {
                return labelRL.GetAllLabel();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public LabelEntity UpdateLabel(LabelModel labelModel, long id)
        {
            try
            {
                return labelRL.UpdateLabel(labelModel, id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public LabelEntity DeleteLabel(long id)
        {
            try
            {
                return labelRL.DeleteLabel(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

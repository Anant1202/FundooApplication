using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity CreateLabel(LabelModel labelModel, long userid, long noteid);
        public IEnumerable<LabelEntity> GetAllLabel();
        public LabelEntity UpdateLabel(LabelModel labelModel, long id);
        public LabelEntity DeleteLabel(long id);
    }
}

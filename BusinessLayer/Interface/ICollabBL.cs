using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity CreateCollab(CollabModel collabModel, long userid, long noteid);
        public IEnumerable<CollabEntity> GetCollabDetails();
        public CollabEntity DeleteCollab(long id);
    }
}

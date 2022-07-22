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
    public class CollabRL : ICollabRL
    {
        private readonly FundooContext fundooContext;
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public CollabEntity CreateCollab(CollabModel collabModel, long userid,long noteid)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();
                collabEntity.EmailID = collabModel.EmailID;
                collabEntity.NoteID = noteid;
                collabEntity.UserId = userid;
                fundooContext.CollabTable.Add(collabEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return collabEntity;
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
        public IEnumerable<CollabEntity> GetCollabDetails(long NoteID)
        {
            return fundooContext.CollabTable.ToList();
        }
        public CollabEntity DeleteCollab(long id)
        {
            CollabEntity data = fundooContext.CollabTable.SingleOrDefault(x => x.CollabID == id);
            if (data != null)
            {

                fundooContext.CollabTable.Remove(data);
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
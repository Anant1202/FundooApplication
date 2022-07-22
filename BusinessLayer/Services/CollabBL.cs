using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public CollabEntity CreateCollab(CollabModel collabModel, long userid, long noteid)
        {
            try
            {
                return collabRL.CreateCollab(collabModel, userid, noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<CollabEntity> GetCollabDetails(long NoteID)
        {
            try
            {
                return collabRL.GetCollabDetails(NoteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public CollabEntity DeleteCollab(long id)
        {
            try
            {
                return collabRL.DeleteCollab(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

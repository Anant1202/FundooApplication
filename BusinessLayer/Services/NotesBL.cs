using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NotesBL : INotesBL 
    {
        private readonly INotesRL notesRL;
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }
        public NoteEntity CreateNote(NoteCreateModel noteCreateModel, long userid)
        {
            try
            {
                return notesRL.CreateNote(noteCreateModel,userid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<NoteEntity> GetNote(long id)
        {
            try
            {
                return notesRL.GetNote(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

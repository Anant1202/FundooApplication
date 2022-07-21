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
        public IEnumerable<NoteEntity> GetNote()
        { 
            try
            {
                return notesRL.GetNote();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NoteEntity UpdateNote(UpdateNoteModel updateNoteModel,long id)
        {
            try
            {
                return notesRL.UpdateNote(updateNoteModel,id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NoteEntity DeleteNote(long id)
        {
            try
            {
                return notesRL.DeleteNote(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NoteEntity Archieve(long id)
        {
            try
            {
                return notesRL.Archieve(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NoteEntity Pin(long id)
        {
            try
            {
                return notesRL.Pin(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NoteEntity Trash(long id)
        {
            try
            {
                return notesRL.Trash(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

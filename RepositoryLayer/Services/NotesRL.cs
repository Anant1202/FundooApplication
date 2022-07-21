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
    public class NotesRL : INotesRL
    {
        private readonly FundooContext fundooContext;
        public NotesRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public NoteEntity CreateNote(NoteCreateModel noteCreateModel, long userid)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = noteCreateModel.Title;
                noteEntity.Description = noteCreateModel.Description;
                noteEntity.Image = noteCreateModel.Image;
                noteEntity.Color = noteCreateModel.Color;
                noteEntity.Archieve = noteCreateModel.Archieve;
                noteEntity.Pin = noteCreateModel.Pin;
                noteEntity.Trash = noteCreateModel.Trash;
                noteEntity.Remainder = noteCreateModel.Remainder;
                noteEntity.CreatedAt = DateTime.Now;
                noteEntity.Modified = noteCreateModel.Modified;
                noteEntity.UserId = userid;
                fundooContext.NotesTable.Add(noteEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return noteEntity;
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
        public IEnumerable<NoteEntity> GetNote()
        {
            return fundooContext.NotesTable.ToList();
        }
        public NoteEntity UpdateNote(UpdateNoteModel updateNoteModel, long id)
        {
            NoteEntity data = fundooContext.NotesTable.SingleOrDefault(x => x.NoteID == id);
            if (data != null)
            {

                data.Title = updateNoteModel.Title;
                data.Description = updateNoteModel.Description;
                data.Image = updateNoteModel.Image;
                data.Color = updateNoteModel.Color;
                data.Archieve = updateNoteModel.Archieve;
                data.Pin = updateNoteModel.Pin;
                data.Trash = updateNoteModel.Trash;
                data.Remainder = updateNoteModel.Remainder;
                data.CreatedAt = updateNoteModel.CreatedAt;
                data.Modified = updateNoteModel.Modified;
                fundooContext.NotesTable.Update(data);
                fundooContext.SaveChanges();
                return data;
            }
            else
            {
                return null;
            }
        }
        public NoteEntity DeleteNote(long id)
        {
            NoteEntity data = fundooContext.NotesTable.SingleOrDefault(x => x.NoteID == id);
            if (data != null)
            {
                
                fundooContext.NotesTable.Remove(data);
                fundooContext.SaveChanges();
                return data;
            }
            else 
            {
                return null;
            }
        }
        public NoteEntity Archieve(long id)
        {
            try
            {
                NoteEntity data = fundooContext.NotesTable.SingleOrDefault(x => x.NoteID == id);
                if (data.Archieve ==true)
                {
                    data.Archieve = false;
                    fundooContext.SaveChanges();
                    return data;
                }
                else
                {
                    data.Archieve = true;
                    fundooContext.SaveChanges();
                    return null;
                }
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
                NoteEntity data = fundooContext.NotesTable.SingleOrDefault(x => x.NoteID == id);
                if (data.Pin == true)
                {
                    data.Pin = false;
                    fundooContext.SaveChanges();
                    return data;
                }
                else
                {
                    data.Pin = true;
                    fundooContext.SaveChanges();
                    return null;
                }
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
                NoteEntity data = fundooContext.NotesTable.SingleOrDefault(x => x.NoteID == id);
                if (data.Trash == true)
                {
                    data.Trash = false;
                    fundooContext.SaveChanges();
                    return data;
                }
                else
                {
                    data.Trash = true;
                    fundooContext.SaveChanges();
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}


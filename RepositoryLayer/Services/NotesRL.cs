using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
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
        public NoteEntity CreateNote(NoteCreateModel noteCreateModel)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title=noteCreateModel.Title;
                noteEntity.Description=noteCreateModel.Description;
                noteEntity.Image=noteCreateModel.Image;
                noteEntity.Color=noteCreateModel.Color;
                noteEntity.Archieve=noteCreateModel.Archieve;
                noteEntity.Pin=noteCreateModel.Pin;
                noteEntity.Trash=noteCreateModel.Trash;
                noteEntity.Remainder=noteCreateModel.Remainder;
                noteEntity.CreatedAt=noteCreateModel.CreatedAt;
                noteEntity.Modified = noteCreateModel.Modified;
                noteEntity.UserId=noteCreateModel.UserId;
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
    }
}

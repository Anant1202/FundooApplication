using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NoteEntity CreateNote(NoteCreateModel noteCreateModel, long userid);
        public IEnumerable<NoteEntity> GetNote();
        public NoteEntity UpdateNote(UpdateNoteModel updateNoteModel, long id);
        public NoteEntity DeleteNote(long id);
        public NoteEntity Archieve(long id);
        public NoteEntity Pin(long id);
        public NoteEntity Trash(long id);
        public NoteEntity Image(long id, IFormFile image);
    }
}

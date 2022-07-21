using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NoteEntity CreateNote(NoteCreateModel noteCreateModel, long userid);
        public IEnumerable<NoteEntity> GetNote(long id);


    }
}

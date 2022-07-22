using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entities
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabID { get; set; }
        public string EmailID { get; set; }
        [ForeignKey("NotesTable")]
        public long NoteID { get; set; }
        [JsonIgnore]
        public virtual NoteEntity NotesTable { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity User { get; set; }
    }
}

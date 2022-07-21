using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UpdateNoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public bool Archieve { get; set; }
        public bool Pin { get; set; }
        public bool Trash { get; set; }
        public DateTime Remainder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Modified { get; set; }
    }
}

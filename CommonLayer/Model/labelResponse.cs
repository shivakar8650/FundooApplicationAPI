using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class labelResponse
    {      
       public long labelID { get; set; }
        public string labelName { get; set; }
        public long? NoteId { get; set; }
        public long UserId { get; set; }
    }
}

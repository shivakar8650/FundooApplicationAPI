using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Enitity
{
    public class collaborator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long collabId { get; set; }
        public virtual Note Note { get; set; }
   
        public long NoteId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        public string EmailId { get; set; }

     
    }
}

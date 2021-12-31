using CommonLayer.Model;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
       public bool GenerateNote(UserNote notes);
        IEnumerable<Note> GetAllNotes();
        UserNote UpdateNotes(UserNote notes);
        bool DeleteNotes(long iD);
    }
}

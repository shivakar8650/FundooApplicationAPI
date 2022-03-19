using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
       public bool GenerateNote(UserNote notes,long UserId);
        IEnumerable<Note> GetAllNotes();
        UpdateNotes UpdateNotes(UpdateNotes notes,long  UserId);
        bool DeleteNotes(takeNoteId note);
        string  PinORUnPinNote(long noteid);
        string ArchiveORUnarchiveNote(takeNoteId note);
        string TrashOrRestoreNote(takeNoteId note);
        string ColorNote(ChangeColorNote note);
        IEnumerable<Note> GetAllNotesOfUser(long UserId);
        bool UploadImage(long noteId, IFormFile image);
    }
}

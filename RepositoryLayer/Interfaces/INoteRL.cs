using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        public bool GenerateNote(UserNote notes,long UserId);
        IEnumerable<Note> GetAllNotes();
        UpdateNotes UpdateNotes(UpdateNotes notes,long UserId);
        bool DeleteNotes(takeNoteId note);
        string PinORUnPinNote(long noteid);
        string TrashOrRestoreNote(takeNoteId note);
        string ColorNote(ChangeColorNote note);
        string ArchiveORUnarchiveNote(takeNoteId note);
        IEnumerable<Note> GetAllNotesOfUser(long UserId);
        bool UploadImage(long noteId, IFormFile image);
    }
}

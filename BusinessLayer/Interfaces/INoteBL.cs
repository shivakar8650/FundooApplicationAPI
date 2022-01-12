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
        UserNote UpdateNotes(UserNote notes,long  UserId,long Noteid);
        bool DeleteNotes(long iD);
        string  PinORUnPinNote(long noteid);
        string ArchiveORUnarchiveNote(long noteid);
        string TrashOrRestoreNote(long noteid);
        string ColorNote(long noteId, string color);
        IEnumerable<Note> GetAllNotesOfUser(long UserId);
        bool UploadImage(long noteId, IFormFile image);
    }
}

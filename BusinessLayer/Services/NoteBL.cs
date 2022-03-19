using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    { 
        INoteRL NoteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.NoteRL = noteRL;
        }
        public string ArchiveORUnarchiveNote(takeNoteId note)
        {
            try
            {
                return this.NoteRL.ArchiveORUnarchiveNote(note);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ColorNote(ChangeColorNote note)
        {
            try
            {
                return this.NoteRL.ColorNote(note);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteNotes(takeNoteId note)
        {
            try
            {
                return this.NoteRL.DeleteNotes(note);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool GenerateNote(UserNote notes,long UserId)
        {
            try
            {
                return this.NoteRL.GenerateNote(notes, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Note> GetAllNotes()
        {
            return this.NoteRL.GetAllNotes();
          
        }
        public IEnumerable<Note> GetAllNotesOfUser(long UserId)
        {
            return this.NoteRL.GetAllNotesOfUser(UserId);
        }
        public string PinORUnPinNote(long noteid)
        {
            try
            {
                return this.NoteRL.PinORUnPinNote( noteid);
            }
            catch (Exception )
            {
                throw ;
            }
        }
        public string TrashOrRestoreNote(takeNoteId note)
        {
            try
            {
                return this.NoteRL.TrashOrRestoreNote(note);
            }
            catch (Exception )
            {
                throw;
            }
        }

        public UpdateNotes UpdateNotes(UpdateNotes notes, long UserId)
        {
            try
            {
                return this.NoteRL.UpdateNotes(notes,UserId);
            }
            catch (Exception )
            {
                throw ;
            }
        }
        public bool UploadImage(long noteId, IFormFile image)
        {
            try
            {
                return this.NoteRL.UploadImage(noteId,image);
            }
            catch (Exception )
            {
                throw ;
            }
        }
    }
}


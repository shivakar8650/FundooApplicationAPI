using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Model;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {

        INoteRL NoteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.NoteRL = noteRL;
        }

        public string ArchiveORUnarchiveNote(long noteid)
        {
            try
            {
                return this.NoteRL.ArchiveORUnarchiveNote(noteid);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }

        }

        public string ColorNote(long noteId, string color)
        {
            try
            {
                return this.NoteRL.ColorNote(noteId, color);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        public bool DeleteNotes(long Id)
        {
            try
            {
                return this.NoteRL.DeleteNotes(Id);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }

        }

        public bool GenerateNote(UserNote notes,long UserId)
        {
            try
            {
                return this.NoteRL.GenerateNote(notes, UserId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return this.NoteRL.GetAllNotes();
          
        }

        public string PinORUnPinNote(long noteid)
        {
            try
            {
                return this.NoteRL.PinORUnPinNote( noteid);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        public string TrashOrRestoreNote(long noteid)
        {
            try
            {
                return this.NoteRL.TrashOrRestoreNote(noteid);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        public UserNote UpdateNotes(UserNote notes, long UserId, long Noteid)
        {
            try
            {
                return this.NoteRL.UpdateNotes(notes,UserId,Noteid);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
    }
}


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
        public bool DeleteNotes(long Id)
        {
            try
            {
                return this.NoteRL.DeleteNotes(Id);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public bool GenerateNote(UserNote notes)
        {
            try
            {
                return this.NoteRL.GenerateNote(notes);
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

        public UserNote UpdateNotes(UserNote notes)
        {
            try
            {
                return this.NoteRL.UpdateNotes(notes);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}


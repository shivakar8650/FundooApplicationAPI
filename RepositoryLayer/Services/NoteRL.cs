using CommonLayer.Model;

using RepositoryLayer.Context;
using RepositoryLayer.Enitity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        readonly UserContext context;  
        public NoteRL(UserContext context)
        {
            this.context = context;
        }

       public bool GenerateNote(UserNote notes )
        {
            try
            {
                Note newNotes = new Note();
                newNotes.Title = notes.Title;
                newNotes.Message = notes.Message;
                newNotes.Remainder = notes.Remainder;
                newNotes.Color = notes.Color;
                newNotes.Image = notes.Image;
                newNotes.IsArchive = notes.IsArchive;
                newNotes.IsPin = notes.IsPin;
                newNotes.IsTrash = notes.IsTrash;
                newNotes.Id = notes.Id;
                newNotes.Createat = notes.Createat;
                //Adding the data to database
                this.context.NoteTable.Add(newNotes);
                //Save the changes in database
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return context.NoteTable.ToList();
        }

        public UserNote UpdateNotes(UserNote notes)
        {
          
                var UpdateNote = this.context.NoteTable.Where(Y => Y.Id == notes.Id).FirstOrDefault();
                if (UpdateNote != null)
                {
                    UpdateNote.Title = notes.Title;
                    UpdateNote.Message = notes.Message;
                    UpdateNote.Remainder = notes.Remainder;
                    UpdateNote.Color = notes.Color;
                    UpdateNote.Image = notes.Image;
                    UpdateNote.IsArchive = notes.IsArchive;
                    UpdateNote.IsPin = notes.IsPin;
                    UpdateNote.IsTrash = notes.IsTrash;
                    UpdateNote.Createat = notes.Createat;
                   
                   
                }
            var result = this.context.SaveChanges();
            if (result > 0)
            {
                return notes;
            }
            else
            {
                return default;
            }


        }
        public bool DeleteNotes(long id)
        {
            try
            {
                var ValidNote = this.context.NoteTable.Where(Y => Y.Id == id).FirstOrDefault();
              
                //Deleting user details from the database user table
                this.context.NoteTable.Remove(ValidNote);

                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
    

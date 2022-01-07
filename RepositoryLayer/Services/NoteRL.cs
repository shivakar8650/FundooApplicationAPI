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

       public bool GenerateNote(UserNote notes,long UserId)
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
                newNotes.Id = UserId;
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
                throw e.InnerException;
            }
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return context.NoteTable.ToList();
        }

        public UserNote UpdateNotes(UserNote notes,long  UserId, long Noteid)
        {
            try
            {
                var UpdateNote = this.context.NoteTable.Where(Y => Y.NoteId == Noteid).FirstOrDefault();
                if (UpdateNote != null && UpdateNote.Id == UserId)
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
            catch(Exception e)
            {
                throw e.InnerException;
            }


        }
        public bool DeleteNotes(long id)
        {
            try
            {
                var ValidNote = this.context.NoteTable.Where(Y => Y.NoteId == id).FirstOrDefault();
              
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
                throw e.InnerException;
            }
        }

        public string PinORUnPinNote(long noteid)
        {
            try
            {
                var Note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == noteid);
                if (Note.IsPin == true)
                {
                    Note.IsPin = false;
                    this.context.SaveChanges();
                    return "Note is UnPinned";
                }
                else
                {
                    Note.IsPin = true;
                    this.context.SaveChanges();
                    return "Note is Pinned";
                }
                
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public string ArchiveORUnarchiveNote(long noteid)
        {
            try
            {
                var Note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == noteid);
                if (Note.IsArchive == true)
                {
                    Note.IsArchive = false;
                    this.context.SaveChanges();
                    return "Note Unarchived";
                }
                else
                {
                    Note.IsArchive = true;
                    this.context.SaveChanges();
                    return "Note Archived";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string TrashOrRestoreNote(long noteid)
        {
            try
            {
                var Note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == noteid);
                if (Note.IsTrash == true)
                {
                    Note.IsTrash = false;
                    this.context.SaveChanges();
                    return "Note is Restored.";
                }
                else
                {
                    Note.IsTrash = true;
                    this.context.SaveChanges();
                    return "Note is Trash";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public string ColorNote(long noteId, string color)
        {
            try
            {
                var Note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == noteId);
                if (Note.Color != color)
                {
                    Note.Color = color;
                    this.context.SaveChanges();
                    return "Note color is changed.";
                }
                else
                {
                    Note.IsTrash = true;
                    this.context.SaveChanges();
                    return "choose different color";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Note> GetAllNotesOfUser(long UserId)
        {
            return context.NoteTable.Where(Y => Y.Id == UserId).ToList();
        }
    }
    
}
    

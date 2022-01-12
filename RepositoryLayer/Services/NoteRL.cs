using CommonLayer.Model;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Enitity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        readonly UserContext context;
        IConfiguration _config;
        public NoteRL(UserContext context, IConfiguration config)
        {
            this.context = context;
            _config = config;
        }
        /// <summary>
        /// to create new note
        /// </summary>
        /// <param name="notes"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
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
                return false;
            }
            catch (Exception )
            {
                throw ;
            }
        }
        /// <summary>
        /// to get all notes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Note> GetAllNotes()
        {
            return context.NoteTable.ToList();
        }
        /// <summary>
        /// update the note 
        /// </summary>
        /// <param name="notes"></param>
        /// <param name="UserId"></param>
        /// <param name="Noteid"></param>
        /// <returns></returns>
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
                return null;
            }
            catch(Exception )
            {
                throw ;
            }
        }
        /// <summary>
        /// Delete the note 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteNotes(long id)
        {
            try
            {
                var ValidNote = this.context.NoteTable.Where(Y => Y.NoteId == id).FirstOrDefault();
                this.context.NoteTable.Remove(ValidNote);
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// pin OR unpin the note.
        /// </summary>
        /// <param name="noteid"></param>
        /// <returns></returns>
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
            catch (Exception)
            {
                throw ;
            }
        }
        /// <summary>
        /// Archive OR UnArchive the Note.
        /// </summary>
        /// <param name="noteid"></param>
        /// <returns></returns>
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
            catch (Exception)
            {
                throw ;
            }
        }
        /// <summary>
        /// Trash Or Restore Note.
        /// </summary>
        /// <param name="noteid"></param>
        /// <returns></returns>
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
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// Color note Note
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="color"></param>
        /// <returns></returns>
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
            catch (Exception)
            {
                throw ;
            }
        }
        /// <summary>
        /// Get all note of user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IEnumerable<Note> GetAllNotesOfUser(long UserId)
        {
            return context.NoteTable.Where(Y => Y.Id == UserId).ToList();
        }
        /// <summary>
        /// upload the inage.
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public bool UploadImage(long noteId, IFormFile image)
        {
            try
            {
                var notes = this.context.NoteTable.FirstOrDefault(x => x.NoteId == noteId);
                if (notes != null)
                {
                    Account account = new Account 
                    (
                    _config["CloudinaryAccount:CloudName"],
                    _config["CloudinaryAccount:ApiKey"],
                    _config["CloudinaryAccount:ApiSecret"]
                    );
                    var path = image.OpenReadStream();
                    Cloudinary cloudinary = new Cloudinary(account);
                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, path)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    context.NoteTable.Attach(notes);
                    notes.Image = uploadResult.Url.ToString();
                    context.SaveChanges();
                    return true;
                }
                else
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        } 
    }   
}


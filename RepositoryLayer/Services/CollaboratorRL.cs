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
    public class CollaboratorRL : ICollaboratorRL
    {
        readonly UserContext context;
        public CollaboratorRL(UserContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collaborate"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        // note creator collaborate other problem if 2nd user collaborate 3rd user(problem)
        public bool NoteCollaborate(NoteCollaborate collaborate, long UserId)
        {
            try
            {
                var note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == collaborate.NoteId && x.Id == UserId);
                var Usertake = this.context.UserTable.FirstOrDefault(x => x.EmailId == collaborate.EmailId);
                collaborator Newcollaborate = new collaborator();
                if (note != null && Usertake.EmailId != null)
                {   Newcollaborate.NoteId = collaborate.NoteId;
                    Newcollaborate.EmailId = collaborate.EmailId;
                    Newcollaborate.UserId = UserId;
                    this.context.collabTable.Add(Newcollaborate);
                }
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
        /// 
        /// </summary>
        /// <param name="collaborate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool RemoveCollaborate(NoteCollaborate collaborate)
        {
            try
            {
              collaborator UserRemoved = this.context.collabTable.FirstOrDefault(x => x.EmailId == collaborate.EmailId && x.NoteId == collaborate.NoteId);
              if(UserRemoved !=null)
              this.context.collabTable.Remove(UserRemoved);
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
    }
}

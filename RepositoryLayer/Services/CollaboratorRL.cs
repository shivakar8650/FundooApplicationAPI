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


        // note creator collaborate other problem if 2nd user collaborate 3rd user(problem)
        public bool NoteCollaborate(NoteCollaborate collaborate, long UserId)
        {
            try
            {
                var note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == collaborate.NoteId && x.Id == UserId);
                var Usertake = this.context.UserTable.FirstOrDefault(x => x.EmailId == collaborate.EmailId);
                collaborator Newcollaborate = new collaborator();
                if (note != null && Usertake.EmailId != null)
                {
                    Newcollaborate.NoteId = collaborate.NoteId;
                    Newcollaborate.EmailId = collaborate.EmailId;
                    this.context.collaborationTable.Add(Newcollaborate);
                }
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
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public string RemoveCollaborate(NoteCollaborate collaborate, long userId)
        {
            try
            {
                var note = this.context.NoteTable.FirstOrDefault(x => x.NoteId == collaborate.NoteId && x.Id == userId);
                var Usertake = this.context.UserTable.FirstOrDefault(x => x.EmailId == collaborate.EmailId);
                if (note != null && Usertake.EmailId != null)
                {
                    collaborator UserRemoved = this.context.collaborationTable.FirstOrDefault(x => x.EmailId == collaborate.EmailId && x.NoteId == collaborate.NoteId);
                    if(UserRemoved !=null)
                    this.context.collaborationTable.Remove(UserRemoved);

                    int result = this.context.SaveChanges();
                    if (result > 0)
                    {
                        return "removed Collaboration";
                    }
                    else
                    {
                        return "this note is not collaborate with user. ";
                    }
                }
                else
                {
                    return "This is user not exist or you have no permission to Collaborate" ;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
    }
}

using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Model;
using RepositoryLayer.Services;

namespace BusinessLayer.Services
{
    public class CollaboratorBL : ICollaboratorBL
    {


        ICollaboratorRL CollaboratorRL;
        public CollaboratorBL(ICollaboratorRL CollaboratorRL)
        {
            this.CollaboratorRL = CollaboratorRL;
        }

        public bool NoteCollaborate(NoteCollaborate collaborate, long UserId)
        {
            try
            {
                return this.CollaboratorRL.NoteCollaborate(collaborate, UserId);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }

        }

        public string RemoveCollaborate(NoteCollaborate collaborate, long userId)
        {
            try
            {
                return this.CollaboratorRL.RemoveCollaborate(collaborate, userId);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }

        }
    }
}

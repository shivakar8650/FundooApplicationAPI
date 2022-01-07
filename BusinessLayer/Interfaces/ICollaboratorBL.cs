using CommonLayer.Model;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public  interface ICollaboratorBL
    {

        bool NoteCollaborate(NoteCollaborate collaborate,long UserId);
        string RemoveCollaborate(NoteCollaborate collaborate, long userId);
    }
}

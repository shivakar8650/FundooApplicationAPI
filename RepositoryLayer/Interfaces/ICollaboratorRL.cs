using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICollaboratorRL
    {
        bool NoteCollaborate(NoteCollaborate collaborate, long UserId);
        string RemoveCollaborate(NoteCollaborate collaborate, long userId);
    }
}

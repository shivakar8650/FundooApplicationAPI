using CommonLayer.Model;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        labelResponse CreateLabel(LabelClass labelInput, long UserId);
        labelResponse AddNoteToExistingLabel(string labelName, long noteId, long userId);
        bool DeleteLabel(string labelName);
        bool RemoveNoteFromLabel(string labelName, long noteId);
    }
}

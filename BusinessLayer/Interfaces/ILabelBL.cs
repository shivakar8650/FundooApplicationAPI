using CommonLayer.Model;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        labelResponse CreateLabel(LabelClass labelInput, long UserId);
        labelResponse AddNoteToExistingLabel(string labelName, long noteId, long userId);
        bool DeleteLabel(string labelName);
        bool RemoveNoteFromLabel(string labelName, long noteId);
    }
}

using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        bool CreateLabel(LabelClass labelInput, long UserId);
        bool AddNoteToExistingLabel(string labelName, long noteId, long userId);
        bool DeleteLabel(string labelName);
        bool RemoveNoteFromLabel(string labelName, long noteId);
    }
}

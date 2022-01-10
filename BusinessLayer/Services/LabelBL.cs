using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Enitity;
using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {

        ILabelRL LabelRL;
        public LabelBL(ILabelRL LabelRL)
        {
            this.LabelRL = LabelRL;
        }

        public bool AddNoteToExistingLabel(string labelName, long noteId, long userId)
        {
            try
            {
                return this.LabelRL.AddNoteToExistingLabel(labelName,noteId, userId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool CreateLabel(LabelClass labelInput, long UserId)
        {
            try
            {
                return this.LabelRL.CreateLabel(labelInput, UserId);
            }
            catch (Exception e)
            {
                throw ;
            }
        }

        public bool DeleteLabel(string labelName)
        {

            try
            {
                return this.LabelRL.DeleteLabel(labelName);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool RemoveNoteFromLabel(string labelName, long noteId)
        { 
            try
            {
                return this.LabelRL.RemoveNoteFromLabel(labelName,noteId);
            }
            catch (Exception e)
            {
                throw;
            } 
        }
    }
}

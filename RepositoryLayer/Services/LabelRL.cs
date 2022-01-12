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
    public class LabelRL : ILabelRL
    {
        readonly Context.UserContext context;
        public LabelRL(UserContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// create Label method.
        /// </summary>
        /// <param name="labelInput"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CreateLabel(LabelClass labelInput, long userId)
        {
            try
            {
                Label NewLabel = new Label();
                NewLabel.labelName = labelInput.labelName;
                NewLabel.UserId = userId;
                this.context.LabelsTable.Add(NewLabel);               
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw ;
            }
        }

        /// <summary>
        /// Add note to existig label
        /// </summary>
        /// <param name="labelName"></param>
        /// <param name="noteId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AddNoteToExistingLabel(string labelName, long noteId, long userId)
        {
            try
            {
                Label label = this.context.LabelsTable.FirstOrDefault(x => x.labelName == labelName);
                if ( (label.labelName == labelName && label.NoteId == null))
                {
                    context.LabelsTable.Attach(label);
                    label.NoteId = noteId;
                    context.SaveChanges();
                    return true;
                }
                else if (label.labelName == labelName && label.NoteId != null)
                {
                    Label NewLabel = new Label();
                    NewLabel.labelName = labelName;
                    NewLabel.UserId = userId;
                    NewLabel.NoteId = noteId;
                    this.context.LabelsTable.Add(NewLabel);
                    int result = this.context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// delete label 
        /// </summary>
        /// <param name="labelName"></param>
        /// <returns></returns>
        public bool DeleteLabel(string labelName)
        {
            try
            {
                List<Label> labels = this.context.LabelsTable.Where(x => x.labelName == labelName).ToList();
                if (labels != null)
                {
                    foreach (var item in labels)
                    {
                        this.context.LabelsTable.Remove(item);
                        this.context.SaveChanges();
                    }
                    return true;
                }
                else
                    return false;
            }
            catch(Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// remove note from label
        /// </summary>
        /// <param name="labelName"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public bool RemoveNoteFromLabel(string labelName, long noteId)
        {
            try
            {
                Label label = this.context.LabelsTable.FirstOrDefault(x => x.labelName == labelName && x.NoteId == noteId);
                if (label != null)
                {
                        this.context.LabelsTable.Remove(label);
                        this.context.SaveChanges();
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

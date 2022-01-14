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
        public labelResponse CreateLabel(LabelClass labelInput, long userId)
        {
            try
            {
                Label NewLabel = new Label();
                NewLabel.labelName = labelInput.labelName;
                NewLabel.UserId = userId;
                this.context.LabelsTable.Add(NewLabel);               
                int result = this.context.SaveChanges();
                Label label = this.context.LabelsTable.FirstOrDefault(Y => Y.labelName == labelInput.labelName);
                if (result > 0)
                {
                    labelResponse Response = new labelResponse();
                     return ReturnResponse(Response, label);
                   
                }
                return null;
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
        public labelResponse AddNoteToExistingLabel(string labelName, long noteId, long userId)
        {
            try
            {
                Label label = this.context.LabelsTable.FirstOrDefault(x => x.labelName == labelName);
                labelResponse Response = new labelResponse();
                if ( (label.labelName == labelName && label.NoteId == null))
                {
                    context.LabelsTable.Attach(label);
                    label.NoteId = noteId;
                    context.SaveChanges();
                    return ReturnResponse(Response, label);
                }
                else if (label.labelName == labelName && label.NoteId != null)
                {
                    Label NewLabel = new Label();
                    NewLabel.labelName = labelName;
                    NewLabel.UserId = userId;
                    NewLabel.NoteId = noteId;
                    this.context.LabelsTable.Add(NewLabel);
                    int result = this.context.SaveChanges();
                    Label label1 = this.context.LabelsTable.FirstOrDefault(Y => Y.labelName == labelName && Y.NoteId == noteId);
                    return ReturnResponse(Response, label1);
                }
                else
                {
                    return null;
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
        /// <summary>
        /// method to return response
        /// </summary>
        /// <param name="Response"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public labelResponse ReturnResponse(labelResponse Response, Label label)
        {
            Response.labelName = label.labelName;
            Response.labelID = label.labelID;
            Response.NoteId = label.NoteId;
            Response.UserId = label.UserId;
            return Response;
        }
    }
}

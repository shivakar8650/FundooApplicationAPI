using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Enitity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;

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

        public bool CreateLabel(LabelClass labelInput, long userId)
        {
            try
            {
                Label NewLabel = new Label();
                NewLabel.labelName = labelInput.labelName;
                NewLabel.UserId = userId;
                //Adding the data to database
                this.context.LabelsTable.Add(NewLabel);
                //Save the changes in database
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
            catch (Exception e)
            {
                throw ;
            }
        }
    }
}

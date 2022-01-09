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
    }
}

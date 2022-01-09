using CommonLayer.Model;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        public bool CreateLabel(LabelClass labelInput, long userId);
    }
}

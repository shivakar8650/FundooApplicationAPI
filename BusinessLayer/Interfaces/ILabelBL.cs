using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
       public bool CreateLabel(LabelClass labelInput, long UserId);
    }
}

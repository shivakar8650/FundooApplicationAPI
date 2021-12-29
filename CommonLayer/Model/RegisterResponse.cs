using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
   public  class RegisterResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public DateTime? Createdat { get; set; }
     
    }
}

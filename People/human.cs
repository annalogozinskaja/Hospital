using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Text;


namespace People
{
    class human 
    {
        public virtual int id { get; set; }
        public virtual string lastname { get; set; }
        public virtual string firstname { get; set; }
        public virtual string DOB { get; set; }
        public virtual int SSN { get; set; }
        public virtual int activeStatus { get; set; }

        public virtual gender gender { get; set; }
    

    public override string ToString()
        {
            return id.ToString() + "." + lastname + " " +firstname+", "+DOB+", "+SSN.ToString()+", "+gender.gendername+",status= "+activeStatus;
        }
    }
}

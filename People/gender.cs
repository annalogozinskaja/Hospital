using System;
using System.Collections.Generic;
using System.Text;

namespace People
{
    class gender
    {
        public virtual int GenderId { get; set; }
        public virtual string gendername { get; set; }

        private IList<human> _human;
        public virtual IList<human> Human
        {
            get
            {
                return _human ?? (_human = new List<human>());
            }
            set { _human=value; }
        }

        //public virtual ISet<human> humans { get; set; }
        //public virtual void AddOrder(human human) 
        //{
        //    humans.Add(human);
        //    human.gender = this; 
        //}
        public override string ToString()
        {
            return GenderId.ToString() + ". " + gendername+", "+_human.ToString();
        }
    }
}

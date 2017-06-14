using System;
using System.Collections;
using System.Collections.Generic;

namespace ReTagger
{
    public class Rule
    {
        public List<Tag> Conditions {get; private set;}
        public List<Modification> Actions { get; private set; }

        public Rule()
        {
            Conditions = new List<Tag>();
            Actions = new List<Modification>();
        }
    
    }
}
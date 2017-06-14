using System;
using System.Collections.Generic;

namespace ReTagger.StateMachine
{
    public class State<T>
    {
        public object StateID { get; private set; }
        public Func<T,bool> Guard { get; private set; }
        public Action<T> Action { get; private set; }
        public IEnumerable<object> NextValidStates { get; private set; }

        public State(object ID, Func<T,bool> guard, Action<T> action, IEnumerable<object> nextStates)
        {
            StateID = ID;
            Guard = guard;
            Action = action;
            NextValidStates = nextStates;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace ReTagger.StateMachine
{
    public class Machine<T>
    {
        private readonly Dictionary<object,State<T>> _states;
        private object _currentStateID;
        private List<object> _validEndStateIDs;

        public IEnumerable<State<T>> States => _states.Values;
        

        public Machine()
        {
            _states = new Dictionary<object, State<T>>();
            _validEndStateIDs = new List<object>();
        }

        public Machine<T> AddState(State<T> state, bool validFinal = false)
        {
            if (_states.ContainsKey(state.StateID))
            {
                throw new Exception();
            }
            _states.Add(state.StateID, state);
            if(_states.Count == 1)
            {
                SetState(state);
            }
            if (validFinal)
            {
                _validEndStateIDs.Add(state.StateID);
            }
            return this;
        }
                

        public void SetState(State<T> state)
        {
            SetState(state.StateID);
        }
        
        public void SetState(object obj)
        {
            _currentStateID = obj;
        }     
        
        public bool IsInValidFinalState()
        {
            return _validEndStateIDs.Contains(_currentStateID);
        }
        
        private State<T> GetState(object id)
        {
            return _states[id];
        }

        public void Next(T input)
        {            
            var currentState = GetState(_currentStateID);
            var nextStateID = currentState.NextValidStates?.FirstOrDefault(st => GetState(st).Guard(input));
            if( nextStateID != null)
            {
                GetState(nextStateID).Action?.Invoke(input);
                SetState(nextStateID);
            }
        }
    }
}

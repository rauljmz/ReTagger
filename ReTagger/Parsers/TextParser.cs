using ReTagger.StateMachine;
using System.Collections.Generic;
using System.Linq;

namespace ReTagger.Parsers
{
    public class TextParser
    {
        private enum StateIDs { Initial, MatchQueryTag, ExtraConditions, ReadFirstAction, ReadExtraActions, Error1, Error2, Error3 };

        private readonly TagParser _tagParser;
        private readonly ModificationParser _modParser;

        private readonly Machine<string> _machine;

        private List<Rule> _result;
        private Rule _currentRule;

        public TextParser():this(new TagParser(), new ModificationParser(new TagParser()),new Machine<string>())
        {

        }
        public TextParser(TagParser tagParser, ModificationParser modParser, Machine<string> machine)
        {
            _modParser = modParser;
            _tagParser = tagParser;
            _machine = machine;

            ConfigureMachine();
        }

        private void ConfigureMachine()
        {
            _machine.AddState(new State<string>(StateIDs.Initial, null, null, new object[] { StateIDs.MatchQueryTag, StateIDs.Error1 }), true)
                .AddState(new State<string>(StateIDs.MatchQueryTag, input => '@' == input[0], SetInitialConditionTag, new object[] { StateIDs.ExtraConditions, StateIDs.ReadFirstAction }))
                .AddState(new State<string>(StateIDs.ExtraConditions, input => '&' == input[0], SetConditionTag, new object[] { StateIDs.ExtraConditions, StateIDs.ReadFirstAction, StateIDs.Error2 }))
                .AddState(new State<string>(StateIDs.ReadFirstAction, input => new char[] { '=', '+', '-' }.Contains(input[0]), SetActionTag, new object[] { StateIDs.ReadExtraActions, StateIDs.MatchQueryTag, StateIDs.Error3 }), true)
                .AddState(new State<string>(StateIDs.ReadExtraActions, input => '+' == input[0], SetActionTag, new object[] { StateIDs.ReadExtraActions, StateIDs.MatchQueryTag, StateIDs.Error3 }), true)
                .AddState(new State<string>(StateIDs.Error1, input => new char[] { '=', '+', '-' }.Contains(input[0]), input => throw new ParsingException(input), null))
                .AddState(new State<string>(StateIDs.Error2, input => new char[] { '@' }.Contains(input[0]), input => throw new ParsingException(input), null))
                .AddState(new State<string>(StateIDs.Error3, input => new char[] { '=', '-' }.Contains(input[0]), input => throw new ParsingException(input), null));
        }

        public IEnumerable<Rule> Parse(IEnumerable<string> lines)
        {
            _result = new List<Rule>();

            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    _machine.Next(line);
                }
            }

            if (_machine.IsInValidFinalState())
            {
                return _result;
            }
            throw new ParsingException("at the end of the file");
        }

        private void SetInitialConditionTag(string input)
        {            
            _currentRule = new Rule();
            _result.Add(_currentRule);
            SetConditionTag(input);
        }

        private void SetConditionTag(string input)
        {
            _currentRule.Conditions.Add(_tagParser.Parse(input.Substring(1)));
        }

        private void SetActionTag(string input)
        {
            _currentRule.Actions.Add(_modParser.Parse(input));
        }
        
        
    }
}
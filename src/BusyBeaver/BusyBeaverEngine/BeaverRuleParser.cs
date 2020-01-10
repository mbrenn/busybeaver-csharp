using System;

namespace BusyBeaver.BusyBeaverEngine
{
    public class BeaverRuleParser
    {
        public static State[] ParseRule(string rule, BusyBeaver beaver)
        {
            var ruleParts = rule.Split(" ");

            var stateCount = ruleParts.Length / 2;
            if (ruleParts.Length % 2 != 0)
            {
                throw new InvalidOperationException("We need an even number of rules");
            }

            var states = new State[stateCount];
            for (var n = 0; n < stateCount; n++)
            {
                states[n] = new State(n.ToString());
            }

            var home = 'H' - 'A';
            for (var n = 0; n < stateCount; n++)
            {
                Rule Parse(string ruleToken)
                {
                    var setValue = ruleToken[0];
                    var direction = ruleToken[1];
                    var newState = ruleToken[2] - 'A';

                    return new Rule(
                        setValue == '1' ? TapeValue.Set : TapeValue.NotSet,
                        direction == 'L' ? Direction.Left : Direction.Right,
                        newState >= 0 && newState != home ? states[newState] : null);
                }

                states[n].NotSetRule = Parse(ruleParts[n * 2]);
                states[n].SetRule = Parse(ruleParts[n * 2 + 1]);
            }

            beaver.CurrentState = states[0];

            return states;
        }
    }
}
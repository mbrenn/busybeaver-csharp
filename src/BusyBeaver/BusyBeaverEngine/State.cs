namespace BusyBeaver.BusyBeaverEngine
{
    public class State
    {
        public State(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        
        /// <summary>
        /// Defines the rule that will be evaluated in case the current position is not set
        /// </summary>
        public Rule? NotSetRule { get; set; }
        
        /// <summary>
        /// Defines the rule that will be evaluated in case the current position is set
        /// </summary>
        public Rule? SetRule { get; set; }

        /// <summary>
        /// Sets the rules of the state
        /// </summary>
        /// <param name="notSetRule">Rule for NotSetRule</param>
        /// <param name="setRule">Rule for SetRule</param>
        public void SetRules(Rule notSetRule, Rule setRule)
        {
            NotSetRule = notSetRule;
            SetRule = setRule;
        }

        public override string ToString()
        {
            return Name + "(" + NotSetRule + "," + SetRule + ")";
        }
    }
}
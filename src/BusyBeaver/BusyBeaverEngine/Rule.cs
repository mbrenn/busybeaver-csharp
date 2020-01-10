namespace BusyBeaver.BusyBeaverEngine
{
    /// <summary>
    /// Defines the rules for
    /// </summary>
    public class Rule
    {
        /// <summary>
        /// Defines the set value
        /// </summary>
        public bool SetValue { get; set; }

        /// <summary>
        /// Defines the direction to move
        /// </summary>
        public Direction Direction { get; set; }

        /// <summary>
        /// Defines the state to which the engine moves
        /// </summary>
        public State? NewState { get; set; }

        public Rule(bool setValue, Direction direction, State? newState)
        {
            SetValue = setValue;
            Direction = direction;
            NewState = newState;
        }

        public override string ToString()
        {
            return (SetValue == TapeValue.Set ? "1" : "0")
                   + (Direction == Direction.Left ? "L" : "R")
                   + (NewState == null ? "H" : NewState.Name);
        }
    }
}
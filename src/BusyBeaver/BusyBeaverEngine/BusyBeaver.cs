using System;

namespace BusyBeaver.BusyBeaverEngine
{
    public class BusyBeaver
    {
        /// <summary>
        /// Gets or sets the current state on which the busy beaver is located.
        /// Null is final state
        /// </summary>
        public State? CurrentState;

        /// <summary>
        /// Gets the current position
        /// </summary>
        public int CurrentPosition;
        
        /// <summary>
        /// Gets the tape on which the beaver runs
        /// </summary>
        public readonly Tape Tape = new Tape();

        /// <summary>
        /// Gets the information that the beaver is finished
        /// </summary>
        public bool IsFinished => CurrentState == null; 

        /// <summary>
        /// Performs one round
        /// </summary>
        public void DoRound()
        {
            if (CurrentState == null)
            {
                // Already finished
                return;
            }
            
            var tapeValue = Tape.UpdateValue(CurrentPosition, CurrentState);

            var rule = (tapeValue == TapeValue.NotSet ? CurrentState.NotSetRule : CurrentState.SetRule)
                   ?? throw new InvalidOperationException("rule == null");
            CurrentState = rule.NewState;

            switch (rule.Direction)
            {
                case Direction.Left:
                    CurrentPosition--;
                    break;
                case Direction.Right:
                    CurrentPosition++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void PrintState()
        {
            Tape.PrintPosition(CurrentPosition);
            Tape.PrintOnConsole();
        }
    }
}
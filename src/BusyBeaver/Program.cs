using System;
using System.Diagnostics;
using BusyBeaver.BusyBeaverEngine;

namespace BusyBeaver
{
    class Program
    {
        static void Main(string[] args)
        {
            var beaver = new BusyBeaverEngine.BusyBeaver();
            //var states = BeaverRuleParser.ParseRule("1L0 1RA", beaver);
            //var states = BeaverRuleParser.ParseRule("1RB 1LB 1LA 1R0", beaver);
            //var states = BeaverRuleParser.ParseRule("1RB 1RH 1LB 0RC 1LC 1LA", beaver);
            //var states = BeaverRuleParser.ParseRule("1RB 1LB 1LA 0LC 1RH 1LD 1RD 0RA", beaver);
            var states = BeaverRuleParser.ParseRule("1RB 1LC 1RC 1RB 1RD 0LE 1LA 1LD 1RH 0LA", beaver);
            //var states = BeaverRuleParser.ParseRule("1RB 1LC 0LA 0LD 1LA 1RH 1LB 1RE 0RD 0RB", beaver);
            //var states = BeaverRuleParser.ParseRule("1RB 0LC 1RC 1RD 1LA 0RB 0RE 1RH 1LC 1RA", beaver);
            

            foreach (var state in states)
            {
                Console.WriteLine(state.ToString());
            }
            
            var count = 0;
            var stopWatch = Stopwatch.StartNew();
            
            while (!beaver.IsFinished){
            
                //beaver.PrintState();
                beaver.DoRound();
                count++;

                if (count == Int32.MaxValue)
                {
                    break;
                }
            }

            Console.WriteLine(beaver.Tape.CountOnes() + " ones established");
            Console.WriteLine(count + " rounds");
            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed + " time elapsed");
        }
    }
}
using System;

namespace Elevator
{
    class Program
    {
        static void Main(string[] args)
        {

            Elevator elevator_f = new Elevator();
            Agent agent = new Agent("Secret",agents_type.Secret,elevator_f);
            Agent agent1 = new Agent("Confidential",agents_type.Confidential,elevator_f);
            Agent agent2 = new Agent("Top-secret",agents_type.Top_secret,elevator_f);
            agent.AllWorksWorker();
            agent1.AllWorksWorker();
            agent2.AllWorksWorker();
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Elevator
{
    public enum Floors { G, S, T1, T2 };
    public enum DoorState { Open, Close };


    public sealed class Elevator
    {
        public List<Floors> ElFloors = new List<Floors>() { Floors.G, Floors.S, Floors.T1, Floors.T2 };
        private Agent agent_in_elevator = null;
        public Floors current_floor, destination_floor;
        public DoorState state_d = DoorState.Close;

        internal void Enter(Agent a) => agent_in_elevator = a;
        private void Getoff() => agent_in_elevator = null;
        internal List<Floors> GetButtons()
        {
            var Temp = new List<Floors>();
            foreach (Floors f in ElFloors)
            {
                if (f == current_floor) continue;
                else Temp.Add(f);

            }
            return Temp;
        }
        private bool check_agent_type()
        {
            bool temp = agent_in_elevator.Agent_floor.Contains(current_floor);
            Console.WriteLine($"Current floor: {current_floor}, Level: {agent_in_elevator.Type_Agent}, Can exit: {temp}");
            return temp;
        }
        internal void Move(Floors dest)
        {
            state_d = DoorState.Close;
            if (dest.Equals(current_floor))
            {
                Console.WriteLine("Elevator is already here.");
                Thread.Sleep(1000);
                state_d = DoorState.Open;
            }
            else
            {
                if (current_floor > dest)
                {
                    state_d = DoorState.Close;
                    Console.WriteLine("Elevator is going down.");
                    Thread.Sleep(1000);

                }
                else
                {
                    state_d = DoorState.Close;
                    Console.WriteLine("Elevator is going up.");
                    Thread.Sleep(1000);

                }
                for (int i = 0; i < Math.Abs(current_floor - dest); i++)
                {
                    Console.WriteLine("*");
                    Thread.Sleep(1000);
                }
                current_floor = dest;
                Console.WriteLine("Elevator came at " + dest);
                Thread.Sleep(1000);
                state_d = DoorState.Open;
            }

        }
        internal void AgentInElevator(Floors floor)
        {
            state_d = DoorState.Close;
            current_floor = floor;
            if (check_agent_type())
            {
                current_floor = floor;
                state_d = DoorState.Open;
                Getoff();
            }
            else
            {
                current_floor = floor;
                Console.WriteLine("Please, choose another floor!");
                Thread.Sleep(1000);

            }
        }
    }
}
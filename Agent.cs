using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Elevator
{
    public enum agents_type { Confidential, Secret, Top_secret };

    public class Agent
    {
        public string Name_Agent;
        public agents_type Type_Agent;
        private Elevator elevator_obj;
        public readonly List<Floors> Agent_floor;
        private int Energy_Agent = 40;
        private Floors current_floor;
        private static Mutex mutex = new Mutex();

        public Agent(string Name, agents_type Type, Elevator elevator)
        {
            Name_Agent = Name;
            Type_Agent = Type;
            elevator_obj = elevator;

            if (Type_Agent == agents_type.Confidential)
            {
                Agent_floor = new List<Floors>() { Floors.G };
            }

            if (Type_Agent == agents_type.Secret)
            {
                Agent_floor = new List<Floors>() { Floors.G, Floors.S };
            }

            if (Type_Agent == agents_type.Top_secret)
            {
                Agent_floor = new List<Floors>() { Floors.G, Floors.S, Floors.T1, Floors.T2 };
            }
        }


        private void LeaveEl()
        {
            Console.WriteLine(Name_Agent + " left the elevator!");
            Thread.Sleep(1000);
            current_floor = elevator_obj.current_floor;
            mutex.ReleaseMutex();
        }
        private void EnterEl()
        {
            Console.WriteLine(Name_Agent + " is waiting for the elevator.");
            Thread.Sleep(1000);
            mutex.WaitOne();
            Console.WriteLine(Name_Agent + " called the elevator.");
            Thread.Sleep(1000);
            elevator_obj.Move(current_floor);
            elevator_obj.Enter(this);
            Console.WriteLine(Name_Agent + " is in the elevator.");
            Thread.Sleep(1000);


            do
            {
                var ChosenButton = ChooseRandomButton();
                Console.WriteLine(Name_Agent + " has chosen " + ChosenButton + "!");
                elevator_obj.Move(ChosenButton);
                Thread.Sleep(1000);
                elevator_obj.AgentInElevator(ChosenButton);
                Console.WriteLine("Door state: " + elevator_obj.state_d);

            } while (elevator_obj.state_d.Equals(DoorState.Close));




            Thread.Sleep(1000);


        }

        private void AllWorks()
        {
            Console.WriteLine(Name_Agent + " starts the working day!");
            Thread.Sleep(1000);
            while (Energy_Agent > 0)
            {
                EnterEl();
                LeaveEl();
                Console.WriteLine(Name_Agent + " is working!");
                Thread.Sleep(3000);
                Random random = new Random();
                Energy_Agent -= random.Next(5, 30);
                if (Energy_Agent < 0) Energy_Agent = 0;
                Console.Write($"Energy left ({Name_Agent}): {Energy_Agent}% \n");
            }
            Console.WriteLine("\t\t\t" + Name_Agent + " ends the working day!");


        }

        public void AllWorksWorker()
        {
            var thread = new Thread(AllWorks);
            thread.Start();
        }


        internal Floors ChooseRandomButton()
        {
            var button = elevator_obj.GetButtons();
            var rand = new Random();
            return button[rand.Next(0, 3)];
        }

    }
}

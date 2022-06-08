using System;

namespace RobotCleanerPrototype
{
    class Program
    {
        static void Main(string[] args)
        {           
            RobotCommander robotCommander = new RobotCommander(Console.In, Console.Out);
            robotCommander.RunRobot();
        }
    }
}

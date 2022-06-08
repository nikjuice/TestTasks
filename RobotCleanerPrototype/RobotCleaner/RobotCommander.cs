using System;
using System.IO;

namespace RobotCleanerPrototype
{
    public class RobotCommander
    {
        private readonly TextReader input;
        private readonly TextWriter output;
       
        public RobotCommander(TextReader input, TextWriter output)
        {
            this.input = input ?? throw new ArgumentNullException(nameof(input));
            this.output = output ?? throw new ArgumentNullException(nameof(output));
        }
        public void RunRobot()
        {
            int numberOfCommands = int.Parse(input.ReadLine());
            string[] startingPoints = input.ReadLine().Split(' ');

            RobotCleaner robotCleaner = new RobotCleaner((int.Parse(startingPoints[0]), int.Parse(startingPoints[1])));

            for (int i = 0; i < numberOfCommands; i++)
            {
                string[] command = input.ReadLine().Split(' ');
                robotCleaner.ExecuteCommand(Convert.ToChar(command[0]), int.Parse(command[1]));
            }

            output.WriteLine($"=> Cleaned: {robotCleaner.CleanedPlacesCount}");
        }
    }
}

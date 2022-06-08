
using System;
using System.Collections.Generic;

namespace RobotCleanerPrototype
{
    public class RobotCleaner
    {
        public int CleanedPlacesCount { get => visitedPoints.Count; }

        private readonly HashSet<(int x, int y)> visitedPoints;
        private (int x, int y) currentPosition;

        public RobotCleaner((int x, int y) startPoint)
        {
            visitedPoints = new HashSet<(int x, int y)>();

            currentPosition = startPoint;
            visitedPoints.Add(startPoint);
        }

        
        public void ExecuteCommand(char direction, int stepsCount)
        {
            switch(direction)
            {
                case 'E':
                   stepsCount.Times(() =>
                    {
                        currentPosition.x++;
                        visitedPoints.Add(currentPosition);
                    });
                break;
                case 'W':
                    stepsCount.Times(() =>
                    {
                        currentPosition.x--;
                        visitedPoints.Add(currentPosition);
                    });
                break;
                case 'S':
                    stepsCount.Times(() =>
                    {
                        currentPosition.y--;
                        visitedPoints.Add(currentPosition);
                    });
                break;
                case 'N':
                    stepsCount.Times(() =>
                    {
                        currentPosition.y++;
                        visitedPoints.Add(currentPosition);
                    });
                break;
            }
        }
    }

    public static class LoopHelper
    {
        public static void Times(this int count, Action actionToRepeat)
        {
            for (int i = 0; i < count; i++)
            {
                actionToRepeat();
            }
        }
    }
}

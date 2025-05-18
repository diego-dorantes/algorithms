using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    //DepthFirstSearch to solve a maze traversal
    internal class DepthFirstSearch
    {
        //Get the input, numbers of rows and columns
        int[,] maze;
        int rows;
        int columns;
        //Get the cell that represents the ending cell
        int[] endCell; 
        //Make a HashTable that represents all the visited cells
        HashSet<string> visited = new HashSet<string>();
        //Make a Stack that stores the path from start to end, if no path is found the Stack will be empty
        Stack<string> traversedPath = new Stack<string>();

        //The constructor gets the input maze the number of rows and columns
        public DepthFirstSearch(int[,] input)
        {
            maze = input;
            rows = input.GetLength(0) - 1;
            columns = input.GetLength(1) - 1;
        }

        public Stack<string> SolvePath(int[] start, int[] end)
        {
            //Assign the ending cell and clean the visited set and path stack
            endCell = end;
            visited.Clear();
            traversedPath.Clear();
            //Start the traversal
            Walk(start[0], start[1]);
            //Return the traversed path
            return traversedPath;
        }

        //DFS tracks every path until solution is found
        //The algorithm works with a stack and follows this path
        //Start with a node (push it into the stack)
        //Mark the node as visited
        //Process the node (do the activity that I have to do in the node)
        //Obtain all the nodes adjacent to the node that haven't been visited and push them into the stack
        //Repeat until the stack is empty
        private bool Walk(int coordX, int coordY)
        {
            //This function is recursive, every time it's called it gets pushed into the call stack
            bool availablePath = false;

            //My first action is to mark the current node as visited
            visited.Add(coordX.ToString() + "_" + coordY.ToString());

            //Since the function is recursive I declare the base function (check if node is the end coordinate)
            if (endCell[0] == coordX && endCell[1] == coordY)
                return true;

            //I process the node, in this case since we are traversing a maze the processing is checking and walking in a valid direction
            //The IsDirectionValid function checks if the direction is inside the maze, if it's available and also if the node hasn't been visited
            //I only need to find one path that is valid so I only add adjacent nodes if the previous direction does not yield a valid path
            //If a direction is valid we call the function (push it into the call stack) and I push the direction into the stack that prints the path

            //Move Up
            if(!availablePath && IsDirectionValid(coordX - 1, coordY))
            {
                traversedPath.Push((coordX - 1).ToString() + "_" + coordY.ToString());
                availablePath = Walk(coordX - 1, coordY);
            }
            //Move Right
            if(!availablePath && IsDirectionValid(coordX, coordY + 1))
            {
                traversedPath.Push(coordX.ToString() + "_" + (coordY + 1).ToString());
                availablePath = Walk(coordX, coordY + 1);
            }
            //Move Down
            if (!availablePath && IsDirectionValid(coordX + 1, coordY))
            {
                traversedPath.Push((coordX + 1).ToString() + "_" + coordY.ToString());
                availablePath = Walk(coordX + 1, coordY);
            }
            //Move Left
            if (!availablePath && IsDirectionValid(coordX, coordY - 1))
            {
                traversedPath.Push(coordX.ToString() + "_" + (coordY - 1).ToString());
                availablePath = Walk(coordX, coordY - 1);
            }

            //If the path is not valid I pop the direction from the stack
            if(!availablePath) { traversedPath.Pop(); }

            //After all the paths have been checked we return the result
            return availablePath;
        }

        private bool IsDirectionValid(int coordX, int coordY)
        {
            //Check if coordinate is inside the maze
            if (coordX < 0 || coordY < 0 || coordX > rows || coordY > columns)
                return false;
            //Check if node hasn't been visited
            if (visited.Contains(coordX.ToString() + "_" + coordY.ToString()))
                return false;
            //Check if tile of maze is available (0 is empty tile, 1 is occupied tile)
            if (maze[coordX, coordY] == 1)
                return false;

            return true;
        }
    }
}

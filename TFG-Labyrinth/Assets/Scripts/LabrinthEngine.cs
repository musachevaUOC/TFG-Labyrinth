using UnityEngine;


public class LabrinthEngine
{

    public int lab_width = 10;
    public int lab_height = 10;


    //Span at which the random generator can generate numbers. Used for determining the random weight assigned to 
    //each node of the labyrinth. 
    public int rand_low = 0;
    public int rand_high = 50;

    public node[,] lab_matrix { get; }

    public struct node
    {
        public node(int x, int y, float weight)
        {
            X = x;
            Y = y;

            Wheight = weight;
        }

        public float Wheight { get; }

        public int X { get; }
        public int Y { get; }
    
        

    }

    public LabrinthEngine()
    {
        lab_matrix = new node[lab_width, lab_height];


        for(int i=0; i < lab_width; i++)
        {
            for(int j=0; j< lab_height; j++)
            {
                lab_matrix[i, j] = new node(i, j, Random.Range(rand_low, rand_high));
            }
        }


    }
}

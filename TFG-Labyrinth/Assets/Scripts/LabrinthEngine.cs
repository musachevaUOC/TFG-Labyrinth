using UnityEngine;
using System;


public class LabrinthEngine
{
    //dimensions of the node matrix representing possible paths
    private int lab_width;
    private int lab_height;


    //Span at which the random generator can generate numbers. Used for determining the random weight assigned to 
    //each node of the labyrinth. 
    private int rand_low = 0;
    private int rand_high = 50;

    private node[,] lab_matrix;
    private vertex[] vert_list;

    public class node
    {
        public node(int x, int y, int val)
        {
            X = x;
            Y = y;

            ValueOfSet = val;
        }

        public int X;
        public int Y;

        public int ValueOfSet; //value used to identify the set and apply kruskal algorithm
    }

    public class vertex{
        public vertex(ref node a, ref node b, int weight, bool vertical)
        {
            A = a;
            B = b;
            Weight = weight;
            conected = false;
            Vertical = vertical;
        }
        public node A;
        public node B;

        public int Weight { get; }
        public bool conected;
        public bool Vertical { get; }
    }

    // initialize node matrix and all vertex pairs
    private void initLab()
    {
        lab_matrix = new node[lab_width, lab_height];
        vert_list = new vertex[(2 * lab_width * lab_height) - lab_width - lab_height];

        int ValueOfSet = 0;

        for (int i = 0; i < lab_height; i++)
        {
            for (int j = 0; j < lab_width; j++, ValueOfSet++)
            {
                lab_matrix[j, i] = new node(j, i, ValueOfSet);
            }
        }

        int index = 0;

        for (int i = 0; i < lab_height * (lab_width - 1); i++, index++)
        {
            vert_list[index] = new vertex(ref lab_matrix[i % (lab_width - 1), i / (lab_width - 1)]
                                      , ref lab_matrix[(i % (lab_width - 1) + 1), i / (lab_width - 1)], UnityEngine.Random.Range(rand_low, rand_high), false);
        }

        for (int i = 0; i < lab_width * (lab_height - 1); i++, index++)
        {
            vert_list[index] = new vertex(ref lab_matrix[i / (lab_height - 1), i % (lab_height - 1)]
                                      , ref lab_matrix[i / (lab_height - 1), i % (lab_height - 1) + 1], UnityEngine.Random.Range(rand_low, rand_high), true);
        }
    }


    private void setChange(int from, int to) //change the set of all nodes of specified by set
    {
  
        for(int i=0; i< lab_matrix.GetLength(0); i++)
        {
            for (int j = 0; j < lab_matrix.GetLength(1); j++)
            {
                if(lab_matrix[i,j].ValueOfSet == from)
                {
                    lab_matrix[i, j].ValueOfSet = to;
                }
            }
        }
    }

    private void prim()
    {
        Array.Sort(vert_list, delegate (vertex vertex1, vertex vertex2)
        {
            return vertex1.Weight.CompareTo(vertex2.Weight);
        });

        

        for(int i=0; i < vert_list.Length; i++)
        {
            if (vert_list[i].A.ValueOfSet != vert_list[i].B.ValueOfSet)
            {
                vert_list[i].conected = true;
                setChange(vert_list[i].A.ValueOfSet, vert_list[i].B.ValueOfSet);
            }
        }
    }

    public LabrinthEngine(int labWith = 15, int labHeight = 15, int randLow = 0, int randHigh = 50 )
    {

        lab_width = labWith;
        lab_height = labHeight;

        rand_low = randLow;
        rand_high = randHigh;

        initLab();
        prim();



    }

    public node[,] getNodes()
    {
        return lab_matrix;
    }

    public vertex[] getVertices() {

        return vert_list;
    }
}

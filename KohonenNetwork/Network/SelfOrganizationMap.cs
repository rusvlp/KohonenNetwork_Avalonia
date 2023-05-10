using System;
using System.Collections.Generic;
using NLog.Fluent;

namespace KohonenNetwork.Network;


public class SelfOrganizationMap
{
    public Neuron[][] Neurons;
    public double[] Inputs;
    public int BSize;
    public double sigma0;
    public SelfOrganizationMap(int width, int height, double[] inputs, int bSize, double rFrom, double rTo)
    {
        this.Inputs = inputs;
        this.BSize = bSize;

        this.sigma0 = Math.Max(width * bSize, height * bSize) / 2;
        
        Neurons = new Neuron[width][];
        for (int i = 0; i < width; i++)
        {
            Neurons[i] = new Neuron[height];
            for (int j = 0; j < height; j++)
            {
                Neurons[i][j] = new Neuron(i, j, Util.Functions.GetConfiguredButton(bSize), Inputs);
                Neurons[i][j].InitializeWeights(rFrom, rTo);
            }
        }
        
    }


    public Neuron GetWinner(double[] inputs)
    {
     
        List<double> distances = new List<double>();
        List<Neuron> neuronsToFind = new List<Neuron>();
//neuronsToFind.Add(Neurons[0][0]);


        // Перебор всех нейронов
         foreach (Neuron[] ns in Neurons)   
        {
            foreach (Neuron n in ns)
            {
                double sum = 0;
                for (int i = 0; i < n.Weights.Length; i++)
                {
                    sum += Math.Pow((inputs[i] - n.Weights[i]), 2);
                }
                
                neuronsToFind.Add(n);
                distances.Add(Math.Sqrt(sum));
            }
        }
        
        // Поиск минимального расстояния
        double min = distances[0];
        Neuron winner = neuronsToFind[0];
        for (int i = 1; i < distances.Count; i++)
        {
            if (min < distances[i])
            {
                min = distances[i];
                winner = neuronsToFind[i];
            }
        }

        return winner; //neuronsToFind[0];
    }

    public void ClearColor()
    {
        foreach (Neuron[] ns in Neurons)
        {
            foreach (Neuron n in ns)
            {
                n.ClearColor();
            }
        }
    }

    public void Search(double[][] inputs)
    {
        ClearColor();
        foreach (double[] inputUnit in inputs)
        {
            Neuron n = GetWinner(inputUnit);
            n.Recolor();
        } 
    }

    public void Learn(int iterations, double speed)
    {
        double lambda = iterations / Math.Log(this.sigma0);
        for (int i = 0; i < )
    }
    
    public void Recolor()
    {
        foreach (Neuron[] ns in Neurons)
        {
            foreach (Neuron n in ns)
            {
                n.Recolor();
            }
        }
    }
}
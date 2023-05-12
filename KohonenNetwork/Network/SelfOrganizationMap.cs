using System;
using System.Collections.Generic;
using System.Linq;
using NLog.Fluent;

namespace KohonenNetwork.Network;


public class SelfOrganizationMap
{
    public Neuron[][] Neurons;
    public double[] Inputs;
    public int BSize;
    public double sigma0;

    private Neuron currentWinner;
    
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
        Normalize();
        foreach (double[] inputUnit in inputs)
        {
            Neuron n = GetWinner(inputUnit);
            n.Recolor();
        } 
    }
    
    
    public void Learn(int iterations, double speed, double[][] trainingSample)
    {
        List<Neuron> allNeurons = new List<Neuron>();
        for (int i = 0; i < this.Neurons.Length; i++)
        {
            for (int j = 0; j < this.Neurons[i].Length; j++)
            {
                allNeurons.Add(this.Neurons[i][j]);
            }
        }

        double lambda = iterations / Math.Log(this.sigma0);
        for (int i = 0; i < trainingSample.Length; i++)
        {
            this.currentWinner = GetWinner(trainingSample[i]);
            for (int j = 0; j < iterations; j++)
            {
                double sigma = this.sigma0 * Math.Exp(-(j / lambda));
                double l = speed * Math.Exp(-(j / lambda));

                List<Neuron> neighbours = allNeurons.Where(neuron => Math.Sqrt(Math.Pow((neuron.x - currentWinner.x), 2) +
                                                                               Math.Pow((neuron.y - currentWinner.y), 2)) < sigma).ToList();

                foreach (Neuron neighbour in neighbours)
                {
                    double destination = Math.Sqrt(Math.Pow((neighbour.x - currentWinner.x), 2) +
                                                   Math.Pow((neighbour.y - currentWinner.y), 2));
                    double theta = Math.Exp(-((destination * destination) / (2 * (sigma * sigma))));
                    
                    // Корректировка весов

                    for (int k = 0; k < neighbour.Weights.Length; k++)
                    {
                        neighbour.Weights[k] += theta * l * (trainingSample[i][k] - neighbour.Weights[k]);
                    }
                    
                }

            }
            Normalize();
            Recolor();
        }
    }


    public void Normalize()
    {
        // Ищем максимальный вес
        double maxWeight = Neurons[0][0].Weights[0];
        for (int i = 0; i < Neurons.Length; i++)
        {
            for (int j = 0; j < Neurons[i].Length; j++)
            {
                for (int k = 0; k < Neurons[i][j].Weights.Length; k++)
                {
                    if (Neurons[i][j].Weights[k] > maxWeight)
                    {
                        maxWeight = Neurons[i][j].Weights[k];
                    }
                }
            }
        }

        maxWeight = maxWeight;
        // Корректируем веса на основе максимального веса
        double tmpWeight = 0;
        double nWeight = 0;
        for (int i = 0; i < Neurons.Length; i++)
        {
            for (int j = 0; j < Neurons[i].Length; j++)
            {
                for (int k = 0; k < Neurons[i][j].Weights.Length; k++)
                {
                    if (maxWeight < Neurons[i][j].Weights.Length)
                    {
                        tmpWeight = Neurons[i][j].Weights[k] / maxWeight;
                        nWeight = Neurons[i][j].Weights[k];
                        Neurons[i][j].Weights[k] = nWeight / tmpWeight;
                    }
                }
            }
        }
        
        
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
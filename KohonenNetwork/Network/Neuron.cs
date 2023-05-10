using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace KohonenNetwork.Network;

public class Neuron
{
    public Button NBtn;
    public int x;
    public int y;

    public double[] Inputs;
    public double[]? Weights;
    public Neuron(int x, int y, Button b, double[] ia)
    {
        this.NBtn = b;
        this.x = x;
        this.y = y;
        this.Inputs = ia;
        
    }

    public Neuron(int x, int y, Button b, double[] ia, double[] weights) : this(x, y, b, ia)
    {
        this.Weights = weights;
    }
    
    public void InitializeWeights(double rFrom, double rTo)
    {
        if (Weights == null)
        {
            Weights = new double[Inputs.Length];
            for (int i = 0; i < Weights.Length; i++)
            {
                double random = Util.Functions.GetRandomNumber(rFrom, rTo);
                Weights[i] = random;
            }
        } 
    }
    
  

    public double avgWeights()
    {
        double sum = 0;
        foreach (double w in Weights)
        {
            sum += w;
        }

        return sum / Weights.Length;
    }
    public void Recolor()
    {
        this.NBtn.Background = new SolidColorBrush( new Color(255, Convert.ToByte(255 * Weights[0]), Convert.ToByte(255 * Weights[1]), Convert.ToByte(255 * Weights[2])));
    }

    public void ClearColor()
    {
        this.NBtn.Background = new SolidColorBrush(new Color(255, 255, 255, 255));
    }
}
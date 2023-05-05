using Avalonia.Controls;

namespace KohonenNetwork.Network;

public class Neuron
{
    public Button NBtn;
    public int x;
    public int y;

    public double[] InputActions;
    public double[] Weights;
    public Neuron(int x, int y, Button b, double[] ia, double[] ws)
    {
        this.NBtn = b;
        this.x = x;
        this.y = y;
        this.InputActions = InputActions;
        this.Weights = ws;
    }

    public void Recolor()
    {
        
    }
}
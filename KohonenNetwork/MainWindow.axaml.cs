using System.Drawing;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using KohonenNetwork.Network;
using KohonenNetwork.Visual;

namespace KohonenNetwork;

public partial class MainWindow : Window
{

    public TextBlock DebugTB;
    private Button[][] mapDisplay;

    public SelfOrganizationMap SOM;
    public Grid mainGrid;

    public TextBox rFromBox;
    public TextBox rToBox;
    public TextBox mapSizeBox;
    
    public MainWindow()
    {
        InitializeComponent();

        mainGrid = this.FindControl<Grid>("KohonenMapPanel");

        this.DebugTB = this.FindControl<TextBlock>("DebugString");
        
        TrainingSample.GenerateIBMSet(40);

        this.rFromBox = this.FindControl<TextBox>("RFrom");
        this.rToBox = this.FindControl<TextBox>("RTo");
        this.mapSizeBox = this.FindControl<TextBox>("MapSize");
        //this.mapDisplay = InitializeMap(4, mainGrid);
        debug();
    }

    private void debug()
    {
        //DebugTB.Text = getDefinitions(4, "Auto");
        DebugTB.Text = Util.Functions.GetRandomNumber(5, 10) + "";
    }
    public void addGrid(int cols, int rows)
    {
        Grid grid = new Grid();
        
    }

    private string getDefinitions(int size, string filler)
    {
        
        string toRet = "";
        for (int i = 0; i < size+1; i++)
        {
            toRet += filler + ", ";
        }
        
        return toRet.Substring(0, size* (filler.Length + 2) - 2);
        
    }
    
    public Button[][] InitializeMap(int size, Grid parent)
    {
        Button reference = Util.Functions.GetConfiguredButton(20);
        
        
        
        // Grid initialization
        Grid grid = new Grid
        {
            RowDefinitions = new RowDefinitions(getDefinitions(size, "Auto")), 
            ColumnDefinitions = new ColumnDefinitions(getDefinitions(size, "Auto"))
        };

        Button[][] buttons = new Button[size][];
        Button tmpButton;
        for (int i = 0; i < size; i++)
        {
            buttons[i] = new Button[size];
            for (int j = 0; j < size; j++)
            {
                tmpButton = Util.Functions.CopyButton(reference, i + "_" + j);
                Grid.SetColumn(tmpButton, i);
                Grid.SetRow(tmpButton, j);
                grid.Children.Add(tmpButton);
                buttons[i][j] = tmpButton;
            }
        }
        Grid.SetRow(parent, 1);
        parent.Children.Add(grid);

        return buttons;
    }

    public SelfOrganizationMap InitializeSOMap(int size, int bSize, double[] inputs, Grid parent, double rFrom, double rTo)
    {
        
        // Grid initialization
        Grid grid = new Grid
        {
            RowDefinitions = new RowDefinitions(getDefinitions(size, "Auto")), 
            ColumnDefinitions = new ColumnDefinitions(getDefinitions(size, "Auto"))
        };
        
        SelfOrganizationMap som = new SelfOrganizationMap(size, size, inputs, bSize, rFrom, rTo);

        for (int i = 0; i < som.Neurons.Length; i++)
        {
            for (int j = 0; j < som.Neurons[i].Length; j++)
            {
                Neuron n = som.Neurons[i][j];
                Button nBtn = n.NBtn;
                nBtn.Name = i + ":" + j;
                Grid.SetRow(nBtn, i);
                Grid.SetColumn(nBtn, j);
                grid.Children.Add(nBtn);
            }
        }
        
        Grid.SetRow(parent, 1);
        
        parent.Children.Add(grid);
        som.Recolor();

        return som;
    }

    public void InitializeSOMapClickHandler(object sender, RoutedEventArgs e)
    {
        double rFrom = double.Parse(rFromBox.Text);
        double rTo = double.Parse(rToBox.Text);
        int mapSize = int.Parse(this.mapSizeBox.Text);
        this.SOM = InitializeSOMap(mapSize, 10, new []{1d, 2d, 3d}, mainGrid, rFrom, rTo);
    }

    public void GetWinners(object sender, RoutedEventArgs e)
    {
        this.SOM.Search(TrainingSample.IBMSet);
    }
    
    public void TeachOneTime(object sender, RoutedEventArgs e)
    {
        this.DebugTB.Text = "OneTimeButtonPressed";
    }
}

using System.Drawing;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.VisualTree;
using KohonenNetwork.Visual;

namespace KohonenNetwork;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Grid mainGrid = this.FindControl<Grid>("KohonenMapPanel");

        
        
        InitializeMap(32, mainGrid);
        
    }

    private void debug()
    {
        TextBlock debugTB = this.FindControl<TextBlock>("DebugString");
        debugTB.Text = getDefinitions(5, "*");
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

        return toRet.Substring(0, size*3 - 2);
        
    }
    
    public void InitializeMap(int size, Grid parent)
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
            }
        }
        Grid.SetRow(parent, 1);
        parent.Children.Add(grid);
    }
}

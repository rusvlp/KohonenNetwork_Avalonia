using Avalonia.Controls;
using Avalonia.Controls.Shapes;

namespace KohonenNetwork.Visual;

public class RectangleControl : Control
{
    public Rectangle Rectangle;
    public RectangleControl()
    {
        Rectangle rect = new Rectangle();
        VisualChildren.Add(Rectangle);
    }
   
}
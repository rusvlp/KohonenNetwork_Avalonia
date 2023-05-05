using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace KohonenNetwork.Util;

public static class Functions
{
    public static Button GetConfiguredButton(int bSize)
    {
        Button button = new Button
        {
            Width = bSize,
            Height = bSize,
            Background = new SolidColorBrush(new Color(255, 255, 0, 0), 1d)
        };

        return button;
    }

    public static Button CopyButton(Button src, string name)
    {
        return new Button
        {
            Width = src.Width,
            Height = src.Height,
            Background = src.Background,
            Name = name
        };

    }

    public static double GetRandomNumber(double from, double to)
    {
        Random rnd = new Random();
        return from + (rnd.NextDouble() * (to - from));
    }   
}
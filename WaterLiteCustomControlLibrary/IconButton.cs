using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WaterLiteCustomControlLibrary;

public class IconButton: Button
{
    static IconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton), 
            new FrameworkPropertyMetadata(typeof(IconButton)));
    }

    #region PropertyDependency

    public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register(
        nameof(MouseOverBackground), typeof(Brush), typeof(IconButton), new PropertyMetadata(default(Brush)));

    public Brush MouseOverBackground
    {
        get => (Brush)GetValue(MouseOverBackgroundProperty);
        set => SetValue(MouseOverBackgroundProperty, value);
    }

    public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.Register(
        nameof(MouseOverForeground), typeof(Brush), typeof(IconButton), new PropertyMetadata(default(Brush)));

    public Brush MouseOverForeground
    {
        get => (Brush)GetValue(MouseOverForegroundProperty);
        set => SetValue(MouseOverForegroundProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius), typeof(CornerRadius), typeof(IconButton), new PropertyMetadata(default(CornerRadius)));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    #endregion
}
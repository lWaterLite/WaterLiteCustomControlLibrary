﻿using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WaterLiteCustomControlLibrary;

public class Switcher : ToggleButton
{
    static Switcher()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Switcher),
            new FrameworkPropertyMetadata(typeof(Switcher)));
    }
    
    public Switcher()
    {
        Loaded += (sender, _) =>
        {
            _buttonMovement =
                ((Switcher)sender).GetTemplateChild("ButtonTransform") as TranslateTransform;
            _textMovement = ((Switcher)sender).GetTemplateChild("TextTransform") as TranslateTransform;
            _checkTextBlock = ((Switcher)sender).GetTemplateChild("CheckTextBlock") as TextBlock;
            if (_checkTextBlock == null) return;
            if (IsChecked == null) return;
            if (_buttonMovement != null)
                _buttonMovement.X = (IsChecked == true ? -1 : 1) * (ActualWidth - ActualHeight) / 2;
            if (_textMovement != null)
                _textMovement.X = IsChecked == true
                    ? ActualWidth - _checkTextBlock.ActualWidth - _checkTextBlock.Padding.Right
                    : 0;
            _checkTextBlock.Text = IsChecked == true ? CheckedText : UncheckedText;
        };
        Checked += CheckedAnimation;
        Unchecked += UncheckedAnimation;
    }

    #region PropertyDependency

    public static readonly DependencyProperty CheckedTextProperty =
        DependencyProperty.Register(nameof(CheckedText), typeof(string), typeof(Switcher), new PropertyMetadata("ON"));

    public static readonly DependencyProperty UncheckedTextProperty =
        DependencyProperty.Register(nameof(UncheckedText), typeof(string), typeof(Switcher),
            new PropertyMetadata("OFF"));

    public static readonly DependencyProperty ButtonColorProperty =
        DependencyProperty.Register(nameof(ButtonColor), typeof(Brush), typeof(Switcher),
            new PropertyMetadata(new SolidColorBrush(Color.FromRgb(43, 43, 43))));

    public static readonly DependencyProperty DisableInsideColorProperty = 
        DependencyProperty.Register(nameof(DisableInsideColor), typeof(Brush), typeof(Switcher),
        new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 0, 0))));

    public string CheckedText
    {
        get => (string)GetValue(CheckedTextProperty);
        set => SetValue(CheckedTextProperty, value);
    }

    public string UncheckedText
    {
        get => (string)GetValue(UncheckedTextProperty);
        set => SetValue(UncheckedTextProperty, value);
    }

    public Brush ButtonColor
    {
        get => (Brush)GetValue(ButtonColorProperty);
        set => SetValue(ButtonColorProperty, value);
    }

    public Brush DisableInsideColor
    {
        get => (Brush)GetValue(DisableInsideColorProperty);
        set => SetValue(DisableInsideColorProperty, value);
    }

    #endregion

    #region Animations

    private TranslateTransform? _buttonMovement;
    private TranslateTransform? _textMovement;
    private TextBlock? _checkTextBlock;

    private readonly DoubleAnimation _buttonAnimation = new(0, new Duration(new TimeSpan(0, 0, 0, 0, 300)))
    {
        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
    };

    private readonly DoubleAnimation _textAnimation = new(0, new Duration(new TimeSpan(0, 0, 0, 0, 300)))
    {
        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
    };

    private void CheckedAnimation(object sender, RoutedEventArgs e)
    {
        if (!IsLoaded) e.Handled = true;
        if (_buttonMovement == null) return;
        if (_textMovement == null) return;
        if (_checkTextBlock == null) return;
        _buttonAnimation.To = -(ActualWidth - ActualHeight) / 2;
        _textAnimation.To = ActualWidth - _checkTextBlock.ActualWidth;
        _buttonMovement.BeginAnimation(TranslateTransform.XProperty, _buttonAnimation);
        _textMovement.BeginAnimation(TranslateTransform.XProperty, _textAnimation);
        _checkTextBlock.Text = CheckedText;
    }

    private void UncheckedAnimation(object sender, RoutedEventArgs e)
    {
        if (!IsLoaded) e.Handled = true;
        if (_buttonMovement == null) return;
        if (_textMovement == null) return;
        if (_checkTextBlock == null) return;
        _buttonAnimation.To = (ActualWidth - ActualHeight) / 2;
        _textAnimation.To = 0;
        _buttonMovement.BeginAnimation(TranslateTransform.XProperty, _buttonAnimation);
        _textMovement.BeginAnimation(TranslateTransform.XProperty, _textAnimation);
        _checkTextBlock.Text = UncheckedText;
    }

    #endregion
}

#region Converters

public class HeightToCornerRadiusConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return 0.5 * double.Parse(value?.ToString() ?? throw new InvalidOperationException());
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new Exception("ConvertBack Not Allowed");
    }
}

public class CheckStatusToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return Visibility.Hidden;
        throw new Exception("ConvertBack Not Allowed");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new Exception("ConvertBack Not Allowed");
    }
}

public class UncheckedStatusToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return Visibility.Hidden;
        return (bool)value ? Visibility.Hidden : Visibility.Visible;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new Exception("ConvertBack Not Allowed");
    }
}

#endregion
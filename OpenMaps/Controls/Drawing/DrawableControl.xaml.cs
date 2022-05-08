using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using OpenMaps.Common;
using OpenMaps.Controls.Drawing.Brushes;
using OpenMaps.MVVM;

namespace OpenMaps.Controls.Drawing;

public partial class DrawableControl
{
    [MemberNotNullWhen(true, nameof(IsStroking))] private DrawStroke? CurrentStroke { get; set; }
    public bool IsStroking { get; private set; }

    public static DependencyProperty PixelDisplayProperty = 
        DependencyProperty.Register(nameof(PixelDisplay), typeof(IPixelDisplay), typeof(DrawableControl));
    public IPixelDisplay? PixelDisplay
    {
        get => (IPixelDisplay)GetValue(PixelDisplayProperty); 
        set => SetValue(PixelDisplayProperty, value);
    }
    
    public static DependencyProperty DrawBrushProperty = 
        DependencyProperty.Register(nameof(DrawBrush), typeof(IDrawBrush), typeof(DrawableControl));
    public IDrawBrush? DrawBrush
    {
        get => (IDrawBrush)GetValue(DrawBrushProperty);
        set => SetValue(DrawBrushProperty, value);
    }

    private readonly List<DrawStroke> b_strokes;
    public IEnumerable<DrawStroke> Strokes => b_strokes;

    public Rect Rect => new(0, 0, Width, Height);

    public DrawableControl()
    {
        b_strokes = new List<DrawStroke>();
        
        InitializeComponent();
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
        if (IsStroking)
            return;
        
        BeginStroke();
        OnMouseMove(sender, e);
    }
    
    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (!IsStroking)
            return;
        
        if (PixelDisplay == null)
            return;
        
        var position = e.GetPosition(this);

        if (!Rect.Contains(position))
            return;

        if (DrawBrush == null)
            return;

        var ratio = new Point(Width / PixelDisplay.Size.X, Height / PixelDisplay.Size.Y);
        var intPosition = new IntVector(Math2.FloorToInt(position.X / ratio.X), Math2.FloorToInt(position.Y / ratio.Y));

        var lastPoint = CurrentStroke?.Points.LastOrDefault(new IntVector(-1, -1))!;

        PixelDisplay.Lock();

        if (lastPoint != new IntVector(-1, -1))
        {
            var alphaAdd = 1d / new IntVector(intPosition.X - lastPoint.Value.X, intPosition.Y - lastPoint.Value.Y).Magnitude;
            var alpha = 0d;

            var xDiff = intPosition.X - lastPoint.Value.X;
            var yDiff = intPosition.Y - lastPoint.Value.Y;

            while (alpha < 1d)
            {
                alpha += alphaAdd;

                var adjusted = new IntVector(
                    Math2.FloorToInt((position.X + (xDiff * alpha)) / ratio.X),
                    Math2.FloorToInt((position.Y + (yDiff * alpha)) / ratio.Y));
                
                DrawBrush.Draw(adjusted, PixelDisplay);
                UpdateStroke(intPosition);
            }
        }

        DrawBrush.Draw(intPosition, PixelDisplay);
        UpdateStroke(intPosition);

        PixelDisplay.Unlock();
    }

    private void OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        EndStroke();
    }
    
    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
        EndStroke();
    }

    private void BeginStroke()
    {
        var captured = CaptureMouse() || CaptureStylus();
        
        if (!captured)
            return;

        CurrentStroke = new DrawStroke();
        IsStroking = true;
    }

    private void UpdateStroke(IntVector position)
    {
        if (!IsStroking)
            return;
        
        CurrentStroke.Points.Add(position);
    }

    private void EndStroke()
    {
        ReleaseMouseCapture(); 
        ReleaseStylusCapture();
        
        IsStroking = false;
    }

    protected override void OnRender(DrawingContext context)
    {
        base.OnRender(context);
        
        PixelDisplay?.Draw(context, Rect);
    }
}
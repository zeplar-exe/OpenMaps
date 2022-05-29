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
using OpenMaps.Tools;
using OpenMaps.Tools.Brushes;

namespace OpenMaps.Controls.Drawing;

public partial class DrawableControl
{
    public bool IsStroking { get; private set; }

    public static DependencyProperty PixelDisplayProperty = 
        DependencyProperty.Register(nameof(PixelDisplay), typeof(IPixelDisplay), typeof(DrawableControl));
    public IPixelDisplay? PixelDisplay
    {
        get => (IPixelDisplay)GetValue(PixelDisplayProperty); 
        set => SetValue(PixelDisplayProperty, value);
    }
    
    public static DependencyProperty SelectedToolProperty = 
        DependencyProperty.Register(nameof(SelectedTool), typeof(Tool), typeof(DrawableControl));
    public Tool? SelectedTool
    {
        get => (Tool)GetValue(SelectedToolProperty);
        set => SetValue(SelectedToolProperty, value);
    }

    public Rect Rect => new(0, 0, Width, Height);

    public DrawableControl()
    {
        InitializeComponent();
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
        if (IsStroking)
            return;
        
        BeginStroke();
        
        if (PixelDisplay == null)
            return;
        
        var position = e.GetPosition(this);

        if (!Rect.Contains(position))
            return;

        if (SelectedTool == null)
            return;

        var ratio = new Point(Width / PixelDisplay.Size.X, Height / PixelDisplay.Size.Y);
        var intPosition = new IntVector(Math2.FloorToInt(position.X / ratio.X), Math2.FloorToInt(position.Y / ratio.Y));
        // the problem is that it doesn't effectively handle the control being different from the draw area
        PixelDisplay.Lock();
        
        SelectedTool.OnCanvasStrokeBegin(intPosition, new PixelDisplayBridge(PixelDisplay));
        SelectedTool.OnCanvasStroke(intPosition, new PixelDisplayBridge(PixelDisplay));
        
        PixelDisplay.Unlock();
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

        if (SelectedTool == null)
            return;

        var ratio = new Point(Width / PixelDisplay.Size.X, Height / PixelDisplay.Size.Y);
        var intPosition = new IntVector(Math2.FloorToInt(position.X / ratio.X), Math2.FloorToInt(position.Y / ratio.Y));
        
        PixelDisplay.Lock();
        
        SelectedTool.OnCanvasStroke(intPosition, new PixelDisplayBridge(PixelDisplay));
        
        PixelDisplay.Unlock();
    }

    private void OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        EndStroke();
        
        if (PixelDisplay == null)
            return;
        
        var position = e.GetPosition(this);

        if (!Rect.Contains(position))
            return;

        if (SelectedTool == null)
            return;

        var ratio = new Point(Width / PixelDisplay.Size.X, Height / PixelDisplay.Size.Y);
        var intPosition = new IntVector(Math2.FloorToInt(position.X / ratio.X), Math2.FloorToInt(position.Y / ratio.Y));
        
        PixelDisplay.Lock();
        
        SelectedTool.OnCanvasStrokeEnd(intPosition, new PixelDisplayBridge(PixelDisplay));
        SelectedTool.OnCanvasStroke(intPosition, new PixelDisplayBridge(PixelDisplay));
        
        PixelDisplay.Unlock();
    }
    
    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
        EndStroke();
        
        if (PixelDisplay == null)
            return;
        
        var position = e.GetPosition(this);

        if (!Rect.Contains(position))
            return;

        if (SelectedTool == null)
            return;

        var ratio = new Point(Width / PixelDisplay.Size.X, Height / PixelDisplay.Size.Y);
        var intPosition = new IntVector(Math2.FloorToInt(position.X / ratio.X), Math2.FloorToInt(position.Y / ratio.Y));
        
        PixelDisplay.Lock();
        
        SelectedTool.OnCanvasStrokeEnd(intPosition, new PixelDisplayBridge(PixelDisplay));
        SelectedTool.OnCanvasStroke(intPosition, new PixelDisplayBridge(PixelDisplay));
        
        PixelDisplay.Unlock();
    }

    private void BeginStroke()
    {
        var captured = CaptureMouse() || CaptureStylus();
        
        if (!captured)
            return;

        IsStroking = true;
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
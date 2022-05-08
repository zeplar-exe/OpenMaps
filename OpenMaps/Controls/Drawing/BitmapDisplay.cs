using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenMaps.Common;

namespace OpenMaps.Controls.Drawing;

public class BitmapDisplay : IPixelDisplay
{
    private bool Locked { get; set; }
    private PixelFormat PixelFormat { get; }
    private int BytesPerPixel => PixelFormat.BitsPerPixel / 8;
    
    public IntVector Size { get; }

    public WriteableBitmap Bitmap { get; }
    
    public IColorPixel this[int x, int y]
    {
        get => GetRect(new Int32Rect(x, y, 1, 1))[0, 0];
        set => SetRect(new Int32Rect(x, y, 1, 1), value);
    }


    public BitmapDisplay(IntVector size, DpiScale dpi)
    {
        PixelFormat = PixelFormats.Bgra32;
        Bitmap = new WriteableBitmap(size.X, size.Y, dpi.DpiScaleX, dpi.DpiScaleY, PixelFormat, null);
        Size = size;
    }

    public void Lock()
    {
        Locked = true;
        Bitmap.Lock();
    }

    public void Unlock()
    {
        Locked = true;
        Bitmap.Unlock();
    }
    
    public IColorPixel[,] GetRect(Int32Rect rect)
    {
        var pixels = new IColorPixel[rect.Width, rect.Height];
        
        unsafe
        {
            var backBufferPointer = Bitmap.BackBuffer;
            var width = rect.X * BytesPerPixel;
            
            backBufferPointer += width;
            backBufferPointer += rect.Y * Bitmap.BackBufferStride;
            
            for (var x = 0; x < rect.Width; x++)
            {
                for (var y = 0; y < rect.Height; y++)
                {
                    backBufferPointer += 1;
                    
                    var bytes = BitConverter.GetBytes(*(int*)backBufferPointer);
                    
                    pixels[x, y] = new ColorPixel(b: bytes[0], g: bytes[1], r: bytes[2], a: bytes[3]);
                }

                backBufferPointer -= width;
                backBufferPointer += rect.Height * Bitmap.BackBufferStride;
            }
        }

        return pixels;
    }
    
    public void SetRect(Int32Rect rect, IColorPixel value)
    {
        if (!Locked)
            throw new InvalidOperationException("Cannot update the bitmap while it is unlocked.");
        
        unsafe
        {
            var backBufferPointer = Bitmap.BackBuffer;

            backBufferPointer += rect.X * BytesPerPixel;
            backBufferPointer += rect.Y * Bitmap.BackBufferStride;

            for (var x = 0; x < rect.Width; x++)
            {
                for (var y = 0; y < rect.Height; y++)
                {
                    *(byte*)backBufferPointer = value.GetB();

                    backBufferPointer += 1;
                    
                    *(byte*)backBufferPointer = value.GetG();
                    
                    backBufferPointer += 1;
                    
                    *(byte*)backBufferPointer = value.GetR();
                    
                    backBufferPointer += 1;
                    
                    *(byte*)backBufferPointer = value.GetA();
                    
                    backBufferPointer += 1;
                }

                backBufferPointer -= rect.Width * BytesPerPixel;
                backBufferPointer += Bitmap.BackBufferStride;
            }
            
            Bitmap.AddDirtyRect(rect);
        }
    }
    
    public void Draw(DrawingContext context, Rect rect)
    {
        context.DrawImage(Bitmap, rect);
    }
}
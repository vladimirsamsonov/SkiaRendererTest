using SkiaSharp;
using SkiaSpike.Wgl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkiaSpike
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SkiaRenderer renderer = new SkiaRenderer();
            renderer.CreateBitmap(500, 300);
        }
    }


    public class SkiaRenderer
    {
        private IntPtr pBitMap;                   // memory handle
        private int rowByte;                      // bytes of one row
        private int bitmapHeight, bitmapWidth;    // height and width of the canvas


        void Init(int width, int height) {
            // make sure that OpenGL is initialized
            // ...


            var ctx = new WglContext();
			
                ctx.MakeCurrent();

                // set up the SkiaSharp context
                   var grContext = GRContext.CreateGl();


                // create the GPU surface
                var surface = SKSurface.Create(grContext, true, new SKImageInfo(width, height));

            // draw
            var canvas = surface.Canvas;

            ctx.Destroy();
        }

    

        public  void CreateBitmap(int width, int height)
        {
            bitmapHeight = height;
            bitmapWidth = width;
            pBitMap = Marshal.AllocHGlobal(bitmapWidth * bitmapHeight * 4);     // allocate a memory block

            rowByte = bitmapWidth * 4;

            Init(width, height);
        }

        private void FreeBitmap()
        {
            if (pBitMap != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(pBitMap);   // free the allocated memory block
                pBitMap = IntPtr.Zero;
            }
        }
    }



    public abstract class GlContext : IDisposable
    {
        public abstract void MakeCurrent();
        public abstract void SwapBuffers();
        public abstract void Destroy();
        public abstract GRGlTextureInfo CreateTexture(SKSizeI textureSize);
        public abstract void DestroyTexture(uint texture);

        void IDisposable.Dispose() => Destroy();
    }

}

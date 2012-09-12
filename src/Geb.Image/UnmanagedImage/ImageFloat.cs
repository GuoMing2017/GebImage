﻿/*************************************************************************
 *  Copyright (c) 2012 Hu Fei(xiaotie@geblab.com; geblab, www.geblab.com)
 ************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Geb.Image
{
    public struct FloatConverter : IColorConverter
    {
        public unsafe void Copy(Rgb24* from, void* to, int length)
        {
            throw new NotImplementedException();
        }

        public unsafe void Copy(Argb32* from, void* to, int length)
        {
            throw new NotImplementedException();
        }

        public unsafe void Copy(byte* from, void* to, int length)
        {
            throw new NotImplementedException();
        }
    }

    public partial class ImageFloat : UnmanagedImage<float>
    {
        public unsafe ImageFloat(Int32 width, Int32 height)
            : base(width, height)
        {
        }

        protected override IColorConverter CreateByteConverter()
        {
            return new FloatConverter();
        }

        public override IImage Clone()
        {
            ImageFloat img = new ImageFloat(this.Width, this.Height);
            img.CloneFrom(this);
            return img;
        }

        public unsafe ImageU8 ToImageU8(int coeff = 255)
        {
            ImageU8 img = new ImageU8(this.Width, this.Height);
            float* p = this.Start;
            float* pEnd = p + this.Length;
            byte* dst = img.Start;
            float val = 0;
            while (p < pEnd)
            {
                val = *p * coeff;
                val = Math.Min(255,Math.Max(val, 0));
                *dst = (Byte)val;
                p++;
                dst++;
            }
            return img;
        }

        public unsafe float[] ToArray()
        {
            float[] array = new float[this.Length];
            for (int i = 0; i < Length; i++)
            {
                array[i] = this[i];
            }
            return array;
        }

        protected override PixelFormat GetOutputBitmapPixelFormat()
        {
            return PixelFormat.Format8bppIndexed;
        }

        protected override unsafe void ToBitmapCore(byte* src, byte* dst, int width)
        {
            throw new NotImplementedException();
        }
    }
}
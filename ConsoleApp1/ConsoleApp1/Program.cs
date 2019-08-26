using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    struct XRECTF
    {
        public float Width;
        public float Height;
    }

    struct XRECT
    {
        public float Width;
        public float Height;
    }

    class Program
    {
        static float GetScaleFactorFromNaturalSize(XRECTF pNaturalSize, XRECT pDestRect)
        {
            float scaleFactor = 1.0f;

            float destWidth = (float)(pDestRect.Width);
            float destHeight = (float)(pDestRect.Height);

            float naturalWidth = pNaturalSize.Width;
            float naturalHeight = pNaturalSize.Height;

            if (naturalWidth > 0.0f && naturalHeight > 0.0f)
            {
                float offsetWidth = destWidth - naturalWidth;
                float offsetHeight = destHeight - naturalHeight;
                if (offsetWidth > 0.0f || offsetHeight > 0.0f)
                {
                    if (offsetWidth<offsetHeight)
                    {
                        scaleFactor = destWidth / naturalWidth;
                    }
                    else
                    {
                        scaleFactor = destHeight / naturalHeight;
                    }
                }
            }

            return scaleFactor;
        }

        static void Main(string[] args)
        {
            var f = GetScaleFactorFromNaturalSize(
                new XRECTF()
                {
                    Height = 1080,
                    Width = 1080,
                },
                new XRECT()
                {
                    Height = 4096,
                    Width= 4096,
                });
        }
    }
}

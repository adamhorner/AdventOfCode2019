using System;
using System.Linq;

namespace Day8
{
    internal class ImageLayer
    {
        private int Width { get; }
        private int Height { get; }
        private readonly int[] _layer;
        private readonly int[] _histogram;
        private int _fillCounter = 0;

        public ImageLayer(int width, int height)
        {
            Width = width;
            Height = height;
            _layer = new int[width * height];
            _histogram = new int[10];
        }

        public void AddNumber(int number)
        {
            if (number<0||number>9) throw new ArgumentException("number must be a digit (0-9)");
            if (_fillCounter == Width * Height) throw new ImageLayerFullException();
            _layer[_fillCounter++] = number;
            _histogram[number]++;
        }

        public int GetLayerCount(int digit)
        {
            if (digit<0||digit>9) throw new ArgumentException("input must be a digit (0-9)");
            return _histogram[digit];
        }

        private int GetLayerCount()
        {
            return _histogram.Sum();
        }

        private int GetLayerSize()
        {
            return Width * Height;
        }

        public bool IsFull()
        {
            return GetLayerCount() == GetLayerSize();
        }

        public override string ToString()
        {
            var layerString = "";
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    layerString += _layer[row * Width + col] == 1 ? "X" : " ";
                }

                layerString += "\n";
            }
            return "Histogram: " + string.Join(',', _histogram) + "\n" +
                "Layer:\n" +
                layerString;
        }

        public void Flatten(ImageLayer lowerLayer)
        {
            for (var i = 0; i < GetLayerSize(); i++)
            {
                if (_layer[i] == 2)
                {
                    _layer[i] = lowerLayer._layer[i];
                    _histogram[2]--;
                    _histogram[_layer[i]]++;
                }
            }
        }

        public bool HasTransparency()
        {
            return _layer.Any(pixel => pixel == 2);
        }
    }
    
    internal class ImageLayerFullException : Exception
    {
    }
}

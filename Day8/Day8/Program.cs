using System;
using System.Collections.Generic;
using System.IO;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1 || string.IsNullOrWhiteSpace(args[0]))
                throw new ArgumentException("No input file provided");

            var inputs = File.ReadAllText(args[0]).ToCharArray();
            var layers = new List<ImageLayer>();
            var currentLayer = new ImageLayer(25, 6);
            layers.Add(currentLayer);
            foreach (var input in inputs)
            {
                if (!int.TryParse(input.ToString(), out int result)) continue;
                if (currentLayer.IsFull())
                {
                    currentLayer = new ImageLayer(25, 6);
                    layers.Add(currentLayer);
                }

                currentLayer.AddNumber(result);
            }

            var fewestZeroes = int.MaxValue;
            foreach (ImageLayer layer in layers)
            {
                var zeroes = layer.GetLayerCount(0);
                if (zeroes < fewestZeroes)
                {
                    currentLayer = layer;
                    fewestZeroes = zeroes;
                }
            }

            Console.WriteLine("Fewest Zeroes is {0} and the layer has {1} ones and {2} twos giving a result of {3}",
                fewestZeroes,
                currentLayer.GetLayerCount(1),
                currentLayer.GetLayerCount(2),
                currentLayer.GetLayerCount(1) * currentLayer.GetLayerCount(2));
            Console.WriteLine(currentLayer.ToString());
            
            // part 2 - flatten image so there is no more transparency
            var mainImage = new ImageLayer(25,6);
            while (!mainImage.IsFull()) mainImage.AddNumber(2);
            var counter = 0;
            do mainImage.Flatten(layers[counter++]);
            while (mainImage.HasTransparency());
            Console.WriteLine(counter + " Loops required");
            Console.WriteLine(mainImage);
        }
    }

}
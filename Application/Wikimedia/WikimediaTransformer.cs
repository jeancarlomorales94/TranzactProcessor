using System;
using Application.Common;

namespace Application.Wikimedia
{
    public class WikimediaTransformer : IActivity
    {
        public bool Execute()
        {
            Console.WriteLine("Transformer");
            return true;
        }
    }
}
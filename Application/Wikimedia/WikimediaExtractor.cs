using System;
using Application.Common;

namespace Application.Wikimedia
{
    public class WikimediaExtractor : IActivity
    {
        public bool Execute()
        {
            Console.WriteLine("Extractor");
            return true;
        }
    }
}

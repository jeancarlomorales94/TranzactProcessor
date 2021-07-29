using System;
using Application.Common;

namespace Application.Wikimedia
{
    public class WikimediaLoader : IActivity
    {
        public bool Execute()
        {
            Console.WriteLine("Loader");
            return true;
        }
    }
}
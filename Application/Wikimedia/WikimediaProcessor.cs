using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common;

namespace Application.Wikimedia
{
    public class WikimediaProcessor
    {
        private readonly List<Type> _addedActivities = new List<Type>();
        private readonly IEnumerable<IActivity> _activities;

        public WikimediaProcessor(IEnumerable<IActivity> activities)
        {
            _activities = activities;
        }
        public void AddActivity<T>() where T : IActivity => _addedActivities.Add(typeof(T));
        public void Process()
        {
            bool continueExecution = true;

            foreach (var activity in _addedActivities)
            {
                if (!continueExecution)
                    return;

                var implementation = _activities.FirstOrDefault(x => x.GetType() == activity);
                if (implementation == null)
                    return;

                continueExecution = implementation.Execute();
            }
        }
    }
}
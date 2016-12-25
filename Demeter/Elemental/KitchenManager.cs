using System;
using System.Collections.Generic;

namespace Demeter.Elemental
{
    /// <summary>
    ///  The kitchen manager
    /// </summary>
    public class KitchenManager
    {
        #region Constructors

        /// <summary>
        ///  Instantiates a KitchenManager object
        /// </summary>
        public KitchenManager()
        {
            ItemTypes = new Dictionary<string, ItemType>();
            ScheduledTasks = new LinkedList<Task>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///  All registered ingredient types
        /// </summary>
        public IDictionary<string, ItemType> ItemTypes { get; private set; }

        /// <summary>
        ///  All scheduled tasks
        /// </summary>
        public LinkedList<Task> ScheduledTasks { get; private set; }

        /// <summary>
        ///  The last time Update() is called to update to
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///  Runs the engine one step from the last update time to the specified time
        /// </summary>        
        /// <param name="time">The time to update to</param>
        public void Update(DateTime time)
        {
            var t = LastUpdateTime;

            while (ScheduledTasks.Count > 0)
            {
                var task = ScheduledTasks.First.Value;
                ScheduledTasks.RemoveFirst();

                if (task.Time > time)
                {
                    break;
                }

                if (t <= task.Time)
                {
                    AgeItems(task.Time - t);
                }
                else
                {
                    // error, the task is dropped
                    continue;
                }

                t = task.Time;

                // handle the task
                var processor = task.Processor;
                var toOutput = true;
                if (!processor.InProgress)
                {
                    var dur = processor.Process();
                    if (dur > TimeSpan.Zero)
                    {
                        var end = t + dur;
                        DelayOutputTask(task, end);
                        toOutput = false;
                    }
                }
                if (toOutput)
                {
                    processor.Output();
                }
            }

            AgeItems(time - t);
            
            LastUpdateTime = time;
        }

        /// <summary>
        ///  Inserts an output task at the specified time
        /// </summary>
        /// <param name="task">The task to insert</param>
        /// <param name="at">The time to insert at</param>
        private void DelayOutputTask(Task task, DateTime at)
        {
            for (var p = ScheduledTasks.First; p != null; p = p.Next)
            {
                // NOTE as the task to insert is an output task so it's always inserted before any other tasks with the same time
                if (at >= p.Value.Time)
                {
                    ScheduledTasks.AddBefore(p, task);
                    return;
                }
            }
            ScheduledTasks.AddLast(task);
        }

        /// <summary>
        ///  Age items
        /// </summary>
        /// <param name="duration">The time to age</param>
        private void AgeItems(TimeSpan duration)
        {
            if (duration == TimeSpan.Zero)
            {
                return;
            }

            foreach (var t in ItemTypes)
            {
                foreach (var i in t.Value.Instances)
                {
                    i.Age(duration);
                }
            }
        }

        #endregion
    }
}

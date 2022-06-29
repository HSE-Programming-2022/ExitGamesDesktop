using System;
using System.Collections.Generic;
using System.Text;

namespace TzClasses
{
    public class CreateParams
    {
        string Name;
        string StartTime;
        string EndTime;


        public CreateParams(string Name, string StartTime, string EndTime)
        {
            this.Name = Name;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }

        public string GetName()
        {
            return Name;
        }

        public string GetStartTime()
        {
            return StartTime;
        }

        public string GetEndTime()
        {
            return EndTime;
        }

    }
}

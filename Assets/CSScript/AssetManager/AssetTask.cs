using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CSScript
{
    public enum TaskType
    {
        Unknow = 0,
        LoadAsset = 1,
    }
    public abstract class AssetTask
    {
        public abstract bool Start();
        public abstract bool Update();
        public abstract void End();
        public abstract void Reset();

        public virtual TaskType TaskType
        {
            get { return TaskType.Unknow; }
        }
    }
}

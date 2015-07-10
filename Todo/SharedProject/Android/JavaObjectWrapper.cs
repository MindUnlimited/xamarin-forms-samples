using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Todo.Android
{
    public class JavaObjectWrapper<T> : Java.Lang.Object
    {
        public T Obj { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TictactoeServer
{
    /// <summary>
    /// ITask interface. every task object has to implement the DoTask method.
    /// We could use ducktyping instead with the new dynamic in C#. But i prefer this way.
    /// </summary>
    interface ITask
    {
        void DoTask();
    }
}

using System;
using System.Collections.Generic;

namespace Demeter.Abstract
{
    public interface IProcessor
    {
        IEnumerable<IItem> Process(IEnumerable<IItem> items, IProcessingContext ctx);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorageLayer.Interfaces;

namespace CoreApi.Helpers
{
    public interface IHelper
    {
        ISaver GetSaver();
    }
}

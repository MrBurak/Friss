using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class Result<T>
    {
        public T ResultEntity { get; set; }

        public string ResultMessage { get; set; }
        public string ResultInnerMessage { get; set; }
        public Boolean ResultStatus { get; set; } = false;
        public int ResultCode { get; set; }

    }
}

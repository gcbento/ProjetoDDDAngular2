using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.AppServices.Results
{
    public class GenericResult<TResult>
    {
        public string[] Errors { get; set; }
        public bool Success
        {
            get
            {
                if (typeof(TResult).Name == "Boolean")
                    return bool.Parse(Result.ToString()) != false;

                return Result != null;
            }
        }
        public TResult Result { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Chello.Core
{
    public static class ChelloUtil
    {
        public static string BuildQueryString(object args)
        {
            string queryString = String.Empty;
            if (args != null)
            {
                StringBuilder sb = new StringBuilder("?");
                foreach (var prop in args.GetType().GetProperties())
                {
                    sb.AppendFormat("{0}={1}&", prop.Name, prop.GetValue(args, null));
                }
                sb.Remove(sb.Length - 1, 1);
                queryString = sb.ToString();
            }
            return queryString;
        }
    }
}

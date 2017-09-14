using System;
using System.Collections.Generic;
using System.Linq;

namespace GlossaryWebUI.Helpers
{
    public class GlobalHelper
    {
        public static string FormatError(Exception ex)
        {
            string message =  "Error Message: "+ex.Message+"\nStack Trace: "+ex.StackTrace;
            return message;
        }
        public static IQueryable ConvertSortableObject<T>(IEnumerable<T> objectToSort)
        {
            var queryableX = objectToSort.AsQueryable();
            return queryableX;
        }
    }
}
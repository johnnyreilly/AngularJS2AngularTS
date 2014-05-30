using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PoorClaresAngular.UnitTests
{
    public static class JsonResultTestExtensions
    {
        public static T GetDataPropertyAs<T>(this JsonResult jsonResult, string propertyName)
        {
            var property = jsonResult.Data.GetType()
                .GetProperties()
                .FirstOrDefault(p => p.Name == propertyName);

            if (null == property)
                throw new ArgumentOutOfRangeException("propertyName", propertyName, "propertyName not found on Data");
            
            return (T)property.GetValue(jsonResult.Data, null);
        }
    }
}

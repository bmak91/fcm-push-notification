using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACB.FCMPushNotifications.Utils
{
    public class SnakeCasePropertyNameContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            for(int i = propertyName.Length - 1; i >= 0; i--)
            {
                if(i > 0 && char.IsUpper(propertyName[i]))
                {
                    propertyName = propertyName.Insert(i, "_");
                }
            }
            return propertyName.ToLower();
        }
    }
}

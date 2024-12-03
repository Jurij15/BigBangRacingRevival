using System;
using Newtonsoft.Json;
using Serilog;

namespace BBRRevival.Services.API.Models;

public class DictionaryModel
{
    public virtual string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public virtual Dictionary<string,object> ToDictionary( bool recursive = true)
    {
        Log.Debug($"Serializing {this.GetType().Name} to Dictionary");

        var dict = new Dictionary<string,object>();
        foreach (var property in this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
        {
            Log.Debug($"Serializing {property}");

            var propertyValue = property.GetValue(this, null);

            if (propertyValue is null && recursive)
            {
                Log.Debug($"Value {property} is null and will be omitted.");
                continue;
            }

            if (propertyValue is DictionaryModel nestedModel)
            {
                dict[property.Name] = nestedModel.ToDictionary();
            }
            else
            {
                dict[property.Name] = propertyValue;
            }
        }

        //TODO: log serialization results

        return dict;
    }
}
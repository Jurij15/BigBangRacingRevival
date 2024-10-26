using System;
using Newtonsoft.Json;

namespace BBRRevival.Services.API.Models;

public class JsonModel
{
    public virtual string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}
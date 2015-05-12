using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json;
using System.IO;

namespace ForestProject
{
    public class Serializer
    {
        public MemoryStream Serialize(object obj)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                BsonWriter writer = new BsonWriter(ms);
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, obj);
            }
            catch
            {
                throw;
            }
            return ms;
        }

        public T Deserialize<T>(MemoryStream ms)
        {
            T obj;
            try 
            {
                BsonReader reader = new BsonReader(ms);
                JsonSerializer serializer = new JsonSerializer();
                obj = serializer.Deserialize<T>(reader);
            }
            catch
            {
                throw;
            }
            return obj;
        }
    }
}

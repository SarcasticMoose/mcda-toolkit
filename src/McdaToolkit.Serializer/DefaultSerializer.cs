using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using LightResults;
using McdaToolkit.Serializer.Abstraction;

namespace McdaToolkit.Serializer
{
    public class DefaultSerializer : ISerializer
    {
        public Result<T> Deserialize<T>(string text)
        {
            try
            {
                XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
                using (var reader = new XmlTextReader(new StringReader(text)))
                {
                    return Result.Ok((T)xsSubmit.Deserialize(reader));
                }
                
            }
            catch (Exception ex)
            {
                return Result.Fail<T>(ex.Message);
            }
        }

        public Result<string> Serialize<T>(T obj)
        {
            try
            {
                XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
                using (var sww = new StringWriter())
                {
                    using (XmlTextWriter writer = new XmlTextWriter(sww))
                    {
                        xsSubmit.Serialize(writer, obj);
                        return Result.Ok(sww.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<string>(ex.Message);
            }
        }

        public string Extension => "xml";
    }
}
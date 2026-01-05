using System;
using LightResults;

namespace McdaToolkit.Exporters.Abstraction.FileWriter.Results.Errors
{
    public class IoError : Error
    {
        public IoError(Exception exception) : base(exception.Message)
        {
        }
    }
}
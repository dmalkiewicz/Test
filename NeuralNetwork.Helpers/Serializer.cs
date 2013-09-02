using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace NeuralNetwork.Helpers
{
    public static class Serializer
    {
        public static void SerializeObject(ISerializable objectToSerialize, string fileName)
        {
            Stream stream = File.Open(fileName, FileMode.Create);
            new BinaryFormatter().Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public static ISerializable DeserializeObject(string fileName)
        {
            Stream stream = File.Open(fileName, FileMode.Open);
            var deserializedObject = (ISerializable)new BinaryFormatter().Deserialize(stream);
            stream.Close();
            return deserializedObject;
        }

        /* TODO
        public static void SerializeToXML(ISerializable objectToSerialize, string filename)
        {
            var serializer = new XmlSerializer(objectToSerialize.GetType());
            TextWriter textWriter = new StreamWriter(filename);
            serializer.Serialize(textWriter, objectToSerialize);
            textWriter.Close();
        }

        public static ISerializable DeserializeFromXML(string filename, Type type)
        {
            var deserializer = new XmlSerializer(type);
            TextReader textReader = new StreamReader(filename);
            var deserializedObject = (ISerializable)deserializer.Deserialize(textReader);
            textReader.Close();

            return deserializedObject;
        }
        */
    }
}

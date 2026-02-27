using System.Text.Json.Nodes;

namespace Woodia.Abstractions.Consts.ConvertJson;

public class ConvertStringToJson
{
    public static JsonNode ValidateJson(string jsonٍstirng)
    {
        if (string.IsNullOrWhiteSpace(jsonٍstirng))
        {
            return null;
        }

        try
        {
            return JsonNode.Parse(jsonٍstirng);
        }
        catch 
        {
            return null;
        }
    }
}
using System.Text.Json;

namespace Retseptiraamat;

public static class FailiHaldur
{
    static string fail = Path.Combine(FileSystem.AppDataDirectory, "retseptid.json");

    public static List<Retsept> Loe()
    {
        try
        {
            if (!File.Exists(fail))
                return new List<Retsept>();

            var json = File.ReadAllText(fail);
            return JsonSerializer.Deserialize<List<Retsept>>(json) ?? new List<Retsept>();
        }
        catch
        {
            return new List<Retsept>();
        }
    }

    public static void SalvestaKõik(List<Retsept> retseptid)
    {
        try
        {
            var json = JsonSerializer.Serialize(retseptid);
            File.WriteAllText(fail, json);
        }
        catch { }
    }
}
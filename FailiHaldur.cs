using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Retseptiraamat
{
    public static class FailiHaldur
    {
        static string fail = Path.Combine(FileSystem.AppDataDirectory, "retseptid.txt");

        public static void Salvesta(string nimi, string kategooria, string pilt)
        {
            string rida = $"{nimi};{kategooria};{pilt}";
            File.AppendAllText(fail, rida + Environment.NewLine);
        }

        public static List<Retsept> Loe()
        {
            var list = new List<Retsept>();

            if (!File.Exists(fail))
                return list;

            var read = File.ReadAllLines(fail);

            foreach (var rida in read)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(rida))
                        continue;

                    var osad = rida.Split(';');

                    if (osad.Length >= 3)
                    {
                        list.Add(new Retsept
                        {
                            Nimi = osad[0],
                            Kategooria = osad[1],
                            PildiLink = osad[2]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Viga: {ex.Message}");
                }
            }

            return list;
        }

        public static void KirjutaKogu(List<Retsept> retseptid)
        {
            var read = retseptid.Select(r =>
                $"{r.Nimi};{r.Kategooria};{r.PildiLink}");

            File.WriteAllLines(fail, read);
        }
    }
}
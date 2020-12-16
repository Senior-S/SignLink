using Newtonsoft.Json;
using SignLink.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace SignLink
{
    public class Utils
    {
        public static string[] GetSignLink(int instanceID)
        {
            List<Sign> pks = GetAllSigns();
            for (int i = 0; i < pks.Count; i++)
            {
                if (pks[i].InstanceID == instanceID)
                {
                    List<string> vs = new List<string>();
                    vs.Add(pks[i].Msg);
                    vs.Add(pks[i].URL);
                    return vs.ToArray();
                }
            }
            return null;
        }

        public static void AddSignLink(int instanceId, string message, string url)
        {
            List<Sign> guns = GetAllSigns();
            for (int i = 0; i < guns.Count; i++)
            {
                if (guns[i].InstanceID == instanceId)
                {
                    return;
                }
                else
                {
                    Sign gunToAdd = new Sign
                    {
                        InstanceID = instanceId,
                        Msg = message,
                        URL = url
                    };

                    guns.Add(gunToAdd);
                    string fileJson = JsonConvert.SerializeObject(guns.ToArray(), Formatting.Indented);

                    File.WriteAllText(path, fileJson);
                }
            }

        }

        public static List<Sign> GetAllSigns()
        {
            string jsonText = File.ReadAllText(path);
            List<Sign> sad = JsonConvert.DeserializeObject<List<Sign>>(jsonText);

            return sad;
        }

        public static void CreateInitialFile()
        {
            Sign defaul = new Sign
            {
                InstanceID = 0,
                Msg = "More plugins here:",
                URL = "www.dvtserver.xyz"
            };
            List<Sign> zz = new List<Sign> { defaul };
            string asd = JsonConvert.SerializeObject(zz.ToArray(), Formatting.Indented);
            File.AppendAllText(path, asd);
        }

        internal static readonly string path = $"{Environment.CurrentDirectory}/Plugins/SignLink/Signs.json";
    }
}

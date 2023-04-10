using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GearHelper_Admin
{
    internal class Item
    {
        private int id;
        private string name;
        private string stat1;
        private int stat1amount;
        private string stat2;
        private int stat2amount;
        private string stat3;
        private int stat3amount;
        private string slot;
        private int material;

        public Item(int id, string name, string stat1, int stat1amount, string stat2, int stat2amount, string stat3, int stat3amount, string slot, int material)
        {
            this.id = id;
            this.name = name;
            this.stat1 = stat1;
            this.stat1amount = stat1amount;
            this.stat2 = stat2;
            this.stat2amount = stat2amount;
            this.stat3 = stat3;
            this.stat3amount = stat3amount;
            this.slot = slot;
            this.material = material;
        }

        [JsonProperty("id")]
        public int Id { get => id; set => id = value; }
        [JsonProperty("name")]
        public string Name { get => name; set => name = value; }
        [JsonProperty("stat1")]
        public string Stat1 { get => stat1; set => stat1 = value; }
        [JsonProperty("stat1amount")]
        public int Stat1amount { get => stat1amount; set => stat1amount = value; }
        [JsonProperty("stat2")]
        public string Stat2 { get => stat2; set => stat2 = value; }
        [JsonProperty("stat2amount")]
        public int Stat2amount { get => stat2amount; set => stat2amount = value; }
        [JsonProperty("stat3")]
        public string Stat3 { get => stat3; set => stat3 = value; }
        [JsonProperty("stat3amount")]
        public int Stat3amount { get => stat3amount; set => stat3amount = value; }
        [JsonProperty("slot")]
        public string Slot { get => slot; set => slot = value; }
        [JsonProperty("material")]
        public int Material { get => material; set => material = value; }

        public override string ToString()
        {
            String materialName;
            if (this.material == 1)
            {
                materialName = "cloth";
            }
            else if (this.material == 2)
            {
                materialName = "leather";
            }
            else
            {
                materialName = "plate";
            }
            return String.Format("{0,-3} {1,-25} {2,-3} {3,-3} {4,-3} {5,-3} {6,-3} {7,-3} {8,-8} {9}", this.id, this.name, this.stat1, this.stat1amount, this.stat2, this.stat2amount, this.stat3, this.stat3amount, this.slot, materialName);
        }
    }
}

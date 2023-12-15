using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Registries
{
    public class ItemRegistry
    {
        //TODO make this list fill dynamically and 
        public static List<string> RestorativeBerries = new List<string>() 
        { "leppaberry", "aguavberry", "enigmaberry", "figyberry", "iapapaberry", "magoberry", "sitrusberry", "wikiberry", "oranberry" };
        public static Item GetItemById(string id)
        {
            throw new NotImplementedException();
        }
    }
}

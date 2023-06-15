using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fin.Models;

namespace Fin.Models
{
    public class PackCom
    {
        public List<PACKAGE> pack { get; set; }
        public List<COMMENT>com { get; set; }
        public List<Business_User> Buser { get; set; }
        public List<User> user { get; set; }
        public List<Category> cat { get; set; }
        public List<Destination> des { get; set; }
        public List<Place> plc { get; set; }
        public List<Booking> book { get; set; }
        public List<WishList> wish { get; set; }
        public List<Payment> pay { get; set; }
        Dictionary<string, PackCom> dictionary = new Dictionary<string, PackCom>();

        

    }
}
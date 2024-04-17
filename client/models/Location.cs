using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    internal class Location
    {
        private String id;
        private String name;
        private Double latitude;
        private Double longitude;

        public Location(String id, String name, Double latitude, Double longitude)
        {
            this.id = id;
            this.name = name;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        public Double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        public Double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
		public override string ToString()
		{
			return name;
		}
	}
}

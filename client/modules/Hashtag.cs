using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace client.models
{
    internal class Hashtag
    {
        private Guid id;
        private String name;

        public Hashtag(String name)
        {
            id = Guid.NewGuid();
            this.name = name;
        }

        public Hashtag(Guid id, String name)
        {
            this.id = id;
            this.name = name;
        }

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
    }

}
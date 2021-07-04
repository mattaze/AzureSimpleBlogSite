using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mattazeblog2021_site.Models
{
    public class PostItem : TableEntity
    {
        //Partition - project category
        //RowKey - unique   date-name
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string PostBody { get; set; }
        public string Tags { get; set; }
        public DateTime PostTime { get; set; }

        public string Project { 
            get { return PartitionKey; } 
            set { PartitionKey = value; } 
        }
        public string PostName {
            get { return RowKey; }
        }

        public string getPostTimeUTC ()
        {
            return PostTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }
    }
}

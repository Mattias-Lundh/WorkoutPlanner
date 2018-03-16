using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WorkoutPlanner.Domain.API
{
    public class Client
    {
        public Client()
        {
            ApiKey = File.ReadAllText(@"../../../API/key.txt");
        }

        public string ApiKey { get; set; }
    }
}

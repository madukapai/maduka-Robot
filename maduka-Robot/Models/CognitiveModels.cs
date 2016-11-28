using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maduka_Robot.Models
{
    public class CognitiveModels
    {
        public class LUISResult
        {
            public string query { get; set; }
            public List<Intent> intents { get; set; }
            public List<Entity> entities { get; set; }

            public class Intent
            {
                public string intent { get; set; }
                public float score { get; set; }
                public List<Action> actions { get; set; }
            }

            public class Action
            {
                public bool triggered { get; set; }
                public string name { get; set; }
                public List<Parameter> parameters { get; set; }
            }

            public class Parameter
            {
                public string name { get; set; }
                public bool required { get; set; }
                public List<Value> value { get; set; }
            }

            public class Value
            {
                public string entity { get; set; }
                public string type { get; set; }
                public float score { get; set; }
            }

            public class Entity
            {
                public string entity { get; set; }
                public string type { get; set; }
                public int startIndex { get; set; }
                public int endIndex { get; set; }
                public float score { get; set; }
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swiss_Tournament
{
    public interface Observerable
    {
        void Refresh();
    }

    public class Observer
    {
        public static Observer Instance = new Observer();

        List<Observerable> obs = new List<Observerable>();

        public void Add(Observerable ob)
        {
            obs.Add(ob);
        }

        public void Refresh()
        {
            obs.ForEach(x => x.Refresh());
        }
    }
}

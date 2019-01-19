using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    static class Program
    {
        
        static void Main()
        {
          
            if (!Client.Instance.Start())
            {
                //TODO: Afficher une pop up d'erreur ? avec par exemple un bouton pour réessayer
            }
        }
    }
}

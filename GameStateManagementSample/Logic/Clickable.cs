using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameStateManagementSample.Logic
{
    abstract class Clickable
    {

        //Eine Klasse zum erben für Oberflaechenelemente die anklickbar sein sollen

        //statische Liste in der alle Elemente abgelegt werden sollen welche klickbar sind, zum durchlaufen nacher
        public static List<Clickable> clickelements = new List<Clickable>();

        Rectangle bounds;
        //protected int x;
        //protected int y;
        //protected int width;
        //protected int height;

        //activelayer sorgt dafür dass bei der überprüfung differenziert werden kann ob das objekt zur zeit überhaupt sichtbar ist, heißt wenn
        //zum beispiel wenn das optionmenü offen ist soll die unterliegende nicht reagieren dazu diese variable, sie muss im einzelfall gesetzt und abgesetzt werden
        private bool activelayer = false;

        public bool Activelayer
        {
            get { return activelayer; }
            set { activelayer = value; }
        }

        public Clickable(int x, int y, int width, int height)
        {
            //this.x = x;
            //this.y = y;
            //this.width = width;
            //this.height = height;
            bounds = new Rectangle(x, y, width, height);
            clickelements.Add(this);
        }

        public bool IsInside(int x, int y)
        {
            return bounds.Contains(new Point(x, y)) && activelayer;
        }

        //TODO in unterklasse implementieren
        abstract public void clickaction();
    }
}

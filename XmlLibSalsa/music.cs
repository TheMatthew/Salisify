using System.Collections;
using System.Collections.Generic;

namespace XmlLibSalsa
{
    public class instrument
    {
        public instrument()
        {
            notes = new Hashtable();
        }
        public string name;
        public int volume;
        public Hashtable notes;
    }

    public class note
    {
        public string name;
        public string location;
        public virtual void play(bool stop) { }
    }

    public class SDL_Note : note
    {
        public override void play(bool stop)
        {
            base.play(stop);
        }
    }

    public class beat
    {
        public beat()
        {
            sequence = new Hashtable();
        }
        public string name;
        public bool follows_clave;
        public bool overlap;
        public Hashtable sequence;
        public string image_location;
    }

    public class sequence
    {
        public sequence()
        {
            notes = new sequence_note[32];
            for (int i = 0; i < 32; i++)
            {
                notes[i] = new sequence_note();
            }
            
        }
        public string name;
        public sequence_note[] notes; 
    }

    public class sequence_note:IComparer
    {
        public int time;
        public note note_;
        public int Compare(object x, object y)
        {
            try
            {
                return ((sequence_note)x).time - ((sequence_note)y).time;
            }
            catch
            {
                return 0;
            }
            
        }
    }
    public class track
    {
        public string name;
        public int length;
        public int bpm;
        public List<List<sequence>> music;
        public track()
        {
            music = new List<List<sequence>>();

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

namespace salsa_beats
{
    #region Salsa classes
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

    public class sequence_note : IComparer
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
    #endregion
    class Salsa_Beat_Loader
    {
        static public Hashtable loadXML(string filename)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(filename);
            XmlNode root = xmldoc.DocumentElement;
            XmlNode end = root.LastChild;
            XmlNode current = root.FirstChild;
            Hashtable ret_hash = new Hashtable();
            while (null != current)
            {
                switch (current.Name)
                {
                    case "Instruments":
                        ret_hash.Add("Instruments" , fill_instruments(current.ChildNodes));
                        break;
                    case "Patterns":
                        ret_hash.Add("Patterns", fill_patterns(current.ChildNodes,(Hashtable)ret_hash["Instruments"]));
                        break;
                    case "Tracks":
                        ret_hash.Add("Tracks", fill_tracks(current.ChildNodes, (Hashtable)ret_hash["Patterns"]));
                        break;

                    default:
                        throw new Exception("Unrecognized section: " + current.Name);
                };
                current = current.NextSibling;
            }
            return ret_hash;
        }

        static Hashtable fill_tracks(XmlNodeList track_root, Hashtable beats )
        {
            Hashtable music = new Hashtable();
            XmlNode current = track_root[0];

            while (current != null)
            {
                track t = new track();
                foreach (XmlAttribute attr in current.Attributes)
                {
                    switch (attr.Name.ToLower())
                    {
                        case "name":
                            t.name = attr.Value;
                            break;
                        case "length":
                            t.length = int.Parse(attr.Value);
                            break;
                        case "beatsperminute":
                            t.bpm = int.Parse(attr.Value);
                            break;
                        default:
                            break;
                    }
                }
                for (int i = 0; i < t.length; i++)
                {
                    t.music.Add(new List<sequence>());
                }
                foreach (XmlNode subtrack in current.ChildNodes)
                {
                    int start = 0;
                    int end = 0;
                    string name = null;
                    string seq = null;
                    string instr = null;
                    foreach (XmlAttribute attr in subtrack.Attributes)
                    {
                        switch (attr.Name.ToLower())
                        {
                            case "name":
                                name = attr.Value;
                                break;
                            case "start":
                                start = int.Parse(attr.Value);
                                break;
                            case "end":
                                end = int.Parse(attr.Value);
                                break;
                            case "sequence":
                                seq = attr.Value;
                                break;
                            case "beat":
                                instr = attr.Value;
                                break;
                            default:
                                break;
                        }
                    }
                    if (seq != null && instr != null)
                    {
                        for (int i = Math.Max(start, 0); i < Math.Min(t.length, end); i++)
                        {
                            sequence s = (sequence)(((beat)beats[instr]).sequence[seq]);
                            t.music[i].Add(s);
                        }
                    }
                }

                music.Add(t.name, t);
                current = current.NextSibling;
            }
            return music;

        }
        static Hashtable fill_patterns(XmlNodeList patterns_root, Hashtable instruments)
        {
            Hashtable beats = new Hashtable(); 
            XmlNode current = patterns_root[0];
            while (current != null)
            {
                if (current.Name == "Beat")
                {
                    beat b = new beat();
                    foreach (XmlAttribute attr in current.Attributes)
                    {

                        string attribute_name = attr.Name.ToLower();
                        if (attribute_name == "name")
                        {
                            b.name = attr.Value;
                        }
                        else if (attribute_name == "img")
                        {
                            b.image_location = attr.Value;
                        }
                        else if (attribute_name == "followClave")
                        {
                            b.follows_clave = bool.Parse(attr.Value);
                        }
                        else if (attribute_name == "overlaps")
                        {
                            b.overlap = bool.Parse(attr.Value);
                        }
                    }
                    foreach (XmlNode sequence_node in current.ChildNodes)
                    {
                        sequence seq = new sequence();
                        foreach (XmlAttribute attr in sequence_node.Attributes)
                        {
                            string attribute_name = attr.Name.ToLower();
                            if (attribute_name == "name")
                            {
                                seq.name = attr.Value;
                            }
                        }
                        foreach (XmlNode sound_xml_in in sequence_node.ChildNodes)
                        {
                            sequence_note s = new sequence_note();
                            string instr = null;
                            string note = null;
                            foreach (XmlAttribute attr in sound_xml_in.Attributes)
                            {
                                string attribute_name = attr.Name.ToLower();

                                if (attribute_name == "time")
                                {
                                    s.time = (int.Parse(attr.Value));
                                }
                                else if (attribute_name == "instrument")
                                {
                                    instr = attr.Value;
                                }
                                else if (attribute_name == "note")
                                {
                                    note = attr.Value;
                                }
                                if (instr != null && note != null)
                                {
                                    s.note_ = ((note)((instrument)instruments[instr]).notes[note]);
                                }
                            }
                            seq.notes[s.time] = s;
                        }
                        b.sequence.Add(seq.name, seq);
                    }
                    beats.Add(b.name, b);
                }
                current = current.NextSibling;
            }
            return beats;
        }

        static Hashtable fill_instruments(XmlNodeList instrument_root)
        {
            Hashtable instruments = new Hashtable();
            XmlNode current = instrument_root[0];
            while (current != null)
            {
                instrument i = new instrument();
                foreach (XmlAttribute attr in current.Attributes)
                {
                    string attribute_name = attr.Name.ToLower();
                    if (attribute_name == "name")
                    {
                        i.name = attr.Value;
                    }
                    else if (attribute_name == "vol")
                    {
                        i.volume = int.Parse(attr.Value);
                    }
                }
                foreach (XmlNode note_in in current.ChildNodes)
                {
                    note n = new note();
                    foreach (XmlAttribute child_attr in note_in.Attributes)
                    {
                        string attribute_name = child_attr.Name.ToLower();
                        if (attribute_name == "name")
                        {
                            n.name = child_attr.Value;
                        }
                        else if (attribute_name == "location")
                        {
                            n.location = child_attr.Value;
                        }
                    }
                    i.notes.Add(n.name, n);
                }
                instruments.Add(i.name, i);
                current = current.NextSibling;
            }
            return instruments;
        }
    }
}

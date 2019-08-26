using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XmlLibSalsa
{
    public partial class Form1 : Form
    {
        Hashtable music;
        Hashtable beats; 
        Hashtable instruments; 
        public Form1()
        {
            InitializeComponent();
            beats = new Hashtable();
            music = new Hashtable();
            instruments = new Hashtable();
            loadXML("Beats.xml");

        }
        void loadXML(string filename)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(filename);
            XmlNode root = xmldoc.DocumentElement;
            XmlNode end = root.LastChild;
            XmlNode current = root.FirstChild;
            while (null != current)
            {
                switch (current.Name)
                {
                    case "Instruments":
                        fill_instruments(current.ChildNodes);
                        break;
                    case "Patterns":
                        fill_patterns(current.ChildNodes);
                        break; 
                    case "Tracks":
                        fill_tracks(current.ChildNodes);
                        break; 

                    default:
                        MessageBox.Show("Unrecognized section: " + current.Name);
                        break;
                };
                current = current.NextSibling;
            }
        }

        void fill_tracks(XmlNodeList track_root)
        {
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
                    string name=null;
                    string seq=null;
                    string instr=null; 
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

        }
        void fill_patterns(XmlNodeList patterns_root)
        {
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

        }

        void fill_instruments(XmlNodeList instrument_root)
        {
            
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
        }
    }
}

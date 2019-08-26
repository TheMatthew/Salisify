using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.DirectX.DirectSound;
using Sound_Stream_dx = Microsoft.DirectX.DirectSound.SecondaryBuffer;
using Sound_Stream_w32 = System.Media.SoundPlayer;
using Sound_Stream_sdl = SdlDotNet.Audio.Sound;
using Sound_List = System.Collections.ArrayList; 

namespace salsa_beats
{
    public partial class Form1 : Form
    {
        public int bpm;
        int pos = 0;
        Hashtable samples;
        Microsoft.DirectX.DirectSound.Device sound_card;
        Microsoft.DirectX.DirectSound.BufferDescription description = null;

        private void init_sdl()
        {
            SdlDotNet.Audio.Mixer.ChannelsAllocated = 1000;
        }

        private void init_dx()
        {
            try
            {
                sound_card = new Device();
                sound_card.SetCooperativeLevel(this, CooperativeLevel.Priority);
            }
            catch
            {
                MessageBox.Show("Failed to create a sound card, check your directX version\nTheSoftware will run in compatibility mode. It will NOT sound good as it can only play one sound at a time");
                return;
            }
            try
            {
                description = new BufferDescription();
                description.ControlVolume = true;
                description.LocateInSoftware = true;
            }
            catch
            {
                MessageBox.Show("Failed to create a sound buffer, check your directX version\nTheSoftware will run in compatibility mode. It will NOT sound good as it can only play one sound at a time");
                sound_card.Dispose();
                sound_card = null; 
                return;
            }
        }

        private void  init_hash()
        {
            
            samples = new Hashtable();
            DirectoryInfo []instrument_dir;
            try
            {
                DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
                bool found = false;
                for (int j = 0; (j < 3) && (found == false); j++)
                {
                    for (int i = 0; i < di.GetDirectories().Length; i++)
                    {
                        if (di.GetDirectories()[i].Name == "wav")
                        {
                            instrument_dir = di.GetDirectories()[i].GetDirectories();
                            foreach (DirectoryInfo d in instrument_dir)
                            {
                                if (sound_card != null)
                                {
                                    Sound_List temp = new Sound_List();
                                    try
                                    {
                                        foreach (FileInfo f in d.GetFiles())
                                        {
                                            if (f.Extension.ToLower() == ".wav")
                                            {

                                                temp.Add(new Sound_Stream_dx(f.FullName, description, sound_card));
                                            }
                                        }
                                    }

                                    catch (Exception e)
                                    {
                                        MessageBox.Show("error with directory " + d.Name.ToString() + "\n" + e.ToString());
                                    }
                                    samples.Add(((string)d.Name.ToLower()), temp);
                                }
                                else
                                {
                                    Sound_List temp = new Sound_List();
                                    try
                                    {
                                        foreach (FileInfo f in d.GetFiles())
                                        {
                                            if (f.Extension.ToLower() == ".wav")
                                            {
                                                FileStream fs = new FileStream(f.FullName, FileMode.Open);
                                                byte[] b_temp = new byte[fs.Length];
                                                fs.Read(b_temp, 0, (int)fs.Length);
                                                fs.Close();
                                                MemoryStream ms = new MemoryStream(b_temp);



                                                temp.Add(new Sound_Stream_w32(ms));
                                            }
                                        }
                                    }

                                    catch (Exception e)
                                    {
                                        MessageBox.Show("error with directory " + d.Name.ToString() + "\n" + e.ToString());
                                    }
                                    samples.Add(((string)d.Name.ToLower()), temp);
                                }
                            }
                            found = true;
                        }
                    }
                    di = di.Parent;
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.ToString() + e.Message.ToString());
            }
            
        }
        private void init_hash_sdl()
        {

            samples = new Hashtable();
            DirectoryInfo[] instrument_dir;
            try
            {
                DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
                bool found = false;
                for (int j = 0; (j < 3) && (found == false); j++)
                {
                    for (int i = 0; i < di.GetDirectories().Length; i++)
                    {
                        if (di.GetDirectories()[i].Name == "ogg")
                        {
                            instrument_dir = di.GetDirectories()[i].GetDirectories();
                            foreach (DirectoryInfo d in instrument_dir)
                            {
                                Sound_List temp = new Sound_List();
                                    try
                                    {
                                        foreach (FileInfo f in d.GetFiles())
                                        {
                                            if (f.Extension.ToLower() == ".ogg")
                                            {
                                                Sound_Stream_sdl sound = new SdlDotNet.Audio.Sound(f.FullName);
                                                if (sound == null)
                                                {
                                                    throw (new Exception("Sound " + f.FullName.ToString() + " not working"));
                                                }
                                                temp.Add(sound);
                                            }
                                        }
                                    }

                                    catch (Exception e)
                                    {
                                        MessageBox.Show("error with directory " + d.Name.ToString() + "\n" + e.ToString());
                                    }
                                    samples.Add(((string)d.Name.ToLower()), temp);
                             }
                            found = true;
                         }
                        
                      
                    }
                    di = di.Parent;
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.ToString() + e.Message.ToString());
            }
        }

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000 * 60 / 4 / int.Parse(textBox1.Text);
            
            progressBar1.Maximum = 7;
            progressBar1.Minimum = 0;
            try
            {
                init_sdl();
                init_hash_sdl();
            }
            catch( Exception e)

            {
                MessageBox.Show( "Could not initialize SDL, reverting to DirectX." );
                MessageBox.Show(e.Data.ToString() + e.Message.ToString() + e.StackTrace.ToString());
                init_dx();
                init_hash();
            }
            ClaveSelect.SelectedIndex = 1;
            timer1_Tick(this, null);
            checkbox_CheckedChanged(this, null);
            timer1.Start();
            
            
        }

        private void play(SecondaryBuffer input, int volume)
        {
            if (input != null)
            {
                input.Stop();
                input.SetCurrentPosition(0);
                input.Volume = volume;
                input.Play(0, BufferPlayFlags.Default);
            }
        }

        private void play(System.Media.SoundPlayer input)
        {
            if (input != null)
            {
                input.Stop();
                input.Stream.Seek(0, SeekOrigin.Begin);
                input.Play();
            }
        }

        private void play(SdlDotNet.Audio.Sound input, int volume)
        {
            if (input != null)
            {
                input.NumberOfChannels = 1000;
                input.Volume = volume;
                input.Play();
                
            }
        }

        private int volume_to_db(int input)
        {
            double db_vol = input/255.0 ;
            db_vol = Math.Log10(db_vol)*2000;
            db_vol = Math.Max(db_vol, -10000);
            return (int)db_vol; 
        }


        private void play_sound(object input, int volume)
        {
            Type t =input.GetType(); 
            Type SDL = typeof(Sound_Stream_sdl);
            Type DX = typeof(Sound_Stream_dx);
            Type W32 = typeof(Sound_Stream_w32);
            if( t == SDL )
            {
                play((Sound_Stream_sdl)input, volume);
            }
            else if ( t == DX ) 
            {
                play((Sound_Stream_dx)input, volume_to_db(volume));
            }
            else if( t == W32 )
            {
                    play((Sound_Stream_w32)input);
            }
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bpm = int.Parse(textBox1.Text);
                timer1.Interval = 1000 * 60/4 / (bpm );
            }
            catch { }
        }

        private void cowbell(int pos, bool en, int volume)
        {
            //1.2.3.4.5.6.7.8.
            //+.*.+++*.++*+.+*
            if (en)
            {
                Sound_List snd_list = (Sound_List)samples["cowbell"];
                object lo = snd_list[1];
                object hi = snd_list[0];

                switch (pos)
                {
                    case 4:
                    case 14:
                    case 22:
                    case 30:
                        play_sound(hi, volume);
                    break;
                    case 0:
                    case 8:
                    case 10:
                    case 12:
                    case 18:
                    case 20:
                    case 24:
                    case 28:
                        play_sound(lo,volume);
                    break;

                }
            }
        }

        private void timbales(int pos, bool en, int volume)
        { 
            //1.2.3.4.5.6.7.8.
            //*.*.**.**.**.*.*
            if (en)
            {
                Sound_List snd_list = (Sound_List)samples["timbale"];
                object snd = snd_list[0];
               
                switch (pos)
                {
                    case 0:
                    case 4:
                    case 8:
                    case 10:
                    case 14:
                    case 16:
                    case 20:
                    case 22:
                    case 26:
                    case 30:
                        play_sound(snd, volume);
                    break;
                }
            }
        }

        private void claves(int pos, bool en, int volume)
        {
            //1.2.3.4.5.6.7.8.
            //*..*..*...*.*...
            if (en)
            {
                Sound_List snd_list = (Sound_List)samples["clave"];
                object snd = snd_list[0];
            
                switch (ClaveSelect.SelectedIndex )
                {
                    case 0:
                        switch (pos)
                        {
                            case 0:
                            case 6:
                            case 12:
                            case 20:
                            case 24:
                                play_sound(snd,volume);
                                break;
                        }
                        break;
                    case 1:
                        switch (pos)
                        {
                            case 16:
                            case 22:
                            case 28:
                            case 4:
                            case 8:
                                play_sound(snd,volume);
                                break;
                        }
                        break;
                }
            }
        }

        private void bongo(int pos, bool en, int volume)
        {
            //1.2.3.4.5.6.7.8.
            //+.*.+.**+.**+.**
            if (en)
            {
                Sound_List snd_list = (Sound_List)samples["bongo"];
                object lo = snd_list[0];
                object hi = snd_list[1];

            
                switch (pos)
                {
                    case 0:
                    case 8:
                    case 16:
                    case 24:
                        play_sound(lo, volume);
                        break;
                    case 4:
                    case 12:
                    case 14:
                    case 20:
                    case 22: 
                    case 28:
                    case 30:
                        play_sound(hi, volume);
                        break;
                }
            }
        }

        private void guiro(int pos, bool en, int volume)
        {
            //1.2.3.4.5.6.7.8.
            //+.*.+.**+.**+.**
            if (en)
            {
                Sound_List snd_list = (Sound_List)samples["guiro"];
                object lo = snd_list[0];
                object hi = snd_list[1];
                switch (pos)
                {
                    case 0:
                    case 4:
                    case 8:
                    case 12:
                    case 16:
                    case 20:
                    case 24:
                    case 28:
                        play_sound(lo, volume);
                        break;
                    case 2:
                    case 6:
                    case 10:
                    case 14:
                    case 18:
                    case 22:
                    case 26:
                    case 30:
                        play_sound(hi, volume);
                        break;
                }
            }
        }

        private void maracas(int pos, bool en, int volume)
        {
            //1.2.3.4.5.6.7.8.
            //+.*.+.**+.**+.**
            if (en)
            {
                Sound_List snd_list = (Sound_List)samples["maracas"];
                object snd = snd_list[0];


                switch (pos)
                {
                    case 1:
                    case 5:
                    case 9:
                    case 13:
                    case 17:
                    case 21:
                    case 25:
                    case 29:
                        play_sound(snd, volume);
                        break;
                }
            }
        }

        private bool cb ; 
        private bool tb ;
        private bool cl ;
        private bool bo ;
        private bool gu ;
        private bool ma ; 

        private void init_instruments()
        {
            cb = CowbellBox.Checked; 
            tb = TimbaleBox.Checked;
            cl = ClaveBox.Checked;
            bo = BongoBox.Checked;
            gu = GuiroBox.Checked;
            ma = MaracasBox.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (pos == 0)
            {
                init_instruments();
            }
            cowbell(pos, cb, CowbellVol.Value);
            timbales(pos, tb, TimbaleVol.Value);
            claves(pos, cl, ClaveVol.Value);
            bongo(pos, bo, BongoVol.Value);
            guiro( pos, gu, GuiroVol.Value);
            maracas(pos, ma, MaracasVol.Value);
            pos++;
            if (pos == 32)
            {
                pos = 0;
                
            }
            progressBar1.Value = pos / 4; 
        }

        private void checkbox_CheckedChanged(object sender, EventArgs e)
        {
            ClaveSelect.Enabled = ClaveVol.Enabled = ClaveBox.Checked;
            GuiroVol.Enabled = GuiroBox.Checked;
            TimbaleVol.Enabled = TimbaleBox.Checked;
            BongoVol.Enabled = BongoBox.Checked;
            CowbellVol.Enabled = CowbellBox.Checked;
            MaracasVol.Enabled = MaracasBox.Checked;
        }
    }
}

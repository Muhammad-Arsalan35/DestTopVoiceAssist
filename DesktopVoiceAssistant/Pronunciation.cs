using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace DesktopVoiceAssistant
{
    public partial class Pronunciation : Form
    {
        SpeechSynthesizer sp = new SpeechSynthesizer();
        public Pronunciation()
        {
            InitializeComponent();
            sp.SetOutputToDefaultAudioDevice();
            sp.SelectVoiceByHints(VoiceGender.Neutral);
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            sp.Speak(txtWord.text);

        }

        private void Pronunciation_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex==0)
            {
                sp.SelectVoiceByHints(VoiceGender.Male);
            }
            else if (this.comboBox1.SelectedIndex == 1)
            {
                sp.SelectVoiceByHints(VoiceGender.Female);
            }
          
        }
    }
}

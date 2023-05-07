using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace DesktopVoiceAssistant
{
    public partial class Form1 : Form
    {

        SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer(); //for text - specch
        SpeechRecognitionEngine recognitionEngine = new SpeechRecognitionEngine(); // recognize the speech
        bool start = false;
        string[] commands;
        string[] process;
        public Form1()
        {


            InitializeComponent();
        }

        public void InitializaAssistent()
        {
            commands = File.ReadAllLines("commands.txt");
            process = File.ReadAllLines("process.txt");

            Choices choices = new Choices();
            // choices.Add(new string[] { "open chrome", "open paint", "open C drive", "open D drive", });
            choices.Add(commands);
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(choices);

            Grammar grammar = new Grammar(grammarBuilder);

            recognitionEngine.LoadGrammarAsync(grammar);
            recognitionEngine.SetInputToDefaultAudioDevice();
            recognitionEngine.SpeechRecognized += RecognitionEngine_SpeechRecognized;


            
            speechSynthesizer.SelectVoiceByHints(VoiceGender.Female);
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            start_stop();
        }

        public void start_stop()
        {
            if (start)
            {
                stopAssistant();
            }
            else
            {
                startAssistant();
            }
        }

        public void stopAssistant()
        {
            start = false;
            pictureBox1.Image = DesktopVoiceAssistant.Properties.Resources.Untitled_2_copy;
            bunifuFlatButton2.Text = "START";
            recognitionEngine.RecognizeAsyncStop();
            recognitionEngine.UnloadAllGrammars();
            
            
        }

        public void startAssistant()
        {
            start = true;
            pictureBox1.Image = DesktopVoiceAssistant.Properties.Resources.animated_microphone_image_0040;
            bunifuFlatButton2.Text = "STOP";
            InitializaAssistent();
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
            speechSynthesizer.Speak("Started");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string temp="";
            if (e.Result.Grammar.Name != "Random")
            {
                speechSynthesizer.Speak("Your command is "+e.Result.Text.ToLower());

                for (int i = 0; i < commands.Length; i++)
                {
                    if (temp != e.Result.Text.ToLower())
                    {
                        if (e.Result.Text.ToLower() == commands[i].ToLower())
                        {

                            Process.Start(process[i]);
                            break;


                        }
                    }
                    else
                    {
                        temp = e.Result.Text.ToLower();
                    }



                }
            }
            






            // switch (e.Result.Text)
            // {
            //     case "open chrome":
            //         speechSynthesizer.Speak("opening chrome"); // speak out as well
            //         Process.Start("chrome.exe");
            //         break;

            //     case "open paint":
            //         speechSynthesizer.Speak("opening paint"); // speak out as well
            //         Process.Start("mspaint.exe");
            //         break;

            //     case "open C drive":
            //         speechSynthesizer.Speak("opening C"); // speak out as well
            //         Process.Start("C:/");
            //         break;

            //     case "open D drive":
            //         speechSynthesizer.Speak("opening D"); // speak out as well
            //         Process.Start("D:/");
            //         break;

            //     default:
            //         speechSynthesizer.Speak("Hi");
            //         break;


            // }

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            stopAssistant();
            AddCommandForm acf = new AddCommandForm();
            acf.ShowDialog();
        }

      

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            viewCommands _view = new viewCommands();
            _view.ShowDialog();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            Pronunciation pr = new Pronunciation();
            pr.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text=="Mute")
            {
                button1.Text = "Unmute";
                speechSynthesizer.SetOutputToNull();
                button1.ForeColor = Color.Red;

            }
            else
            {
                button1.Text = "Mute";
                button1.ForeColor = Color.Black;
                speechSynthesizer.SetOutputToDefaultAudioDevice();
            }
        }
    }
}

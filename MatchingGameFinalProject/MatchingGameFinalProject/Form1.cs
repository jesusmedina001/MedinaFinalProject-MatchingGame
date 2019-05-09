using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#region Project Solution
namespace MatchingGameFinalProject
{
    #region Initialization
    public partial class Form1 : Form
    {

        bool allowClick = false;
        PictureBox firstGuess;
        Random rnd = new Random();
        Timer clickTimer = new Timer();
        int time = 00;
        Timer timer = new Timer { Interval = 1000 };
        int counter = 0;

        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region PictureBoxes
        private PictureBox[] pictureBoxes
        {
            get { return Controls.OfType<PictureBox>().ToArray(); }
        }
        private static IEnumerable<Image> images
        {
            get
            {
                return new Image[]
                {
                    Properties.Resources.City_Image_Dubai,
                    Properties.Resources.City_Image_Hong_Kong,
                    Properties.Resources.City_Image_London,
                    Properties.Resources.City_Image_Rio,
                    Properties.Resources.City_Image_Shanghai,
                    Properties.Resources.City_Image_Paris,
                    Properties.Resources.City_Image_Madrid,
                    Properties.Resources.City_Image_Tokyo,
                    Properties.Resources.City_Image_Hartford,
                };
            }
        }
        #endregion

        #region StartGameTimer
        private void StartGameTimer()
        {
            timer.Start();
            timer.Tick += delegate
            {
                time++;
                if (time > 1000)
                {
                    timer.Stop();
                    MessageBox.Show("Out of time");
                    ResetImages();
                }
                var ssTime = TimeSpan.FromSeconds(time).TotalSeconds;

                label1.Text = "Time: " + time.ToString();
                
            };
        }
        #endregion

        #region ResetImages
        private void ResetImages()
        {
            foreach (var pic in pictureBoxes)
            {
                pic.Tag = null;
                pic.Visible = true;
            }

            HideImages();
            SetRandomImages();
            time = 60;
            timer.Start();
        }
        #endregion

        #region HideImages
        private void HideImages()
        {
            foreach (var pic in pictureBoxes)
            {
                pic.Image = Properties.Resources.City_Image_UCONN;
            }
        }
        #endregion

        #region GetFreeSlot
        private PictureBox GetFreeSlot()
        {
            int num;

            do
            {
                num = rnd.Next(0, pictureBoxes.Count());
            }
            while (pictureBoxes[num].Tag != null);
            return pictureBoxes[num];
        }
        #endregion

        #region SetRandomImage
        private void SetRandomImages()
        {
            foreach (var image in images)
            {
                GetFreeSlot().Tag = image;
                GetFreeSlot().Tag = image;
            }
        }
        #endregion

        #region ClickTimerTick
        private void CLICKTIMER_TICK(object sender, EventArgs e)
        {
            HideImages();

            allowClick = true;
            clickTimer.Stop();
        }
        #endregion

        #region ClickImage
        private void ClickImage(object sender, EventArgs e)
        {
            counter++;
            label3.Text = "Number of Clicks: " + counter.ToString();

            if (!allowClick) return;

            var pic = (PictureBox)sender;

            if (firstGuess == null)
            {
                firstGuess = pic;
                pic.Image = (Image)pic.Tag;
                return;
            }

            pic.Image = (Image)pic.Tag;

            if (pic.Image == firstGuess.Image && pic != firstGuess)
            {
                pic.Visible = firstGuess.Visible = false;
                {
                    firstGuess = pic;
                }
                HideImages();
            }
            else
            {
                allowClick = false;
                clickTimer.Start();
            }

            firstGuess = null;
            if (pictureBoxes.Any(p => p.Visible)) return;
            MessageBox.Show("Congratulations, you won in " + TimeSpan.FromSeconds(time).TotalSeconds + " Seconds and " + counter.ToString() + " Clicks" );
            ResetImages();
        }
        #endregion

        #region StartGame
        private void startGame(object sender, EventArgs e)
        {
            allowClick = true;
            SetRandomImages();
            HideImages();
            StartGameTimer();
            clickTimer.Interval = 1000;
            clickTimer.Tick += CLICKTIMER_TICK;
            button1.Enabled = false;
        }
        #endregion

        #region ClickerCounter
        private void button3_Click(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
#endregion
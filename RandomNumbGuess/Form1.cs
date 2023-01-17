using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomNumbGuess
{
    public partial class Form1 : Form
    {

        private int secretnum = RandomNumber.Between(0, 100);
        private int numberguesses = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void winmsg()
        {
            highlowLB.Text = "YOU WIN!!!";
            MessageBox.Show($"YOU WON!! After {numberguesses} guesses, you guessed {secretnum}!!");
        }

        private int Turnintoint(string inputstring)
        {
            if (int.TryParse(inputstring, out var i) && int.Parse(inputstring) > -1)
            {
                return int.Parse(inputstring);
            }
            return -1;
        }


        private void numberBUT_Click(object sender, EventArgs e)
        {
            if (numberBUT.Text == "Reset")
            {
                secretnum = RandomNumber.Between(0, 100);
                numberBUT.Text = "Reveal";
            }
            else if (numberBUT.Text == "Reveal")
            {
                numberBUT.Text = "Reset";
                highlowLB.Text = "Higher or Lower";
                winmsg();
            }
        }

        private void guessBUT_Click(object sender, EventArgs e)
        {
            int guessnum = Turnintoint(inputTB.Text);
            if (guessnum > -1)
            {
                numberguesses++;
                if (guessnum == secretnum)
                {
                    winmsg();
                }
                else if (guessnum < secretnum)
                {
                    highlowLB.Text = "You need to guess higher";
                }
                else if (guessnum > secretnum)
                {
                    highlowLB.Text = "You need to guess lower";
                }
            }
        }
    }

    public static class RandomNumber
    {
        private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();
        public static int Between(int minimumValue, int maximumValue)
        {
            byte[] randomNumbers = new byte[1];
            _generator.GetBytes(randomNumbers);
            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumbers[0]);
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);
            int range = (maximumValue - 1) - minimumValue;
            double randomValueInRange = Math.Floor(multiplier * range);
            return (int)(minimumValue + randomValueInRange);
        }
    }
}

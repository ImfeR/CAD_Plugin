using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RocketPlugin.UI
{
    using RocketPlugin.BL;

    using System.Windows.Forms;
                          
    public partial class MainForm : Form
    {
        private RocketParameters _parameters;

        public MainForm()
        {
            InitializeComponent();

            _parameters = new RocketParameters();

            InitState();
        }

        private void InitState()
        {
            BodyLengthTextBox.Text = _parameters.BodyLength.ToString();
            BodyDiameterTextBox.Text = _parameters.BodyDiameter.ToString();
            NoseLengthTextBox.Text = _parameters.NoseLength.ToString();

            WingsCountComboBox.SelectedItem = _parameters.WingsCount.ToString();
            WingsLengthTextBox.Text = _parameters.WingsLength.ToString();
            WingsWidthTextBox.Text = _parameters.WingsWidth.ToString();

            GuidingsCountComboBox.SelectedItem = _parameters.GuidesCount.ToString();
            GuidingsInnderRibLengthTextBox.Text = _parameters.GuidesInnerRibLength.ToString();
        }

        private void BodyLengthTextBox_Leave(object sender, EventArgs e)
        {
            if(!double.TryParse(BodyLengthTextBox.Text, out var value))
                BodyLengthErrorIcon.Visible = true;

            _parameters.BodyLength = value;
        }

        private void BodyDiameterTextBox_Leave(object sender, EventArgs e)
        {
            if(!double.TryParse(BodyDiameterTextBox.Text, out var value))
                BodyWidthErrorIcon.Visible = true;

            _parameters.BodyDiameter = value;
        }

        private void NoseLengthTextBox_Leave(object sender, EventArgs e)
        {
            if(!double.TryParse(NoseLengthTextBox.Text, out var value))
                NoseLengthErrorIcon.Visible = true;

            _parameters.NoseLength = value;
        }

        private void WingsLengthTextBox_Leave(object sender, EventArgs e)
        {
            if (!double.TryParse(WingsLengthTextBox.Text, out var value))
                WingsLengthErrorIcon.Visible = true;

            _parameters.WingsLength = value;
        }

        private void WingsWidthTextBox_Leave(object sender, EventArgs e)
        {
            if (!double.TryParse(WingsWidthTextBox.Text, out var value))
                WingsWidthErrorIcon.Visible = true;

            _parameters.WingsWidth = value;
        }

        private void GuidingsInnderRibLengthTextBox_Leave(object sender, EventArgs e)
        {
            if (!double.TryParse(GuidingsInnderRibLengthTextBox.Text, out var value))
                GuidingsInnderRibLengthErrorIcon.Visible = true;

            _parameters.WingsWidth = value;
        }

        private void GuidingsCountComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            int.TryParse(GuidingsCountComboBox.SelectedItem.ToString(), out var value);

            _parameters.GuidesCount = value;
        }

        private void WingsCountComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            int.TryParse(WingsCountComboBox.SelectedItem.ToString(), out var value);

            _parameters.WingsCount = value;
        }
    }
}
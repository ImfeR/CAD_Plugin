namespace RocketPlugin.UI
{
    using RocketPlugin.BL;

    using System;
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

            ChangeMinMaxLabels(nameof(RocketParameters.BodyDiameter));
            ChangeMinMaxLabels(nameof(RocketParameters.BodyLength));
        }

        private void BodyLengthTextBox_Leave(object sender, EventArgs e)
        {
            if(!double.TryParse(BodyLengthTextBox.Text, out var value))
            {
                BodyLengthErrorIcon.Visible = true;
                return;
            }

            try
            {
                _parameters.BodyLength = value;
                BodyLengthErrorIcon.Visible = false;
            }
            catch
            {
                BodyLengthErrorIcon.Visible = true;
            }

            ChangeMinMaxLabels(nameof(RocketParameters.BodyLength));
        }

        private void BodyDiameterTextBox_Leave(object sender, EventArgs e)
        {
            if(!double.TryParse(BodyDiameterTextBox.Text, out var value))
            {
                BodyWidthErrorIcon.Visible = true;
                return;
            }

            try
            {
                _parameters.BodyDiameter = value;
                BodyWidthErrorIcon.Visible = false;
            }
            catch
            {
                BodyWidthErrorIcon.Visible = true;
            }
        }

        private void NoseLengthTextBox_Leave(object sender, EventArgs e)
        {
            if(!double.TryParse(NoseLengthTextBox.Text, out var value))
            {
                NoseLengthErrorIcon.Visible = true;
                return;
            }

            try
            {
                _parameters.NoseLength = value;
                NoseLengthErrorIcon.Visible = false;
            }
            catch
            {
                NoseLengthErrorIcon.Visible = true;
            }
        }

        private void WingsLengthTextBox_Leave(object sender, EventArgs e)
        {
            if (!double.TryParse(WingsLengthTextBox.Text, out var value))
            {
                WingsLengthErrorIcon.Visible = true;
                return;
            }

            try
            {
                _parameters.WingsLength = value;
                WingsLengthErrorIcon.Visible = false;
            }
            catch
            {
                WingsLengthErrorIcon.Visible = true;
            }
        }

        private void WingsWidthTextBox_Leave(object sender, EventArgs e)
        {
            if (!double.TryParse(WingsWidthTextBox.Text, out var value))
            {
                WingsWidthErrorIcon.Visible = true;
                return;
            }

            try
            {
                _parameters.WingsWidth = value;
                WingsWidthErrorIcon.Visible = false;
            }
            catch
            {
                WingsWidthErrorIcon.Visible = true;
            }
        }

        private void GuidingsInnderRibLengthTextBox_Leave(object sender, EventArgs e)
        {
            if (!double.TryParse(GuidingsInnderRibLengthTextBox.Text, out var value))
            {
                GuidingsInnderRibLengthErrorIcon.Visible = true;
                return;
            }

            try
            {
                _parameters.WingsWidth = value;
                GuidingsInnderRibLengthErrorIcon.Visible = false;
            }
            catch
            {
                GuidingsInnderRibLengthErrorIcon.Visible = true;
            }
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

        private void ChangeMinMaxLabels(string changedParameterName)
        {
            switch (changedParameterName)
            {
                case nameof(RocketParameters.BodyLength):
                    MinNoseLength.Text = (_parameters.BodyLength * 
                        _parameters.MIN_NOSE_LENGTH_MULTIPLIER).ToString();
                    MaxNoseLength.Text = (_parameters.BodyLength * 
                        _parameters.MAX_NOSE_LENGTH_MULTIPLIER).ToString();

                    MinWingsLength.Text = (_parameters.BodyLength * 
                        _parameters.MIN_WING_LENGTH_MULTIPLIER).ToString();
                    MaxWingsLength.Text = (_parameters.BodyLength * 
                        _parameters.MAX_WING_LENGTH_MULTIPLIER).ToString();

                    MinGuidesInnerRibLength.Text = (_parameters.BodyLength * 
                        _parameters.MIN_GUIDES_INNER_RIB_LENGTH_MULTIPLIER).ToString();
                    MaxGuidesInnerRibLength.Text = (_parameters.BodyLength * 
                        _parameters.MAX_GUIDES_INNER_RIB_LENGTH_MULTIPLIER).ToString();

                    MinBodyDiameter.Text = _parameters.BodyLength * 
                        _parameters.MIN_BODY_DIAMTER_MULTIPLIER > _parameters.MIN_BODY_DIAMTER ? 
                        (_parameters.BodyLength * _parameters.MIN_BODY_DIAMTER_MULTIPLIER).ToString() :
                        _parameters.MIN_BODY_DIAMTER.ToString();
                    MaxBodyDiameter.Text = _parameters.BodyLength * 
                        _parameters.MAX_BODY_DIAMTER_MULTIPLIER < _parameters.MAX_BODY_DIAMTER ? 
                        (_parameters.BodyLength * _parameters.MAX_BODY_DIAMTER_MULTIPLIER).ToString() :
                        _parameters.MAX_BODY_DIAMTER.ToString();
                    break;
                case nameof(RocketParameters.BodyDiameter):
                    MaxWingsWidth.Text = (_parameters.BodyDiameter * 
                        _parameters.MAX_WING_WIDTH_MULTIPLIER).ToString();
                    MinWingsWidth.Text = (_parameters.BodyDiameter * 
                        _parameters.MIN_WING_WIDTH_MULTIPLIER).ToString();
                    break;
            }
        }
    }
}
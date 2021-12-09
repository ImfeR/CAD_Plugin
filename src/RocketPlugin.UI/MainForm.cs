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
            GuidingsInnerRibLengthTextBox.Text = _parameters.GuidesInnerRibLength.ToString();

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

            OnChangeBodyLengthParameter();
        }

        private void BodyDiameterTextBox_Leave(object sender, EventArgs e)
        {
            if(!double.TryParse(BodyDiameterTextBox.Text, out var value))
            {
                BodyDiameterErrorIcon.Visible = true;
                return;
            }

            try
            {
                _parameters.BodyDiameter = value;
                BodyDiameterErrorIcon.Visible = false;
            }
            catch
            {
                BodyDiameterErrorIcon.Visible = true;
            }

            OnChangeBodyWidthParameter();
        }

        private void NoseLengthTextBox_Leave(object sender, EventArgs e)
        {
            CheckParameterValuesValidity();
        }

        private void WingsLengthTextBox_Leave(object sender, EventArgs e)
        {
            CheckParameterValuesValidity();
        }

        private void WingsWidthTextBox_Leave(object sender, EventArgs e)
        {
            CheckParameterValuesValidity();
        }

        private void GuidingsInnderRibLengthTextBox_Leave(object sender, EventArgs e)
        {
            CheckParameterValuesValidity();
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

                    MinBodyDiameter.Text = (_parameters.BodyLength *
                        _parameters.MIN_BODY_DIAMTER_MULTIPLIER).ToString();
                    MaxBodyDiameter.Text = (_parameters.BodyLength * 
                        _parameters.MAX_BODY_DIAMTER_MULTIPLIER).ToString();
                    break;
                case nameof(RocketParameters.BodyDiameter):
                    MaxWingsWidth.Text = (_parameters.BodyDiameter * 
                        _parameters.MAX_WING_WIDTH_MULTIPLIER).ToString();
                    MinWingsWidth.Text = (_parameters.BodyDiameter * 
                        _parameters.MIN_WING_WIDTH_MULTIPLIER).ToString();
                    break;
            }
        }

        private void OnChangeBodyLengthParameter()
        {
            ChangeMinMaxLabels(nameof(RocketParameters.BodyLength));
            CheckParameterValuesValidity();
        }

        private void OnChangeBodyWidthParameter()
        {
            ChangeMinMaxLabels(nameof(RocketParameters.BodyDiameter));
            CheckParameterValuesValidity();
        }

        private void CheckParameterValuesValidity()
        {
            BuildButton.Enabled = true;

            try
            {
                _parameters.BodyLength = double.Parse(BodyLengthTextBox.Text);
                BodyLengthErrorIcon.Visible = false;
            }
            catch 
            {
                BodyLengthErrorIcon.Visible = true;
                BuildButton.Enabled = false;
            }

            try
            {
                _parameters.BodyDiameter = double.Parse(BodyDiameterTextBox.Text);
                BodyDiameterErrorIcon.Visible = false;
            }
            catch
            {
                BodyDiameterErrorIcon.Visible = true;
                BuildButton.Enabled = false;
            }

            try
            {
                _parameters.NoseLength = double.Parse(NoseLengthTextBox.Text);
                NoseLengthErrorIcon.Visible = false;
            }
            catch
            {
                NoseLengthErrorIcon.Visible = true;
                BuildButton.Enabled = false;
            }

            try
            {
                _parameters.WingsLength = double.Parse(WingsLengthTextBox.Text);
                WingsLengthErrorIcon.Visible = false;
            }
            catch
            {
                WingsLengthErrorIcon.Visible = true;
                BuildButton.Enabled = false;
            }

            try
            {
                _parameters.WingsWidth = double.Parse(WingsWidthTextBox.Text);
                WingsWidthErrorIcon.Visible = false;
            }
            catch
            {
                WingsWidthErrorIcon.Visible = true;
                BuildButton.Enabled = false;
            }

            try
            {
                _parameters.GuidesInnerRibLength = double.Parse(GuidingsInnerRibLengthTextBox.Text);
                GuidingsInnderRibLengthErrorIcon.Visible = false;
            }
            catch
            {
                GuidingsInnderRibLengthErrorIcon.Visible = true;
                BuildButton.Enabled = false;
            }
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            CheckParameterValuesValidity();

            if (!BuildButton.Enabled)
                return;
        }
    }
}
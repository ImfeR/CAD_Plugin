namespace RocketPlugin.UI
{
    using BL;

    using Builder;

    using System;
    using System.Windows.Forms;

     //TODO: XML
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
            //try
            //{
            //    _parameters.BodyLength = double.Parse(BodyLengthTextBox.Text);
            //}
            //catch (Exception exeption)
            //{
            //    errorProvider.SetError(BodyLengthTextBox, exeption.Message);
            //}

            OnChangeBodyLengthParameter();
        }

        private void BodyDiameterTextBox_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    _parameters.BodyDiameter = double.Parse(BodyDiameterTextBox.Text);
            //}
            //catch (Exception exeption)
            //{
            //    errorProvider.SetError(BodyDiameterTextBox, exeption.Message);
            //}

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

        /// <summary>
        /// Изменение отображаемых значений max/min у зависимых параметров
        /// при изменеии основного параметра
        /// </summary>
        /// <param name="changedParameterName">Название измененного параметра.</param>
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

        //TODO: Дубли
        private void OnChangeBodyLengthParameter()
        {
            CheckParameterValuesValidity();

            if(errorProvider.GetError(BodyLengthTextBox) == string.Empty)
            {
                ChangeMinMaxLabels(nameof(RocketParameters.BodyLength));
            }
        }

        private void OnChangeBodyWidthParameter()
        {
            CheckParameterValuesValidity();

            if (errorProvider.GetError(BodyDiameterTextBox) == string.Empty)
            {
                ChangeMinMaxLabels(nameof(RocketParameters.BodyDiameter));
            }
        }

        /// <summary>
        /// Проверка всех значений на валидносить.
        /// </summary>
        private void CheckParameterValuesValidity()
        {
            BuildButton.Enabled = true;
            errorProvider.Clear();

            //TODO: Дубли
            try
            {
                _parameters.BodyLength = double.Parse(BodyLengthTextBox.Text);
            }
            catch (Exception e)
            {
                errorProvider.SetError(BodyLengthTextBox, e.Message);
                BuildButton.Enabled = false;
            }

            try
            {
                _parameters.BodyDiameter = double.Parse(BodyDiameterTextBox.Text);
            }
            catch (Exception e)
            {
                errorProvider.SetError(BodyDiameterTextBox, e.Message);
                BuildButton.Enabled = false;
            }

            try
            {
                _parameters.NoseLength = double.Parse(NoseLengthTextBox.Text);
            }
            catch (Exception e)
            {
                errorProvider.SetError(NoseLengthTextBox, e.Message);
                BuildButton.Enabled = false;
            }

            try
            {
                _parameters.WingsLength = double.Parse(WingsLengthTextBox.Text);
            }
            catch (Exception e)
            {
                errorProvider.SetError(WingsLengthTextBox, e.Message);
                BuildButton.Enabled = false;
            }

            try
            {
                _parameters.WingsWidth = double.Parse(WingsWidthTextBox.Text);
            }
            catch (Exception e)
            {
                errorProvider.SetError(WingsWidthTextBox, e.Message);
                BuildButton.Enabled = false;
            }

            try
            {
                _parameters.GuidesInnerRibLength = double.Parse(GuidingsInnerRibLengthTextBox.Text);
            }
            catch (Exception e)
            {
                errorProvider.SetError(GuidingsInnerRibLengthTextBox, e.Message);
                BuildButton.Enabled = false;
            }
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            CheckParameterValuesValidity();

            if (!BuildButton.Enabled)
                return;

            RocketBuilder builder = new RocketBuilder(_parameters);
            builder.Build();
        }
    }
}
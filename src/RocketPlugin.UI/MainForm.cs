namespace RocketPlugin.UI
{
    using BL;

    using Builder;

    using System;
    using System.Windows.Forms;
    using System.Collections.Generic;

     //TODO: XML

    /// <summary>
    /// Главная форма для создания модели.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Fields

        /// <summary>
        /// Параметры модели.
        /// </summary>
        private RocketParameters _parameters;

        /// <summary>
        /// Словарь с TextBox и соответствующими им именами параметрамов.
        /// </summary>
        private readonly Dictionary<TextBox, string> _textoBoxDictionary;

        /// <summary>
        /// Словарь с ComboBox и соответствующими им именами параметрамов.
        /// </summary>
        private readonly Dictionary<ComboBox, string> _comboBoxDictionary;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _parameters = new RocketParameters();

            _textoBoxDictionary = new Dictionary<TextBox, string>()
            {
                { BodyLengthTextBox, nameof(RocketParameters.BodyLength) },
                { BodyDiameterTextBox, nameof(RocketParameters.BodyDiameter) },
                { NoseLengthTextBox, nameof(RocketParameters.NoseLength) },
                { WingsLengthTextBox, nameof(RocketParameters.WingsLength) },
                { WingsWidthTextBox, nameof(RocketParameters.WingsWidth) },
                { GuidingsInnerRibLengthTextBox, nameof(RocketParameters.GuidesInnerRibLength) },
            };

            _comboBoxDictionary = new Dictionary<ComboBox, string>()
            {
                { WingsCountComboBox, nameof(RocketParameters.WingsCount) },
                { GuidingsCountComboBox, nameof(RocketParameters.GuidesCount) },
            };

            InitState();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Инициализация всех полей.
        /// </summary>
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

        /// <summary>
        /// Обработчик события покидания поля ввода(TextBox).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextBoxLeave(object sender, EventArgs e)
        {
            if (!_textoBoxDictionary.TryGetValue((TextBox)sender, out string parameterName))
            {
                return;
            }

            CheckValueInTextBox((TextBox)sender, parameterName);

            if (parameterName == nameof(RocketParameters.BodyDiameter) ||
                parameterName == nameof(RocketParameters.BodyLength))
            {
                ChangeMinMaxLabels(parameterName);
                CheckValueInAllTextBox();
            }
        }

        /// <summary>
        /// Обработчик события смены значения ComboBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnComboBoxSelectedValueChanged(object sender, EventArgs e)
        {
            if (!_comboBoxDictionary.TryGetValue((ComboBox)sender, out string parameterName))
            {
                return;
            }

            SetValueInComboBox((ComboBox)sender, parameterName);
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
                        RocketParameters.MIN_NOSE_LENGTH_MULTIPLIER).ToString();
                    MaxNoseLength.Text = (_parameters.BodyLength *
                        RocketParameters.MAX_NOSE_LENGTH_MULTIPLIER).ToString();

                    MinWingsLength.Text = (_parameters.BodyLength *
                        RocketParameters.MIN_WING_LENGTH_MULTIPLIER).ToString();
                    MaxWingsLength.Text = (_parameters.BodyLength *
                        RocketParameters.MAX_WING_LENGTH_MULTIPLIER).ToString();

                    MinGuidesInnerRibLength.Text = (_parameters.BodyLength *
                        RocketParameters.MIN_GUIDES_INNER_RIB_LENGTH_MULTIPLIER).ToString();
                    MaxGuidesInnerRibLength.Text = (_parameters.BodyLength *
                        RocketParameters.MAX_GUIDES_INNER_RIB_LENGTH_MULTIPLIER).ToString();

                    MinBodyDiameter.Text = (_parameters.BodyLength *
                        RocketParameters.MIN_BODY_DIAMTER_MULTIPLIER).ToString();
                    MaxBodyDiameter.Text = (_parameters.BodyLength *
                        RocketParameters.MAX_BODY_DIAMTER_MULTIPLIER).ToString();
                    break;
                case nameof(RocketParameters.BodyDiameter):
                    MaxWingsWidth.Text = (_parameters.BodyDiameter *
                        RocketParameters.MAX_WING_WIDTH_MULTIPLIER).ToString();
                    MinWingsWidth.Text = (_parameters.BodyDiameter *
                        RocketParameters.MIN_WING_WIDTH_MULTIPLIER).ToString();
                    break;
            }
        }

        //TODO: Дубли
        // Были дубли методов проверки значений и их установка 
        // для BodyLengthTextBox и BodyDiameterTextBox

        //TODO: Дубли
        // Был метод с множественнымы try catch, в связи с добавление CheckValueInTextBox
        // избавился от него в принципе

        /// <summary>
        /// Проверка и установка значения из поля ввода.
        /// </summary>
        /// <param name="textBox">Элемент управления <see cref="TextBox">.</param>
        /// <param name="propertyName">Название изменяемого параметра.</param>
        private void CheckValueInTextBox(TextBox textBox, string propertyName)
        {
            try
            {
                errorProvider.SetError(textBox, string.Empty);

                var propertyInfo = typeof(RocketParameters).
                    GetProperty(propertyName);
                propertyInfo.SetValue(_parameters, 
                    double.Parse(textBox.Text));
            }
            catch (Exception e)
            {
                if(e.InnerException != null)
                {
                    errorProvider.SetError(textBox, e.InnerException.Message);
                }
                else
                {
                    errorProvider.SetError(textBox, e.Message);
                }
            }
            finally
            {
                SetBuildButtonStatus();
            }
        }

        /// <summary>
        /// Установка значения из ComboBox.
        /// </summary>
        /// <param name="comboBox">Элемент управления <see cref="ComboBox">.</param>
        /// <param name="propertyName">Название изменяемого параметра.</param>
        private void SetValueInComboBox(ComboBox comboBox, string propertyName)
        {
            var propertyInfo = typeof(RocketParameters).
                GetProperty(propertyName);
            propertyInfo.SetValue(_parameters,
                int.Parse(comboBox.SelectedIndex.ToString()));
        }

        /// <summary>
        /// Проверка всех значений в полях ввода на валидность.
        /// </summary>
        private void CheckValueInAllTextBox()
        {
            foreach(var pair in _textoBoxDictionary)
            {
                CheckValueInTextBox(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Проверка на наличие не валидных данных и включение/выключение кнопки.
        /// </summary>
        private void SetBuildButtonStatus()
        {
            BuildButton.Enabled = true;

            foreach (var control in _textoBoxDictionary.Keys)
            {
                if(errorProvider.GetError(control) != string.Empty)
                {
                    BuildButton.Enabled = false;
                    return;
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Построить".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuildButton_Click(object sender, EventArgs e)
        {
            RocketBuilder builder = new RocketBuilder(_parameters);
            builder.Build();
        }

        #endregion Methods
    }
}
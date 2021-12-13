namespace RocketPlugin.BL
{
    using System;

    /// <summary>
    /// Класс, в котором содержатся параметры для построения модели ракеты.
    /// </summary>
    public class RocketParameters
    {
        #region Constants

        public double MAX_BODY_DIAMTER_MULTIPLIER = 0.15;
        public double MIN_BODY_DIAMTER_MULTIPLIER = 0.1;

        public double MAX_BODY_LENGTH = 25;
        public double MIN_BODY_LENGTH = 10;

        public double MAX_NOSE_LENGTH_MULTIPLIER = 0.4;
        public double MIN_NOSE_LENGTH_MULTIPLIER = 0.2;

        public double MAX_WING_WIDTH_MULTIPLIER = 0.4;
        public double MIN_WING_WIDTH_MULTIPLIER = 0.2;

        public double MAX_WING_LENGTH_MULTIPLIER = 0.35;
        public double MIN_WING_LENGTH_MULTIPLIER = 0.15;

        public double MAX_GUIDES_INNER_RIB_LENGTH_MULTIPLIER = 0.3;
        public double MIN_GUIDES_INNER_RIB_LENGTH_MULTIPLIER = 0.2;

        public double GUIDES_OUTER_RIB_LENGTH_MULTIPLIER = 0.5;
        public double GUIDES_WIDTH_MULTIPLIER = 0.2;

        #endregion

        #region Fields

        private double _bodyDiameter;
        private double _bodyLength;
        private double _guidesInnerRibLength;
        private double _noseLength;
        private double _wingsLength;
        private double _wingsWidth;

        #endregion

        #region Properties

        /// <summary>
        /// Димаетр корпуса ракеты.
        /// </summary>
        public double BodyDiameter 
        { 
            get => _bodyDiameter;
            set
            {   
                if(!Validator.ValidateValue(MIN_BODY_DIAMTER_MULTIPLIER * BodyLength, 
                    MAX_BODY_DIAMTER_MULTIPLIER * BodyLength, value))
                {
                    throw new ArgumentException("Введено неверное значение диаметра корпуса.");
                }

                _bodyDiameter = value;
                WingsDepth = BodyDiameter * 0.1;
                GuidesDepth = BodyDiameter * 0.1;
                GuidesWidth = BodyDiameter * GUIDES_WIDTH_MULTIPLIER;
            }
        }

        /// <summary>
        /// Высота корпуса ракеты.
        /// </summary>
        public double BodyLength 
        {
            get => _bodyLength;
            set
            {
                if(!Validator.ValidateValue(MIN_BODY_LENGTH, MAX_BODY_LENGTH, value))
                {
                    throw new ArgumentException("Введено неверное значение длины корпуса.");
                }

                _bodyLength = value;
            }
        }

        /// <summary>
        /// Количесвто направляющих
        /// </summary>
        public int GuidesCount { get; set; }

        /// <summary>
        /// Толщина направляющих.
        /// </summary>
        public double GuidesDepth { get; set; }

        /// <summary>
        /// Длина грани направляющей, прилигающей к корпусу ракеты.
        /// </summary>
        public double GuidesInnerRibLength
        {
            get => _guidesInnerRibLength;
            set
            {
                if (!Validator.ValidateValue(MIN_GUIDES_INNER_RIB_LENGTH_MULTIPLIER * BodyLength, 
                    MAX_GUIDES_INNER_RIB_LENGTH_MULTIPLIER * BodyLength, value))
                {
                    throw new ArgumentException("Введено неверное значение длины внутренней грани направляющей.");
                }

                _guidesInnerRibLength = value;
                GuidesOuterRibLength = GUIDES_OUTER_RIB_LENGTH_MULTIPLIER * GuidesInnerRibLength;
            }
        }

        /// <summary>
        /// Длина внешней грани направляющей.
        /// </summary>
        public double GuidesOuterRibLength { get; set; }

        /// <summary>
        /// Ширина направляющих.
        /// </summary>
        public double GuidesWidth { get; set; }

        /// <summary>
        /// Длина носа ракеты.
        /// </summary>
        public double NoseLength
        {
            get => _noseLength;
            set
            {
                if (!Validator.ValidateValue( MIN_NOSE_LENGTH_MULTIPLIER * BodyLength, 
                    MAX_NOSE_LENGTH_MULTIPLIER * BodyLength, value))
                {
                    throw new ArgumentException("Введено неверное значение длины носа.");
                }

                _noseLength = value;
            }
        }

        /// <summary>
        /// Количесвто крыльев.
        /// </summary>
        public int WingsCount { get; set; }

        /// <summary>
        /// Толщина крыльев.
        /// </summary>
        public double WingsDepth { get; set; }

        /// <summary>
        /// Длина крыльев.
        /// </summary>
        public double WingsLength
        {
            get => _wingsLength;
            set
            {
                if (!Validator.ValidateValue( MIN_WING_LENGTH_MULTIPLIER * BodyLength, 
                    MAX_WING_LENGTH_MULTIPLIER * BodyLength, value))
                {
                    throw new ArgumentException("Введено неверная длина крыльев.");
                }

                _wingsLength = value;
            }
        }

        /// <summary>
        /// Ширина крыльев.
        /// </summary>
        public double WingsWidth
        {
            get => _wingsWidth;
            set
            {
                if (!Validator.ValidateValue(MIN_WING_WIDTH_MULTIPLIER * BodyDiameter,
                    MAX_WING_WIDTH_MULTIPLIER * BodyDiameter, value))
                {
                    throw new ArgumentException("Введено неверная ширина крыльев.");
                }

                _wingsWidth = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию, задающий начальные значения параметров.
        /// </summary>
        public RocketParameters()
        {
            BodyLength = 20;
            BodyDiameter = 2.5;
            GuidesCount = 4;
            GuidesDepth = 0.25;
            GuidesInnerRibLength = 5;
            GuidesOuterRibLength = 2.5;
            GuidesWidth = 0.5;
            NoseLength = 6;
            WingsCount = 4;
            WingsDepth = 0.25;
            WingsLength = 4;
            WingsWidth = 0.625;
        }

        #endregion
    }
}

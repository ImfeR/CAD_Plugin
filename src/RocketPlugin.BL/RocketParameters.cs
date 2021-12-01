namespace RocketPlugin.BL
{
    public class RocketParameters
    {
        #region Fields

        private double _bodyDiameter;

        private double _bodyLength;

        private int _guidesCount;

        private double _guidesDepth;

        private double _guidesInnerRibLength;

        private double _guiderOuterRibLength;

        private double _guidesWidth;

        private double _noseLength;

        private int _wingsCount;

        private double _wingsDepth;

        private double _wingsLength;

        private double _wingsWidth;

        #endregion

        #region Properties

        public double BodyDiameter 
        { 
            get => _bodyDiameter;
            set
            {
                _bodyDiameter = value;
            }
        }

        public double BodyLength 
        {
            get => _bodyLength;
            set
            {
                _bodyLength = value;
            }
        }

        public int GuidesCount 
        {
            get => _guidesCount;
            set
            {
                _guidesCount = value;
            }
        }

        public double GuidesDepth
        {
            get => _guidesDepth;
            set
            {
                _guidesDepth = value;
            }
        }

        public double GuidesInnerRibLength
        {
            get => _guidesInnerRibLength;
            set
            {
                _guidesInnerRibLength = value;
            }
        }

        public double GuiderOuterRibLength
        {
            get => _guiderOuterRibLength;
            set
            {
                _guiderOuterRibLength = value;
            }
        }

        public double GuidesWidth
        {
            get => _guidesWidth;
            set
            {
                _guidesWidth = value;
            }
        }

        public double NoseLength
        {
            get => _noseLength;
            set
            {
                _noseLength = value;
            }
        }

        public int WingsCount
        {
            get => _wingsCount;
            set
            {
                _wingsCount = value;
            }
        }

        public double WingsDepth
        {
            get => _wingsDepth;
            set
            {
                _wingsDepth = value;
            }
        }

        public double WingsLength
        {
            get => _wingsLength;
            set
            {
                _wingsLength = value;
            }
        }

        public double WingsWidth
        {
            get => _wingsWidth;
            set
            {
                _wingsWidth = value;
            }
        }

        #endregion

        #region Constructors

        public RocketParameters()
        {
            BodyDiameter = 2.5;
            BodyLength = 20;
            GuidesCount = 4;
            GuidesDepth = 0.25;
            GuidesInnerRibLength = 5;
            GuiderOuterRibLength = 2.5;
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

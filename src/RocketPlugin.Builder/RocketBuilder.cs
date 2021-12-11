namespace RocketPlugin.Builder
{
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;

    using BL;

    using System;

    /// <summary>
    /// Класс, отвеающий за построение модели ракеты.
    /// </summary>
    public class RocketBuilder
    {
        #region Fields

        private RocketParameters _parameters;

        #endregion Fields

        #region Constructors

        public RocketBuilder(RocketParameters parameters)
        {
            _parameters = parameters;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Строит модель ракеты.
        /// </summary>
        public void Build()
        {
            var activeDocument = Application.DocumentManager.MdiActiveDocument;
            var database = activeDocument.Database;

            using (var documentLock = activeDocument.LockDocument())
            {
                using (var transaction = database.TransactionManager.StartTransaction())
                {
                    BlockTable blockTable = transaction.GetObject(database.BlockTableId,
                        OpenMode.ForRead) as BlockTable;
                    BlockTableRecord blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace],
                        OpenMode.ForWrite) as BlockTableRecord;

                    double bodyLength = _parameters.BodyLength;
                    double bodyRadius = _parameters.BodyDiameter / 2;
                    int guidesCount = _parameters.GuidesCount;
                    double guidesDepth = _parameters.GuidesDepth;
                    double guidesInnerRib = _parameters.GuidesInnerRibLength;
                    double guidesOuterRib = _parameters.GuidesOuterRibLength;
                    double guidesWidth = _parameters.GuidesWidth;
                    double noseLength = _parameters.NoseLength;
                    int wingsCount = _parameters.WingsCount;
                    double wingsDepth = _parameters.WingsDepth;
                    double wingsLength = _parameters.WingsLength;
                    double wingsWidth = _parameters.WingsWidth;

                    var body = CreateBodyOrNose(bodyLength, bodyRadius, bodyRadius);
                    body.TransformBy(Matrix3d.Displacement(new Point3d(0, 0, bodyLength / 2) - Point3d.Origin));

                    var nose = CreateBodyOrNose(noseLength, bodyRadius, 0);
                    nose.TransformBy(Matrix3d.Displacement(new Point3d(0, 0, bodyLength + noseLength / 2) - Point3d.Origin));

                    body.BooleanOperation(BooleanOperationType.BoolUnite, nose);

                    var wing = CreateWedge(wingsWidth, wingsDepth, wingsLength);
                    wing.TransformBy(Matrix3d.Displacement(new Point3d(_parameters.BodyDiameter / 2 + _parameters.WingsWidth / 2, 0, _parameters.WingsLength / 2) - Point3d.Origin));

                    body = ApplyPolarArrayOnBody(body, wingsCount, wing);

                    var Guides = CreateGuides(guidesWidth, guidesDepth, guidesOuterRib, guidesInnerRib);

                    var guidesShiftDistanceZ = (guidesInnerRib / 2) + (bodyLength / 2);
                    var guidesShiftDistanceX = bodyRadius + (guidesWidth / 2);
                    Guides.TransformBy(Matrix3d.Displacement(new Point3d(guidesShiftDistanceX, 0, guidesShiftDistanceZ) - Point3d.Origin));

                    body = ApplyPolarArrayOnBody(body, guidesCount, Guides);

                    blockTableRecord.AppendEntity(body);
                    transaction.AddNewlyCreatedDBObject(body, true);

                    transaction.Commit();
                }
            }

        }

        /// <summary>
        /// Создает объект <see cref="Solid3d"/>, который является либо носом ракеты,
        /// либо корпусом ракеты.
        /// </summary>
        /// <param name="heigth">Высота элемента.</param>
        /// <param name="bottomRadius">Радиус основания.</param>
        /// <param name="topRaduis">Радиус вершины (для построения носа = 0).</param>
        /// <returns>Объект типа <see cref="Solid3d"/>, являющийся частью ракеты.</returns>
        private Solid3d CreateBodyOrNose(double heigth, double bottomRadius, double topRaduis)
        {
            var element = new Solid3d();
            element.SetDatabaseDefaults();
            element.CreateFrustum(heigth, bottomRadius, bottomRadius, topRaduis);

            return element;
        }

        /// <summary>
        /// Создает и принминяет из элементов круг-массив на корпус ракеты.
        /// </summary>
        /// <param name="body">Корпус ракеты.</param>
        /// <param name="elementsCount">Количесвто элементов для построения.</param>
        /// <param name="element">Элемент из которого строится круг-массив.</param>
        /// <returns>Корпус ракеты с круг-массивом из эелементов.</returns>
        private Solid3d ApplyPolarArrayOnBody(Solid3d body, int elementsCount, Solid3d element)
        {
            double angle = 360.0 / elementsCount * Math.PI / 180;

            for (int i = 0; i < _parameters.GuidesCount; i++)
            {
                var newGuid = element.Clone() as Solid3d;
                newGuid.TransformBy(Matrix3d.Rotation(angle * i, Vector3d.ZAxis, Point3d.Origin));
                body.BooleanOperation(BooleanOperationType.BoolUnite, newGuid);
            }

            return body;
        }

        /// <summary>
        /// Создает клин(оно же крыло).
        /// </summary>
        /// <param name="width">Ширина клина.</param>
        /// <param name="depth">Толщина клина.</param>
        /// <param name="length">Высота клина.</param>
        /// <returns>Построенный клин.</returns>
        private Solid3d CreateWedge(double width, double depth, double length)
        {
            var wing = new Solid3d();
            wing.SetDatabaseDefaults();
            wing.CreateWedge(width, depth, length);

            return wing;
        }

        /// <summary>
        /// Создание направялющей ракеты.
        /// </summary>
        /// <param name="width">Ширина направляющей</param>
        /// <param name="depth">Толщина направляющей</param>
        /// <param name="outerRibLength">Внешняя грань направляющей.</param>
        /// <param name="innerRibLength">Внутренняя грань направляющей.</param>
        /// <returns>Построенная и смещенная на необходимое значение направляющая</returns>
        private Solid3d CreateGuides(double width, double depth, double outerRibLength, double innerRibLength)
        {
            var guides = new Solid3d();
            guides.CreateBox(width, depth, outerRibLength);

            var wedgeLength = (innerRibLength - outerRibLength) / 2;
            var wedgeShiftDistance = (outerRibLength + wedgeLength) / 2;

            var topPartGuides = CreateWedge(width, depth, wedgeLength);
            topPartGuides.TransformBy(Matrix3d.Displacement(new Point3d(0, 0, wedgeShiftDistance) - Point3d.Origin));
            guides.BooleanOperation(BooleanOperationType.BoolUnite, topPartGuides);

            var bottomPartGuides = CreateWedge(width, depth, -wedgeLength);
            bottomPartGuides.TransformBy(Matrix3d.Displacement(new Point3d(0, 0, -wedgeShiftDistance) - Point3d.Origin));
            guides.BooleanOperation(BooleanOperationType.BoolUnite, bottomPartGuides);

            return guides;
        }

        #endregion Methods
    }
}

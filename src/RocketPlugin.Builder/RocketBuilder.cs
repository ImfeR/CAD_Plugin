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
        
        /// <summary>
        /// Параметры модели.
        /// </summary>
        private RocketParameters _parameters;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="parameters">Параметры для построения ракеты.</param>
        public RocketBuilder(RocketParameters parameters)
        {
            _parameters = parameters;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Уничтожает уже существующую модель ракеты, если такая есть.
        /// </summary>
        /// <param name="database">База данных текущего документа AutoCAD.</param>
        /// <param name="blockName">Название блока.</param>
        private void EraseExistingModel(Database database, string blockName)
        {
            ObjectId blockId = ObjectId.Null;

            using (Transaction transaction = database.TransactionManager.
                StartTransaction())
            {
                BlockTable blockTable = (BlockTable)transaction.GetObject(
                    database.BlockTableId, OpenMode.ForRead);

                if (blockTable.Has(blockName))
                {
                    blockId = blockTable[blockName];
                }

                if (blockId.IsNull)
                {
                    return;
                }

                BlockTableRecord blockTabelRecord = (BlockTableRecord)transaction.
                    GetObject(blockId, OpenMode.ForRead);

                var blkRefs = blockTabelRecord.GetBlockReferenceIds(true, true);
                if (blkRefs != null && blkRefs.Count > 0)
                {
                    foreach (ObjectId blkRefId in blkRefs)
                    {
                        BlockReference blkRef = (BlockReference)transaction.
                            GetObject(blkRefId, OpenMode.ForWrite);
                        blkRef.Erase();
                    }
                }

                blkRefs = blockTabelRecord.GetBlockReferenceIds(true, true);
                if (blkRefs == null || blkRefs.Count == 0)
                {
                    blockTabelRecord.UpgradeOpen();
                    blockTabelRecord.Erase();
                }

                transaction.Commit();
            }
        }

        /// <summary>
        /// Строит модель ракеты.
        /// </summary>
        public void Build()
        {
            var activeDocument = Application.DocumentManager.MdiActiveDocument;
            var database = activeDocument.Database;

            var blockName = "Rocket";

            using (var documentLock = activeDocument.LockDocument())
            {
                // Удаляем существующую модель ракеты, если такая есть.
                EraseExistingModel(database, blockName);

                using (var transaction = database.TransactionManager.
                    StartTransaction())
                {
                    // Открываем таблицу блоков для записи.
                    BlockTable blockTable = (BlockTable)transaction.
                        GetObject(database.BlockTableId, OpenMode.ForWrite);

                    // Создаем новое определение блока и даем ему имя.
                    BlockTableRecord blockTableRecord = new BlockTableRecord();
                    blockTableRecord.Name = blockName;

                    // Добавляем созданное определение блока в таблицу блоков и в транзакцию,
                    // запоминаем ID созданного определения блока.
                    ObjectId blockTableRecordId = blockTable.Add(blockTableRecord);
                    transaction.AddNewlyCreatedDBObject(blockTableRecord, true);

                    // Задаем параметры.
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
                    GuidesType guidType = _parameters.GuidesType;

                    // Создание корпуса ракеты и смещение его на позицию 0, 0, 0.
                    var body = CreateBodyOrNose(bodyLength, bodyRadius, 
                        bodyRadius);
                    body.TransformBy(Matrix3d.Displacement(
                        new Point3d(0, 0, bodyLength / 2) - Point3d.Origin));

                    // Создание носа ракеты, смещение на 
                    // необходиму позицию и объединение с корпусом.
                    var nose = CreateBodyOrNose(noseLength, bodyRadius, 0);
                    nose.TransformBy(Matrix3d.Displacement(
                        new Point3d(0, 0, bodyLength + noseLength / 2) 
                        - Point3d.Origin));
                    body.BooleanOperation(BooleanOperationType.BoolUnite, 
                        nose);

                    // Создание крыла ракеты.
                    if (wingsCount != 0)
                    {
                        var wing = CreateWedge(wingsWidth, wingsDepth, wingsLength);
                        wing.TransformBy(Matrix3d.Displacement(
                            new Point3d(bodyRadius + wingsWidth / 2, 0, wingsLength / 2)
                            - Point3d.Origin));

                        body = ApplyPolarArrayOnBody(body, wingsCount, wing);
                    }

                    // Создание направляющей ракеты.
                    if (guidesCount != 0)
                    {
                        Solid3d guides = CreateGuides(guidesWidth,
                            guidesDepth, guidesOuterRib, guidesInnerRib, guidType);

                        var guidesShiftDistanceZ = (guidesInnerRib / 2) +
                            (bodyLength / 2);
                        var guidesShiftDistanceX = bodyRadius +
                            (guidesWidth / 2);
                        guides.TransformBy(Matrix3d.Displacement(
                            new Point3d(guidesShiftDistanceX, 0, guidesShiftDistanceZ)
                            - Point3d.Origin));

                        body = ApplyPolarArrayOnBody(body, guidesCount, guides);
                    }

                    // Добавление модели ракеты в транзакцию
                    blockTableRecord.AppendEntity(body);
                    transaction.AddNewlyCreatedDBObject(body, true);

                    // Открываем пространсто моделей для записи,
                    // создаем новое вхождение блока, используя
                    // ранее сохраненный ID определения блока
                    BlockTableRecord modelSpace = (BlockTableRecord)transaction.
                        GetObject(blockTable[BlockTableRecord.ModelSpace], 
                        OpenMode.ForWrite);
                    BlockReference blockReference = new BlockReference(
                        Point3d.Origin, blockTableRecordId);

                    // Добавляем созданное вхождение блока
                    // на пространство модели и в транзакцию
                    modelSpace.AppendEntity(blockReference);
                    transaction.AddNewlyCreatedDBObject(blockReference, true);

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
        private Solid3d CreateBodyOrNose(double heigth, 
            double bottomRadius, double topRaduis)
        {
            var element = new Solid3d();
            element.SetDatabaseDefaults();
            element.CreateFrustum(heigth, bottomRadius, 
                bottomRadius, topRaduis);

            return element;
        }

        /// <summary>
        /// Создает и принминяет из элементов круг-массив на корпус ракеты.
        /// </summary>
        /// <param name="body">Корпус ракеты.</param>
        /// <param name="elementsCount">Количесвто элементов для построения.</param>
        /// <param name="element">Элемент из которого строится круг-массив.</param>
        /// <returns>Корпус ракеты с круг-массивом из эелементов.</returns>
        private Solid3d ApplyPolarArrayOnBody(Solid3d body, 
            int elementsCount, Solid3d element)
        {
            double angle = 360 / elementsCount * Math.PI / 180;

            for (int i = 0; i < elementsCount; i++)
            {
                var newGuid = element.Clone() as Solid3d;
                newGuid.TransformBy(Matrix3d.Rotation(angle * i, 
                    Vector3d.ZAxis, Point3d.Origin));
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
        private Solid3d CreateWedge(double width, 
            double depth, double length)
        {
            var wing = new Solid3d();
            wing.SetDatabaseDefaults();
            wing.CreateWedge(width, depth, length);

            return wing;
        }

        /// <summary>
        /// Создание направляющей ракеты.
        /// </summary>
        /// <param name="guidesWidth">Ширина направляющих.</param>
        /// <param name="guidesDepth">Толщина направляющих.</param>
        /// <param name="guidesOuterRib">Длина внешней грани направляющей.</param>
        /// <param name="guidesInnerRib">Длина внутренней грани направляющей.</param>
        /// <param name="guidType">Тип направляющей.</param>
        /// <returns>Направляющая ракеты.</returns>
        private Solid3d CreateGuides(double guidesWidth, double guidesDepth, 
            double guidesOuterRib, double guidesInnerRib, GuidesType guidType)
        {
            Solid3d guides = new Solid3d();

            switch (guidType)
            {
                case GuidesType.Trapezoid:
                    return CreateTrapezoidGuides(guidesWidth,
                        guidesDepth, guidesOuterRib, guidesInnerRib);
                case GuidesType.Rectangle:
                    guides.CreateBox(guidesWidth, guidesDepth,
                        guidesInnerRib);
                    return guides;
                case GuidesType.Triangle:
                    guides.CreateWedge(guidesWidth, guidesDepth,
                        guidesInnerRib);
                    return guides;
                default:
                    return new Solid3d();
            }
        }

        /// <summary>
        /// Создание направялющей ракеты трапециевидной формы.
        /// </summary>
        /// <param name="width">Ширина направляющей</param>
        /// <param name="depth">Толщина направляющей</param>
        /// <param name="outerRibLength">Внешняя грань направляющей.</param>
        /// <param name="innerRibLength">Внутренняя грань направляющей.</param>
        /// <returns>Построенная и смещенная на необходимое значение направляющая</returns>
        private Solid3d CreateTrapezoidGuides(double width, double depth, 
            double outerRibLength, double innerRibLength)
        {
            var guides = new Solid3d();
            guides.CreateBox(width, depth, outerRibLength);

            var wedgeLength = (innerRibLength - outerRibLength) / 2;
            var wedgeShiftDistance = (outerRibLength + wedgeLength) / 2;

            guides.BooleanOperation(BooleanOperationType.BoolUnite,
                CreateTrapezoidGuidesPart(width, depth, wedgeLength,
                wedgeShiftDistance));

            guides.BooleanOperation(BooleanOperationType.BoolUnite,
                CreateTrapezoidGuidesPart(width, depth, -wedgeLength,
                -wedgeShiftDistance));

            return guides;
        }

        /// <summary>
        /// Создает часть направялющей.
        /// </summary>
        /// <param name="width">Ширина части.</param>
        /// <param name="depth">Толщина части.</param>
        /// <param name="length">Длина части.</param>
        /// <param name="shiftDistanceZ">Растояние смещения по оси Z</param>
        /// <returns>Часть направляющей в виде объекта <see cref="Solid3d"></returns>
        private Solid3d CreateTrapezoidGuidesPart(double width, double depth,
            double length, double shiftDistanceZ)
        {
            var guidesPart = CreateWedge(width, depth, length);
            guidesPart.TransformBy(Matrix3d.Displacement(
                new Point3d(0, 0, shiftDistanceZ) - Point3d.Origin));

            return guidesPart;
        }

        #endregion Methods
    }
}

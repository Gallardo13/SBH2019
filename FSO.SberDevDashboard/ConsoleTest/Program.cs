using System;
using System.Diagnostics;
using System.Linq;
using FSO.SDD.DataBaseEfStore;
using FSO.SDD.DbModel;

namespace ConsoleTest
{
    class Program
    {

        private static void GenerateBurnDown(StoreContext db)
        {
            /// Метрика - это норматив индикатора
            /// Индикатор - это некая цифра или набор цифр (серии данных)
            /// Значение индикатора вычисляется для набора сущностей-источников
            /// Значение может быть вычислено по одному или нескольким измерениям/разрезам.
            /// У метрики может быть несколько допустимых аггрегатов - сущностей, по которым можно считать значение.
            /// Например, диаграмма сгорания:
            /// - строится по дням (измерение),
            /// - вычисляется актуальный процент выполненных задач и оптимальный процент (серии данных)
            /// - значение вычисляется для релиза, спринта, эпика (сущности), задачи и юзера как независимых ключей, т.е. для каждой сущности вычисляется своё значение.
            /// - аггрегировать можно по релизау, спринту, эпику (таким образом можно по одной метрики нарисовать три диаграммы)
            /// Значение метрики тоже может быть источником данных для других индикаторов.
            /// Например, посчитанное отклонение реального процента исполнения от оптимального, можно вывести как индикатор качества планирования, который считать по спринтам


            var indicator = db.Indicators.FirstOrDefault(d => d.Name == "BurnDownStoryPoints");
            if (indicator == null)
            {
                indicator = new Indicator() { Id = 1, Name = "BurnDownStoryPoints" };
                db.Indicators.Add(indicator);

            }


            var seriesOptimal = db.IndicatorValuesSeries.FirstOrDefault(s => s.Name == "OptimalStoryPointsCompleted");
            if (seriesOptimal == null)
            {
                seriesOptimal = new IndicatorValuesSeries() { Id = 1, Name = "OptimalStoryPointsCompleted" };
                db.IndicatorValuesSeries.Add(seriesOptimal);

            }

            var seriesActual = db.IndicatorValuesSeries.FirstOrDefault(s => s.Name == "ActualStoryPointsCompleted");
            if (seriesActual == null)
            {
                seriesActual = new IndicatorValuesSeries() { Id = 2, Name = "ActualStoryPointsCompleted" };
                db.IndicatorValuesSeries.Add(seriesActual);

            }

            var sourceRelease = db.IndicatorValueSourceKeys.FirstOrDefault(s => s.SourceType == "JiraRelease" && s.SourceItemId == 7);
            if (sourceRelease == null)
            {
                sourceRelease = new IndicatorValueSourceKey() { Id = 1, SourceType = "JiraRelease", SourceItemId = 7 };
                db.IndicatorValueSourceKeys.Add(sourceRelease);

            }

            var sourceEpic = db.IndicatorValueSourceKeys.FirstOrDefault(s => s.SourceType == "JiraEpic" && s.SourceItemId == 17);
            if (sourceEpic == null)
            {
                sourceEpic = new IndicatorValueSourceKey() { Id = 2, SourceType = "JiraEpic", SourceItemId = 17 };
                db.IndicatorValueSourceKeys.Add(sourceEpic);

            }

            var sourceTask = db.IndicatorValueSourceKeys.FirstOrDefault(s => s.SourceType == "JiraTask" && s.SourceItemId == 94);
            if (sourceTask == null)
            {
                sourceTask = new IndicatorValueSourceKey() { Id = 3, SourceType = "JiraTask", SourceItemId = 94 };
                db.IndicatorValueSourceKeys.Add(sourceTask);

            }

            var dim = db.IndicatorDimensions.FirstOrDefault(d => d.Name == "Day");
            if (dim == null)
            {
                dim = new IndicatorDimension() { Id = 1, Name = "Day" };
                db.IndicatorDimensions.Add(dim);

            }

            int valId = 0;
            int sId = 0;

            FillSprint1(db, ref valId, dim, indicator, seriesActual, ref sId, sourceRelease, sourceEpic, sourceTask, seriesOptimal);


        }

        private static void FillSprint1(StoreContext db, ref int valId, IndicatorDimension dim, Indicator indicator,
            IndicatorValuesSeries seriesActual, ref int sId, IndicatorValueSourceKey sourceRelease,
            IndicatorValueSourceKey sourceEpic, IndicatorValueSourceKey sourceTask, IndicatorValuesSeries seriesOptimal)
        {
//------------------ Day 1

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesActual.Id,
                DimensionValue = "2019-10-07",
                Value = 1
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesOptimal.Id,
                DimensionValue = "2019-10-07",
                Value = (decimal) 0.9
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            //------------------ Day 2

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesActual.Id,
                DimensionValue = "2019-10-08",
                Value = 1
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesOptimal.Id,
                DimensionValue = "2019-10-08",
                Value = (decimal) 0.8
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            //------------------ Day 3

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesActual.Id,
                DimensionValue = "2019-10-09",
                Value = (decimal) 0.9
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesOptimal.Id,
                DimensionValue = "2019-10-09",
                Value = (decimal) 0.7
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            //------------------ Day 4

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesActual.Id,
                DimensionValue = "2019-10-10",
                Value = (decimal) 0.7
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesOptimal.Id,
                DimensionValue = "2019-10-10",
                Value = (decimal) 0.6
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            //------------------ Day 5

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesActual.Id,
                DimensionValue = "2019-10-11",
                Value = (decimal) 0.7
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesOptimal.Id,
                DimensionValue = "2019-10-11",
                Value = (decimal) 0.5
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            //------------------ Day 6

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesActual.Id,
                DimensionValue = "2019-10-14",
                Value = (decimal) 0.5
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesOptimal.Id,
                DimensionValue = "2019-10-14",
                Value = (decimal) 0.4
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            //------------------ Day 7

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesActual.Id,
                DimensionValue = "2019-10-15",
                Value = (decimal) 0.3
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesOptimal.Id,
                DimensionValue = "2019-10-15",
                Value = (decimal) 0.3
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            //------------------ Day 8

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesActual.Id,
                DimensionValue = "2019-10-16",
                Value = (decimal) 0.2
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesOptimal.Id,
                DimensionValue = "2019-10-16",
                Value = (decimal) 0.2
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            //------------------ Day 9

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesActual.Id,
                DimensionValue = "2019-10-17",
                Value = (decimal) 0.2
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesOptimal.Id,
                DimensionValue = "2019-10-17",
                Value = (decimal) 0.1
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            //------------------ Day 10

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesActual.Id,
                DimensionValue = "2019-10-18",
                Value = (decimal) 0.1
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            db.IndicatorValues.Add(new IndicatorValue()
            {
                Id = ++valId,
                DimensionId = dim.Id,
                IndicatorId = indicator.Id,
                IndicatorSeriesId = seriesOptimal.Id,
                DimensionValue = "2019-10-18",
                Value = (decimal) 0.0
            });

            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceRelease.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceEpic.Id});
            db.IndicatorValueSources.Add(new IndicatorValueSource()
                {Id = ++sId, IndicatorValueId = valId, IndicatorValueSourceKeyId = sourceTask.Id});

            //------------------ 
        }

        static void Main(string[] args)
        {

            using (var db = new StoreContext(@"..\..\..\..\FSO.SDD.DataBase.db;"))
            {
                GenerateBurnDown(db);
                db.SaveChanges();
            }

            Console.WriteLine("Hello World!");
            Console.ReadLine();

        }
    }
}

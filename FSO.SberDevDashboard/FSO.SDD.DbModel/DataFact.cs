using System;
using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    public class DataFact
    {
        /// <summary>
        /// Идентификатор (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        public DataSource Source { get; set; }

        public int SourceId { get; set; }

        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// синтетичеаский ключ данных. Формат зависит от источника.
        /// Для jira-task это уникальный ключ.
        /// Для релиза - КЭ + номер релиза.
        /// Для спринта - ключ проекта + название спринта
        /// </summary>
        public string Key { get; set; }

        public string PropertyKey { get; set; }
        public string PropertyValue { get; set; }
    }
}
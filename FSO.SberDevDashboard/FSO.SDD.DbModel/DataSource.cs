using System.ComponentModel.DataAnnotations;

namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Истоник данных конкретной системы
    /// Например, для jira это может быть таск, релиз, спринт
    /// </summary>
    public class DataSource
    {
        /// <summary>
        /// Идентификатор источника (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        public SourceSystem System { get; set; }

        public int SourceSystemId { get; set; }
    }
}
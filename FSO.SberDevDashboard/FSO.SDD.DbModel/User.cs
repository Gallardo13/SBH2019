using System.ComponentModel.DataAnnotations;


namespace FSO.SDD.DbModel
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя (автоинкремент)
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Имя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }

    }
}

using SQLite;

namespace Compras.MVVM.Models
{
    public class Unit
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

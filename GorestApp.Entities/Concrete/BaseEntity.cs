namespace GorestApp.Entities.Concrete
{
    /// <summary>
    /// Varlıklar için temel sınıfı temsil eder
    /// </summary>
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// Varlık tanımlayıcısını alır veya ayarlar
        /// </summary>        
        public int Id { get; set; }
    }
}

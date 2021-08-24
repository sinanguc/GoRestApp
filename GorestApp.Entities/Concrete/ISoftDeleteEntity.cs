namespace GorestApp.Entities.Concrete
{
    /// <summary>
    /// Yazılımla silinen (aslında depodan silmeden) bir varlığı temsil eder
    /// </summary>
    public partial interface ISoftDeleteEntity
    {
        /// <summary>
        /// Kaydın silinip silinmediğini gösteren bir değer alır veya ayarlar
        /// </summary>
        bool Deleted { get; set; }
    }
}

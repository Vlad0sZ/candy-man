namespace Runtime.GameEngine.Models
{
    /// <summary>
    /// Статус после передачи конфет
    /// </summary>
    public enum GiftStatus
    {
        /// <summary>
        /// Нужно больше конфет
        /// </summary>
        NeedMoreCandies,
        
        /// <summary>
        /// Не та конфета
        /// </summary>
        TastelessCandy,
        
        /// <summary>
        /// Хватит конфет
        /// </summary>
        EnoughCandy,
    }
}
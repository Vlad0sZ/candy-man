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
        NeedMoreCandies = 0,
        
        /// <summary>
        /// Не та конфета
        /// </summary>
        TastelessCandy =  1,
        
        /// <summary>
        /// Хватит конфет
        /// </summary>
        EnoughCandy = 2,
    }
}
namespace CriminalCheckerBackend.Model.DataBase.Exceptions
{
    /// <summary>
    /// This exception is being throw, when user sends bad data.
    /// </summary>
    public class NewUserNotValidValueException : Exception
    {
        /// <summary>
        /// Create new instance of <see cref="NewUserNotValidValueException"/>.
        /// </summary>
        /// <param name="conflictPropertyName">Conflict property name.</param>
        /// <param name="conflictPropertyValue">Conflict property value.</param>
        public NewUserNotValidValueException(string conflictPropertyName, string conflictPropertyValue)
        {
            ConflictPropertyName = conflictPropertyName;
            ConflictPropertyValue = conflictPropertyValue;
            IsValueEmpty = string.IsNullOrWhiteSpace(conflictPropertyValue);
        }

        /// <summary>
        /// Get conflict property name.
        /// </summary>
        public string ConflictPropertyName { get; }
        
        /// <summary>
        /// Get conflict property value.
        /// </summary>
        public string ConflictPropertyValue { get; }

        /// <summary>
        /// Get value is indicating that value is empty or null.
        /// </summary>
        public bool IsValueEmpty { get; }
    }
}

namespace API.Domain.Triggers
{
    
///<summary>
    /// Represents a database trigger with its properties and associated information.
    /// </summary>
    public class DatabaseTrigger
    {
        /// <summary>
        /// Gets or sets the name of the trigger.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the trigger.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the definition of the trigger.
        /// </summary>
        public string Definition { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the trigger.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the modification date of the trigger.
        /// </summary>
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// Gets or sets the additional information about the trigger.
        /// </summary>
        public TriggerInfo triggerInfo { get; set; }
    }
}

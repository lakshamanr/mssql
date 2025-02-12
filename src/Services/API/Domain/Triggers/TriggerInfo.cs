namespace API.Domain.Triggers
{
    /// <summary>
    /// Represents information about a database trigger.
    /// </summary>
    public class TriggerInfo
    {
        /// <summary>
        /// Gets or sets the name of the trigger.
        /// </summary>
        public string TriggerName { get; set; }  // Name of the trigger

        /// <summary>
        /// Gets or sets the schema of the object the trigger belongs to.
        /// </summary>
        public string SchemaName { get; set; }   // Schema of the object the trigger belongs to

        /// <summary>
        /// Gets or sets the name of the table/view the trigger is attached to.
        /// </summary>
        public string ObjectName { get; set; }   // Name of the table/view the trigger is attached to

        /// <summary>
        /// Gets or sets the type of the object (e.g., Table, View).
        /// </summary>
        public string ObjectType { get; set; }   // Type of the object (e.g., Table, View)

        /// <summary>
        /// Gets or sets the date when the trigger was created.
        /// </summary>
        public DateTime CreateDate { get; set; } // When the trigger was created

        /// <summary>
        /// Gets or sets the date when the trigger was last modified.
        /// </summary>
        public DateTime ModifyDate { get; set; } // When the trigger was last modified

        /// <summary>
        /// Gets or sets a value indicating whether the trigger is disabled.
        /// </summary>
        public bool IsDisabled { get; set; }     // Whether the trigger is disabled

        /// <summary>
        /// Gets or sets a value indicating whether QUOTED_IDENTIFIER is ON.
        /// </summary>
        public bool QuotedIdentifierOn { get; set; } // Whether QUOTED_IDENTIFIER is ON

        /// <summary>
        /// Gets or sets a value indicating whether ANSI_NULLS is ON.
        /// </summary>
        public bool AnsiNullsOn { get; set; }    // Whether ANSI_NULLS is ON
    }
}

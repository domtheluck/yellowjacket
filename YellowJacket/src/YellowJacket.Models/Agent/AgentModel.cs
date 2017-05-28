using System;

namespace YellowJacket.Models.Agent
{
    /// <summary>
    /// Represents an Agent model.
    /// </summary>
    public class AgentModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the last update date time.
        /// </summary>
        /// <value>
        /// The last update date time.
        /// </value>
        public DateTime LastUpdateOn { get; set; }

        /// <summary>
        /// Gets or sets the registration date time.
        /// </summary>
        /// <value>
        /// The registration date time.
        /// </value>
        public DateTime RegisteredOn { get; set; }

        #endregion
    }
}

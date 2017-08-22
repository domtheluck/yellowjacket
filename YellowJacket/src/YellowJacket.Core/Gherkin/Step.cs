namespace YellowJacket.Core.Gherkin
{
    /// <summary>
    /// Represents a Gherkin scenario step.
    /// </summary>
    internal class Step
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        #endregion
    }
}

using System.Collections.Generic;

namespace YellowJacket.Core.Gherkin
{
    /// <summary>
    /// Represents a Gherkin feature scenario.
    /// </summary>
    internal class Scenario
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
        /// Gets or sets the steps.
        /// </summary>
        /// <value>
        /// The steps.
        /// </value>
        public IEnumerable<Step> Steps { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Scenario"/> class.
        /// </summary>
        internal Scenario()
        {
            Steps = new List<Step>();
        }

        #endregion
    }
}

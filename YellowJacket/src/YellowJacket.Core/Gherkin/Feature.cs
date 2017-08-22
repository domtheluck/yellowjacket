using System.Collections.Generic;

namespace YellowJacket.Core.Gherkin
{
    /// <summary>
    /// Represents a Gherkin feature.
    /// </summary>
    internal class Feature
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
        /// Gets or sets the scenarios.
        /// </summary>
        /// <value>
        /// The scenarios.
        /// </value>
        public IEnumerable<Scenario> Scenarios { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Feature"/> class.
        /// </summary>
        internal Feature()
        {
            Scenarios = new List<Scenario>();
        }

        #endregion
    }
}

using System;

namespace Test.NewSolution.Data.Clients
{
    /// <summary>
    /// Base client.
    /// </summary>
    public class BaseClient
    {
        #region Private Members

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Sin4U.Data.Clients.BaseClient"/> class.
        /// </summary>
        public BaseClient()
        {
        }

        #region IBaseClient implementation

        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value>true</value>
        /// <c>false</c>
        public bool IsAuthenticated
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}


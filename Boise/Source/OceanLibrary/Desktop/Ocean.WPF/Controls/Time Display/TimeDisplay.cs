
using System;
using System.Timers;
using Ocean.Infrastructure;

namespace Ocean.Wpf.Controls {

    /// <summary>
    /// Represents TimeDisplay
    /// </summary>
    public class TimeDisplay : ObservableObject, IDisposable {

        #region  Declarations

        Boolean _disposedValue;
        DateTime _now = DateTime.Now;
        readonly Timer _timer = new Timer();

        #endregion

        #region  Properties

        /// <summary>
        /// Gets the now.
        /// </summary>
        /// <value>The now.</value>
        public DateTime Now {
            get {
                return _now;
            }
        }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeDisplay"/> class.
        /// </summary>
        public TimeDisplay() {
            //This will get the clock to change on the next minute
            _timer.Interval = (60 - DateTime.Now.Second) * 1000;
            _timer.Start();
            _timer.Elapsed += _objTimer_Elapsed;
        }

        #endregion

        #region  Methods

        void _objTimer_Elapsed(Object sender, ElapsedEventArgs e) {
            _timer.Interval = 60000;
            _now = DateTime.Now;
            RaisePropertyChanged("Now");
        }

        #endregion

        #region  IDisposable Support

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(Boolean disposing) {

            if(!_disposedValue) {

                if(disposing) {
                    _timer.Dispose();
                }

            }

            _disposedValue = true;
        }

        #endregion
    }
}
using System;

namespace ThePhoneCompany.Common.Infrastructure {

    public class RepositoryResult<T> {
        
        readonly T _package;
        readonly Exception _error;

        public T Package { get { return _package; } }
        public Exception Error { get { return _error; } }

        public RepositoryResult(T package, Exception error) {
            _package = package;
            _error = error;
        }
    }
}

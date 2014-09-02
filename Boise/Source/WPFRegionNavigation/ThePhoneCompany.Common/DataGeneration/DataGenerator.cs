using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ThePhoneCompany.Common.DataGeneration {

    public enum StringCase { None, Upper, Lower }

    public sealed class DataGenerator {

        #region  Declarations

        readonly List<String> _firstNames = new List<String> { "Tom", "Dick", "Harry", "Jim", "Susan", "Susie", "Abel", "Ann", "William", "Josh" };
        readonly List<String> _lastNames = new List<String> { "Washington", "McDonnald", "Smith", "Jones", "O'Malley", "Love", "Fox", "Smithfield", "West", "Jordon" };
        readonly List<String> _companyNames = new List<String> { "Acme Inc.", "Microsoft", "Little Richie Software", "Complex Objects LTD", "Designers Rock", "Happy Capitalist", "Simple Solutions", "Power Developers", "WPF Disciples", "Silverlight Source Inc." };
        readonly List<String> _urls = new List<String> { "http://microsoft.com", "http://karlshifflett.wordpress.com", "http://agsmith.wordpress.com/", "http://www.beacosta.com/blog/", "http://wekempf.spaces.live.com/default.aspx", "http://x-coders.com/blogs/sneaky/default.aspx", "http://blogs.ugidotnet.org/corrado/Default.aspx", "http://sachabarber.net/", "http://weblogs.asp.net/scottgu/" };
        Int32 _currentFirstName;
        Int32 _currentLastName;
        Int32 _currentCompanyName;
        Int32 _currentUrl;
        Int32 _seedValue;
        Int32 _incrementValue = 1;
        const Int32 WordLowerBound = 0;
        Int32 _wordUpperBound = -1;
        const Int32 StatesLowerBound = 0;
        Int32 _statesUpperBound = -1;
        readonly StringBuilder _sb = new StringBuilder();
        readonly Random _random;
        readonly String[] _words = new[] { "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum", "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum", "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "nam", "liber", "tempor", "cum", "soluta", "nobis", "eleifend", "option", "congue", "nihil", "imperdiet", "doming", "id", "quod", "mazim", "placerat", "facer", "possim", "assum", "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "at", "accusam", "aliquyam", "diam", "diam", "dolore", "dolores", "duo", "eirmod", "eos", "erat", "et", "nonumy", "sed", "tempor", "et", "et", "invidunt", "justo", "labore", "stet", "clita", "ea", "et", "gubergren", "kasd", "magna", "no", "rebum", "sanctus", "sea", "sed", "takimata", "ut", "vero", "voluptua", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum" };
        readonly String[] _states = new[] { "AL", "AK", "AS", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FM", "FL", "GA", "GU", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MH", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "MP", "OH", "OK", "OR", "PW", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VI", "VA", "WA", "WV", "WI", "WY", "AA", "AE", "AP" };

        #endregion

        #region  Constructor

        public DataGenerator() {
            var randomBytes = new Byte[4];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            Int32 seed = ((randomBytes[0] & 0X7F) << 24 | randomBytes[1] << 16 | randomBytes[2] << 8 | randomBytes[3]);
            _random = new Random(seed);
            _wordUpperBound = _words.Count() - 1;
            _statesUpperBound = _states.Count() - 1;
            SeedSequentialInteger(1000, 1);
        }

        #endregion

        #region  Methods

        public void SeedSequentialInteger(Int32 seedValue, Int32 incrementValue) {
            _seedValue = seedValue;
            _incrementValue = incrementValue;
        }

        public Int32 GetSequentialInteger() {
            _seedValue += _incrementValue;
            return _seedValue;
        }

        public DateTime GetDate(DateTime minValue, DateTime maxValue) {

            TimeSpan ts = maxValue - minValue;

            Int32 dateDiff = Math.Abs(ts.TotalDays) > Int32.MaxValue ? Int32.MaxValue : Convert.ToInt32(Math.Abs(ts.TotalDays));
            
            return minValue.AddDays(GetInteger(0, dateDiff));
        }

        public Decimal GetDecimal(Int32 minValue, Int32 maxValue) {
            return Convert.ToDecimal(_random.Next(minValue, maxValue) + _random.NextDouble());
        }

        public Double GetDouble(Int32 minValue, Int32 maxValue) {
            return _random.Next(minValue, maxValue) + _random.NextDouble();
        }

        public String GetEmail() {
            return String.Format("{0}@{1}.com", GetString(7, StringCase.Lower, true), GetString(11, StringCase.Lower, true));
        }

        public String GetFirstName() {
            _currentFirstName += 1;
            if(_currentFirstName > _firstNames.Count - 1) {
                _currentFirstName = 0;
            }
            return _firstNames[_currentFirstName];
        }

        public String GetUrl() {
            _currentUrl += 1;
            if(_currentUrl > _urls.Count - 1) {
                _currentUrl = 0;
            }
            return _urls[_currentUrl];
        }

        public String GetCompanyName() {
            _currentCompanyName += 1;
            if(_currentCompanyName > _companyNames.Count - 1) {
                _currentCompanyName = 0;
            }
            return _companyNames[_currentCompanyName];
        }

        public String GetLastName() {
            _currentLastName += 1;
            if(_currentLastName > _lastNames.Count - 1) {
                _currentLastName = 0;
            }
            return _lastNames[_currentLastName];
        }

        public String GetFullName() {
            return String.Concat(GetFirstName(), " ", GetLastName());
        }

        public Int32 GetInteger(Int32 minValue, Int32 maxValue) {
            return _random.Next(minValue, maxValue);
        }

        public String GetSSN() {
            return String.Format("({0}-{1}-{2}", GetInteger(100, 999), GetInteger(10, 99), GetInteger(1000, 9999));
        }

        public bool GetBoolean() {
            return _random.Next(0, 49) % 2 == 0;
        }

        public String GetPhoneNumber() {
            return String.Format("({0} {1}-{2}", GetInteger(100, 999), GetInteger(100, 999), GetInteger(1000, 9999));
        }

        public String GetZipCode() {
            return GetInteger(10000, 99999).ToString();
        }

        public String GetStateAbbreviation() {
            return _states[_random.Next(StatesLowerBound, _statesUpperBound)];
        }

        public String GetString(Int32 maxLength) {
            return GetString(maxLength, StringCase.None, false);
        }

        public String GetString(Int32 maxLength, StringCase stringCase) {
            return GetString(maxLength, stringCase, false);
        }

        public String GetString(Int32 maxLength, StringCase stringCase, Boolean removeSpaces) {
            _sb.Clear();
            _sb.Length = 0;
            while(_sb.Length < maxLength) {
                _sb.Append(_words[_random.Next(WordLowerBound, _wordUpperBound)]);
                if(!removeSpaces) {
                    _sb.Append(" ");
                }
            }
            _sb.Length = maxLength;

            switch(stringCase) {
                case StringCase.Lower:
                    return _sb.ToString().ToLower();
                case StringCase.None:
                    return _sb.ToString();
                case StringCase.Upper:
                    return _sb.ToString().ToUpper();
                default:
                    throw new ArgumentOutOfRangeException("stringCase", "Programmer did not program this value.");
            }
        }

        #endregion
    }
}

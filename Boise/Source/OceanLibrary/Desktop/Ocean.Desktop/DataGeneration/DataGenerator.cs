using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Ocean.Infrastructure;
using Ocean.Properties;


namespace Ocean.DataGeneration {

    /// <summary>
    /// Specifies the string case
    /// </summary>
    public enum StringCase {
        /// <summary>
        /// Do not alter the string case
        /// </summary>
        None,
        /// <summary>
        /// Change string to upper case
        /// </summary>
        Upper,
        /// <summary>
        /// Change string to lower case
        /// </summary>
        Lower
    }

    /// <summary>
    /// Represents DataGenerator, used to create sample data at design-time, test-time, or run-time
    /// </summary>
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
        const Int32 _WORD_LOWER_BOUND = 0;
        readonly Int32 _wordUpperBound = -1;
        const Int32 _STATES_LOWER_BOUND = 0;
        readonly Int32 _statesUpperBound = -1;
        readonly StringBuilder _sb = new StringBuilder();
        readonly Random _random;
        readonly String[] _words = new[] { "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum", "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum", "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "nam", "liber", "tempor", "cum", "soluta", "nobis", "eleifend", "option", "congue", "nihil", "imperdiet", "doming", "id", "quod", "mazim", "placerat", "facer", "possim", "assum", "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "at", "accusam", "aliquyam", "diam", "diam", "dolore", "dolores", "duo", "eirmod", "eos", "erat", "et", "nonumy", "sed", "tempor", "et", "et", "invidunt", "justo", "labore", "stet", "clita", "ea", "et", "gubergren", "kasd", "magna", "no", "rebum", "sanctus", "sea", "sed", "takimata", "ut", "vero", "voluptua", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum" };
        readonly String[] _states = new[] { "AL", "AK", "AS", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FM", "FL", "GA", "GU", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MH", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "MP", "OH", "OK", "OR", "PW", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VI", "VA", "WA", "WV", "WI", "WY", "AA", "AE", "AP" };

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGenerator"/> class.
        /// </summary>
        public DataGenerator() {
            var randomBytes = new Byte[4];
            using (var rng = new RNGCryptoServiceProvider()) {
                rng.GetBytes(randomBytes);    
            }
            Int32 seed = ((randomBytes[0] & 0X7F) << 24 | randomBytes[1] << 16 | randomBytes[2] << 8 | randomBytes[3]);
            _random = new Random(seed);
            _wordUpperBound = _words.Count() - 1;
            _statesUpperBound = _states.Count() - 1;
            SeedSequentialInteger(1000, 1);
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Seeds the sequential integer.
        /// </summary>
        /// <param name="seedValue">The seed value.</param>
        /// <param name="incrementValue">The increment value.</param>
        public void SeedSequentialInteger(Int32 seedValue, Int32 incrementValue) {
            _seedValue = seedValue;
            _incrementValue = incrementValue;
        }

        /// <summary>
        /// Gets the sequential integer.
        /// </summary>
        /// <returns></returns>
        public Int32 GetSequentialInteger() {
            _seedValue += _incrementValue;
            return _seedValue;
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns></returns>
        public DateTime GetDate(DateTime minValue, DateTime maxValue) {

            TimeSpan ts = maxValue - minValue;

            Int32 dateDiff = Math.Abs(ts.TotalDays) > Int32.MaxValue ? Int32.MaxValue : Convert.ToInt32(Math.Abs(ts.TotalDays));
            
            return minValue.AddDays(GetInteger(0, dateDiff));
        }

        /// <summary>
        /// Gets the decimal.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns></returns>
        public Decimal GetDecimal(Int32 minValue, Int32 maxValue) {
            return Convert.ToDecimal(_random.Next(minValue, maxValue) + _random.NextDouble());
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns></returns>
        public Double GetDouble(Int32 minValue, Int32 maxValue) {
            return _random.Next(minValue, maxValue) + _random.NextDouble();
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <returns></returns>
        public String GetEmail() {
            return String.Format("{0}@{1}.com", GetString(7, StringCase.Lower, true), GetString(11, StringCase.Lower, true));
        }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <returns></returns>
        public String GetFirstName() {
            _currentFirstName += 1;
            if(_currentFirstName > _firstNames.Count - 1) {
                _currentFirstName = 0;
            }
            return _firstNames[_currentFirstName];
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <returns></returns>
        public String GetUrl() {
            _currentUrl += 1;
            if(_currentUrl > _urls.Count - 1) {
                _currentUrl = 0;
            }
            return _urls[_currentUrl];
        }

        /// <summary>
        /// Gets the name of the company.
        /// </summary>
        /// <returns></returns>
        public String GetCompanyName() {
            _currentCompanyName += 1;
            if(_currentCompanyName > _companyNames.Count - 1) {
                _currentCompanyName = 0;
            }
            return _companyNames[_currentCompanyName];
        }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <returns></returns>
        public String GetLastName() {
            _currentLastName += 1;
            if(_currentLastName > _lastNames.Count - 1) {
                _currentLastName = 0;
            }
            return _lastNames[_currentLastName];
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <returns></returns>
        public String GetFullName() {
            return String.Concat(GetFirstName(), GlobalConstants.STRING_WHITE_SPACE, GetLastName());
        }

        /// <summary>
        /// Gets the integer.
        /// </summary>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns></returns>
        public Int32 GetInteger(Int32 minValue, Int32 maxValue) {
            return _random.Next(minValue, maxValue);
        }

        /// <summary>
        /// Gets the SSN.
        /// </summary>
        /// <returns></returns>
        public String GetSSN() {
            return String.Format("({0}-{1}-{2}", GetInteger(100, 999), GetInteger(10, 99), GetInteger(1000, 9999));
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <returns></returns>
        public bool GetBoolean() {
            return _random.Next(0, 49) % 2 == 0;
        }

        /// <summary>
        /// Gets the phone number.
        /// </summary>
        /// <returns></returns>
        public String GetPhoneNumber() {
            return String.Format("({0} {1}-{2}", GetInteger(100, 999), GetInteger(100, 999), GetInteger(1000, 9999));
        }

        /// <summary>
        /// Gets the zip code.
        /// </summary>
        /// <returns></returns>
        public String GetZipCode() {
            return GetInteger(10000, 99999).ToString();
        }

        /// <summary>
        /// Gets the state abbreviation.
        /// </summary>
        /// <returns></returns>
        public String GetStateAbbreviation() {
            return _states[_random.Next(_STATES_LOWER_BOUND, _statesUpperBound)];
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="maxLength">Length of the max.</param>
        /// <returns></returns>
        public String GetString(Int32 maxLength) {
            return GetString(maxLength, StringCase.None, false);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="maxLength">Length of the max.</param>
        /// <param name="stringCase">The string case.</param>
        /// <returns></returns>
        public String GetString(Int32 maxLength, StringCase stringCase) {
            return GetString(maxLength, stringCase, false);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="maxLength">Length of the max.</param>
        /// <param name="stringCase">The string case.</param>
        /// <param name="removeSpaces">if set to <c>true</c> [remove spaces].</param>
        /// <returns></returns>
        public String GetString(Int32 maxLength, StringCase stringCase, Boolean removeSpaces) {
            _sb.Clear();
            _sb.Length = 0;
            while(_sb.Length < maxLength) {
                _sb.Append(_words[_random.Next(_WORD_LOWER_BOUND, _wordUpperBound)]);
                if(!removeSpaces) {
                    _sb.Append(GlobalConstants.STRING_WHITE_SPACE);
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
                    throw new OverflowException(Resources.DataGenerator_GetString_Programmer_did_not_program_this_value_);
            }
        }

        #endregion
    }
}

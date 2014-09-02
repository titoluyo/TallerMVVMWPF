using System;
using System.Collections.Generic;
using Ocean.InputStringFormatting;
using Ocean.Properties;


namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents StateAbbreviationValidator
    /// </summary>
    public class StateAbbreviationValidator {

        #region  Declarations

        static StateAbbreviationValidator _instance;
        readonly Dictionary<String, String> _states;

        #endregion

        #region  Constructor

        StateAbbreviationValidator() {
// ReSharper disable UseObjectOrCollectionInitializer
            _states = new Dictionary<String, String>();
// ReSharper restore UseObjectOrCollectionInitializer
            _states.Add("AL", "ALABAMA");
            _states.Add("AK", "ALASKA");
            _states.Add("AS", "AMERICAN SAMOA");
            _states.Add("AZ", "ARIZONA");
            _states.Add("AR", "ARKANSAS");
            _states.Add("CA", "CALIFORNIA");
            _states.Add("CO", "COLORADO");
            _states.Add("CT", "CONNECTICUT");
            _states.Add("DE", "DELAWARE");
            _states.Add("DC", "DISTRICT OF COLUMBIA");
            _states.Add("FM", "FEDERATED STATES OF MICRONESIA");
            _states.Add("FL", "FLORIDA");
            _states.Add("GA", "GEORGIA");
            _states.Add("GU", "GUAM");
            _states.Add("HI", "HAWAII");
            _states.Add("ID", "IDAHO");
            _states.Add("IL", "ILLINOIS");
            _states.Add("IN", "INDIANA");
            _states.Add("IA", "IOWA");
            _states.Add("KS", "KANSAS");
            _states.Add("KY", "KENTUCKY");
            _states.Add("LA", "LOUISIANA");
            _states.Add("ME", "MAINE");
            _states.Add("MH", "MARSHALL ISLANDS");
            _states.Add("MD", "MARYLAND");
            _states.Add("MA", "MASSACHUSETTS");
            _states.Add("MI", "MICHIGAN");
            _states.Add("MN", "MINNESOTA");
            _states.Add("MS", "MISSISSIPPI");
            _states.Add("MO", "MISSOURI");
            _states.Add("MT", "MONTANA");
            _states.Add("NE", "NEBRASKA");
            _states.Add("NV", "NEVADA");
            _states.Add("NH", "NEW HAMPSHIRE");
            _states.Add("NJ", "NEW JERSEY");
            _states.Add("NM", "NEW MEXICO");
            _states.Add("NY", "NEW YORK");
            _states.Add("NC", "NORTH CAROLINA");
            _states.Add("ND", "NORTH DAKOTA");
            _states.Add("MP", "NORTHERN MARIANA ISLANDS");
            _states.Add("OH", "OHIO");
            _states.Add("OK", "OKLAHOMA");
            _states.Add("OR", "OREGON");
            _states.Add("PW", "PALAU");
            _states.Add("PA", "PENNSYLVANIA");
            _states.Add("PR", "PUERTO RICO");
            _states.Add("RI", "RHODE ISLAND");
            _states.Add("SC", "SOUTH CAROLINA");
            _states.Add("SD", "SOUTH DAKOTA");
            _states.Add("TN", "TENNESSEE");
            _states.Add("TX", "TEXAS");
            _states.Add("UT", "UTAH");
            _states.Add("VT", "VERMONT");
            _states.Add("VI", "VIRGIN ISLANDS");
            _states.Add("VA", "VIRGINIA");
            _states.Add("WA", "WASHINGTON");
            _states.Add("WV", "WEST VIRGINIA");
            _states.Add("WI", "WISCONSIN");
            _states.Add("WY", "WYOMING");
            //
            //leave it up to the government to screw this up!!!
            //Using AE 4 times.  Who's incharge up there!  Your tax dollars hard at work.
            //
            //     _objStates.Add("AE", "Armed Forces Africa")
            _states.Add("AA", "Armed Forces Americas");
            //        _objStates.Add("AE", "Armed Forces Canada")
            _states.Add("AE", "Armed Forces Europe");
            //      _objStates.Add("AE", "Armed Forces Middle East")
            _states.Add("AP", "Armed Forces Pacific");
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        public static StateAbbreviationValidator CreateInstance() {
            return _instance ?? (_instance = new StateAbbreviationValidator());
        }

        /// <summary>
        /// Gets the name of the state.
        /// </summary>
        /// <param name="stateAbbreviation">The state abbreviation.</param>
        /// <returns></returns>
        public String GetStateName(String stateAbbreviation) {
            return IsValid(stateAbbreviation) ? FormatText.ApplyCharacterCasing(_states[stateAbbreviation.ToUpper()], CharacterCasing.ProperName) : String.Format(Resources.StateAbbreviationValidator_GetStateName_State_abbreviation___0___is_not_valid_FormatString, stateAbbreviation);
        }

        /// <summary>
        /// Determines whether the specified state abbreviation is valid.
        /// </summary>
        /// <param name="stateAbbreviation">The state abbreviation.</param>
        /// <returns>
        /// 	<c>true</c> if the specified state abbreviation is valid; otherwise, <c>false</c>.
        /// </returns>
        public Boolean IsValid(String stateAbbreviation) {
            return _states.ContainsKey(stateAbbreviation.ToUpper());
        }

        #endregion
    }
}


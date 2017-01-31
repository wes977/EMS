using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EMS.Web.ems.Create
{
    public enum Errors
    {
        Ok,
        InvalidBirthDateFormat,
        InvalidDateOfHireFormat,
        InvalidDateOfTermFormat,
        BirthDateGreaterThanToday,
        EndDateGreaterThanStart,
        FullTimeTooYoung,
        PartTimeTooYoung,
        SeasonalTooYoung,
        InvalidPay,
        InvalidSinFormat,
        InvalidSin,
        InvalidBusinessNum,
        IncorporationMismatch,
        InvalidBusinessNumFormat,
        InvalidFirstNameCharacter,
        InvalidLastNameCharacter,
        FirstNameBlank,
        LastNameBlank
    };

    public enum DateOf
    {
        Hire,
        Termination
    }

    public enum Name
    {
        FirstName,
        LastName
    }

    public enum EmployeeType
    {
        FullTime,
        PartTime,
        Seasonal,
        Contract
    }

    /// <summary>
    /// Contains methods to validate certain fields for
    /// employees.
    /// </summary>
    public class EmployeeValidator
    {
        List<string> errorList = new List<string>();

        //------------------------------------------------------------------------------//

        //======================================================================//
        //                           Getters / Setters                          //
        //======================================================================//

        public List<string> ErrorList
        {
            get
            {
                return errorList;
            }
        }

        public bool HasErrors
        {
            get
            {
                bool hasErrors = true;

                if (ErrorList.Count == 0)
                {
                    hasErrors = false;
                }

                return hasErrors;
            }
        }

        //======================================================================//
        //                               Methods                                //
        //======================================================================//

        public void ValidateBirthDate(string date, EmployeeType type)
        {
            Errors status = Errors.Ok;

            /* Validate the format of the date, get datetime if valid */
            if (ValidateDateFormat(date) == false)
            {
                AddError(Errors.InvalidBirthDateFormat);
            }

            /* Process if date valid */
            if (status == Errors.Ok)
            {
                /* Convert birthdate to DateTime format */
                DateTime birthDate = DateTime.Parse(date);

                /* Ensure birthdate is previous to today */
                if (birthDate > DateTime.Now)
                {
                    AddError(Errors.BirthDateGreaterThanToday);
                }

                /* Ensure candidate is old enough for position */
                switch (type)
                {
                    case EmployeeType.FullTime:
                        if (birthDate > DateTime.Now.AddYears(-18))
                        {
                            AddError(Errors.FullTimeTooYoung);
                        }
                        break;

                    case EmployeeType.Seasonal:
                        if (birthDate > DateTime.Now.AddYears(-18))
                        {
                            AddError(Errors.SeasonalTooYoung);
                        }
                        break;

                    case EmployeeType.PartTime:
                        if (birthDate > DateTime.Now.AddYears(-16))
                        {
                            AddError(Errors.PartTimeTooYoung);
                        }
                        break;
                }
            }
        }

        public void ValidateDates(string startDate, string endDate)
        {
            bool invalidDate = false;

            /* Validate start and end dates */
            if (ValidateDateOf(startDate, DateOf.Hire) == false)
            {
                AddError(Errors.InvalidDateOfHireFormat);
                invalidDate = true;
            }
            if (ValidateDateOf(endDate, DateOf.Termination) == false)
            {
                AddError(Errors.InvalidDateOfTermFormat);
                invalidDate = true;
            }

            /* Verify that start date is before end */
            if (invalidDate == false && string.IsNullOrEmpty(endDate) == false)
            {
                /* Convert strings to datetime for comparison */
                DateTime start = DateTime.Parse(startDate);
                DateTime end = DateTime.Parse(endDate);

                /* Add error if end date is before start date */
                if (start > end)
                {
                    AddError(Errors.EndDateGreaterThanStart);
                }
            }
        }

        private bool ValidateDateFormat(string date)
        {
            bool isValid = false;    //Signals if the date is a valid field or not
            DateTime dateValidator;  //Used to check if the date being entered is a valid date

            /* Check if the date being entered is a real date, returns true if valid */
            isValid = DateTime.TryParse(date, out dateValidator);

            return isValid;
        }

        private bool ValidateDateOf(string date, DateOf dateOf)
        {
            bool valid = true;

            if (dateOf == DateOf.Hire)
            {
                /* Validate the format of date of hire, can't be blank */
                valid = ValidateDateFormat(date);
            }
            else
            {
                /* Check the format if date of term is not empty */
                if (!string.IsNullOrEmpty(date))
                {
                    valid = ValidateDateFormat(date);
                }
            }

            return valid;
        }

        /// <summary>
        /// Does a number of mathematical calculations in order to check if the sin
        /// handed to it is a legitimate Canadian sin number. This is done by comparing
        /// the final number from the resulting calculation to the final digit in the
        /// SIN card. If they do not match, the SIN is invalid.
        /// </summary>
        /// <param name="sin">The sin number to be checked for validity</param>
        /// <returns><c>true</c> if SIN is valid, <c>false</c> otherwise.</returns>
        public bool CheckDigit(string sin, bool AddErrors = true)
        {
            string multipliedNumString = "";       //Used to hold all even digits from SIN that were multiplied
            string parsedSin = "";                 //Holds the Sin number after the dashes and spaces are removed
            int extractedNum = 0;                  //Holds a number extracted from the sinNum string
            int evenNumSum = 0;                    //The running total of all even numbers from the SIN
            int oddNumSum = 0;                     //Holds the sum of all odd sin digits
            int totalSum = 0;                      //Holds the product of evenNumSum+oddNumSum
            int roundedNum = 0;                    //Holds the nearest 10th value from totalSum
            int validSinNum = 0;                   //Will hold final number, should match last sin digit to be valid
            int lastIndexInt = 0;                  //Holds the last digit from the sin number
            bool validSin = true;

            if (ValidateSinLength(sin) == true)
            {
                /* Get the last digit of the SIN number */
                lastIndexInt = (int)char.GetNumericValue(sin[sin.Length - 1]);

                /* Remove dashes or spaces if nessesary, else skip */
                if (sin.Contains('-') == true || sin.Contains(' ') == true)
                {
                    parsedSin = Regex.Replace(sin, @"[-\s]", "");
                }
                else
                {
                    parsedSin = sin;
                }

                /* Multiply each even number and add each odd number, -1 to exclude last sin digit */
                for (int i = 0; i < parsedSin.Length - 1; ++i)
                {
                    /* Extract a SIN digit from the sin string */
                    extractedNum = (int)char.GetNumericValue(parsedSin[i]);

                    if (i % 2 != 0)
                    {
                        /* Multiply each even number and store accumulated value into string */
                        multipliedNumString += (extractedNum * 2).ToString();
                    }
                    else
                    {
                        /* Get the running total of each odd number */
                        oddNumSum += extractedNum;
                    }
                }

                /* Add all the even numbers that were multiplied together to get evenNumSum */
                for (int j = 0; j < multipliedNumString.Length; j++)
                {
                    evenNumSum += (int)char.GetNumericValue(multipliedNumString[j]);
                }

                /* Add evenNumSUm and oddNumSum to get total sum, use sum to get next highest number starting with 0 */
                totalSum = evenNumSum + oddNumSum;
                roundedNum = RoundOff(totalSum);

                /* Product of the roundedNum - totalSum will produce last SIN digit if a valid SIN */
                validSinNum = (roundedNum - totalSum);

                /* If last digit is equal to validSinNum, is a valid SIN */
                if (lastIndexInt != validSinNum)
                {
                    if (AddErrors == true)
                    {
                        AddError(Errors.InvalidSin);
                    }
                    validSin = false;
                }
            }
            else
            {
                if (AddErrors == true)
                {
                    AddError(Errors.InvalidSinFormat);
                }
                validSin = false;
            }
            return validSin;
        }

        /// <summary>
        /// Validates the business number(sin) of the contractor. This method checks to ensure
        /// that the last two digits of the year and the first two digits of the business number
        /// match. If it matches, the business number undergoes normal sin number checkDigit validation.
        /// Returns true if the number checks out.
        /// </summary>
        /// <param name="sin">The business number(sin) to be validated</param>
        /// <param name="dateOfIncorporation">The incorporation date of the company.</param>
        /// <returns><c>true</c> if business number is valid, <c>false</c> otherwise.</returns>
        public void ValidateBusinessNumber(string sin, string dateOfIncorporation)
        {
            const int BN_LENGTH = 9;      //The amount of digits in a Business number

            string incorporationStr = "";  //Holds the year of incorporation
            string businessNumYear = "";   //Used to hold the first 2 numbers of the sin number

            try
            {
                if (sin.Length == BN_LENGTH)
                {
                    /* Get the last two digits of the year & first two digits of business number */
                    incorporationStr = dateOfIncorporation.Substring(2, 2);
                    businessNumYear = sin.Substring(0, 2);

                    /* If the first two numbers of the businessNum are equal to incorportation year, then business num is valid */
                    if (businessNumYear != incorporationStr)
                    {
                        /* Return false if first 2 numbers on business num do not match incorporation date */
                        AddError(Errors.IncorporationMismatch);
                    }
                    /* Check the business number is legitimate using CheckDigit */
                    if (CheckDigit(sin) == false)
                    {
                        AddError(Errors.InvalidBusinessNum);
                    }
                }
                else
                {
                    /* Mark business number as invalid if not equal to BN_LENGTH */
                    AddError(Errors.InvalidBusinessNumFormat);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                /* Catch if any paramaters are not large enough to be parsed */
                AddError(Errors.InvalidBusinessNumFormat);
            }
        }

        /// <summary>
        /// Validates the salary.
        /// </summary>
        /// <param name="salary">The salary.</param>
        /// <returns> A boolean representing whether the salary was valid. </returns>
        public bool ValidatePay(double pay)
        {
            bool result = false;

            if (pay <= 0.0)
            {
                AddError(Errors.InvalidPay);
            }

            return (result);
        }

        /// <summary>
        /// Checks to ensure a SIN number is a valid field. A SIN is considered
        /// a valid field if it contains 9 numbers. A valid field SIN can also be blank.
        /// </summary>
        /// <param name="SIN">The sin number to be validated.</param>
        /// <returns><c>true</c> if sin is valid, <c>false</c> otherwise.</returns>
        private bool ValidateSinLength(string SIN)
        {
            const int VALID_DIGIT_COUNT = 9; //Amount of digits needed to be a valid SIN field
            bool result = false;             //Determines if SIN is a valid field or not

            /* Remove spaces if they exist */
            if (SIN.Contains(' ') == true)
            {
                SIN = Regex.Replace(SIN, @"[\s]", "");
            }

            /* Sin is valid field if it contains exacly 9 digits */
            if (SIN.Count(char.IsDigit) == VALID_DIGIT_COUNT && SIN.Length == VALID_DIGIT_COUNT)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Takes a number and rounds it up to its nearest 10th value.
        /// </summary>
        /// <param name="num">The number to be rounded up.</param>
        /// <returns>Int, the nearest 10th place value.</returns>
        private static int RoundOff(int num)
        {
            return (int)(Math.Ceiling(num / 10.0d) * 10);
        }

        /// <summary>
        /// Checks the name to ensure that it is a valid field. A valid name
        /// field can be blank, or can only contain alpha characters, apostrophies,
        /// or colons.
        /// </summary>
        /// <param name="name">The name to be checked.</param>
        /// <returns><c>true</c> if name is valid, <c>false</c> otherwise.</returns>
        public void ValidateName(string name, Name nameField)
        {
            Regex regexBuilder;

            /* Trim the string of trailing spaces */
            name.Trim();

            /* Only process if the string is not empty*/
            if (!string.IsNullOrEmpty(name))
            {
                /* Setup to accept only a certain subset of characters */
                string regexString = "^[A-Za-z-' ]*$";
                regexBuilder = new Regex(regexString);
                bool isMatch = regexBuilder.IsMatch(name);

                /* Add error if name contains invalid characters */
                if (isMatch == false)
                {
                    if (nameField == Name.FirstName)
                    {
                        AddError(Errors.InvalidFirstNameCharacter);
                    }
                    else
                    {
                        AddError(Errors.InvalidLastNameCharacter);
                    }
                }
            }
            else
            {
                /* Add error if field was left blank */
                if (nameField == Name.FirstName)
                {
                    AddError(Errors.FirstNameBlank);
                }
                else
                {
                    AddError(Errors.LastNameBlank);
                }
            }
           
        }

        private void AddError(Errors status)
        {
            string errorString = "";

            switch (status)
            {
                case Errors.InvalidBirthDateFormat:
                    errorString = "The birth date is entered in an unrecognizable format.";
                    break;

                case Errors.InvalidDateOfHireFormat:
                    errorString = "The date of hire is entered in an unrecognizable format.";
                    break;

                case Errors.InvalidDateOfTermFormat:
                    errorString = "The date of termination is entered in an unrecognizable format.";
                    break;

                case Errors.BirthDateGreaterThanToday:
                    errorString = "The date of birth cannot be greater than the current date.";
                    break;

                case Errors.EndDateGreaterThanStart:
                    errorString = "The date of termination cannot be before the date of hire.";
                    break;

                case Errors.FullTimeTooYoung:
                    errorString = "The employee is too young to work full time. Minimum 18.";
                    break;

                case Errors.SeasonalTooYoung:
                    errorString = "The employee is too young to work seasonal. Minimum 18.";
                    break;

                case Errors.PartTimeTooYoung:
                    errorString = "The employee is too young to work part time. Minimum 16.";
                    break;

                case Errors.IncorporationMismatch:
                    errorString = "The first two digits of the business number do not match the last two digits of the companies incorporation year.";
                    break;

                case Errors.InvalidBusinessNum:
                    errorString = "The business number entered is not valid.";
                    break;

                case Errors.InvalidBusinessNumFormat:
                    errorString = "The business number entered was in the wrong format or contained improper characters.";
                    break;

                case Errors.InvalidFirstNameCharacter:
                    errorString = "Invalid character found in first name, valid characters are A-Z, apostophe, hyphen, and space.";
                    break;

                case Errors.InvalidLastNameCharacter:
                    errorString = "Invalid character found in last name, valid characters are A-Z, apostophe, hyphen, and space.";
                    break;

                case Errors.InvalidPay:
                    errorString = "Employee pay must not be 0 or a negative number.";
                    break;

                case Errors.InvalidSin:
                    errorString = "The SIN number entered is not valid";
                    break;

                case Errors.InvalidSinFormat:
                    errorString = "The SIN entered was in the wrong format or contained improper characters.";
                    break;

                case Errors.FirstNameBlank:
                    errorString = "First name field was left blank.";
                    break;

                case Errors.LastNameBlank:
                    errorString = "Last name field was left blank.";
                    break;

                default:
                    errorString = "An unknown error occured with this employee";
                    break;
            }

            errorList.Add(errorString);
        }
    }
}
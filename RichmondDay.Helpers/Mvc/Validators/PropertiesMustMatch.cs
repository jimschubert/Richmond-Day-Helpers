using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace RichmondDay.Helpers {
    [AttributeUsage(AttributeTargets.Class)]
    public class PropertiesMustMatch : ValidationAttribute {

        public String FirstPropertyName { get; set; }
        public String SecondPropertyName { get; set; }

        //Constructor to take in the property names that are supposed to be checked
        public PropertiesMustMatch(String firstPropertyName, String secondPropertyName) {
            FirstPropertyName = firstPropertyName;
            SecondPropertyName = secondPropertyName;
        }

        public override Boolean IsValid(Object value) {
            if (value == null)
                return false;

            Type objectType = value.GetType();
            //Get the property info for the object passed in.  This is the class the attribute is
            //  attached to
            //I would suggest caching this part... at least the PropertyInfo[]
            PropertyInfo[] neededProperties =
              objectType.GetProperties()
              .Where(propertyInfo => propertyInfo.Name == FirstPropertyName || propertyInfo.Name == SecondPropertyName)
              .ToArray();

            if (neededProperties.Count() != 2) {
                throw new ApplicationException("PropertiesMatchAttribute error on " + objectType.Name);
            }

            Boolean isValid = true;

            //Convert both values to string and compare...  Probably could be done better than this
            //  but let's not get bogged down with how dumb I am.  We should be concerned about
            //  dumb you are, jerkface.
            if (!Convert.ToString(neededProperties[0].GetValue(value, null)).Equals(Convert.ToString(neededProperties[1].GetValue(value, null)))) {
                isValid = false;
            }

            return isValid;
        }
    }
}

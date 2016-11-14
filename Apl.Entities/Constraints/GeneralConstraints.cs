namespace Apl.Entities.Constraints
{
    public static class GeneralConstraints
    {

        //Regular Expression
        public const string RegExpressionTrim = @"^([\S]|[\S][\S]|[\S].+[\S])$";
        public const string RegExpressionEmail = @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$";
        public const string RegExpressionPhoneCell = @"^[0-9]{2,3}-? ?[0-9]{4,12}$";
        public const string RegExpressionPhone = @"^[0-9]{2,3}-? ?[0-9]{4,12}$";
        public const string RegExpressionCell = @"^[0-9]{2,3}-? ?[0-9]{6,7}$";
        public const string RegExpressionDecimal2 = @"^(([0])|([1-9][0-9]*))(.(([0-9])|([0-9][0-9])))?$";

        //StringLength
        public const byte StringLengthNames = 100;
        public const byte StringLengthLastNames = 50;
        public const byte StringLengthEmail = 100;
        public const byte StringLengthPhone = 20;
        public const byte StringLengthMovil = 20;
        public const byte StringLengthId = 13;
        public const byte StringLengthAddress = 255;
        public const byte StringLengthCmnt = 255;
        public const byte StringLengthNamesPlusLastNames = 150;
        public const byte StringLengthReference = 20;
        public const byte StringLengthFileName = 255;
        public const byte StringLengthToMonth = 7;

        //Range Number

        public const short MinYear = 1900;
        public const short MaxYear = 3000;

        //Range Number
        public const double MinDecimal0Value = 0.00;
        public const double MinDecimal001Value = 0.01;
        public const double MaxDecimalValue = double.MaxValue;

    }
}

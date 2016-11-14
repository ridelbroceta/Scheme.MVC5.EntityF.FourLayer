namespace Apl.BusinessLayer.Domain
{
  public static class MyConstant
  {

      public enum EnumValidSoft
    {
      NoError = 0,
      MoreOneAdministrator = 1,
      NoNameAdministrator = 2,
      MoreTwoOwner = 3,
      SoftOutOfDate = 4,
      NoData = 5,
      NoInDate = 6,
      Violation = 7
    }

    public enum EnumLoginError
    {
      NoError = 0,
      UserLocked = 1,
      IncorrectPassword = 2,
      NoUserRegistered = 3,
      PasswordOlder = 4,
      UserConnected = 5,
      UserWait = 6  
    };

    public enum EnumRoles
    {
      Administrators = 1,  
      Others = 2
    };

    public enum EnumUserError
    {
      NoError = 0,
      UserNotEmpty = 1,
      UserNotDeleted = 2
    }


  }
}

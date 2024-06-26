using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Models.Enums
{
    public enum Errors
    {
        Success,
        TheModelIsInvalid,
        ThisProjectNotExistOrDeleted,
        SomeThingWentwrong,
        YouForgetTOEnterTheTransferImage,
        TheUserNotExistOrDeleted,
        UserIsDeleted,
        UserIsPending,
        UserIsRejected,
        ThisPhoneNumberAlreadyExist,
        ThisPhoneNumberNotExist,
        YourMoneyInTheWalletNotEnough,
        TheHelperPhoneNumberNotExist,
        TheUsernameOrPasswordIsIncorrect,
        TheOldPasswordIsInCorrect,
        TheVerificationCodeUnvalid,
        TheCountryNotExistOrDeleted,
        TheCityNotExistOrDeleted,
        TheDepartmentNotExistOrDeleted,
        WrongFacebookAccessToken,
        PageMustBeGreaterThanZero
    }
}

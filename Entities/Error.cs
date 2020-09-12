using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Error
    {

        public static string[] errorText =
        {
        "Şifrenizi yanlış tuşladınız lütfen tekrar deneyiniz.",
        "Girdiniz bilgilerin karşılığı bulunamadı. Tekrar deneyiniz.",
        "Api kodunuz doğrulanamadı.",
        "Geçersiz Qr Kodu tarattınız",
        "Kaldırmak istediğiniz miktar stoktakinden az olmalı",
        "Belirnenemeyen bir hata oluştu",
        "Sistemde kayıtlı bir mail hesabı giriniz."
        };
        public static string getErrorText(int ErrorCode)
        {
            if (ErrorCode == 000)
                return errorText[0];
            else if (ErrorCode == 001)
                return errorText[1];
            else if (ErrorCode == 002)
                return errorText[3];
            else
                return "Belirlenemeyen bir hata ile karşılaşınıldı.";

        }

    }

    
}

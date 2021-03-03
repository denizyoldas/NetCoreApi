using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductList = "Ürünler Listelendi";
        public static string ProductCountOfCategory = "Bir kategoriye 10 üründen fazla girilemez";
        public static string ProductNameAlreadyExists = "Ayni isme sahip bir ürün var!";
        public static string CategoryLimitExceded = "Categori limit aşıldı";
        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string UserRegistered = "Kullanıcı kaydı başarılı";
        public static string UserNotFound = "Kullanıcı bulunanadı.";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Giriş başarılı";
        public static string UserAlreadyExists = "Kullanıcı mevcut.";
        public static string AccessTokenCreated = "Token oluşturuldu";
    }
}

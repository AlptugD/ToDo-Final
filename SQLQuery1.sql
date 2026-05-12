-- 1. Veritabanını Oluştur ve Kullanmaya Başla
CREATE DATABASE ASyncTaskDB;
GO

USE ASyncTaskDB;
GO

-- 2. Kullanıcılar Tablosu (Giriş bilgileri, roller ve tema rengi)
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL,
    Role NVARCHAR(20) NOT NULL, -- 'Yönetici' veya 'Çalışan'
    ThemeColor NVARCHAR(20) DEFAULT '#FFFFFF' -- Arka plan renk kodu
);
GO

-- 3. Görevler Tablosu (Takvim bağlantılı görev detayları)
CREATE TABLE Tasks (
    TaskID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    TaskDate DATETIME, -- Takvim için tarih
    IsCompleted BIT DEFAULT 0 -- 0: Tamamlanmadı, 1: Tamamlandı (Tik koyma)
);
GO

-- 4. Yorumlar Tablosu (Görevlere yapılan stickerlı yorumlar)
CREATE TABLE Comments (
    CommentID INT IDENTITY(1,1) PRIMARY KEY,
    TaskID INT FOREIGN KEY REFERENCES Tasks(TaskID),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    CommentText NVARCHAR(MAX),
    StickerCode NVARCHAR(50) NULL -- Sticker eklenmezse boş (NULL) kalabilir
);
GO

-- 5. C# Uygulamasında Test Edebilmen İçin Örnek Kullanıcıları Ekle
INSERT INTO Users (Username, Password, Role, ThemeColor) 
VALUES ('admin', '123', 'Yönetici', '#1A1F2E');

INSERT INTO Users (Username, Password, Role, ThemeColor) 
VALUES ('calisan', '123', 'Çalışan', '#FFFFFF');
GO
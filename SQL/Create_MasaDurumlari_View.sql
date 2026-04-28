-- MasaDurumlari View'i oluştur (Masa planı için)
-- Bu view, tables tablosundan masa durumlarını getirir

-- MasaDurumlari view'ini oluştur veya güncelle
IF EXISTS (SELECT * FROM sys.views WHERE name = 'MasaDurumlari')
BEGIN
    DROP VIEW MasaDurumlari;
    PRINT 'Eski MasaDurumlari view\'i silindi.';
END

CREATE VIEW MasaDurumlari AS
SELECT 
    id,
    table_name,
    capacity,
    location,
    status,
    reservation_time
FROM tables;

PRINT 'MasaDurumlari view\'i başarıyla oluşturuldu.';

PRINT 'Kurulum tamamlandı.';
